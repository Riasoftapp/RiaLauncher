Imports System.Data.SQLite
Imports System.IO
Imports System.Xml.Linq

Public Class DatabaseManager

    Private Shared _dataDir As String = ""

    Public Shared Sub SetDataDir(dir As String)
        _dataDir = dir
    End Sub

    Private Shared ReadOnly Property DbPath As String
        Get
            Return Path.Combine(_dataDir, "RiaLauncher.db")
        End Get
    End Property

    Public Shared Function GetConnectionString() As String
        Return "Data Source=" & DbPath & ";Version=3;"
    End Function

    ' Mevcut şema sürümü (yeni sürümlerde artır)
    Public Const SchemaVersion As Integer = 1

    Public Shared Function GetSchemaVersion() As Integer
        If Not File.Exists(DbPath) Then Return 0

        Using conn As New SQLiteConnection(GetConnectionString())
            conn.Open()
            Using cmd As New SQLiteCommand("PRAGMA user_version", conn)
                Return CInt(cmd.ExecuteScalar())
            End Using
        End Using
    End Function

    ' Şema sürümünü hedef sürüme yükseltir; migration'lar başarısız olursa geri alınır
    Public Shared Sub RunMigrations()
        If _dataDir = "" OrElse Not File.Exists(DbPath) Then Return
        If GetSchemaVersion() >= SchemaVersion Then Return

        Using conn As New SQLiteConnection(GetConnectionString())
            conn.Open()
            Using transaction = conn.BeginTransaction()
                Try
                    Dim version As Integer = GetSchemaVersion()
                    While version < SchemaVersion
                        version += 1
                        ApplyMigration(conn, version)

                        Using vCmd As New SQLiteCommand("PRAGMA user_version = " & version, conn)
                            vCmd.Transaction = transaction
                            vCmd.ExecuteNonQuery()
                        End Using
                    End While

                    transaction.Commit()
                Catch ex As Exception
                    transaction.Rollback()
                    Try
                        Dim logDir = Form1.sLogDir
                        If logDir <> "" AndAlso Not Directory.Exists(logDir) Then Directory.CreateDirectory(logDir)
                        If logDir <> "" Then
                            File.AppendAllText(Path.Combine(logDir, "error.log"),
                                               DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "  DB migration hatasi: " & ex.Message & Environment.NewLine,
                                               System.Text.Encoding.UTF8)
                        End If
                    Catch
                    End Try
                End Try
            End Using
        End Using
    End Sub

    Private Shared Sub ApplyMigration(conn As SQLiteConnection, targetVersion As Integer)
        Select Case targetVersion
            Case 1
                ' Sürüm 1: mevcut başlangıç şeması (categories + items)
                ' Tablolar InitializeDatabase'de zaten oluşturuluyor; sadece sürüm damgası işlenir.
            Case 2
                ' Örnek: gelecekteki bir migration
                ' Using cmd As New SQLiteCommand("ALTER TABLE items ADD COLUMN note TEXT DEFAULT ''", conn)
                '     cmd.ExecuteNonQuery()
                ' End Using
        End Select
    End Sub

    Public Shared Function DatabaseExists() As Boolean
        Return File.Exists(DbPath)
    End Function

    Public Shared Sub InitializeDatabase()
        If _dataDir = "" Then Return
        If Not Directory.Exists(_dataDir) Then
            Directory.CreateDirectory(_dataDir)
        End If

        If Not File.Exists(DbPath) Then
            SQLiteConnection.CreateFile(DbPath)
        End If

        Using conn As New SQLiteConnection(GetConnectionString())
            conn.Open()
            Using cmd As New SQLiteCommand(conn)
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS categories (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL UNIQUE);" & vbCrLf &
                                  "CREATE TABLE IF NOT EXISTS items (id INTEGER PRIMARY KEY AUTOINCREMENT, category_id INTEGER NOT NULL, name TEXT NOT NULL, path TEXT NOT NULL, icon_path TEXT DEFAULT '', order_index INTEGER DEFAULT 0, icon_source TEXT DEFAULT 'Auto', FOREIGN KEY (category_id) REFERENCES categories(id) ON DELETE CASCADE);"
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Shared Sub ImportFromXml(xmlPath As String)
        If Not File.Exists(xmlPath) Then Return
        If _dataDir = "" Then Return

        Dim doc = XDocument.Load(xmlPath)

        Using conn As New SQLiteConnection(GetConnectionString())
            conn.Open()

            Using checkCmd As New SQLiteCommand("SELECT COUNT(*) FROM categories", conn)
                Dim count = CInt(checkCmd.ExecuteScalar())
                If count > 0 Then Return
            End Using

            Using transaction = conn.BeginTransaction()
                Using cmd As New SQLiteCommand(conn)
                    cmd.Transaction = transaction

                    Dim categories = doc.Root.Element("Categories")
                    If categories IsNot Nothing Then
                        For Each cat In categories.Elements("Category")
                            Dim catAttr = cat.Attribute("Name")
                            If catAttr Is Nothing OrElse String.IsNullOrEmpty(catAttr.Value) Then Continue For
                            Dim catName As String = catAttr.Value

                            cmd.CommandText = "INSERT OR IGNORE INTO categories (name) VALUES (@name)"
                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@name", catName)
                            cmd.ExecuteNonQuery()

                            cmd.CommandText = "SELECT id FROM categories WHERE name = @name"
                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@name", catName)
                            Dim catId = CInt(cmd.ExecuteScalar())

                            For Each item In cat.Elements("Item")
                                Dim nameEl = item.Element("Name")
                                Dim pathEl = item.Element("Path")
                                Dim iconPathEl = item.Element("IconPath")
                                Dim orderIdxEl = item.Element("OrderIndex")
                                Dim iconSrcEl = item.Element("IconSource")

                                Dim itemName As String = If(nameEl IsNot Nothing, nameEl.Value, "")
                                Dim itemPath As String = If(pathEl IsNot Nothing, pathEl.Value, "")
                                Dim iconPath As String = If(iconPathEl IsNot Nothing, iconPathEl.Value, "")
                                Dim orderIndex As Integer = 0
                                If orderIdxEl IsNot Nothing Then Integer.TryParse(orderIdxEl.Value, orderIndex)
                                Dim iconSource As String = If(iconSrcEl IsNot Nothing, iconSrcEl.Value, "Auto")

                                cmd.CommandText = "INSERT INTO items (category_id, name, path, icon_path, order_index, icon_source) VALUES (@catId, @name, @path, @iconPath, @orderIdx, @iconSrc)"
                                cmd.Parameters.Clear()
                                cmd.Parameters.AddWithValue("@catId", catId)
                                cmd.Parameters.AddWithValue("@name", itemName)
                                cmd.Parameters.AddWithValue("@path", itemPath)
                                cmd.Parameters.AddWithValue("@iconPath", If(String.IsNullOrEmpty(iconPath), "", iconPath))
                                cmd.Parameters.AddWithValue("@orderIdx", orderIndex)
                                cmd.Parameters.AddWithValue("@iconSrc", iconSource)
                                cmd.ExecuteNonQuery()
                            Next
                        Next
                    End If
                End Using

                transaction.Commit()
            End Using
        End Using
    End Sub

    Public Class DbItem
        Public Property Name As String
        Public Property Path As String
        Public Property IconPath As String
        Public Property OrderIndex As Integer
    End Class

    Public Class DbCategory
        Public Property Id As Integer
        Public Property Name As String
        Public Property Items As List(Of DbItem)
    End Class

    Public Shared Function GetCategories() As List(Of DbCategory)
        Dim result As New List(Of DbCategory)
        If _dataDir = "" OrElse Not File.Exists(DbPath) Then Return result

        Using conn As New SQLiteConnection(GetConnectionString())
            conn.Open()

            Using catCmd As New SQLiteCommand("SELECT id, name FROM categories ORDER BY id", conn)
                Using catReader = catCmd.ExecuteReader()
                    While catReader.Read()
                        Dim cat As New DbCategory With {
                            .Id = catReader.GetInt32(0),
                            .Name = catReader.GetString(1),
                            .Items = New List(Of DbItem)
                        }
                        result.Add(cat)
                    End While
                End Using
            End Using

            For Each cat In result
                Using itemCmd As New SQLiteCommand("SELECT name, path, icon_path, order_index FROM items WHERE category_id = @catId ORDER BY order_index", conn)
                    itemCmd.Parameters.AddWithValue("@catId", cat.Id)
                    Using itemReader = itemCmd.ExecuteReader()
                        While itemReader.Read()
                            cat.Items.Add(New DbItem With {
                                .Name = itemReader.GetString(0),
                                .Path = itemReader.GetString(1),
                                .IconPath = If(itemReader.IsDBNull(2), "", itemReader.GetString(2)),
                                .OrderIndex = itemReader.GetInt32(3)
                            })
                        End While
                    End Using
                End Using
            Next
        End Using

        Return result
    End Function

    Public Shared Function GetItemsByCategory(categoryName As String) As List(Of DbItem)
        Dim result As New List(Of DbItem)
        If _dataDir = "" OrElse Not File.Exists(DbPath) Then Return result

        Using conn As New SQLiteConnection(GetConnectionString())
            conn.Open()

            Using catCmd As New SQLiteCommand("SELECT id FROM categories WHERE name = @name", conn)
                catCmd.Parameters.AddWithValue("@name", categoryName)
                Dim catObj = catCmd.ExecuteScalar()
                If catObj Is Nothing Then Return result
                Dim catId = CInt(catObj)

                Using itemCmd As New SQLiteCommand("SELECT name, path, icon_path, order_index FROM items WHERE category_id = @catId ORDER BY order_index", conn)
                    itemCmd.Parameters.AddWithValue("@catId", catId)
                    Using itemReader = itemCmd.ExecuteReader()
                        While itemReader.Read()
                            result.Add(New DbItem With {
                                .Name = itemReader.GetString(0),
                                .Path = itemReader.GetString(1),
                                .IconPath = If(itemReader.IsDBNull(2), "", itemReader.GetString(2)),
                                .OrderIndex = itemReader.GetInt32(3)
                            })
                        End While
                    End Using
                End Using
            End Using
        End Using

        Return result
    End Function

    Public Shared Function AddCategory(name As String) As Integer
        If _dataDir = "" OrElse Not File.Exists(DbPath) Then Return -1

        Using conn As New SQLiteConnection(GetConnectionString())
            conn.Open()
            Using cmd As New SQLiteCommand("INSERT OR IGNORE INTO categories (name) VALUES (@name); SELECT id FROM categories WHERE name = @name", conn)
                cmd.Parameters.AddWithValue("@name", name)
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing Then Return CInt(result)
                Return -1
            End Using
        End Using
    End Function

    Public Shared Sub RenameCategory(oldName As String, newName As String)
        If _dataDir = "" OrElse Not File.Exists(DbPath) Then Return

        Using conn As New SQLiteConnection(GetConnectionString())
            conn.Open()
            Using cmd As New SQLiteCommand("UPDATE categories SET name = @newName WHERE name = @oldName", conn)
                cmd.Parameters.AddWithValue("@newName", newName)
                cmd.Parameters.AddWithValue("@oldName", oldName)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Shared Sub DeleteCategory(name As String)
        If _dataDir = "" OrElse Not File.Exists(DbPath) Then Return

        Using conn As New SQLiteConnection(GetConnectionString())
            conn.Open()
            Using cmd As New SQLiteCommand("DELETE FROM categories WHERE name = @name", conn)
                cmd.Parameters.AddWithValue("@name", name)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Shared Sub AddItem(categoryId As Integer, name As String, path As String, iconPath As String, orderIndex As Integer, Optional iconSource As String = "Auto")
        If _dataDir = "" OrElse Not File.Exists(DbPath) Then Return

        Using conn As New SQLiteConnection(GetConnectionString())
            conn.Open()
            Using cmd As New SQLiteCommand("INSERT INTO items (category_id, name, path, icon_path, order_index, icon_source) VALUES (@catId, @name, @path, @iconPath, @orderIdx, @iconSrc)", conn)
                cmd.Parameters.AddWithValue("@catId", categoryId)
                cmd.Parameters.AddWithValue("@name", name)
                cmd.Parameters.AddWithValue("@path", path)
                cmd.Parameters.AddWithValue("@iconPath", If(String.IsNullOrEmpty(iconPath), "", iconPath))
                cmd.Parameters.AddWithValue("@orderIdx", orderIndex)
                cmd.Parameters.AddWithValue("@iconSrc", iconSource)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Shared Sub UpdateItemIcon(categoryName As String, itemPath As String, newIconPath As String)
        If _dataDir = "" OrElse Not File.Exists(DbPath) Then Return

        Using conn As New SQLiteConnection(GetConnectionString())
            conn.Open()
            Using cmd As New SQLiteCommand("UPDATE items SET icon_path = @iconPath WHERE path = @path AND category_id = (SELECT id FROM categories WHERE name = @catName)", conn)
                cmd.Parameters.AddWithValue("@iconPath", newIconPath)
                cmd.Parameters.AddWithValue("@path", itemPath)
                cmd.Parameters.AddWithValue("@catName", categoryName)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Shared Sub UpdateItemPath(categoryName As String, oldPath As String, newPath As String)
        If _dataDir = "" OrElse Not File.Exists(DbPath) Then Return

        Using conn As New SQLiteConnection(GetConnectionString())
            conn.Open()
            Using cmd As New SQLiteCommand("UPDATE items SET path = @newPath WHERE path = @oldPath AND category_id = (SELECT id FROM categories WHERE name = @catName)", conn)
                cmd.Parameters.AddWithValue("@newPath", newPath)
                cmd.Parameters.AddWithValue("@oldPath", oldPath)
                cmd.Parameters.AddWithValue("@catName", categoryName)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Shared Sub UpdateItemName(categoryName As String, itemPath As String, newName As String)
        If _dataDir = "" OrElse Not File.Exists(DbPath) Then Return

        Using conn As New SQLiteConnection(GetConnectionString())
            conn.Open()
            Using cmd As New SQLiteCommand("UPDATE items SET name = @newName WHERE path = @path AND category_id = (SELECT id FROM categories WHERE name = @catName)", conn)
                cmd.Parameters.AddWithValue("@newName", newName)
                cmd.Parameters.AddWithValue("@path", itemPath)
                cmd.Parameters.AddWithValue("@catName", categoryName)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Shared Sub DeleteItem(categoryName As String, itemPath As String)
        If _dataDir = "" OrElse Not File.Exists(DbPath) Then Return

        Using conn As New SQLiteConnection(GetConnectionString())
            conn.Open()
            Using cmd As New SQLiteCommand("DELETE FROM items WHERE path = @path AND category_id = (SELECT id FROM categories WHERE name = @catName)", conn)
                cmd.Parameters.AddWithValue("@path", itemPath)
                cmd.Parameters.AddWithValue("@catName", categoryName)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Shared Function CopyItemToCategory(sourceCategoryName As String, targetCategoryName As String, itemName As String, itemPath As String, itemIconPath As String) As Boolean
        If _dataDir = "" OrElse Not File.Exists(DbPath) Then Return False

        Using conn As New SQLiteConnection(GetConnectionString())
            conn.Open()

            Dim sourceCatId = GetCategoryId(conn, sourceCategoryName)
            Dim targetCatId = GetCategoryId(conn, targetCategoryName)
            If sourceCatId < 0 Then Return False

            Dim sourceItemExists = False
            Using checkCmd As New SQLiteCommand("SELECT COUNT(*) FROM items WHERE category_id = @catId AND path = @path", conn)
                checkCmd.Parameters.AddWithValue("@catId", sourceCatId)
                checkCmd.Parameters.AddWithValue("@path", itemPath)
                sourceItemExists = CInt(checkCmd.ExecuteScalar()) > 0
            End Using
            If Not sourceItemExists Then Return False

            If targetCatId < 0 Then
                targetCatId = AddCategory(targetCategoryName)
            End If

            Dim targetOrderIndex As Integer = 0
            Using maxCmd As New SQLiteCommand("SELECT COALESCE(MAX(order_index), -1) FROM items WHERE category_id = @catId", conn)
                maxCmd.Parameters.AddWithValue("@catId", targetCatId)
                targetOrderIndex = CInt(maxCmd.ExecuteScalar()) + 1
            End Using

            Dim existingItem = False
            Using checkCmd As New SQLiteCommand("SELECT COUNT(*) FROM items WHERE category_id = @catId AND path = @path", conn)
                checkCmd.Parameters.AddWithValue("@catId", targetCatId)
                checkCmd.Parameters.AddWithValue("@path", itemPath)
                existingItem = CInt(checkCmd.ExecuteScalar()) > 0
            End Using

            If existingItem Then
                Using cmd As New SQLiteCommand("UPDATE items SET name = @name, icon_path = @iconPath WHERE category_id = @catId AND path = @path", conn)
                    cmd.Parameters.AddWithValue("@name", itemName)
                    cmd.Parameters.AddWithValue("@iconPath", If(String.IsNullOrEmpty(itemIconPath), "", itemIconPath))
                    cmd.Parameters.AddWithValue("@catId", targetCatId)
                    cmd.Parameters.AddWithValue("@path", itemPath)
                    cmd.ExecuteNonQuery()
                End Using
            Else
                Using cmd As New SQLiteCommand("INSERT INTO items (category_id, name, path, icon_path, order_index, icon_source) VALUES (@catId, @name, @path, @iconPath, @orderIdx, 'Auto')", conn)
                    cmd.Parameters.AddWithValue("@catId", targetCatId)
                    cmd.Parameters.AddWithValue("@name", itemName)
                    cmd.Parameters.AddWithValue("@path", itemPath)
                    cmd.Parameters.AddWithValue("@iconPath", If(String.IsNullOrEmpty(itemIconPath), "", itemIconPath))
                    cmd.Parameters.AddWithValue("@orderIdx", targetOrderIndex)
                    cmd.ExecuteNonQuery()
                End Using
            End If

            Return True
        End Using
    End Function

    Public Shared Function MoveItemToCategory(sourceCategoryName As String, targetCategoryName As String, itemName As String, itemPath As String, itemIconPath As String) As Boolean
        If CopyItemToCategory(sourceCategoryName, targetCategoryName, itemName, itemPath, itemIconPath) Then
            DeleteItem(sourceCategoryName, itemPath)
            Return True
        End If
        Return False
    End Function

    Public Shared Sub SaveAllData(tabControl As TabControl)
        If _dataDir = "" OrElse Not File.Exists(DbPath) Then Return

        Using conn As New SQLiteConnection(GetConnectionString())
            conn.Open()
            Using transaction = conn.BeginTransaction()
                Using cmd As New SQLiteCommand(conn)
                    cmd.Transaction = transaction

                    cmd.CommandText = "DELETE FROM items"
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "DELETE FROM categories"
                    cmd.ExecuteNonQuery()

                    For Each tab As TabPage In tabControl.TabPages
                        cmd.CommandText = "INSERT INTO categories (name) VALUES (@name)"
                        cmd.Parameters.Clear()
                        cmd.Parameters.AddWithValue("@name", tab.Text)
                        cmd.ExecuteNonQuery()

                        Dim catId = GetCategoryId(conn, tab.Text)

                        For Each ctrl In tab.Controls.OfType(Of FlowLayoutPanel)()
                            Dim orderIdx As Integer = 0
                            For Each itemPanel In ctrl.Controls.OfType(Of Panel)()
                                Dim itemData = TryCast(itemPanel.Tag, Object)
                                If itemData Is Nothing Then Continue For
                                Dim itemPath = itemData.Path
                                If String.IsNullOrEmpty(itemPath) Then Continue For

                                Dim itemName = ""
                                Dim lbl = itemPanel.Controls.OfType(Of Label)().FirstOrDefault()
                                If lbl IsNot Nothing Then itemName = lbl.Text

                                Dim itemIconPath = If(itemData.IconPath IsNot Nothing, itemData.IconPath.ToString(), "")

                                cmd.CommandText = "INSERT INTO items (category_id, name, path, icon_path, order_index, icon_source) VALUES (@catId, @name, @path, @iconPath, @orderIdx, 'Auto')"
                                cmd.Parameters.Clear()
                                cmd.Parameters.AddWithValue("@catId", catId)
                                cmd.Parameters.AddWithValue("@name", itemName)
                                cmd.Parameters.AddWithValue("@path", itemPath)
                                cmd.Parameters.AddWithValue("@iconPath", If(String.IsNullOrEmpty(itemIconPath), "", itemIconPath))
                                cmd.Parameters.AddWithValue("@orderIdx", orderIdx)
                                cmd.ExecuteNonQuery()

                                orderIdx += 1
                            Next
                        Next
                    Next
                End Using

                transaction.Commit()
            End Using
        End Using
    End Sub

    Private Shared Function GetCategoryId(conn As SQLiteConnection, categoryName As String) As Integer
        Using cmd As New SQLiteCommand("SELECT id FROM categories WHERE name = @name", conn)
            cmd.Parameters.AddWithValue("@name", categoryName)
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing Then Return CInt(result)
            Return -1
        End Using
    End Function

End Class
