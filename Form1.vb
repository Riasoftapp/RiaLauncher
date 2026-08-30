Imports System.IO
Imports System.Drawing
Imports System.Reflection

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
    Private winX As Integer = -1
    Private winY As Integer = -1
    Private winWidth As Integer = 0
    Private winHeight As Integer = 0
    Private autoUpdateEnabled As Boolean = True
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

        ' Önceki pencere konumu ve boyutunu geri yükle
        RestoreWindowBounds()

        InitializeLanguageManager()
        LoadComboLang()
        ApplyCurrentLang()

        ' SQLite veritabanını başlat ve XML'den içe aktar (ilk geçiş)
        InitDatabase()

        ' Mevcut .url ogelerini gercek URL ile guncelle (kisayol silinince kaybolmasin)
        UpgradeUrlItems()

        ' Veritabanından verileri yükle
        LoadDataFromDb()

        ' Son açılan tab'ı geri yükle
        RestoreLastActiveTab()

        FlowLayoutPanel1.AllowDrop = True

        CheckForUpdates(False)

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

    Private Function GetCurrentVersion() As String
        Dim asmVersion = Assembly.GetExecutingAssembly().GetName().Version
        If asmVersion IsNot Nothing Then
            Return asmVersion.ToString()
        End If

        Return Application.ProductVersion
    End Function

    Private Function GetConfigValue(keyName As String) As String
        Try
            Dim configPath As String = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile
            If String.IsNullOrWhiteSpace(configPath) OrElse Not File.Exists(configPath) Then Return ""

            Dim doc = System.Xml.Linq.XDocument.Load(configPath)
            Dim appSettings = doc...<appSettings>.<add>
            For Each setting In appSettings
                Dim keyAttr = setting.Attribute("key")
                If keyAttr IsNot Nothing AndAlso keyAttr.Value = keyName Then
                    Dim valAttr = setting.Attribute("value")
                    If valAttr IsNot Nothing Then Return valAttr.Value
                End If
            Next
        Catch
        End Try

        Return ""
    End Function

    Private Sub CheckForUpdates(isManualCheck As Boolean)
        If (Not isManualCheck) AndAlso (Not autoUpdateEnabled) Then
            Return
        End If

        Try
            Dim repo As String = GetConfigValue("UpdateRepo")
            Dim assetName As String = GetConfigValue("UpdateAsset")
            If String.IsNullOrWhiteSpace(repo) OrElse String.IsNullOrWhiteSpace(assetName) Then
                If isManualCheck Then
                    MessageBox.Show(langManager.GetText("MsgUpdateUrlMissing", "Update URL is not configured."),
                                    langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Return
            End If

            Dim updaterPath As String = Path.Combine(sRootDir, "update", "Updater.exe")
            If Not File.Exists(updaterPath) Then
                If isManualCheck Then
                    MessageBox.Show(langManager.GetText("MsgUpdateUpdaterNotFound", "Updater not found: {0}").Replace("{0}", updaterPath),
                                    langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Return
            End If

            Dim mgr As New UpdateManager(sRootDir, repo, assetName, GetCurrentVersion())
            Dim release As ReleaseInfo = mgr.GetLatestRelease()

            If Not mgr.IsNewer(release) Then
                If isManualCheck Then
                    MessageBox.Show(langManager.GetText("MsgUpdateNoNewVersion", "You are using the latest version."),
                                    langManager.GetText("MsgInfo", "Information"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                Return
            End If

            Dim changelog As String = release.Changelog
            If changelog = "" Then changelog = "-"
            Dim question As String = String.Format(langManager.GetText("MsgUpdateFound", "A new version is available: v{0}{1}{1}{2}{1}{1}Download and install now?"),
                                                   release.Version, vbCrLf, changelog)
            Dim savedCulture As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentUICulture
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US")
            Dim updateAnswer As DialogResult = MessageBox.Show(question, "RiaLauncher Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            System.Threading.Thread.CurrentThread.CurrentUICulture = savedCulture
            If updateAnswer <> DialogResult.Yes Then
                Return
            End If

            Dim stagingDir As String = mgr.DownloadAndStage(release)
            If mgr.LaunchUpdater(stagingDir) Then
                Application.Exit()
            Else
                MessageBox.Show(langManager.GetText("MsgUpdateUpdaterNotFound", "Updater not found: {0}").Replace("{0}", updaterPath),
                                langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            If isManualCheck Then
                Dim msg As String = String.Format(langManager.GetText("MsgUpdateCheckError", "Update check failed: {0}"), ex.Message)
                MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub
    Public Function isSettingsIniExist() As Boolean
        Dim iniPath As String = Path.Combine(sAssetDir, "settings.ini")
        Return File.Exists(iniPath)
    End Function
    Public Sub InitDatabase()
        Try
            DatabaseManager.SetDataDir(sDataDir)
            DatabaseManager.InitializeDatabase()
            DatabaseManager.RunMigrations()
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
            lines.Add("AutoUpdate=true")
            lines.Add("WindowX=0")
            lines.Add("WindowY=0")
            lines.Add("WindowWidth=0")
            lines.Add("WindowHeight=0")

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
                ElseIf line.StartsWith("AutoUpdate=") Then
                    Boolean.TryParse(line.Split("=")(1), autoUpdateEnabled)
                ElseIf line.StartsWith("WindowX=") Then
                    Integer.TryParse(line.Split("=")(1), winX)
                ElseIf line.StartsWith("WindowY=") Then
                    Integer.TryParse(line.Split("=")(1), winY)
                ElseIf line.StartsWith("WindowWidth=") Then
                    Integer.TryParse(line.Split("=")(1), winWidth)
                ElseIf line.StartsWith("WindowHeight=") Then
                    Integer.TryParse(line.Split("=")(1), winHeight)
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
            lines.Add("AutoUpdate=" & autoUpdateEnabled.ToString().ToLower())
            lines.Add("WindowX=" & winX.ToString())
            lines.Add("WindowY=" & winY.ToString())
            lines.Add("WindowWidth=" & winWidth.ToString())
            lines.Add("WindowHeight=" & winHeight.ToString())

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

    ' Veritabanındaki bir öğeyi ekrana ekler.
    ' - http(s) ile başlayanlar (URL) her zaman gösterilir.
    ' - Dosya/klasör diskte varsa normal gösterilir.
    ' - Bunların dışında kalanlar (diskte olmayan programlar) "unavailable"
    '   ikonuyla ve adına " (missing)" eklenerek gösterilir.
    Private Sub AddDbItem(flowPanel As FlowLayoutPanel, item As DatabaseManager.DbItem, unavailableIcon As String)
        If IsUrl(item.Path) Then
            AddLauncherItem(flowPanel, item.Name, item.Path, item.IconPath)
        ElseIf File.Exists(item.Path) OrElse Directory.Exists(item.Path) Then
            AddLauncherItem(flowPanel, item.Name, item.Path, item.IconPath)
        Else
            AddLauncherItem(flowPanel, item.Name & " (missing)", item.Path, item.IconPath, unavailableIcon)
        End If
    End Sub

    Private Sub LoadDataFromDb()
        Try
            TabControl1.TabPages.Clear()

            Dim categories = DatabaseManager.GetCategories()
            Dim unavailableIcon As String = IO.Path.Combine(sIconDir, "unavailable24.png")

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
                    AddDbItem(flowPanel, item, unavailableIcon)
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
        If HasUrlDragData(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub FlowPanel_DragDrop(sender As Object, e As DragEventArgs)
        Dim flowPanel As FlowLayoutPanel = GetFlowPanel(sender)
        If flowPanel Is Nothing Then Return

        ' 1) Dosya sürükle-bırak (Windows Explorer / masaüstü .url, .lnk, uygulama, klasör)
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim files() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())

            For Each filePath In files
                Dim itemName As String = Path.GetFileNameWithoutExtension(filePath)
                Dim targetPath As String = filePath

                If Path.GetExtension(filePath).ToLower() = ".url" Then
                    Dim url As String = ReadUrlFromShortcut(filePath)
                    If IsUrl(url) Then targetPath = url
                ElseIf Path.GetExtension(filePath).ToLower() = ".lnk" Then
                    Dim t As String = ResolveShortcut(filePath)
                    If Not String.IsNullOrEmpty(t) Then targetPath = t
                End If

                AddLauncherItem(flowPanel, itemName, targetPath, "")
            Next

            SaveDataToDb()
        Else
            ' 2) Doğrudan URL bırakıldı (tarayıcı adres çubuğu, bağlantı sürükleme,
            '    masaüstü .url sanal dosyası, vb.). Mevcut tüm format'lardan ilk geçerli
            '    http(s) adresini bulur.
            Dim url As String = ExtractUrlFromDragData(e)
            If IsUrl(url) Then AddUrlItem(flowPanel, url)
        End If
    End Sub

    ' Metin içinden ilk geçerli http/https adresini bulur.
    ' (düz URL, "URL=...", ya da <a href="..."> içeren HTML olabilir)
    Private Function ExtractUrlFromText(text As String) As String
        If String.IsNullOrEmpty(text) Then Return ""
        Dim idx As Integer = text.IndexOf("https://", StringComparison.OrdinalIgnoreCase)
        If idx < 0 Then idx = text.IndexOf("http://", StringComparison.OrdinalIgnoreCase)
        If idx < 0 Then Return ""
        Dim rest As String = text.Substring(idx)
        Dim endIdx As Integer = rest.IndexOfAny(New Char() {ControlChars.Cr, ControlChars.Lf, " "c, vbTab, """"c, ">"c, "<"c, ")"c, "}"c, ";"c})
        If endIdx >= 0 Then rest = rest.Substring(0, endIdx)
        Return rest.Trim()
    End Function

    ' Sürükleme verisinde kabul edilebilir bir URL/dosya formatı var mı?
    Private Function HasUrlDragData(e As DragEventArgs) As Boolean
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then Return True

        For Each fmt In e.Data.GetFormats()
            If fmt = "HTML Format" OrElse
               fmt = "UniformResourceLocator" OrElse
               fmt = "UniformResourceLocatorW" OrElse
               fmt = "text/uri-list" OrElse
               fmt = DataFormats.UnicodeText OrElse
               fmt = DataFormats.Text OrElse
               fmt = DataFormats.StringFormat OrElse
               fmt = "FileGroupDescriptorW" OrElse
               fmt = "Shell IDList Array" Then
                Return True
            End If
        Next

        Return False
    End Function

    ' Sürükleme verisindeki tüm olası format'lardan ilk geçerli http(s) adresini çıkarır.
    ' Tarayıcılar URL'yi genellikle UnicodeText, text/uri-list veya HTML Format olarak verir;
    ' bunların hiçbiri eski kodda denenmiyordu (sadece ANSI Text), bu yüzden bırakma reddediliyordu.
    Private Function ExtractUrlFromDragData(e As DragEventArgs) As String
        ' Öncelik sırasıyla denenecek bilinen metin tabanlı formatlar
        Dim tryFormats As New List(Of String) From {
            DataFormats.Text,
            DataFormats.UnicodeText,
            DataFormats.StringFormat,
            "text/uri-list",
            "HTML Format",
            "UniformResourceLocator",
            "UniformResourceLocatorW",
            "FileContents"
        }

        For Each fmt In tryFormats
            If e.Data.GetDataPresent(fmt) Then
                Dim s As String = TryCast(e.Data.GetData(fmt), String)
                If s Is Nothing Then
                    Dim stream = TryCast(e.Data.GetData(fmt), IO.Stream)
                    If stream IsNot Nothing Then
                        Try
                            Using r = New IO.StreamReader(stream)
                                s = r.ReadToEnd()
                            End Using
                        Catch
                        End Try
                    End If
                End If
                If s IsNot Nothing Then
                    Dim candidate As String = ExtractUrlFromText(s)
                    If IsUrl(candidate) Then Return candidate
                End If
            End If
        Next

        ' Fallback: mevcut tüm formatları tara (bilinmeyen tarayıcı formatları için)
        For Each fmt In e.Data.GetFormats()
            Try
                Dim obj = e.Data.GetData(fmt)
                Dim s As String = TryCast(obj, String)
                If s Is Nothing Then
                    Dim stream = TryCast(obj, IO.Stream)
                    If stream IsNot Nothing Then
                        Using r = New IO.StreamReader(stream)
                            s = r.ReadToEnd()
                        End Using
                    End If
                End If
                If s IsNot Nothing AndAlso s.Length > 0 Then
                    Dim candidate As String = ExtractUrlFromText(s)
                    If IsUrl(candidate) Then Return candidate
                End If
            Catch
            End Try
        Next

        Return ""
    End Function

    Private Sub AddUrlItem(flowPanel As FlowLayoutPanel, url As String)
        If String.IsNullOrEmpty(url) Then Return
        AddLauncherItem(flowPanel, UrlToName(url), url.Trim(), "")
        SaveDataToDb()
    End Sub

    Private Function UrlToName(url As String) As String
        Try
            Dim u As New Uri(url)
            For i As Integer = u.Segments.Length - 1 To 0 Step -1
                Dim seg = u.Segments(i).TrimEnd("/"c)
                If seg.Length > 0 Then Return Uri.UnescapeDataString(seg)
            Next
            Return u.Host
        Catch
            Return url
        End Try
    End Function
    Private Function ResolveShortcut(shortcutPath As String) As String
        Try
            Dim shell = CreateObject("WScript.Shell")
            Dim shortcut = shell.CreateShortcut(shortcutPath)
            Return shortcut.TargetPath
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function IsUrl(path As String) As Boolean
        If String.IsNullOrEmpty(path) Then Return False
        Return path.StartsWith("http://", StringComparison.OrdinalIgnoreCase) OrElse
               path.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
    End Function

    ' Sürükle-bırak olayını tetikleyen kontrolden (öğe paneli, ikon, etiket) en yakın
    ' FlowLayoutPanel'e ulaşır. Böylece bırakma boş alanda da, mevcut bir öğenin
    ' üzerinde de olsa aynı akış çalışır.
    Private Function GetFlowPanel(sender As Object) As FlowLayoutPanel
        Dim c = TryCast(sender, Control)
        While c IsNot Nothing
            If TypeOf c Is FlowLayoutPanel Then Return DirectCast(c, FlowLayoutPanel)
            c = c.Parent
        End While
        Return Nothing
    End Function

    Private Function ReadUrlFromShortcut(shortcutPath As String) As String
        Try
            Dim bytes = IO.File.ReadAllBytes(shortcutPath)
            ' .url dosyaları bazen UTF-16 (Unicode) veya ANSI ile kaydedilir;
            ' birkaç kodlamayla deneyip ilk bulunan http(s) adresini döndürelim.
            Dim candidates As New List(Of String) From {
                System.Text.Encoding.UTF8.GetString(bytes),
                System.Text.Encoding.Unicode.GetString(bytes),
                System.Text.Encoding.Default.GetString(bytes)
            }
            For Each content In candidates
                For Each line In content.Split(New Char() {ControlChars.Cr, ControlChars.Lf}, StringSplitOptions.RemoveEmptyEntries)
                    Dim t = line.Trim()
                    If t.StartsWith("URL", StringComparison.OrdinalIgnoreCase) Then
                        Dim eq = t.IndexOf("="c)
                        If eq >= 0 Then
                            Dim u = t.Substring(eq + 1).Trim()
                            If IsUrl(u) Then Return u
                        End If
                    End If
                Next
            Next
        Catch
        End Try
        Return ""
    End Function
    Private Sub AddLauncherItem(flowPanel As FlowLayoutPanel, name As String, path As String, iconPath As String, Optional forcedIconPath As String = "")
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

        If Not String.IsNullOrEmpty(forcedIconPath) AndAlso IO.File.Exists(forcedIconPath) Then
            ' Zorunlu ikon (ör. diskte olmayan öğeler için "unavailable" ikonu)
            Try
                picBox.Image = New Bitmap(forcedIconPath)
            Catch
            End Try
        ElseIf IsUrl(path) Then
            ' URL ogeleri icin web ikonu (web48.png)
            Dim webIconPath As String = IO.Path.Combine(sIconDir, "web48.png")
            If IO.File.Exists(webIconPath) Then
                Try
                    picBox.Image = New Bitmap(webIconPath)
                Catch
                End Try
            End If
        ElseIf extension = ".svg" Then
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

        ' Öğe paneli ve alt kontrolleri de sürükle-bırak hedefi olsun; böylece
        ' bir öğenin / ikonun üzerine bırakıldığında "yasak" imleci çıkmaz.
        ' Olaylar üst FlowLayoutPanel'e yönlendirilir.
        itemPanel.AllowDrop = True
        picBox.AllowDrop = True
        lblName.AllowDrop = True
        AddHandler itemPanel.DragEnter, AddressOf FlowPanel_DragEnter
        AddHandler itemPanel.DragDrop, AddressOf FlowPanel_DragDrop
        AddHandler picBox.DragEnter, AddressOf FlowPanel_DragEnter
        AddHandler picBox.DragDrop, AddressOf FlowPanel_DragDrop
        AddHandler lblName.DragEnter, AddressOf FlowPanel_DragEnter
        AddHandler lblName.DragDrop, AddressOf FlowPanel_DragDrop
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
            If IsUrl(path) Then
                ' URL dogrudan tarayicida acilir (.url kisayolu silinmis olsa bile calisir)
                Process.Start(path)
            ElseIf File.Exists(path) Then
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
            If IsUrl(itemPath) Then
                ' URL öğeleri için Explorer'da Aç anlamsız; işlem yapma
                Return
            ElseIf File.Exists(itemPath) Then
                ' Dosya ise, dosyanın bulunduğu klasörü aç ve dosyayı seç
                Process.Start("explorer.exe", "/select,""" & itemPath & """")
            ElseIf Directory.Exists(itemPath) Then
                ' Klasör ise, direkt klasörü aç
                Process.Start("explorer.exe", itemPath)
            Else
                ' Dosya/klasör silinmiş olabilir: üst klasörü yine de aç
                Dim parentDir As String = IO.Path.GetDirectoryName(itemPath)
                If Not String.IsNullOrEmpty(parentDir) AndAlso Directory.Exists(parentDir) Then
                    Process.Start("explorer.exe", parentDir)
                Else
                    Dim msg As String = String.Format(langManager.GetText("MsgFileOrFolderNotFound", "File or folder not found: {0}"), itemPath)
                    MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
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

        Dim propsForm As New PropertiesForm(itemName, itemPath, itemIconPath)
        propsForm.ShowDialog(Me)
        propsForm.Dispose()
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

        MenuSystem.Text = langManager.GetText("MenuSystem", "&System")
        MenuSystemKlasor.Text = langManager.GetText("MenuSystemKlasor", "Open RiaLauncher &Folder")
        MenuUpdate.Text = langManager.GetText("MenuUpdate", "&Update")
        MenuUpdateKontrol.Text = langManager.GetText("MenuUpdateKontrol", "Check for &Updates")
        MenuYardim.Text = langManager.GetText("MenuYardim", "&Help")
        MenuYardimDokumanlar.Text = langManager.GetText("MenuYardimDokumanlar", "&Help Page")
        MenuYardimWeb.Text = langManager.GetText("MenuYardimWeb", "&Web Site")
        MenuYardimWebSite.Text = langManager.GetText("MenuYardimWebSite", "Rialauncher &Web Site")
        MenuYardimGithub.Text = langManager.GetText("MenuYardimGithub", "&Github Repo")
        MenuYardimLisans.Text = langManager.GetText("MenuYardimLisans", "&License Terms")
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
            settingsForm.AutoUpdateEnabled = autoUpdateEnabled
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
                autoUpdateEnabled = settingsForm.AutoUpdateEnabled
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
        ' Pencere konumu ve boyutunu kaydet (maximize ise normal boyutlar kaydedilir)
        If Me.WindowState = FormWindowState.Normal Then
            winX = Me.Location.X
            winY = Me.Location.Y
            winWidth = Me.Width
            winHeight = Me.Height
        Else
            Dim rb = Me.RestoreBounds
            winX = rb.X
            winY = rb.Y
            winWidth = rb.Width
            winHeight = rb.Height
        End If
        SaveSettingsToIni()
    End Sub

    Private Sub RestoreWindowBounds()
        If winWidth <= 0 OrElse winHeight <= 0 OrElse winX < 0 OrElse winY < 0 Then Return

        Dim rect As New Rectangle(winX, winY, winWidth, winHeight)
        For Each scr In Screen.AllScreens
            If scr.WorkingArea.IntersectsWith(rect) Then
                Me.StartPosition = FormStartPosition.Manual
                Me.Location = New Point(winX, winY)
                Me.Size = New Size(winWidth, winHeight)
                Exit For
            End If
        Next
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
    ' System / Update / Help Menu Event Handlers
    ' ============================================

    Private Sub MenuSystemKlasor_Click(sender As Object, e As EventArgs) Handles MenuSystemKlasor.Click
        Try
            If Not String.IsNullOrEmpty(sRootDir) AndAlso Directory.Exists(sRootDir) Then
                Process.Start("explorer.exe", sRootDir)
            End If
        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgOpenFolderError", "An error occurred while opening the folder: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuUpdateKontrol_Click(sender As Object, e As EventArgs) Handles MenuUpdateKontrol.Click
        CheckForUpdates(True)
    End Sub

    Private Sub MenuYardimDokumanlar_Click(sender As Object, e As EventArgs) Handles MenuYardimDokumanlar.Click
        Try
            Process.Start("https://riasoft.net/assets/docs/rialauncher/RiaLauncherHelp-en.html?lang=en")
        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgHelpDocError", "The document page could not be opened: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuYardimWebSite_Click(sender As Object, e As EventArgs) Handles MenuYardimWebSite.Click
        Try
            Process.Start("https://riasoft.net/en/rialauncher.html")
        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgWebSiteError", "Website could not be opened: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuYardimGithub_Click(sender As Object, e As EventArgs) Handles MenuYardimGithub.Click
        Try
            Process.Start("https://github.com/Riasoftapp/RiaLauncher")
        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgWebSiteError", "Website could not be opened: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuYardimLisans_Click(sender As Object, e As EventArgs) Handles MenuYardimLisans.Click
        Using f As New LicenseForm()
            f.ShowDialog(Me)
        End Using
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
        If HasUrlDragData(e) Then
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
            Dim unavailableIcon As String = IO.Path.Combine(sIconDir, "unavailable24.png")

            For Each item In items
                AddDbItem(flowPanel, item, unavailableIcon)
            Next

        Catch ex As Exception
            Dim msg As String = String.Format(langManager.GetText("MsgTabRefreshError", "Tab yenileme hatası: {0}"), ex.Message)
            MessageBox.Show(msg, langManager.GetText("MsgError", "Hata"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Mevcut .url ogelerini gercek URL ile gunceller; boylece kisayol dosyasi
    ' silinse bile ogeler veritabaninda kalir ve calistirilabilir durumda olur.
    Private Sub UpgradeUrlItems()
        Try
            Dim cats = DatabaseManager.GetCategories()
            For Each cat In cats
                For Each it In cat.Items
                    If it.Path IsNot Nothing AndAlso it.Path.ToLower().EndsWith(".url") AndAlso IO.File.Exists(it.Path) Then
                        Dim u = ReadUrlFromShortcut(it.Path)
                        If Not String.IsNullOrEmpty(u) Then
                            DatabaseManager.UpdateItemPath(cat.Name, it.Path, u)
                        End If
                    End If
                Next
            Next
        Catch
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
