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
        Return $"Data Source={DbPath};Version=3;"
    End Function

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
                cmd.CommandText = "
                    CREATE TABLE IF NOT EXISTS categories (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        name TEXT NOT NULL UNIQUE
                    );

                    CREATE TABLE IF NOT EXISTS items (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        category_id INTEGER NOT NULL,
                        name TEXT NOT NULL,
                        path TEXT NOT NULL,
                        icon_path TEXT DEFAULT '',
                        order_index INTEGER DEFAULT 0,
                        icon_source TEXT DEFAULT 'Auto',
                        FOREIGN KEY (category_id) REFERENCES categories(id) ON DELETE CASCADE
                    );
                "
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
                            Dim catName = cat.Attribute("Name")?.Value
                            If String.IsNullOrEmpty(catName) Then Continue For

                            cmd.CommandText = "INSERT OR IGNORE INTO categories (name) VALUES (@name)"
                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@name", catName)
                            cmd.ExecuteNonQuery()

                            cmd.CommandText = "SELECT id FROM categories WHERE name = @name"
                            cmd.Parameters.Clear()
                            cmd.Parameters.AddWithValue("@name", catName)
                            Dim catId = CInt(cmd.ExecuteScalar())

                            For Each item In cat.Elements("Item")
                                Dim itemName = item.Element("Name")?.Value
                                Dim itemPath = item.Element("Path")?.Value
                                Dim iconPath = item.Element("IconPath")?.Value
                                Dim orderIndex As Integer = 0
                                Integer.TryParse(item.Element("OrderIndex")?.Value, orderIndex)
                                Dim iconSource = If(item.Element("IconSource")?.Value, "Auto")

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

End Class
