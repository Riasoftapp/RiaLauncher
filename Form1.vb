Imports System.IO
Imports System.Drawing

Public Class Form1

    Private Const ICON_SIZE As Integer = 48
    ' IniManager removed - using local INI helpers in this form

    ' Global LanguageManager - Tüm formlardan erişilebilir
    Public Shared langManager As LanguageManager

    ' Private launchMode As String = "DoubleClick"
    ' Private viewMode As String = "IconText"
    ' Private alwaysOnTop As Boolean = False
    ' Private lastActiveTab As Integer = 0
    ' Private currentLanguage As String = "en"

    ' variable for directories - Public Shared so other forms can access
    Public Shared sRootDir As String = ""
    Public Shared sAssetDir As String = ""
    Public Shared sLogoDir As String = ""
    Public Shared sIconDir As String = ""
    Public Shared sDataDir As String = ""
    Public Shared slangDir As String = ""
    Public Shared sLogDir As String = ""
    Public Shared sDocDir As String = "" ' documentation dir 
    Public Shared sHelpDir As String = "" ' helpdir 

    ' variable for settings.ini
    Private launchMode As String = ""
    Private viewMode As String = ""
    Private alwaysOnTop As Boolean = False
    Private lastActiveTab As Integer = 0
    Private lastOpenDir As String = ""
    Private currentLanguage As String = ""
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        sRootDir = GetRootDir()
        sAssetDir = sRootDir & "\assets\"
        sLogoDir = sRootDir & "\assets\logo\"
        sIconDir = sRootDir & "\assets\icon\"
        sDataDir = sRootDir & "\data\"
        slangDir = sRootDir & "\assets\lang\"
        sLogDir = sRootDir & "\log\"
        sDocDir = sRootDir & "\assets\documentation\"
        sHelpDir = sRootDir & "\assets\documentation\RiaLauncherHelp"

        ' TabControl drag bloğu - AllowDrop = False yap
        TabControl1.AllowDrop = False

        If Not isSettingsIniExist() Then CreateDefaultIni()

        LoadIni()

        InitializeLanguageManager()
        LoadComboLang()
        ApplyCurrentLang()

        ' SQLite veritabanını başlat ve XML'den içe aktar (ilk geçiş)
        InitDatabase()

        ' Veritabanından verileri yükle
        LoadDataFromDb()

        ' Son açılan tab'ı geri yükle
        RestoreLastActiveTab()

        FlowLayoutPanel1.AllowDrop = True

        ' Bilgilendirme mesajı
        ' MsgBox("Startup işlemleri tamamlandı:" & vbCrLf &
        '     "sRootDir    : " & sRootDir & vbCrLf &
        '     "sAssetDir   : " & sAssetDir & vbCrLf &
        '     "sLogoDir    : " & sLogoDir & vbCrLf &
        '     "sIconDir    : " & sIconDir & vbCrLf &
        '     "sDataDir    : " & sDataDir & vbCrLf &
        '     "Programdan çıkılacak",
        '     MsgBoxStyle.Information,
        '     "Bilgi")


        ' Application.Exit() ' Programdan çıkış
        'PopulateLanguageComboBox()
        'CreateDefaultXmlIfNotExists()
        'LoadDataFromXml()
        'ApplySettings()
        'ApplyLanguage()
        'RestoreLastActiveTab()

        'Me.KeyPreview = True
    End Sub

    Public Function GetRootDir() As String
        Dim exePath As String = Application.ExecutablePath
        Dim exeDir As String = Path.GetDirectoryName(exePath)
        Dim lowerPath As String = exeDir.ToLower()

        If lowerPath.Contains("\bin\") Then
            ' Development ortamı → iki klasör yukarı çık
            Return Directory.GetParent(Directory.GetParent(exeDir).FullName).FullName
        Else
            ' Product ortamı → exe’nin bulunduğu klasör
            Return exeDir
        End If
    End Function
    Public Function isSettingsIniExist() As Boolean
        Dim iniPath As String = Path.Combine(sAssetDir, "settings.ini")
        Return File.Exists(iniPath)
    End Function
    Public Sub InitDatabase()
        Try
            DatabaseManager.SetDataDir(sDataDir)
            DatabaseManager.InitializeDatabase()
            If Not DatabaseManager.DatabaseExists() Then Return
            Dim sXmlPath As String = Path.Combine(sDataDir, "RiaLauncher.xml")
            If File.Exists(sXmlPath) Then
                DatabaseManager.ImportFromXml(sXmlPath)
            End If
        Catch ex As Exception
            MsgBox("SQLite veritabanı hatası: " & ex.Message, MsgBoxStyle.Critical, "Hata")
        End Try
    End Sub
    Public Function CreateDefaultIni() As Boolean
        Try
            Dim iniPath As String = Path.Combine(sAssetDir, "settings.ini")

            ' Dizinin varlığını kontrol et
            If Not System.IO.Directory.Exists(sAssetDir) Then
                System.IO.Directory.CreateDirectory(sAssetDir)
            End If

            ' INI dosyasına varsayılan değerleri yaz
            Dim lines As New List(Of String)
            lines.Add("[General]")
            lines.Add("Language=en")
            lines.Add("LastActiveTab=0")
            lines.Add("LastOpenDir=")
            lines.Add("[Launch]")
            lines.Add("LaunchMode=DoubleClick")
            lines.Add("ViewMode=IconText")
            lines.Add("AlwaysOnTop=false")

            File.WriteAllLines(iniPath, lines)
            Return True

        Catch ex As Exception
            MsgBox("Settings.ini oluşturulamadı: " & vbCrLf & ex.Message, MsgBoxStyle.Critical, "Hata")
            Return False
        End Try
    End Function
    Public Sub LoadIni()
        Try
            Dim iniPath As String = Path.Combine(sAssetDir, "settings.ini")

            ' INI dosyasından değerleri oku
            Dim lines = File.ReadAllLines(iniPath)

            For Each line In lines
                If line.StartsWith("Language=") Then
                    currentLanguage = line.Split("=")(1)
                ElseIf line.StartsWith("LastActiveTab=") Then
                    Integer.TryParse(line.Split("=")(1), lastActiveTab)
                ElseIf line.StartsWith("LastOpenDir=") Then
                    lastOpenDir = line.Split("=")(1)
                ElseIf line.StartsWith("LaunchMode=") Then
                    launchMode = line.Split("=")(1)
                ElseIf line.StartsWith("ViewMode=") Then
                    viewMode = line.Split("=")(1)
                ElseIf line.StartsWith("AlwaysOnTop=") Then
                    Boolean.TryParse(line.Split("=")(1), alwaysOnTop)
                End If
            Next

            ' MsgBox("loadini :" & vbCrLf &
            ' "launchMode                      : " & launchMode & vbCrLf &
            ' "viewMode                        : " & viewMode & vbCrLf &
            ' "alwaysOnTop                     : " & alwaysOnTop & vbCrLf &
            ' "lastActiveTab                   : " & lastActiveTab & vbCrLf &
            ' "lastOpenDir       : " & lastOpenDir & vbCrLf &
            ' "currentLanguage                 : " & currentLanguage,
            ' MsgBoxStyle.Information,
            ' "Bilgi")


        Catch ex As Exception
            MsgBox("Settings.ini okunamadı: " & vbCrLf & ex.Message, MsgBoxStyle.Critical, "Hata")
            Application.Exit()
        End Try
    End Sub

    ' ======Yeni Prosedür ve Fonksiyonlar========    
    ''' <summary>
    ''' ComboLang'ı dil dosyalarıyla doldurur. Form başlangıcında bir kez çağrılır.
    ''' currentLanguage'ı INI'den okunan değerle senkronize eder.
    ''' </summary>
    Private Sub LoadComboLang()
        ComboLang.Items.Clear()

        ' Lang dizinindeki dil dosyalarını oku
        Dim langFiles() As String = {}
        If System.IO.Directory.Exists(slangDir) Then
            langFiles = System.IO.Directory.GetFiles(slangDir, "*.lng")
        End If

        If langFiles.Length = 0 Then
            ' Hiç dil dosyası yok — sadece "en" ekle
            ComboLang.Items.Add("en")
            If String.IsNullOrEmpty(currentLanguage) Then
                currentLanguage = "en"
            End If
            ComboLang.SelectedIndex = 0
        Else
            ' Dil dosyalarını combo'ya ekle
            For Each f In langFiles
                Dim langCode As String = System.IO.Path.GetFileNameWithoutExtension(f).ToLower()
                ComboLang.Items.Add(langCode)
            Next

            ' INI'den okunan currentLanguage combo'da var mı kontrol et
            Dim found As Boolean = False
            If Not String.IsNullOrEmpty(currentLanguage) Then
                For i As Integer = 0 To ComboLang.Items.Count - 1
                    If ComboLang.Items(i).ToString().ToLower() = currentLanguage.ToLower() Then
                        ComboLang.SelectedIndex = i
                        found = True
                        Exit For
                    End If
                Next
            End If

            If Not found Then
                MsgBox("Uygulama dili (" & currentLanguage & ") dil dosyası bulunamadı. Default dil ""EN"" kullanılacak.",
                       MsgBoxStyle.Exclamation, "Dil Uyarısı")
                currentLanguage = "en"
                For i As Integer = 0 To ComboLang.Items.Count - 1
                    If ComboLang.Items(i).ToString().ToLower() = "en" Then
                        ComboLang.SelectedIndex = i
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub
    ''' <summary>
    ''' Seçili dili uygulamaya yüklenir ve UI'ı günceller.
    ''' ComboLang değişince veya Form başlangıcında LoadComboLang() sonrası çağrılır.
    ''' </summary>
    Private Sub ApplyCurrentLang()
        If langManager Is Nothing OrElse ComboLang.SelectedIndex < 0 Then Return

        Dim selectedLang As String = ComboLang.SelectedItem.ToString().ToLower()
        currentLanguage = selectedLang

        ' Dil yöneticisine dili bildir
        langManager.SetLanguage(selectedLang)

        ' RTL desteği: Arapça seçildiğinde UI yönünü değiştir
        If langManager.IsRTLLanguage(selectedLang) Then
            Me.RightToLeft = RightToLeft.Yes
            Me.RightToLeftLayout = True
        Else
            Me.RightToLeft = RightToLeft.No
            Me.RightToLeftLayout = False
        End If

        ' Tüm UI öğelerini seçili dille güncelle
        ApplyLanguage()

        ' Ayarları kaydet
        SaveSettingsToIni()
    End Sub


    ' =======Eski Prosedür ve Fonksiyonlar ===========
    Private Sub SetFormIcon()
        Try
            Dim iconPath As String = Path.Combine(sLogoDir, "winLuncher48x48.ico")
            If File.Exists(iconPath) Then
                Me.Icon = New Icon(iconPath)
            End If
        Catch ex As Exception
            ' Icon yüklenemezse sessizce devam et
        End Try
    End Sub
    Private Sub MenuManuelSiralama_Click(sender As Object, e As EventArgs) Handles MenuManuelSiralama.Click
        ShowManualSortForm()
    End Sub

    Private Sub InitializeIniManager()
        ' IniManager removed - no initialization required
    End Sub

    Private Sub InitializeLanguageManager()
        langManager = New LanguageManager(slangDir, "en")
    End Sub

    Private Sub LoadSettingsFromIni()
        ' General settings
        'currentLanguage = iniManager.ReadValue("General", "Language", "en")
        'If String.IsNullOrWhiteSpace(currentLanguage) OrElse currentLanguage.Equals("tr", StringComparison.OrdinalIgnoreCase) Then
        '    currentLanguage = "en"
        'End If
        'lastActiveTab = iniManager.ReadInteger("General", "LastActiveTab", 0)
        '' Launch settings
        'launchMode = iniManager.ReadValue("Launch", "LaunchMode", "DoubleClick")
        'viewMode = iniManager.ReadValue("Launch", "ViewMode", "IconText")
        'alwaysOnTop = iniManager.ReadBoolean("Launch", "AlwaysOnTop", False)
        '
        '' Apply language from ini
        'If langManager IsNot Nothing Then
        '    langManager.SetLanguage(currentLanguage)
        '
        '    ' RTL desteği: Arapça seçilmişse UI yönünü değiştir
        '    If langManager.IsRTLLanguage(currentLanguage) Then
        '        Me.RightToLeft = RightToLeft.Yes
        '        Me.RightToLeftLayout = True
        '    Else
        '        Me.RightToLeft = RightToLeft.No
        '        Me.RightToLeftLayout = False
        '    End If
        'End If
    End Sub

    Private Sub SaveSettingsToIni()
        Try
            Dim iniPath As String = Path.Combine(sAssetDir, "settings.ini")
            If Not Directory.Exists(sAssetDir) Then Directory.CreateDirectory(sAssetDir)

            Dim lines As New List(Of String)
            lines.Add("[General]")
            lines.Add("Language=" & currentLanguage)
            lines.Add("LastActiveTab=" & TabControl1.SelectedIndex.ToString())
            lines.Add("LastOpenDir=" & lastOpenDir)
            lines.Add("[Launch]")
            lines.Add("LaunchMode=" & launchMode)
            lines.Add("ViewMode=" & viewMode)
            lines.Add("AlwaysOnTop=" & alwaysOnTop.ToString().ToLower())

            File.WriteAllLines(iniPath, lines)
        Catch ex As Exception
            MsgBox("Settings.ini kaydedilemedi: " & vbCrLf & ex.Message, MsgBoxStyle.Critical, "Hata")
        End Try
    End Sub
    Private Function ReadIniKey(keyName As String) As String
        Dim iniPath As String = Path.Combine(sAssetDir, "settings.ini")
        If Not File.Exists(iniPath) Then Return String.Empty
        For Each line In File.ReadAllLines(iniPath)
            If line.StartsWith(keyName & "=") Then
                Dim parts = line.Split(New Char() {"="c}, 2)
                If parts.Length > 1 Then Return parts(1)
            End If
        Next
        Return String.Empty
    End Function
    Private Sub RestoreLastActiveTab()
        If lastActiveTab >= 0 AndAlso lastActiveTab < TabControl1.TabPages.Count Then
            TabControl1.SelectedIndex = lastActiveTab
        End If
    End Sub

    Private Sub LoadDataFromDb()
        Try
            TabControl1.TabPages.Clear()

            Dim categories = DatabaseManager.GetCategories()

            For Each cat In categories
                Dim newTab As New TabPage(cat.Name)

                Dim flowPanel As New FlowLayoutPanel With {
                    .Dock = DockStyle.Fill,
                    .AutoScroll = True,
                    .AllowDrop = True
                }

                AddHandler flowPanel.DragEnter, AddressOf FlowPanel_DragEnter
                AddHandler flowPanel.DragOver, AddressOf FlowPanel_DragOver
                AddHandler flowPanel.DragDrop, AddressOf FlowPanel_DragDrop

                For Each item In cat.Items
                    If File.Exists(item.Path) OrElse Directory.Exists(item.Path) Then
                        AddLauncherItem(flowPanel, item.Name, item.Path, item.IconPath)
                    End If
                Next

                newTab.Controls.Add(flowPanel)
                TabControl1.TabPages.Add(newTab)
            Next

            If TabControl1.TabPages.Count = 0 Then
                AddDefaultTab()
            End If

        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgXMLLoadError", "Veritabanı yükleme hatası: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            AddDefaultTab()
        End Try
    End Sub
    Private Sub AddDefaultTab()
        Dim defaultTab As New TabPage("Development")
        Dim flowPanel As New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .AutoScroll = True,
            .AllowDrop = True
        }

        AddHandler flowPanel.DragEnter, AddressOf FlowPanel_DragEnter
        AddHandler flowPanel.DragOver, AddressOf FlowPanel_DragOver
        AddHandler flowPanel.DragDrop, AddressOf FlowPanel_DragDrop

        defaultTab.Controls.Add(flowPanel)
        TabControl1.TabPages.Add(defaultTab)
    End Sub

    Private Sub FlowPanel_DragEnter(sender As Object, e As DragEventArgs)
        ' FileDrop (dosya), Text (metin), Shell IDList Array (Denetim Masası, vb.) ve diğer format'ları accept et
        If e.Data.GetDataPresent(DataFormats.FileDrop) OrElse
            e.Data.GetDataPresent(DataFormats.Text) OrElse
            e.Data.GetDataPresent("Shell IDList Array") OrElse
            e.Data.GetDataPresent("FileGroupDescriptorW") Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub FlowPanel_DragDrop(sender As Object, e As DragEventArgs)
        Dim flowPanel As FlowLayoutPanel = DirectCast(sender, FlowLayoutPanel)

        ' Dosya sürükle-bırak (Windows Explorer'dan)
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim files() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())

            For Each filePath In files
                Dim itemName As String = Path.GetFileNameWithoutExtension(filePath)
                Dim targetPath As String = filePath

                If Path.GetExtension(filePath).ToLower() = ".lnk" Then
                    targetPath = ResolveShortcut(filePath)
                    If String.IsNullOrEmpty(targetPath) Then targetPath = filePath
                End If

                AddLauncherItem(flowPanel, itemName, targetPath, "")
            Next

            SaveDataToDb()
        End If
    End Sub
    Private Function ResolveShortcut(shortcutPath As String) As String
        Try
            Dim shell = CreateObject("WScript.Shell")
            Dim shortcut = shell.CreateShortcut(shortcutPath)
            Return shortcut.TargetPath
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Private Sub AddLauncherItem(flowPanel As FlowLayoutPanel, name As String, path As String, iconPath As String)
        Dim itemPanel As New Panel With {
            .Width = 80,
            .Height = 100,
            .Margin = New Padding(5)
        }
        ' Tag'de path ve iconPath'i sakla
        itemPanel.Tag = New With {.Path = path, .IconPath = iconPath}

        Dim picBox As New PictureBox With {
            .Width = ICON_SIZE,
            .Height = ICON_SIZE,
            .Location = New Point((itemPanel.Width - ICON_SIZE) \ 2, 5),
            .SizeMode = PictureBoxSizeMode.StretchImage,
            .Cursor = Cursors.Hand
        }

        ' Resim dosyası ise thumbnail yükle, SVG ise özel icon, değilse icon çıkar
        Dim imageExtensions() As String = {".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".webp"}
        Dim extension As String = IO.Path.GetExtension(path).ToLower()

        If extension = ".svg" Then
            ' SVG dosyası ise özel SVG iconu kullan
            Dim svgIconPath As String = IO.Path.Combine(sIconDir, "system", "svg64.png")
            If File.Exists(svgIconPath) Then
                Try
                    picBox.Image = New Bitmap(svgIconPath)
                Catch
                    ' SVG icon yüklenemezse sistem imajı
                    Dim icon As Icon = ExtractIcon(path, iconPath)
                    If icon IsNot Nothing Then
                        picBox.Image = icon.ToBitmap()
                    End If
                End Try
            Else
                ' SVG icon yoksa sistem imajı
                Dim icon As Icon = ExtractIcon(path, iconPath)
                If icon IsNot Nothing Then
                    picBox.Image = icon.ToBitmap()
                End If
            End If
        ElseIf imageExtensions.Contains(extension) Then
            ' Resim dosyası için thumbnail yükle
            Dim thumbnail As Image = LoadImageThumbnail(path)
            If thumbnail IsNot Nothing Then
                picBox.Image = thumbnail
            Else
                ' Thumbnail yüklenemezse sistem imajı
                Dim icon As Icon = ExtractIcon(path, iconPath)
                If icon IsNot Nothing Then
                    picBox.Image = icon.ToBitmap()
                End If
            End If
        Else
            ' Diğer dosyalar için icon çıkar
            Dim icon As Icon = ExtractIcon(path, iconPath)
            If icon IsNot Nothing Then
                picBox.Image = icon.ToBitmap()
            End If
        End If

        ' Label metnini belirle
        Dim displayName As String = name
        Try
            ' Eğer path bir disk sürücüsü ise (C:\, D:\, P:\ gibi)
            If Not String.IsNullOrEmpty(path) AndAlso path.Length >= 2 Then
                If path.Length = 3 AndAlso path.EndsWith(":\") Then
                    ' Disk root (C:\, D:\, P:\) - sadece harfi göster
                    displayName = path.Substring(0, 2) ' "C:", "D:", "P:"
                ElseIf Directory.Exists(path) Then
                    ' Klasör ise - eğer name kısa ise path'in son kısmını göster
                    If String.IsNullOrEmpty(name) OrElse name.Length < 3 Then
                        displayName = IO.Path.GetFileName(path.TrimEnd("\"c))
                        If String.IsNullOrEmpty(displayName) Then
                            displayName = path
                        End If
                    End If
                End If
            End If
        Catch
            ' Hata varsa name'i kullan
        End Try

        Dim lblName As New Label With {
            .Text = displayName,
            .AutoSize = False,
            .Width = itemPanel.Width,
            .Height = 40,
            .Location = New Point(0, ICON_SIZE + 10),
            .TextAlign = ContentAlignment.TopCenter,
            .Cursor = Cursors.Hand
        }

        ' Event handler'ları ekle
        If launchMode = "SingleClick" Then
            AddHandler picBox.Click, Sub() LaunchItem(path)
            AddHandler lblName.Click, Sub() LaunchItem(path)
        Else
            AddHandler picBox.DoubleClick, Sub() LaunchItem(path)
            AddHandler lblName.DoubleClick, Sub() LaunchItem(path)
        End If

        ' Context menu için MouseDown
        AddHandler picBox.MouseDown, Sub(s, e)
                                         If e.Button = MouseButtons.Right Then
                                             selectedItemPanel = itemPanel
                                             ContextMenuStripItem.Show(Cursor.Position)
                                         End If
                                     End Sub

        AddHandler lblName.MouseDown, Sub(s, e)
                                          If e.Button = MouseButtons.Right Then
                                              selectedItemPanel = itemPanel
                                              ContextMenuStripItem.Show(Cursor.Position)
                                          End If
                                      End Sub

        ' itemPanel için sağ tık menüsü
        AddHandler itemPanel.MouseDown, Sub(s, e)
                                            If e.Button = MouseButtons.Right Then
                                                selectedItemPanel = itemPanel
                                                ContextMenuStripItem.Show(Cursor.Position)
                                            End If
                                        End Sub

        If viewMode = "IconOnly" Then
            lblName.Visible = False
        End If

        itemPanel.Controls.Add(picBox)
        itemPanel.Controls.Add(lblName)
        flowPanel.Controls.Add(itemPanel)
    End Sub
    Private Function ExtractIcon(filePath As String, customIconPath As String) As Icon
        Try
            If Not String.IsNullOrEmpty(customIconPath) AndAlso File.Exists(customIconPath) Then
                Return Icon.ExtractAssociatedIcon(customIconPath)
            End If

            If File.Exists(filePath) Then
                Return Icon.ExtractAssociatedIcon(filePath)
            ElseIf Directory.Exists(filePath) Then
                Return GetFolderIcon(filePath)
            End If
        Catch ex As Exception
        End Try

        Return SystemIcons.Application
    End Function

    ' Resim dosyası için thumbnail yükle
    Private Function LoadImageThumbnail(imagePath As String) As Image
        Try
            If Not File.Exists(imagePath) Then
                Return Nothing
            End If

            ' Dosyayı memory'e yükle ve Bitmap oluştur
            Using stream As New FileStream(imagePath, FileMode.Open, FileAccess.Read)
                ' Stream'den Bitmap oluştur (thumbnail cache olmadan)
                Dim bitmap As New Bitmap(stream)
                Return bitmap
            End Using
        Catch ex As Exception
            ' Hata durumunda null dön (fallback icon yapılacak)
            Return Nothing
        End Try
    End Function
    Private Function GetFolderIcon(folderPath As String) As Icon
        Try
            Dim shInfo As New SHFILEINFO()
            Dim flags As Integer = SHGFI_ICON Or SHGFI_LARGEICON

            ' folderPath'i kullan - eğer drive root ise (C:\, D:\ etc.) drive icon gelir
            ' Normal klasörse klasör icon gelir
            SHGetFileInfo(folderPath, 0, shInfo, Runtime.InteropServices.Marshal.SizeOf(shInfo), flags)

            If shInfo.hIcon <> IntPtr.Zero Then
                Dim icon As Icon = Icon.FromHandle(shInfo.hIcon).Clone()
                DestroyIcon(shInfo.hIcon)
                Return icon
            End If
        Catch ex As Exception
        End Try

        Return SystemIcons.Application
    End Function

    <Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Sequential)>
    Private Structure SHFILEINFO
        Public hIcon As IntPtr
        Public iIcon As Integer
        Public dwAttributes As Integer
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=260)>
        Public szDisplayName As String
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=80)>
        Public szTypeName As String
    End Structure

    Private Const SHGFI_ICON As Integer = &H100
    Private Const SHGFI_LARGEICON As Integer = &H0

    <Runtime.InteropServices.DllImport("shell32.dll")>
    Private Shared Function SHGetFileInfo(pszPath As String, dwFileAttributes As Integer, ByRef psfi As SHFILEINFO, cbFileInfo As Integer, uFlags As Integer) As IntPtr
    End Function

    <Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function DestroyIcon(hIcon As IntPtr) As Boolean
    End Function

    Private Sub LaunchItem(path As String)
        Try
            If File.Exists(path) Then
                ' Tüm dosyalar sistemin default viewer/uygulaması ile açılır
                Process.Start(path)
            ElseIf Directory.Exists(path) Then
                ' Klasör ise Explorer ile aç
                Process.Start(path)
            Else
                Dim msg As String = String.Format(langManager.GetText("MsgFileNotFound", "File not found: {0}"), path)
                MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgLaunchError", "Launch error: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveDataToDb()
        Try
            DatabaseManager.SaveAllData(TabControl1)
        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgSaveError", "Save error: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnNewTab_Click(sender As Object, e As EventArgs)
        Dim prompt As String = langManager.GetText("MenuSekmelerYeni", "New Tab")
        Dim title As String = langManager.GetText("MenuSekmelerYeni", "New Tab")
        Dim tabName As String = InputBox(prompt, title, "New Category")
        If String.IsNullOrWhiteSpace(tabName) Then Return

        Dim newTab As New TabPage(tabName)
        Dim flowPanel As New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .AutoScroll = True,
            .AllowDrop = True
        }

        AddHandler flowPanel.DragEnter, AddressOf FlowPanel_DragEnter
        AddHandler flowPanel.DragOver, AddressOf FlowPanel_DragOver
        AddHandler flowPanel.DragDrop, AddressOf FlowPanel_DragDrop

        newTab.Controls.Add(flowPanel)
        TabControl1.TabPages.Add(newTab)
        TabControl1.SelectedTab = newTab

        SaveDataToDb()
    End Sub

    Private Sub btnDeleteTab_Click(sender As Object, e As EventArgs)
        If TabControl1.TabPages.Count <= 1 Then
            MessageBox.Show(langManager.GetText("MsgAtLeastOneTab", "At least one tab is required."), langManager.GetText("MsgWarning", "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If TabControl1.SelectedTab Is Nothing Then Return

        Dim confirmMsg As String = String.Format(langManager.GetText("MsgDeleteTabConfirm", "Are you sure you want to delete '{0}'?"), TabControl1.SelectedTab.Text)
        Dim result = MessageBox.Show(confirmMsg, langManager.GetText("MsgDeleteTabTitle", "Delete Tab"), MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            TabControl1.TabPages.Remove(TabControl1.SelectedTab)
            SaveDataToDb()
        End If
    End Sub

    Private Sub btnRenameTab_Click(sender As Object, e As EventArgs)
        If TabControl1.SelectedTab Is Nothing Then Return

        Dim prompt As String = langManager.GetText("MenuSekmelerAdDegistir", "Rename Tab")
        Dim title As String = langManager.GetText("MenuSekmelerAdDegistir", "Rename Tab")
        Dim newName As String = InputBox(prompt, title, TabControl1.SelectedTab.Text)
        If Not String.IsNullOrWhiteSpace(newName) Then
            TabControl1.SelectedTab.Text = newName
            SaveDataToDb()
        End If
    End Sub

    Private selectedItemPanel As Panel = Nothing

    Private Sub MenuItemLaunch_Click(sender As Object, e As EventArgs) Handles MenuItemLaunch.Click
        If selectedItemPanel IsNot Nothing AndAlso selectedItemPanel.Tag IsNot Nothing Then
            Dim itemData = TryCast(selectedItemPanel.Tag, Object)
            If itemData IsNot Nothing Then
                LaunchItem(itemData.Path)
            End If
        End If
    End Sub

    Private Sub MenuItemRename_Click(sender As Object, e As EventArgs) Handles MenuItemRename.Click
        If selectedItemPanel Is Nothing Then Return

        Dim lblName = selectedItemPanel.Controls.OfType(Of Label)().FirstOrDefault()
        If lblName Is Nothing Then Return

        Dim prompt As String = langManager.GetText("MenuItemRename", "Rename")
        Dim title As String = langManager.GetText("MenuItemRename", "Rename Item")
        Dim newName As String = InputBox(prompt, title, lblName.Text)
        If Not String.IsNullOrWhiteSpace(newName) Then
            lblName.Text = newName
            SaveDataToDb()
        End If
    End Sub

    Private Sub MenuItemChangeIcon_Click(sender As Object, e As EventArgs) Handles MenuItemChangeIcon.Click
        If selectedItemPanel Is Nothing Then Return

        Dim wasTopMost As Boolean = Me.TopMost
        Me.TopMost = False

        Try
            Using ofd As New OpenFileDialog()
                ofd.Filter = "Icon Files|*.ico;*.png;*.jpg;*.bmp|All Files|*.*"
                ofd.InitialDirectory = If(String.IsNullOrEmpty(lastOpenDir), sIconDir, lastOpenDir)
                ofd.Title = "Select Icon"

                If ofd.ShowDialog(Me) = DialogResult.OK Then
                    ' Son kullanılan klasörü kaydet
                    lastOpenDir = Path.GetDirectoryName(ofd.FileName)
                    SaveSettingsToIni()

                    Dim picBox = selectedItemPanel.Controls.OfType(Of PictureBox)().FirstOrDefault()
                    If picBox IsNot Nothing Then
                        Dim newIcon As Icon = Icon.ExtractAssociatedIcon(ofd.FileName)
                        If newIcon IsNot Nothing Then
                            picBox.Image = newIcon.ToBitmap()

                            ' Tag'deki iconPath'i güncelle
                            Dim currentTag = TryCast(selectedItemPanel.Tag, Object)
                            If currentTag IsNot Nothing Then
                                selectedItemPanel.Tag = New With {.Path = currentTag.Path, .IconPath = ofd.FileName}
                            End If

                            SaveDataToDb()
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgIconLoadError", "Icon loading error: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Me.TopMost = wasTopMost
    End Sub

    Private Sub MenuItemUpdatePath_Click(sender As Object, e As EventArgs) Handles MenuItemUpdatePath.Click
        If selectedItemPanel Is Nothing Then Return

        Dim wasTopMost As Boolean = Me.TopMost
        Me.TopMost = False

        Using ofd As New OpenFileDialog()
            ofd.Filter = "All Files|*.*|Applications|*.exe;*.lnk"
            ofd.Title = "Select New Path"
            ofd.CheckFileExists = True

            If ofd.ShowDialog(Me) = DialogResult.OK Then
                Dim newPath As String = ofd.FileName
                If Path.GetExtension(newPath).ToLower() = ".lnk" Then
                    newPath = ResolveShortcut(newPath)
                    If String.IsNullOrEmpty(newPath) Then newPath = ofd.FileName
                End If

                ' Tag'i güncelle
                Dim currentTag = TryCast(selectedItemPanel.Tag, Object)
                Dim currentIconPath As String = If(currentTag IsNot Nothing, currentTag.IconPath, "")
                selectedItemPanel.Tag = New With {.Path = newPath, .IconPath = currentIconPath}

                Dim picBox = selectedItemPanel.Controls.OfType(Of PictureBox)().FirstOrDefault()
                If picBox IsNot Nothing Then
                    Dim newIcon As Icon = ExtractIcon(newPath, "")
                    If newIcon IsNot Nothing Then
                        picBox.Image = newIcon.ToBitmap()
                    End If
                End If

                SaveDataToDb()
                MessageBox.Show(langManager.GetText("MsgPathUpdated", "Path updated successfully."), langManager.GetText("MsgInfo", "Information"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Using

        Me.TopMost = wasTopMost
    End Sub

    Private Sub MenuItemOpenFolder_Click(sender As Object, e As EventArgs) Handles MenuItemOpenFolder.Click
        If selectedItemPanel Is Nothing OrElse selectedItemPanel.Tag Is Nothing Then Return

        Dim itemData = TryCast(selectedItemPanel.Tag, Object)
        If itemData Is Nothing Then Return

        Dim itemPath As String = itemData.Path

        Try
            If File.Exists(itemPath) Then
                ' Dosya ise, dosyanın bulunduğu klasörü aç ve dosyayı seç
                Process.Start("explorer.exe", "/select,""" & itemPath & """")
            ElseIf Directory.Exists(itemPath) Then
                ' Klasör ise, direkt klasörü aç
                Process.Start("explorer.exe", itemPath)
            Else
                Dim msg As String = String.Format(langManager.GetText("MsgFileOrFolderNotFound", "File or folder not found: {0}"), itemPath)
                MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgOpenFolderError", "An error occurred while opening the folder: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuItemDelete_Click(sender As Object, e As EventArgs) Handles MenuItemDelete.Click
        If selectedItemPanel Is Nothing Then Return

        Dim lblName = selectedItemPanel.Controls.OfType(Of Label)().FirstOrDefault()
        Dim itemName As String = If(lblName IsNot Nothing, lblName.Text, "This item")

        Dim confirmMsg As String = String.Format(langManager.GetText("MsgDeleteItemConfirm", "Are you sure you want to delete '{0}'?"), itemName)
        Dim result = MessageBox.Show(confirmMsg, langManager.GetText("MsgDeleteItemTitle", "Delete Item"), MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Dim parent = selectedItemPanel.Parent
            If parent IsNot Nothing Then
                parent.Controls.Remove(selectedItemPanel)
                selectedItemPanel.Dispose()
                SaveDataToDb()
            End If
        End If
    End Sub

    Private Sub MenuItemProperties_Click(sender As Object, e As EventArgs) Handles MenuItemProperties.Click
        If selectedItemPanel Is Nothing Then Return

        Dim lblName = selectedItemPanel.Controls.OfType(Of Label)().FirstOrDefault()
        Dim itemName As String = If(lblName IsNot Nothing, lblName.Text, "Unknown")

        Dim itemData = TryCast(selectedItemPanel.Tag, Object)
        Dim itemPath As String = If(itemData IsNot Nothing, itemData.Path, "Unknown")
        Dim itemIconPath As String = If(itemData IsNot Nothing, itemData.IconPath, "")

        Dim fileExists As String = "No"
        If File.Exists(itemPath) Then
            fileExists = "Yes (File)"
        ElseIf Directory.Exists(itemPath) Then
            fileExists = "Yes (Folder)"
        End If

        Dim properties As String = "Name: " & itemName & vbCrLf &
                                   "Path: " & itemPath & vbCrLf &
                                   "Exists: " & fileExists & vbCrLf &
                                   "Custom Icon: " & If(String.IsNullOrEmpty(itemIconPath), "None", "Yes")

        MessageBox.Show(properties, langManager.GetText("MsgItemPropertiesTitle", "Item Properties"), MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub ApplySettings()
        Me.TopMost = alwaysOnTop

        ' Mevcut sekmelerdeki öğelere view mode'u uygula
        For Each tab As TabPage In TabControl1.TabPages
            Dim flowPanel = tab.Controls.OfType(Of FlowLayoutPanel)().FirstOrDefault()
            If flowPanel IsNot Nothing Then
                For Each panel As Panel In flowPanel.Controls.OfType(Of Panel)()
                    Dim lbl = panel.Controls.OfType(Of Label)().FirstOrDefault()
                    If lbl IsNot Nothing Then
                        lbl.Visible = (viewMode <> "IconOnly")
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub ApplyLanguage()
        If langManager Is Nothing Then Return

        ' MenuStrip
        MenuDosya.Text = langManager.GetText("MenuDosya", "&File")
        MenuDosyaCikis.Text = langManager.GetText("MenuDosyaCikis", "Exit")

        MenuSekmeler.Text = langManager.GetText("MenuSekmeler", "&Tabs")
        MenuSekmelerYeni.Text = langManager.GetText("MenuSekmelerYeni", "New Tab")
        MenuSekmelerAdDegistir.Text = langManager.GetText("MenuSekmelerAdDegistir", "Rename Tab")
        MenuSekmelerSil.Text = langManager.GetText("MenuSekmelerSil", "Delete Tab")
        MenuSekmelerYenile.Text = langManager.GetText("MenuSekmelerYenile", "Refresh Tab")

        MenuSiralama.Text = langManager.GetText("MenuSiralama", "&Sort")
        MenuManuelSiralama.Text = langManager.GetText("MenuManuelSiralama", "&Manual Sort...")

        MenuTools.Text = langManager.GetText("MenuAraclar", "&Tools")
        MenuToolsCmd.Text = langManager.GetText("MenuToolsCmd", "Command Prompt")
        MenuToolsPowershell.Text = langManager.GetText("MenuToolsPowershell", "PowerShell")
        MenuToolsTaskMgr.Text = langManager.GetText("MenuToolsTaskMgr", "Task Manager")
        MenuToolsServices.Text = langManager.GetText("MenuToolsServices", "Services Manager")
        MenuToolsShowDesktop.Text = langManager.GetText("MenuToolsShowDesktop", "Show Desktop")
        MenuToolsRestoreDesktop.Text = langManager.GetText("MenuToolsRestoreDesktop", "Restore Desktop")
        MenuToolsControlPanel.Text = langManager.GetText("MenuToolsControlPanel", "Control Panel")
        MenuToolsNetworkCenter.Text = langManager.GetText("MenuToolsNetworkCenter", "Network and Sharing Center")
        MenuToolsDeviceManager.Text = langManager.GetText("MenuToolsDeviceManager", "Device Manager")
        MenuToolsComputerName.Text = langManager.GetText("MenuToolsComputerName", "Show Computer Name")
        MenuToolsIPAddress.Text = langManager.GetText("MenuToolsIPAddress", "Show IP Addresses")

        REM MenuAyarlar.Text = langManager.GetText("MenuAyarlar", "&Settings")

        MenuYardim.Text = langManager.GetText("MenuYardim", "&Help")
        MenuYardimDokumanlar.Text = langManager.GetText("MenuYardimDokumanlar", "&Help")
        MenuYardimDokumanIndir.Text = langManager.GetText("MenuYardimDokumanIndir", "&Download Docs")
        MenuYardimLisans.Text = langManager.GetText("MenuYardimLisans", "&License Terms")
        MenuYardimBagis.Text = langManager.GetText("MenuYardimBagis", "&Donate")
        MenuYardimAnaSayfa.Text = langManager.GetText("MenuYardimAnaSayfa", "Home &Page")
        MenuYardimHakkinda.Text = langManager.GetText("MenuYardimHakkinda", "&About...")

        ' Context Menu - Item
        MenuItemLaunch.Text = langManager.GetText("MenuItemLaunch", "Launch")
        MenuItemCopyMove.Text = langManager.GetText("MenuItemCopyMove", "Copy/Move...")
        MenuItemRename.Text = langManager.GetText("MenuItemRename", "Rename")
        MenuItemChangeIcon.Text = langManager.GetText("MenuItemChangeIcon", "Change Icon")
        MenuItemUpdatePath.Text = langManager.GetText("MenuItemUpdatePath", "Update Path")
        MenuItemOpenFolder.Text = langManager.GetText("MenuItemOpenFolder", "Show in Folder")
        MenuItemDelete.Text = langManager.GetText("MenuItemDelete", "Delete")
        MenuItemProperties.Text = langManager.GetText("MenuItemProperties", "Properties")

        ' Context Menu - Tab
        MenuTabYeni.Text = langManager.GetText("MenuTabYeni", "New Tab")
        MenuTabAdDegistir.Text = langManager.GetText("MenuTabAdDegistir", "Rename Tab")
        MenuTabSil.Text = langManager.GetText("MenuTabSil", "Delete Tab")

        ' Search Panel
        Label1.Text = langManager.GetText("lblSearch", "Search")

        ' Search button - if the button has no image, set the text
        If btnSearch.Image Is Nothing AndAlso btnSearch.BackgroundImage Is Nothing Then
            btnSearch.Text = langManager.GetText("btnSearch", "Search")
        End If
    End Sub

    Private Sub btnSettings_Click(sender As Object, e As EventArgs)
        Dim wasTopMost As Boolean = Me.TopMost
        Me.TopMost = False

        Using settingsForm As New SettingsForm()
            ' Mevcut ayarları form'a aktar
            settingsForm.LaunchMode = launchMode
            settingsForm.ViewMode = viewMode
            settingsForm.AlwaysOnTop = alwaysOnTop
            settingsForm.CurrentLanguage = currentLanguage
            settingsForm.LastActiveTab = TabControl1.SelectedIndex

            ' Tab başlıklarını aktar
            Dim tabNames As New List(Of String)
            For Each tabPage In TabControl1.TabPages
                tabNames.Add(CType(tabPage, TabPage).Text)
            Next
            settingsForm.AvailableTabs = tabNames.ToArray()

            If settingsForm.ShowDialog(Me) = DialogResult.OK Then
                launchMode = settingsForm.LaunchMode
                viewMode = settingsForm.ViewMode
                alwaysOnTop = settingsForm.AlwaysOnTop
                Dim newLanguage = settingsForm.CurrentLanguage
                Dim newTab = settingsForm.LastActiveTab

                ' Dil değiştiyse güncelle
                If newLanguage <> currentLanguage Then
                    currentLanguage = newLanguage
                    If langManager IsNot Nothing Then
                        langManager.SetLanguage(currentLanguage)
                        ApplyLanguage()

                        ' RTL desteği: Arapça seçilmişse UI yönünü değiştir
                        If langManager.IsRTLLanguage(currentLanguage) Then
                            Me.RightToLeft = RightToLeft.Yes
                            Me.RightToLeftLayout = True
                        Else
                            Me.RightToLeft = RightToLeft.No
                            Me.RightToLeftLayout = False
                        End If
                    End If
                End If

                ' Son tab bilgisini kaydet
                lastActiveTab = newTab

                SaveSettingsToIni()
                ApplySettings()

                MessageBox.Show(langManager.GetText("MsgSettingsSaved", "Settings saved. Please exit the application for changes to take full effect."),
                               langManager.GetText("MsgInfo", "Information"), MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Mesaj OK'dan sonra uygulamayı kapat
                Me.Close()
            End If
        End Using

        Me.TopMost = wasTopMost
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Tab
                If Not e.Control AndAlso Not e.Shift Then
                    Dim currentIndex = TabControl1.SelectedIndex
                    If currentIndex < TabControl1.TabPages.Count - 1 Then
                        TabControl1.SelectedIndex = currentIndex + 1
                    Else
                        TabControl1.SelectedIndex = 0
                    End If
                    e.Handled = True
                    e.SuppressKeyPress = True
                End If

            Case Keys.Oemcomma, Keys.OemPeriod
                If e.KeyCode = Keys.Oemcomma AndAlso e.Shift Then
                    Dim currentIndex = TabControl1.SelectedIndex
                    If currentIndex > 0 Then
                        TabControl1.SelectedIndex = currentIndex - 1
                    Else
                        TabControl1.SelectedIndex = TabControl1.TabPages.Count - 1
                    End If
                    e.Handled = True
                ElseIf e.KeyCode = Keys.OemPeriod AndAlso e.Shift Then
                    Dim currentIndex = TabControl1.SelectedIndex
                    If currentIndex < TabControl1.TabPages.Count - 1 Then
                        TabControl1.SelectedIndex = currentIndex + 1
                    Else
                        TabControl1.SelectedIndex = 0
                    End If
                    e.Handled = True
                End If

            Case Keys.N
                If e.Control Then
                    btnNewTab_Click(Nothing, Nothing)
                    e.Handled = True
                End If

            Case Keys.W
                If e.Control Then
                    btnDeleteTab_Click(Nothing, Nothing)
                    e.Handled = True
                End If

            Case Keys.Delete
                If selectedItemPanel IsNot Nothing Then
                    MenuItemDelete_Click(Nothing, Nothing)
                    e.Handled = True
                End If

            Case Keys.F5
                RefreshCurrentTab()
                e.Handled = True

            Case Keys.F
                If e.Control Then
                    txtSearch.Focus()
                    e.Handled = True
                End If
        End Select
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Son aktif tab'ı kaydet (opsiyonel, Settings'te zaten kaydedilmiş olabilir)
        ' Tüm ayarları yeniden yazmıyoruz, sadece lastActiveTab güncelleniyor
        ' iniManager.WriteInteger("General", "LastActiveTab", TabControl1.SelectedIndex)
    End Sub

    ' ============================================
    ' MenuStrip Event Handlers
    ' ============================================

    Private Sub MenuDosyaCikis_Click(sender As Object, e As EventArgs) Handles MenuDosyaCikis.Click
        Me.Close()
    End Sub

    Private Sub MenuSekmelerYeni_Click(sender As Object, e As EventArgs) Handles MenuSekmelerYeni.Click
        btnNewTab_Click(sender, e)
    End Sub

    Private Sub MenuSekmelerAdDegistir_Click(sender As Object, e As EventArgs) Handles MenuSekmelerAdDegistir.Click
        btnRenameTab_Click(sender, e)
    End Sub

    Private Sub MenuSekmelerSil_Click(sender As Object, e As EventArgs) Handles MenuSekmelerSil.Click
        btnDeleteTab_Click(sender, e)
    End Sub

    Private Sub MenuSekmelerYenile_Click(sender As Object, e As EventArgs) Handles MenuSekmelerYenile.Click
        RefreshCurrentTab()
    End Sub

    Private Sub RefreshCurrentTab()
        If TabControl1.SelectedTab Is Nothing Then Return

        Try
            Dim currentTabIndex As Integer = TabControl1.SelectedIndex
            Dim currentTabName As String = TabControl1.SelectedTab.Text

            LoadDataFromDb()

            If currentTabIndex < TabControl1.TabPages.Count Then
                TabControl1.SelectedIndex = currentTabIndex
            End If

            Dim msg As String = String.Format(langManager.GetText("MsgTabRefreshed", "'{0}' tab refreshed."), currentTabName)
            MessageBox.Show(msg, langManager.GetText("MsgInfo", "Information"), MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgTabRefreshError", "Tab refresh error: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuAyarlar_Click(sender As Object, e As EventArgs)
        btnSettings_Click(sender, e)
    End Sub

    ' ============================================
    ' Araçlar Menu Event Handlers
    ' ============================================

    Private Sub MenuToolsCmd_Click(sender As Object, e As EventArgs) Handles MenuToolsCmd.Click
        Try
            Process.Start("cmd.exe")
        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgToolsCmdError", "Command Prompt could not be launched: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuToolsPowershell_Click(sender As Object, e As EventArgs) Handles MenuToolsPowershell.Click
        Try
            Process.Start("powershell.exe")
        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgToolsPowershellError", "PowerShell could not be launched: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuToolsTaskMgr_Click(sender As Object, e As EventArgs) Handles MenuToolsTaskMgr.Click
        Try
            Process.Start("taskmgr.exe")
        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgToolsTaskMgrError", "Task Manager could not be launched: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuToolsServices_Click(sender As Object, e As EventArgs) Handles MenuToolsServices.Click
        Try
            Process.Start("services.msc")
        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgToolsServicesError", "Services Manager could not be launched: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuToolsShowDesktop_Click(sender As Object, e As EventArgs) Handles MenuToolsShowDesktop.Click
        Try
            Process.Start("explorer.exe", "shell:::{3080F90D-D7AD-11D9-BD98-0000947B0257}")
        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgToolsShowDesktopError", "Desktop could not be shown: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuToolsRestoreDesktop_Click(sender As Object, e As EventArgs) Handles MenuToolsRestoreDesktop.Click
        Try
            ' Explorer'i yeniden başlat
            Dim explorerProcesses = Process.GetProcessesByName("explorer")
            For Each proc In explorerProcesses
                proc.Kill()
                proc.WaitForExit()
            Next
            Process.Start("explorer.exe")
            MessageBox.Show(langManager.GetText("MsgToolsDesktopRestored", "Desktop restored."), langManager.GetText("MsgInfo", "Information"), MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgToolsRestoreDesktopError", "Desktop could not be restored: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuToolsControlPanel_Click(sender As Object, e As EventArgs) Handles MenuToolsControlPanel.Click
        Try
            Process.Start("control.exe")
        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgToolsControlPanelError", "Control Panel could not be launched: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuToolsNetworkCenter_Click(sender As Object, e As EventArgs) Handles MenuToolsNetworkCenter.Click
        Try
            Process.Start("control.exe", "/name Microsoft.NetworkAndSharingCenter")
        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgToolsNetworkCenterError", "Network and Sharing Center could not be launched: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuToolsDeviceManager_Click(sender As Object, e As EventArgs) Handles MenuToolsDeviceManager.Click
        Try
            Process.Start("devmgmt.msc")
        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgToolsDeviceManagerError", "Device Manager could not be launched: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuToolsComputerName_Click(sender As Object, e As EventArgs) Handles MenuToolsComputerName.Click
        Try
            Dim computerName As String = Environment.MachineName
            Clipboard.SetText(computerName)
            Dim msg As String = String.Format(langManager.GetText("MsgToolsComputerName", "Computer Name: {0}"), computerName) & vbCrLf & vbCrLf & langManager.GetText("MsgCopiedToClipboard", "(Copied to clipboard)")
            MessageBox.Show(msg, langManager.GetText("MsgToolsComputerNameTitle", "Computer Name"), MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Dim errMsg As String = String.Format(langManager.GetText("MsgToolsComputerNameError", "Computer name could not be retrieved: {0}"), ex.Message)
            MessageBox.Show(errMsg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuToolsIPAddress_Click(sender As Object, e As EventArgs) Handles MenuToolsIPAddress.Click
        Try
            Dim localIP As String = GetLocalIPAddress()
            Dim publicIP As String = GetPublicIPAddress()

            Dim localIPMsg As String = String.Format(langManager.GetText("MsgToolsLocalIP", "Local IP: {0}"), localIP)
            Dim remoteIPMsg As String = String.Format(langManager.GetText("MsgToolsRemoteIP", "Public IP: {0}"), publicIP)
            Dim ipInfo As String = localIPMsg & vbCrLf & remoteIPMsg
            Clipboard.SetText(ipInfo)

            MessageBox.Show(ipInfo & vbCrLf & vbCrLf & langManager.GetText("MsgCopiedToClipboard", "(Copied to clipboard)"),
                           langManager.GetText("MsgToolsIPTitle", "IP Addresses"), MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Dim errMsg As String = String.Format(langManager.GetText("MsgToolsIPError", "IP addresses could not be retrieved: {0}"), ex.Message)
            MessageBox.Show(errMsg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetLocalIPAddress() As String
        Try
            Dim host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName())
            For Each ip In host.AddressList
                If ip.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                    Return ip.ToString()
                End If
            Next
            Return langManager.GetText("MsgToolsIPNotFound", "Not found")
        Catch
            Return langManager.GetText("MsgToolsIPNotFound", "Not found")
        End Try
    End Function

    Private Function GetPublicIPAddress() As String
        Try
            Using client As New System.Net.WebClient()
                Return client.DownloadString("https://api.ipify.org").Trim()
            End Using
        Catch
            Return langManager.GetText("MsgToolsIPNotFound", "Not found")
        End Try
    End Function

    ' ============================================
    ' Yardım Menu Event Handlers
    ' ============================================

    Private Sub MenuYardimDokumanlar_Click(sender As Object, e As EventArgs) Handles MenuYardimDokumanlar.Click
        Try
            ' ComboLang'dan seçili dili al
            Dim selectedLang As String = If(ComboLang.SelectedValue IsNot Nothing, ComboLang.SelectedValue.ToString(), "")

            ' Eğer seçili dil Türkçe ise Türkçe help dosyasını aç
            Dim helpFileName As String = If(selectedLang = "tr", "RiaLauncherHelp-tr.html", "RiaLauncherHelp-tr.html")
            Dim helpPath As String = IO.Path.Combine(sHelpDir, helpFileName)

            If IO.File.Exists(helpPath) Then
                System.Diagnostics.Process.Start(helpPath)
            Else
                MessageBox.Show("Help dosyası bulunamadı: " & helpPath, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Help dosyası açılırken hata oluştu: " & ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuYardimDokumanIndir_Click(sender As Object, e As EventArgs) Handles MenuYardimDokumanIndir.Click
        Try
            ' Aktif dile göre doğru dökümanı aç
            Dim currentLang As String = langManager.GetCurrentLanguage()
            Dim docFileName As String = If(currentLang = "tr", "KullanımKlavuzu.md", "UserManual.md")
            Dim docPath As String = IO.Path.Combine(sAssetDir, "documentation", docFileName)

            If IO.File.Exists(docPath) Then
                ' MD dosyasını varsayılan editör ile aç
                Process.Start(docPath)
            Else
                ' Dosya yoksa GitHub sayfasını aç
                Process.Start("https://github.com/hikmetalemdaroglu/999Projects/wiki")
            End If
        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgHelpDocError", "The document page could not be opened: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuYardimLisans_Click(sender As Object, e As EventArgs) Handles MenuYardimLisans.Click
        Dim lisansBaslik As String = langManager.GetText("LicenseTitle", "WinLauncher - Personal Use License")
        Dim lisansFree As String = langManager.GetText("LicenseFree", "This software is free for personal use.")
        Dim lisansCopyright As String = langManager.GetText("AboutCopyright", "© 2024-2025 Hikmet Alp Alemdaroğlu")
        Dim lisansRights As String = langManager.GetText("LicenseRights", "All rights reserved.")
        Dim lisansAsIs As String = langManager.GetText("LicenseAsIs", "This software is provided ""AS IS"".")

        Dim lisansMetni As String = lisansBaslik & vbCrLf & vbCrLf &
                                    lisansFree & vbCrLf & vbCrLf &
                                    lisansCopyright & vbCrLf & vbCrLf &
                                    lisansRights & vbCrLf & vbCrLf &
                                    lisansAsIs

        MessageBox.Show(lisansMetni, langManager.GetText("MenuYardimLisans", "License Terms"), MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MenuYardimBagis_Click(sender As Object, e As EventArgs) Handles MenuYardimBagis.Click
        Dim bagisMsg As String = langManager.GetText("MsgDonateMessage", "If you like the WinLauncher project," & vbCrLf &
                                 "you can donate to support its development." & vbCrLf & vbCrLf &
                                 "GitHub Sponsors: github.com/sponsors/hikmetalemdaroglu" & vbCrLf & vbCrLf &
                                 "Thank you! ??")

        Dim result = MessageBox.Show(bagisMsg, langManager.GetText("MsgDonateTitle", "Donate"), MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
        If result = DialogResult.OK Then
            Try
                Process.Start("https://github.com/sponsors/hikmetalemdaroglu")
            Catch ex As Exception
                Dim errMsg As String = String.Format(langManager.GetText("MsgDonateError", "The donation page could not be opened: {0}"), ex.Message)
                MessageBox.Show(errMsg, langManager.GetText("MsgError", "Hata"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub MenuYardimAnaSayfa_Click(sender As Object, e As EventArgs) Handles MenuYardimAnaSayfa.Click
        Try
            Process.Start("https://github.com/hikmetalemdaroglu/999Projects/tree/winluncher-v1.2-release/ProjectVs/ProjectVb.net/winLuncher")
        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgHomePageError", "Ana sayfa açılamadı: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Hata"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuYardimHakkinda_Click(sender As Object, e As EventArgs) Handles MenuYardimHakkinda.Click
        Using aboutForm As New AboutForm()
            aboutForm.ShowDialog(Me)
        End Using
    End Sub

    ' ============================================
    ' Tab Context Menu Event Handlers
    ' ============================================

    Private Sub TabControl1_MouseClick(sender As Object, e As MouseEventArgs) Handles TabControl1.MouseClick
        If e.Button = MouseButtons.Right Then
            ' Hangi tab'a tıklandığını bul
            For i As Integer = 0 To TabControl1.TabCount - 1
                Dim tabRect As Rectangle = TabControl1.GetTabRect(i)
                If tabRect.Contains(e.Location) Then
                    TabControl1.SelectedIndex = i
                    ContextMenuStripTab.Show(TabControl1, e.Location)
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub MenuTabYeni_Click(sender As Object, e As EventArgs) Handles MenuTabYeni.Click
        btnNewTab_Click(sender, e)
    End Sub

    Private Sub MenuTabAdDegistir_Click(sender As Object, e As EventArgs) Handles MenuTabAdDegistir.Click
        btnRenameTab_Click(sender, e)
    End Sub

    Private Sub MenuTabSil_Click(sender As Object, e As EventArgs) Handles MenuTabSil.Click
        btnDeleteTab_Click(sender, e)
    End Sub

    ' ============================================
    ' Search Functionality
    ' ============================================

    Private Sub SearchItems()
        Dim searchText As String = txtSearch.Text.Trim().ToLower()

        If String.IsNullOrEmpty(searchText) Then
            ' Tüm panel'leri göster
            For Each tab As TabPage In TabControl1.TabPages
                Dim flowPanel = tab.Controls.OfType(Of FlowLayoutPanel)().FirstOrDefault()
                If flowPanel IsNot Nothing Then
                    For Each panel As Panel In flowPanel.Controls.OfType(Of Panel)()
                        panel.Visible = True
                    Next
                End If
            Next
            Return
        End If

        ' Panel'leri filtrele
        For Each tab As TabPage In TabControl1.TabPages
            Dim flowPanel = tab.Controls.OfType(Of FlowLayoutPanel)().FirstOrDefault()
            If flowPanel IsNot Nothing Then
                For Each panel As Panel In flowPanel.Controls.OfType(Of Panel)()
                    Dim lbl = panel.Controls.OfType(Of Label)().FirstOrDefault()
                    If lbl IsNot Nothing Then
                        panel.Visible = lbl.Text.ToLower().Contains(searchText)
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        SearchItems()
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            SearchItems()
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub

    ' ============================================
    ' Drag-Drop Helper Methods
    ' ============================================

    Private Sub FlowPanel_DragOver(sender As Object, e As DragEventArgs)
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    ' FlowPanel_DragDrop yukarıda tanımlı (sadece dosya drag-drop için)

    ' ============================================
    ' Manuel Sıralama için Public Metodlar
    ' ============================================

    Public Function GetCurrentTabItems() As List(Of Object)
        Dim items As New List(Of Object)

        If TabControl1.SelectedTab Is Nothing Then Return items

        Dim flowPanel = TabControl1.SelectedTab.Controls.OfType(Of FlowLayoutPanel)().FirstOrDefault()
        If flowPanel Is Nothing Then Return items

        For Each itemPanel As Panel In flowPanel.Controls.OfType(Of Panel)()
            Dim lblName = itemPanel.Controls.OfType(Of Label)().FirstOrDefault()
            Dim picBox = itemPanel.Controls.OfType(Of PictureBox)().FirstOrDefault()
            Dim itemData = TryCast(itemPanel.Tag, Object)

            If itemData IsNot Nothing AndAlso lblName IsNot Nothing Then
                Dim icon As Icon = Nothing
                If picBox IsNot Nothing AndAlso picBox.Image IsNot Nothing Then
                    Try
                        icon = Icon.FromHandle(DirectCast(picBox.Image, Bitmap).GetHicon())
                    Catch
                    End Try
                End If

                items.Add(New With {
                    .Name = lblName.Text,
                    .Path = itemData.Path,
                    .IconPath = itemData.IconPath,
                    .Icon = icon
                })
            End If
        Next

        Return items
    End Function

    Public Sub ApplyNewItemOrder(newOrder As List(Of ManualSortForm.ItemData))
        If TabControl1.SelectedTab Is Nothing Then Return

        Dim flowPanel = TabControl1.SelectedTab.Controls.OfType(Of FlowLayoutPanel)().FirstOrDefault()
        If flowPanel Is Nothing Then Return

        ' Mevcut panelleri temizle
        flowPanel.Controls.Clear()

        ' Yeni sıralamaya göre yeniden ekle
        For Each item In newOrder
            AddLauncherItem(flowPanel, item.Name, item.Path, item.IconPath)
        Next

        ' Veritabanına kaydet
        SaveDataToDb()

        MessageBox.Show(langManager.GetText("MsgSortSaved", "Sıralama başarıyla kaydedildi!"), langManager.GetText("MsgInfo", "Bilgi"), MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' Manuel sıralama formunu aç
    Public Sub ShowManualSortForm()
        If TabControl1.SelectedTab Is Nothing Then
            MessageBox.Show(langManager.GetText("MsgSortNoTabSelected", "Lütfen önce bir sekme seçin."), langManager.GetText("MsgWarning", "Uyarı"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim flowPanel = TabControl1.SelectedTab.Controls.OfType(Of FlowLayoutPanel)().FirstOrDefault()
        If flowPanel Is Nothing OrElse flowPanel.Controls.Count = 0 Then
            MessageBox.Show(langManager.GetText("MsgSortNoItems", "Bu sekmede sıralanacak öğe bulunmuyor."), langManager.GetText("MsgWarning", "Uyarı"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using sortForm As New ManualSortForm(TabControl1.SelectedTab.Text)
            sortForm.Owner = Me
            If sortForm.ShowDialog(Me) = DialogResult.OK Then
                ' Sıralama kaydedildi, ekran otomatik refresh edildi
            End If
        End Using
    End Sub

    ' ============================================
    ' Context Menu - Copy/Move Event Handlers
    ' ============================================

    Private Sub MenuItemCopyMove_Click(sender As Object, e As EventArgs) Handles MenuItemCopyMove.Click
        ShowCopyMoveForm()
    End Sub

    Private Sub ShowCopyMoveForm()
        If selectedItemPanel Is Nothing Then Return
        If TabControl1.SelectedTab Is Nothing Then Return

        ' Seçili simgenin bilgilerini al
        Dim lblName = selectedItemPanel.Controls.OfType(Of Label)().FirstOrDefault()
        If lblName Is Nothing Then Return

        Dim itemData = TryCast(selectedItemPanel.Tag, Object)
        If itemData Is Nothing Then Return

        Dim picBox = selectedItemPanel.Controls.OfType(Of PictureBox)().FirstOrDefault()
        Dim itemIcon As Icon = Nothing
        If picBox IsNot Nothing AndAlso picBox.Image IsNot Nothing Then
            Try
                itemIcon = Icon.FromHandle(DirectCast(picBox.Image, Bitmap).GetHicon())
            Catch
                itemIcon = Nothing
            End Try
        End If

        ' CopyMoveForm'u aç
        Using copyMoveForm As New CopyMoveForm(
            TabControl1.SelectedTab.Text,
            lblName.Text,
            itemData.Path,
            If(itemData.IconPath IsNot Nothing, itemData.IconPath.ToString(), ""),
            itemIcon
        )
            copyMoveForm.Owner = Me
            copyMoveForm.ShowDialog(Me)
        End Using
    End Sub

    ' Public metodlar - CopyMoveForm tarafından çağrılacak
    Public Function CopyItemToTab(sourceTabName As String, targetTabName As String, itemName As String, itemPath As String, itemIconPath As String, itemIcon As Icon) As Boolean
        Try
            Dim targetTab As TabPage = Nothing
            For Each tab As TabPage In TabControl1.TabPages
                If tab.Text = targetTabName Then
                    targetTab = tab
                    Exit For
                End If
            Next

            If targetTab Is Nothing Then Return False

            If Not DatabaseManager.CopyItemToCategory(sourceTabName, targetTabName, itemName, itemPath, itemIconPath) Then
                Return False
            End If

            RefreshTab(targetTab)
            Return True

        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgCopyError", "Kopyalama hatası: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Hata"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Function MoveItemToTab(sourceTabName As String, targetTabName As String, itemName As String, itemPath As String, itemIconPath As String, itemIcon As Icon) As Boolean
        Try
            Dim sourceTab As TabPage = Nothing
            For Each tab As TabPage In TabControl1.TabPages
                If tab.Text = sourceTabName Then
                    sourceTab = tab
                    Exit For
                End If
            Next

            If Not DatabaseManager.MoveItemToCategory(sourceTabName, targetTabName, itemName, itemPath, itemIconPath) Then
                Return False
            End If

            If sourceTab IsNot Nothing Then
                RefreshTab(sourceTab)
            End If

            Dim targetTab As TabPage = Nothing
            For Each tab As TabPage In TabControl1.TabPages
                If tab.Text = targetTabName Then
                    targetTab = tab
                    Exit For
                End If
            Next
            If targetTab IsNot Nothing Then
                RefreshTab(targetTab)
            End If

            Return True

        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgMoveError", "Taşıma hatası: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Hata"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub RefreshTab(tab As TabPage)
        Try
            Dim flowPanel = tab.Controls.OfType(Of FlowLayoutPanel)().FirstOrDefault()
            If flowPanel Is Nothing Then Return

            flowPanel.Controls.Clear()

            Dim items = DatabaseManager.GetItemsByCategory(tab.Text)

            For Each item In items
                If File.Exists(item.Path) OrElse Directory.Exists(item.Path) Then
                    AddLauncherItem(flowPanel, item.Name, item.Path, item.IconPath)
                End If
            Next

        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgTabRefreshError", "Tab yenileme hatası: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Hata"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        ' Anlık arama (opsiyonel)
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub btn_setup_Click(sender As Object, e As EventArgs) Handles btn_setup.Click
        btnSettings_Click(sender, e)
    End Sub

    Private Sub ComboLang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboLang.SelectedIndexChanged
        ' Combo değişince yeni dili uygula
        ApplyCurrentLang()
    End Sub
End Class
