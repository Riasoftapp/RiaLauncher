Imports System.IO
Imports System.Text

Public Class LanguageManager
    Private langDirPath As String
    Private currentLanguage As String
    Private translations As New Dictionary(Of String, Dictionary(Of String, String))
    Private availableLanguages As New List(Of String)

    Public Sub New(langDirPath As String, defaultLanguage As String)
        Me.langDirPath = langDirPath
        Me.currentLanguage = defaultLanguage
        LoadLanguages()
    End Sub

    ''' <summary>
    ''' assets\lang dizinindeki tüm .lng dosyalarını yükler
    ''' </summary>
    Private Sub LoadLanguages()
        If Not Directory.Exists(langDirPath) Then
            Directory.CreateDirectory(langDirPath)
        End If

        translations.Clear()
        availableLanguages.Clear()

        Dim langFiles = Directory.GetFiles(langDirPath, "*.lng")
        
        ' Eğer hiç dil dosyası yoksa varsayılanları oluştur
        If langFiles.Length = 0 Then
            CreateDefaultLanguageFiles()
            langFiles = Directory.GetFiles(langDirPath, "*.lng")
        End If

        For Each filePath In langFiles
            Dim langCode = Path.GetFileNameWithoutExtension(filePath).ToLower()
            Dim currentDict As New Dictionary(Of String, String)
            
            translations(langCode) = currentDict
            availableLanguages.Add(langCode)

            For Each line In File.ReadAllLines(filePath, Encoding.UTF8)
                Dim trimmedLine = line.Trim()

                ' Boş satır veya yorum satırı veya section başlığı (eski format uyumluluğu için [tr] vs. atlıyoruz)
                If String.IsNullOrEmpty(trimmedLine) OrElse trimmedLine.StartsWith(";") OrElse (trimmedLine.StartsWith("[") AndAlso trimmedLine.EndsWith("]")) Then
                    Continue For
                End If

                ' Key=Value çifti
                If trimmedLine.Contains("=") Then
                    Dim parts = trimmedLine.Split({"="c}, 2)
                    Dim key = parts(0).Trim()
                    Dim value = If(parts.Length > 1, parts(1).Trim(), "")
                    currentDict(key) = value
                End If
            Next
        Next
    End Sub

    ''' <summary>
    ''' Varsayılan dil dosyalarını (tr.lng ve en.lng) oluşturur
    ''' </summary>
    Private Sub CreateDefaultLanguageFiles()
        Dim trContent As String = GetDefaultTurkishContent()
        Dim enContent As String = GetDefaultEnglishContent()

        File.WriteAllText(Path.Combine(langDirPath, "tr.lng"), trContent, Encoding.UTF8)
        File.WriteAllText(Path.Combine(langDirPath, "en.lng"), enContent, Encoding.UTF8)
    End Sub

    Private Function GetDefaultTurkishContent() As String
        Return "; WinLauncher Turkish Language File" & vbCrLf &
            "; Format: Key=Value" & vbCrLf & vbCrLf &
            "; Menu - Dosya" & vbCrLf &
            "MenuDosya=Dosya" & vbCrLf &
            "MenuDosyaCikis=Çıkış" & vbCrLf & vbCrLf &
            "; Menu - Sekmeler" & vbCrLf &
            "MenuSekmeler=Sekmeler" & vbCrLf &
            "MenuSekmelerYeni=Yeni Sekme" & vbCrLf &
            "MenuSekmelerAdDegistir=Sekme Adını Değiştir" & vbCrLf &
            "MenuSekmelerSil=Sekmeyi Sil" & vbCrLf &
            "MenuSekmelerYenile=Sekmeyi Yenile" & vbCrLf & vbCrLf &
            "; Menu - Sıralama" & vbCrLf &
            "MenuSiralama=Sıralama" & vbCrLf &
            "MenuManuelSiralama=Manuel Sıralama..." & vbCrLf & vbCrLf &
            "; Menu - Araçlar" & vbCrLf &
            "MenuAraclar=Araçlar" & vbCrLf &
            "MenuToolsCmd=Komut İstemi" & vbCrLf &
            "MenuToolsPowershell=PowerShell" & vbCrLf &
            "MenuToolsTaskMgr=Görev Yöneticisi" & vbCrLf &
            "MenuToolsServices=Hizmet Yöneticisi" & vbCrLf &
            "MenuToolsShowDesktop=Masaüstünü Göster" & vbCrLf &
            "MenuToolsRestoreDesktop=Masaüstünü Geri Yükle" & vbCrLf &
            "MenuToolsControlPanel=Denetim Masası" & vbCrLf &
            "MenuToolsNetworkCenter=Ağ ve Paylaşım Merkezi" & vbCrLf &
            "MenuToolsDeviceManager=Aygıt Yöneticisi" & vbCrLf &
            "MenuToolsComputerName=Bilgisayar Adını Göster" & vbCrLf &
            "MenuToolsIPAddress=IP Adreslerini Göster" & vbCrLf & vbCrLf &
            "; Menu - Ayarlar" & vbCrLf &
            "MenuAyarlar=Ayarlar" & vbCrLf & vbCrLf &
            "; Menu - Yardım" & vbCrLf &
            "MenuYardim=Yardım" & vbCrLf &
            "MenuYardimDokumanlar=Yardım" & vbCrLf &
            "MenuYardimDokumanIndir=Döküman İndir" & vbCrLf &
            "MenuYardimLisans=Lisans Koşulları" & vbCrLf &
            "MenuYardimBagis=Bağış Yap" & vbCrLf &
            "MenuYardimAnaSayfa=Ana Sayfa" & vbCrLf &
            "MenuYardimHakkinda=Hakkında..." & vbCrLf & vbCrLf &
            "; Context Menu - Item" & vbCrLf &
            "MenuItemLaunch=Başlat" & vbCrLf &
            "MenuItemCopyMove=Kopyala/Taşı..." & vbCrLf &
            "MenuItemRename=Yeniden Adlandır" & vbCrLf &
            "MenuItemChangeIcon=İkonu Değiştir" & vbCrLf &
            "MenuItemUpdatePath=Yolu Güncelle" & vbCrLf &
            "MenuItemOpenFolder=Klasörde Göster" & vbCrLf &
            "MenuItemDelete=Sil" & vbCrLf &
            "MenuItemProperties=Özellikler" & vbCrLf & vbCrLf &
            "; Context Menu - Tab" & vbCrLf &
            "MenuTabYeni=Yeni Sekme" & vbCrLf &
            "MenuTabAdDegistir=Sekme Adını Değiştir" & vbCrLf &
            "MenuTabSil=Sekmeyi Sil" & vbCrLf & vbCrLf &
            "; Search Panel" & vbCrLf &
            "lblSearch=Ara:" & vbCrLf &
            "btnSearch=Ara" & vbCrLf & vbCrLf &
            "; Messages - General" & vbCrLf &
            "MsgError=Hata" & vbCrLf &
            "MsgWarning=Uyarı" & vbCrLf &
            "MsgInfo=Bilgi" & vbCrLf &
            "MsgConfirm=Onay" & vbCrLf &
            "MsgSuccess=Başarılı" & vbCrLf &
            "MsgCopiedToClipboard=(Panoya kopyalandı)" & vbCrLf & vbCrLf &
            "; Messages - Tools Menu" & vbCrLf &
            "MsgToolsCmdError=Komut İstemi açılamıyor: {0}" & vbCrLf &
            "MsgToolsPowershellError=PowerShell açılamıyor: {0}" & vbCrLf &
            "MsgToolsTaskMgrError=Görev Yöneticisi açılamıyor: {0}" & vbCrLf &
            "MsgToolsServicesError=Hizmet Yöneticisi açılamıyor: {0}" & vbCrLf &
            "MsgToolsShowDesktopError=Masaüstü gösterilemiyor: {0}" & vbCrLf &
            "MsgToolsDesktopRestored=Masaüstü geri yüklendi." & vbCrLf &
            "MsgToolsRestoreDesktopError=Masaüstü geri yüklenemedi: {0}" & vbCrLf &
            "MsgToolsControlPanelError=Denetim Masası açılamıyor: {0}" & vbCrLf &
            "MsgToolsNetworkCenterError=Ağ ve Paylaşım Merkezi açılamıyor: {0}" & vbCrLf &
            "MsgToolsDeviceManagerError=Aygıt Yöneticisi açılamıyor: {0}" & vbCrLf &
            "MsgToolsComputerName=Bilgisayar Adı: {0}" & vbCrLf &
            "MsgToolsComputerNameTitle=Bilgisayar Adı" & vbCrLf &
            "MsgToolsComputerNameError=Bilgisayar adı alınamıyor: {0}" & vbCrLf &
            "MsgToolsLocalIP=Yerel IP: {0}" & vbCrLf &
            "MsgToolsRemoteIP=Uzak IP: {0}" & vbCrLf &
            "MsgToolsIPTitle=IP Adresleri" & vbCrLf &
            "MsgToolsIPError=IP adresleri alınamıyor: {0}" & vbCrLf &
            "MsgToolsIPNotFound=Bulunamadı" & vbCrLf & vbCrLf &
            "; Messages - Help Menu" & vbCrLf &
            "MsgHelpComingSoon=Yardım dökümanları yakında eklenecektir." & vbCrLf &
            "MsgHelpTitle=Yardım" & vbCrLf &
            "MsgHelpDocError=Döküman sayfası açılamadı: {0}" & vbCrLf &
            "MsgDonateTitle=Bağış Yap" & vbCrLf &
            "MsgDonateMessage=WinLauncher projesini beğendiyseniz," & vbCrLf &
            "                         geliştirilmesine destek olmak için bağış yapabilirsiniz." & vbCrLf &
            "                         GitHub Sponsors: github.com/sponsors/hikmetalemdaroglu" & vbCrLf &
            "                         Teşekkür ederiz! 🙏" & vbCrLf &
            "MsgDonateError=Bağış sayfası açılamadı: {0}" & vbCrLf &
            "MsgHomePageError=Ana sayfa açılamadı: {0}" & vbCrLf & vbCrLf &
            "; Messages - AboutForm" & vbCrLf &
            "MsgWebSiteError=Web sitesi açılamadı: {0}" & vbCrLf &
            "MsgEmailError=E-posta uygulaması açılamadı: {0}" & vbCrLf & vbCrLf &
            "; Messages - Manual Sort" & vbCrLf &
            "MsgSortSaved=Sıralama başarıyla kaydedildi!" & vbCrLf &
            "MsgSortNoTabSelected=Lütfen önce bir sekme seçin." & vbCrLf &
            "MsgSortNoItems=Bu sekmede sıralanacak öğe bulunmuyor." & vbCrLf & vbCrLf &
            "; Messages - Copy/Move" & vbCrLf &
            "MsgCopyError=Kopyalama hatası: {0}" & vbCrLf &
            "MsgMoveError=Taşıma hatası: {0}" & vbCrLf &
            "MsgCopyFailed=Kopyalama işlemi başarısız oldu." & vbCrLf &
            "MsgMoveFailed=Taşıma işlemi başarısız oldu." & vbCrLf &
            "MsgCannotMoveSameTab=Simge aynı sekme içinde taşınamaz." & vbCrLf & vbCrLf &
            "; Messages - Tab Operations" & vbCrLf &
            "MsgTabRefreshError=Tab yenileme hatası: {0}" & vbCrLf &
            "MsgTabRefreshed='{0}' sekmesi yenilendi." & vbCrLf &
            "MsgAtLeastOneTab=En az bir sekme bulunmalıdır." & vbCrLf &
            "MsgDeleteTabConfirm='{0}' sekmesini silmek istediğinizden emin misiniz?" & vbCrLf &
            "MsgDeleteTabTitle=Sekme Sil" & vbCrLf & vbCrLf &
            "; Messages - General Operations" & vbCrLf &
            "MsgXMLLoadError=XML yükleme hatası: {0}" & vbCrLf &
            "MsgFileNotFound=Dosya bulunamadı: {0}" & vbCrLf &
            "MsgLaunchError=Başlatma hatası: {0}" & vbCrLf &
            "MsgSaveError=Kaydetme hatası: {0}" & vbCrLf &
            "MsgIconLoadError=İkon yükleme hatası: {0}" & vbCrLf &
            "MsgPathUpdated=Yol başarıyla güncellendi." & vbCrLf &
            "MsgFileOrFolderNotFound=Dosya veya klasör bulunamadı: {0}" & vbCrLf &
            "MsgOpenFolderError=Klasör açılırken hata oluştu: {0}" & vbCrLf &
            "MsgDeleteItemConfirm='{0}' öğesini silmek istediğinizden emin misiniz?" & vbCrLf &
            "MsgDeleteItemTitle=Öğe Sil" & vbCrLf &
            "MsgItemPropertiesTitle=Öğe Özellikleri" & vbCrLf &
            "MsgSettingsSaved=Ayarlar kaydedildi. Değişikliklerin tam olarak uygulanması için uygulamayı yeniden başlatın." & vbCrLf & vbCrLf &
            "; Common Buttons" & vbCrLf &
            "BtnOK=Tamam" & vbCrLf &
            "BtnCancel=İptal" & vbCrLf &
            "BtnYes=Evet" & vbCrLf &
            "BtnNo=Hayır" & vbCrLf &
            "BtnClose=Kapat" & vbCrLf & vbCrLf &
            "; AboutForm" & vbCrLf &
            "AboutTitle=Hakkında" & vbCrLf &
            "AboutAppName=WinLauncher - Windows Launcher" & vbCrLf &
            "AboutVersion=Versiyon 2.0" & vbCrLf &
            "AboutLicenseStatus=Ticari Kullanım İçin Henüz Lisanslanmamıştır" & vbCrLf &
            "AboutFreeUse=Kişisel Kullanım İçin Ücretsizdir" & vbCrLf &
            "AboutCopyright=© 2024-2025 Hikmet Alp Alemdaroğlu" & vbCrLf &
            "AboutWebSiteLabel=Web Site:" & vbCrLf &
            "AboutEmailLabel=Destek E-posta:" & vbCrLf &
            "AboutBtnAnaSayfa=Ana Sayfa" & vbCrLf &
            "AboutBtnLisans=Lisans Koşulları" & vbCrLf &
            "AboutBtnKapat=Kapat" & vbCrLf & vbCrLf &
            "; License Text" & vbCrLf &
            "LicenseFree=Bu yazılım kişisel kullanım için ücretsizdir." & vbCrLf & vbCrLf &
            "; Settings Form" & vbCrLf &
            "SettingsTitle=Ayarlar" & vbCrLf &
            "SettingsLaunchMode=Başlatma Modu:" & vbCrLf &
            "SettingsSingleClick=Tek Tık" & vbCrLf &
            "SettingsDoubleClick=Çift Tık" & vbCrLf &
            "SettingsViewMode=Görünüm Modu:" & vbCrLf &
            "SettingsIconText=İkon + Metin" & vbCrLf &
            "SettingsIconOnly=Sadece İkon" & vbCrLf &
            "SettingsAlwaysOnTop=Her Zaman Üstte" & vbCrLf &
            "SettingsBtnSave=Kaydet" & vbCrLf &
            "SettingsBtnCancel=İptal" & vbCrLf & vbCrLf &
            "; Manual Sort Form" & vbCrLf &
            "ManualSortTitle=Manuel Sıralama" & vbCrLf &
            "ManualSortBtnUp=Yukarı" & vbCrLf &
            "ManualSortBtnDown=Aşağı" & vbCrLf &
            "ManualSortBtnSave=Kaydet" & vbCrLf &
            "ManualSortBtnCancel=İptal" & vbCrLf & vbCrLf &
            "; Copy/Move Form" & vbCrLf &
            "CopyMoveTitle=Kopyala/Taşı" & vbCrLf &
            "CopyMoveSourceTab=Kaynak Sekme:" & vbCrLf &
            "CopyMoveTargetTab=Hedef Sekme:" & vbCrLf &
            "CopyMoveItemName=Öğe Adı:" & vbCrLf &
            "CopyMoveBtnCopy=Kopyala" & vbCrLf &
            "CopyMoveBtnMove=Taşı" & vbCrLf &
            "CopyMoveBtnCancel=İptal" & vbCrLf & vbCrLf

    End Function

    Private Function GetDefaultEnglishContent() As String
        Return "; WinLauncher English Language File" & vbCrLf &
            "; Format: Key=Value" & vbCrLf & vbCrLf &
            "; Menu - File" & vbCrLf &
            "MenuDosya=File" & vbCrLf &
            "MenuDosyaCikis=Exit" & vbCrLf & vbCrLf &
            "; Menu - Tabs" & vbCrLf &
            "MenuSekmeler=Tabs" & vbCrLf &
            "MenuSekmelerYeni=New Tab" & vbCrLf &
            "MenuSekmelerAdDegistir=Rename Tab" & vbCrLf &
            "MenuSekmelerSil=Delete Tab" & vbCrLf &
            "MenuSekmelerYenile=Refresh Tab" & vbCrLf & vbCrLf &
            "; Menu - Sorting" & vbCrLf &
            "MenuSiralama=Sorting" & vbCrLf &
            "MenuManuelSiralama=Manual Sort..." & vbCrLf & vbCrLf &
            "; Menu - Tools" & vbCrLf &
            "MenuAraclar=Tools" & vbCrLf &
            "MenuToolsCmd=Command Prompt" & vbCrLf &
            "MenuToolsPowershell=PowerShell" & vbCrLf &
            "MenuToolsTaskMgr=Task Manager" & vbCrLf &
            "MenuToolsServices=Services Manager" & vbCrLf &
            "MenuToolsShowDesktop=Show Desktop" & vbCrLf &
            "MenuToolsRestoreDesktop=Restore Desktop" & vbCrLf &
            "MenuToolsControlPanel=Control Panel" & vbCrLf &
            "MenuToolsNetworkCenter=Network and Sharing Center" & vbCrLf &
            "MenuToolsDeviceManager=Device Manager" & vbCrLf &
            "MenuToolsComputerName=Show Computer Name" & vbCrLf &
            "MenuToolsIPAddress=Show IP Addresses" & vbCrLf & vbCrLf &
            "; Menu - Settings" & vbCrLf &
            "MenuAyarlar=Settings" & vbCrLf & vbCrLf &
            "; Menu - Help" & vbCrLf &
            "MenuYardim=Help" & vbCrLf &
            "MenuYardimDokumanlar=Help" & vbCrLf &
            "MenuYardimDokumanIndir=Download Documentation" & vbCrLf &
            "MenuYardimLisans=License Terms" & vbCrLf &
            "MenuYardimBagis=Donate" & vbCrLf &
            "MenuYardimAnaSayfa=Home Page" & vbCrLf &
            "MenuYardimHakkinda=About..." & vbCrLf & vbCrLf &
            "; Context Menu - Item" & vbCrLf &
            "MenuItemLaunch=Launch" & vbCrLf &
            "MenuItemCopyMove=Copy/Move..." & vbCrLf &
            "MenuItemRename=Rename" & vbCrLf &
            "MenuItemChangeIcon=Change Icon" & vbCrLf &
            "MenuItemUpdatePath=Update Path" & vbCrLf &
            "MenuItemOpenFolder=Show in Folder" & vbCrLf &
            "MenuItemDelete=Delete" & vbCrLf &
            "MenuItemProperties=Properties" & vbCrLf & vbCrLf &
            "; Context Menu - Tab" & vbCrLf &
            "MenuTabYeni=New Tab" & vbCrLf &
            "MenuTabAdDegistir=Rename Tab" & vbCrLf &
            "MenuTabSil=Delete Tab" & vbCrLf & vbCrLf &
            "; Search Panel" & vbCrLf &
            "lblSearch=Search:" & vbCrLf &
            "btnSearch=Search" & vbCrLf & vbCrLf &
            "; Messages - General" & vbCrLf &
            "MsgError=Error" & vbCrLf &
            "MsgWarning=Warning" & vbCrLf &
            "MsgInfo=Info" & vbCrLf &
            "MsgConfirm=Confirm" & vbCrLf &
            "MsgSuccess=Success" & vbCrLf &
            "MsgCopiedToClipboard=(Copied to clipboard)" & vbCrLf & vbCrLf &
            "; Messages - Tools Menu" & vbCrLf &
            "MsgToolsCmdError=Cannot open Command Prompt: {0}" & vbCrLf &
            "MsgToolsPowershellError=Cannot open PowerShell: {0}" & vbCrLf &
            "MsgToolsTaskMgrError=Cannot open Task Manager: {0}" & vbCrLf &
            "MsgToolsServicesError=Cannot open Services Manager: {0}" & vbCrLf &
            "MsgToolsShowDesktopError=Cannot show desktop: {0}" & vbCrLf &
            "MsgToolsDesktopRestored=Desktop restored." & vbCrLf &
            "MsgToolsRestoreDesktopError=Cannot restore desktop: {0}" & vbCrLf &
            "MsgToolsControlPanelError=Cannot open Control Panel: {0}" & vbCrLf &
            "MsgToolsNetworkCenterError=Cannot open Network and Sharing Center: {0}" & vbCrLf &
            "MsgToolsDeviceManagerError=Cannot open Device Manager: {0}" & vbCrLf &
            "MsgToolsComputerName=Computer Name: {0}" & vbCrLf &
            "MsgToolsComputerNameTitle=Computer Name" & vbCrLf &
            "MsgToolsComputerNameError=Cannot get computer name: {0}" & vbCrLf &
            "MsgToolsLocalIP=Local IP: {0}" & vbCrLf &
            "MsgToolsRemoteIP=Remote IP: {0}" & vbCrLf &
            "MsgToolsIPTitle=IP Addresses" & vbCrLf &
            "MsgToolsIPError=Cannot get IP addresses: {0}" & vbCrLf &
            "MsgToolsIPNotFound=Not found" & vbCrLf & vbCrLf &
            "; Messages - Help Menu" & vbCrLf &
            "MsgHelpComingSoon=Help documentation will be added soon." & vbCrLf &
            "MsgHelpTitle=Help" & vbCrLf &
            "MsgHelpDocError=Cannot open documentation page: {0}" & vbCrLf &
            "MsgDonateTitle=Donate" & vbCrLf &
            "MsgDonateMessage=If you like the WinLauncher project," & vbCrLf &
            "                         you can donate to support its development." & vbCrLf &
            "                         GitHub Sponsors: github.com/sponsors/hikmetalemdaroglu" & vbCrLf &
            "                         Thank you! 🙏" & vbCrLf &
            "MsgDonateError=Cannot open donation page: {0}" & vbCrLf &
            "MsgHomePageError=Cannot open home page: {0}" & vbCrLf & vbCrLf &
            "; Messages - AboutForm" & vbCrLf &
            "MsgWebSiteError=Cannot open website: {0}" & vbCrLf &
            "MsgEmailError=Cannot open email application: {0}" & vbCrLf & vbCrLf &
            "; Messages - Manual Sort" & vbCrLf &
            "MsgSortSaved=Sorting saved successfully!" & vbCrLf &
            "MsgSortNoTabSelected=Please select a tab first." & vbCrLf &
            "MsgSortNoItems=No items to sort in this tab." & vbCrLf & vbCrLf &
            "; Messages - Copy/Move" & vbCrLf &
            "MsgCopyError=Copy error: {0}" & vbCrLf &
            "MsgMoveError=Move error: {0}" & vbCrLf &
            "MsgCopyFailed=Copy operation failed." & vbCrLf &
            "MsgMoveFailed=Move operation failed." & vbCrLf &
            "MsgCannotMoveSameTab=Cannot move item within the same tab." & vbCrLf & vbCrLf &
            "; Messages - Tab Operations" & vbCrLf &
            "MsgTabRefreshError=Tab refresh error: {0}" & vbCrLf &
            "MsgTabRefreshed=Tab '{0}' refreshed." & vbCrLf &
            "MsgAtLeastOneTab=At least one tab must remain." & vbCrLf &
            "MsgDeleteTabConfirm=Are you sure you want to delete the tab '{0}'?" & vbCrLf &
            "MsgDeleteTabTitle=Delete Tab" & vbCrLf & vbCrLf &
            "; Messages - General Operations" & vbCrLf &
            "MsgXMLLoadError=XML load error: {0}" & vbCrLf &
            "MsgFileNotFound=File not found: {0}" & vbCrLf &
            "MsgLaunchError=Launch error: {0}" & vbCrLf &
            "MsgSaveError=Save error: {0}" & vbCrLf &
            "MsgIconLoadError=Icon load error: {0}" & vbCrLf &
            "MsgPathUpdated=Path successfully updated." & vbCrLf &
            "MsgFileOrFolderNotFound=File or folder not found: {0}" & vbCrLf &
            "MsgOpenFolderError=Error opening folder: {0}" & vbCrLf &
            "MsgDeleteItemConfirm=Are you sure you want to delete '{0}'?" & vbCrLf &
            "MsgDeleteItemTitle=Delete Item" & vbCrLf &
            "MsgItemPropertiesTitle=Item Properties" & vbCrLf &
            "MsgSettingsSaved=Settings saved. Please restart the application for changes to take full effect." & vbCrLf & vbCrLf &
            "; Common Buttons" & vbCrLf &
            "BtnOK=OK" & vbCrLf &
            "BtnCancel=Cancel" & vbCrLf &
            "BtnYes=Yes" & vbCrLf &
            "BtnNo=No" & vbCrLf &
            "BtnClose=Close" & vbCrLf & vbCrLf &
            "; AboutForm" & vbCrLf &
            "AboutTitle=About" & vbCrLf &
            "AboutAppName=WinLauncher - Windows Launcher" & vbCrLf &
            "AboutVersion=Version {0}" & vbCrLf &
            "AboutLicenseStatus=Not Yet Licensed for Commercial Use" & vbCrLf &
            "AboutFreeUse=Free for Personal Use" & vbCrLf &
            "AboutCopyright=© 2024-2025 Hikmet Alp Alemdaroğlu" & vbCrLf &
            "AboutWebSiteLabel=Web Site:" & vbCrLf &
            "AboutEmailLabel=Support Email:" & vbCrLf &
            "AboutBtnAnaSayfa=Home Page" & vbCrLf &
            "AboutBtnLisans=License Terms" & vbCrLf &
            "AboutBtnKapat=Close" & vbCrLf & vbCrLf &
            "; License Text" & vbCrLf &
            "LicenseFree=This software is free for personal and commercial use." & vbCrLf & vbCrLf &
            "; Settings Form" & vbCrLf &
            "SettingsTitle=Settings" & vbCrLf &
            "SettingsLaunchMode=Launch Mode:" & vbCrLf &
            "SettingsSingleClick=Single Click" & vbCrLf &
            "SettingsDoubleClick=Double Click" & vbCrLf &
            "SettingsViewMode=View Mode:" & vbCrLf &
            "SettingsIconText=Icon + Text" & vbCrLf &
            "SettingsIconOnly=Icon Only" & vbCrLf &
            "SettingsAlwaysOnTop=Always On Top" & vbCrLf &
            "SettingsBtnSave=Save" & vbCrLf &
            "SettingsBtnCancel=Cancel" & vbCrLf & vbCrLf &
            "; Manual Sort Form" & vbCrLf &
            "ManualSortTitle=Manual Sort" & vbCrLf &
            "ManualSortBtnUp=Up" & vbCrLf &
            "ManualSortBtnDown=Down" & vbCrLf &
            "ManualSortBtnSave=Save" & vbCrLf &
            "ManualSortBtnCancel=Cancel" & vbCrLf & vbCrLf &
            "; Copy/Move Form" & vbCrLf &
            "CopyMoveTitle=Copy/Move" & vbCrLf &
            "CopyMoveSourceTab=Source Tab:" & vbCrLf &
            "CopyMoveTargetTab=Target Tab:" & vbCrLf &
            "CopyMoveItemName=Item Name:" & vbCrLf &
            "CopyMoveBtnCopy=Copy" & vbCrLf &
            "CopyMoveBtnMove=Move" & vbCrLf &
            "CopyMoveBtnCancel=Cancel" & vbCrLf & vbCrLf

    End Function

    ''' <summary>
    ''' Aktif dili değiştirir
    ''' </summary>
    Public Sub SetLanguage(languageCode As String)
        If translations.ContainsKey(languageCode.ToLower()) Then
            currentLanguage = languageCode.ToLower()
        End If
    End Sub

    ''' <summary>
    ''' Aktif dili döndürür
    ''' </summary>
    Public Function GetCurrentLanguage() As String
        Return currentLanguage
    End Function

    ''' <summary>
    ''' Kullanılabilir dillerin listesini döndürür
    ''' </summary>
    Public Function GetAvailableLanguages() As List(Of String)
        Return New List(Of String)(availableLanguages)
    End Function

    ''' <summary>
    ''' Key'e karşılık gelen çeviriyi döndürür
    ''' </summary>
    Public Function GetText(key As String) As String
        If translations.ContainsKey(currentLanguage) Then
            Dim langDict = translations(currentLanguage)
            If langDict.ContainsKey(key) Then
                Return langDict(key)
            End If
        End If

        ' Çeviri bulunamazsa key'in kendisini döndür
        Return key
    End Function

    ''' <summary>
    ''' Key'e karşılık gelen çeviriyi döndürür, bulamazsa defaultValue döndürür
    ''' </summary>
    Public Function GetText(key As String, defaultValue As String) As String
        If translations.ContainsKey(currentLanguage) Then
            Dim langDict = translations(currentLanguage)
            If langDict.ContainsKey(key) Then
                Return langDict(key)
            End If
        End If

        ' Çeviri bulunamadı - error.log'a kayıt at
        LogTranslationError(key, defaultValue)

        Return defaultValue
    End Function

    ''' <summary>
    ''' Bulunamayan çeviri anahtarını error.log'a kaydeder
    ''' </summary>
    Private Sub LogTranslationError(key As String, defaultValue As String)
        Try
            Dim logPath = Path.Combine(Form1.sLogDir, "error.log")
            Dim timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            Dim errorMessage = String.Format("[{0}] [LanguageManager.GetText] Line: 469 | Translation key not found: {1}:{2}", timestamp, currentLanguage, key)

            ' Log dosyasını aç ve satır ekle
            If Not Directory.Exists(Form1.sLogDir) Then
                Directory.CreateDirectory(Form1.sLogDir)
            End If

            File.AppendAllText(logPath, errorMessage & Environment.NewLine, Encoding.UTF8)
        Catch ex As Exception
            ' Log yazma hatasını sessizce yoksay
        End Try
    End Sub

    ''' <summary>
    ''' lang.ini dosyasını yeniden yükler (yeni dil eklendiğinde)
    ''' </summary>
    Public Sub ReloadLanguages()
        LoadLanguages()
    End Sub

    ''' <summary>
    ''' Verilen dil kodunun sağdan sola (RTL) olup olmadığını kontrol eder
    ''' </summary>
    Public Function IsRTLLanguage(languageCode As String) As Boolean
        Dim rtlLanguages As New List(Of String) From {"ar"}
        Return rtlLanguages.Contains(languageCode.ToLower())
    End Function

    ''' <summary>
    ''' Aktif dil RTL olup olmadığını kontrol eder
    ''' </summary>
    Public Function IsCurrentLanguageRTL() As Boolean
        Return IsRTLLanguage(currentLanguage)
    End Function
End Class
