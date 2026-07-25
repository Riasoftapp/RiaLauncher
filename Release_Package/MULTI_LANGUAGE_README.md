# WinLauncher - Multi-Language (i18n) System Documentation

## 📚 Genel Bakış

WinLauncher v2.0 ile birlikte tam çok dil desteği (multi-language/i18n) tamamlanmıştır. Sistem, `lang.ini` dosyasından çevirileri okur ve kullanıcı arayüzündeki tüm metinleri runtime'da değiştirebilir.

---

## 🗂️ Dosya Yapısı

### 1. **LanguageManager.vb**
Dil yönetiminden sorumlu ana sınıf.

**Temel Metodlar:**
- `SetLanguage(languageCode As String)` - Aktif dili değiştirir
- `GetText(key As String)` - Key'e karşılık gelen çeviriyi döndürür
- `GetAvailableLanguages()` - Kullanılabilir dillerin listesini döndürür
- `ReloadLanguages()` - lang.ini'yi yeniden yükler

### 2. **lang.ini**
Tüm dil çevirilerini içeren INI formatında dosya.

**Format:**
```ini
[tr]
MenuDosya=Dosya
MenuDosyaCikis=Çıkış
...

[en]
MenuDosya=File
MenuDosyaCikis=Exit
...
```

### 3. **settings.ini**
Kullanıcının seçtiği dil tercihi saklanır.

**Yeni Eklenen:**
```ini
[General]
DefaultLang=tr
```

---

## 🛠️ Nasıl Çalışır?

### 1. **Başlangıç (Initialization)**

Form1.vb > Form1_Load():
```vb
InitializeLanguageManager()      ' LanguageManager oluştur
LoadSettingsFromIni()            ' settings.ini'den DefaultLang oku
PopulateLanguageComboBox()       ' Combobox'a dilleri doldur
ApplyLanguage()                  ' UI'a dili uygula
```

### 2. **Dil Değiştirme (Language Switching)**

MenuStrip'teki ComboBox'tan dil seçildiğinde:
```vb
ToolStripComboBoxLanguage_SelectedIndexChanged()
  ↓
langManager.SetLanguage(selectedLang)  ' Dili değiştir
ApplyLanguage()                        ' UI'ı güncelle
SaveSettingsToIni()                    ' settings.ini'ye kaydet
```

### 3. **UI Güncellemesi (UI Update)**

ApplyLanguage() metodu tüm kontrollerin Text özelliklerini günceller:
```vb
MenuDosya.Text = langManager.GetText("MenuDosya", "Dosya")
MenuDosyaCikis.Text = langManager.GetText("MenuDosyaCikis", "Çıkış")
...
```

---

## 🌍 Yeni Dil Ekleme

### Adım 1: lang.ini'ye Yeni Section Ekle

```ini
[de]  ; Almanca
MenuDosya=Datei
MenuDosyaCikis=Beenden
MenuSekmeler=Registerkarten
MenuSekmelerYeni=Neuer Tab
...

[fr]  ; Fransızca
MenuDosya=Fichier
MenuDosyaCikis=Quitter
MenuSekmeler=Onglets
MenuSekmelerYeni=Nouvel onglet
...
```

### Adım 2: Uygulamayı Yeniden Başlat

- LanguageManager otomatik olarak lang.ini'yi okur
- Yeni diller ComboBox'ta otomatik görünür
- Hiçbir kod değişikliği gerekmez!

---

## 📋 Çeviri Anahtarları (Translation Keys)

### Menu - Dosya
- `MenuDosya` - Ana menü başlığı
- `MenuDosyaCikis` - Çıkış menü öğesi

### Menu - Sekmeler
- `MenuSekmeler` - Ana menü başlığı
- `MenuSekmelerYeni` - Yeni sekme
- `MenuSekmelerAdDegistir` - Sekme adını değiştir
- `MenuSekmelerSil` - Sekmeyi sil
- `MenuSekmelerYenile` - Sekmeyi yenile

### Menu - Sıralama
- `MenuSiralama` - Ana menü başlığı
- `MenuManuelSiralama` - Manuel sıralama

### Menu - Araçlar
- `MenuAraclar` - Ana menü başlığı
- `MenuToolsCmd` - Komut İstemi
- `MenuToolsPowershell` - PowerShell
- `MenuToolsTaskMgr` - Görev Yöneticisi
- `MenuToolsServices` - Hizmet Yöneticisi
- `MenuToolsShowDesktop` - Masaüstünü Göster
- `MenuToolsRestoreDesktop` - Masaüstünü Geri Yükle
- `MenuToolsControlPanel` - Denetim Masası
- `MenuToolsNetworkCenter` - Ağ ve Paylaşım Merkezi
- `MenuToolsDeviceManager` - Aygıt Yöneticisi
- `MenuToolsComputerName` - Bilgisayar Adını Göster
- `MenuToolsIPAddress` - IP Adreslerini Göster

### Menu - Ayarlar
- `MenuAyarlar` - Ayarlar menüsü

### Menu - Yardım
- `MenuYardim` - Ana menü başlığı
- `MenuYardimDokumanlar` - Yardım
- `MenuYardimDokumanIndir` - Döküman İndir
- `MenuYardimLisans` - Lisans Koşulları
- `MenuYardimBagis` - Bağış Yap
- `MenuYardimAnaSayfa` - Ana Sayfa
- `MenuYardimHakkinda` - Hakkında

### Context Menu - Item
- `MenuItemLaunch` - Başlat
- `MenuItemCopyMove` - Kopyala/Taşı
- `MenuItemRename` - Yeniden Adlandır
- `MenuItemChangeIcon` - İkonu Değiştir
- `MenuItemUpdatePath` - Yolu Güncelle
- `MenuItemOpenFolder` - Klasörde Göster
- `MenuItemDelete` - Sil
- `MenuItemProperties` - Özellikler

### Context Menu - Tab
- `MenuTabYeni` - Yeni Sekme
- `MenuTabAdDegistir` - Sekme Adını Değiştir
- `MenuTabSil` - Sekmeyi Sil

### Search Panel
- `lblSearch` - Ara: etiketi
- `btnSearch` - Ara butonu

### Messages
- `MsgError` - Hata
- `MsgWarning` - Uyarı
- `MsgInfo` - Bilgi
- `MsgConfirm` - Onay
- `MsgSuccess` - Başarılı

### Common Buttons
- `BtnOK` - Tamam
- `BtnCancel` - İptal
- `BtnYes` - Evet
- `BtnNo` - Hayır
- `BtnClose` - Kapat

### AboutForm
- `AboutTitle` - Hakkında form başlığı
- `AboutAppName` - Uygulama adı
- `AboutVersion` - Versiyon
- `AboutLicenseStatus` - Lisans durumu
- `AboutFreeUse` - Ücretsiz kullanım
- `AboutCopyright` - Telif hakkı
- `AboutWebSiteLabel` - Web site etiketi
- `AboutEmailLabel` - E-posta etiketi
- `AboutBtnAnaSayfa` - Ana Sayfa butonu
- `AboutBtnLisans` - Lisans Koşulları butonu
- `AboutBtnKapat` - Kapat butonu

### SettingsForm
- `SettingsTitle` - Ayarlar form başlığı
- `SettingsLaunchMode` - Başlatma modu
- `SettingsSingleClick` - Tek tık
- `SettingsDoubleClick` - Çift tık
- `SettingsViewMode` - Görünüm modu
- `SettingsIconText` - İkon + Metin
- `SettingsIconOnly` - Sadece İkon
- `SettingsAlwaysOnTop` - Her zaman üstte
- `SettingsBtnSave` - Kaydet butonu
- `SettingsBtnCancel` - İptal butonu

### ManualSortForm
- `ManualSortTitle` - Manuel sıralama başlığı
- `ManualSortBtnUp` - Yukarı butonu
- `ManualSortBtnDown` - Aşağı butonu
- `ManualSortBtnSave` - Kaydet butonu
- `ManualSortBtnCancel` - İptal butonu

### CopyMoveForm
- `CopyMoveTitle` - Kopyala/Taşı başlığı
- `CopyMoveSourceTab` - Kaynak sekme
- `CopyMoveTargetTab` - Hedef sekme
- `CopyMoveItemName` - Öğe adı
- `CopyMoveBtnCopy` - Kopyala butonu
- `CopyMoveBtnMove` - Taşı butonu
- `CopyMoveBtnCancel` - İptal butonu

---

## 💡 Önemli Notlar

### 1. **Encoding**
lang.ini dosyası **UTF-8** encoding ile kaydedilmelidir (Türkçe karakterler için).

### 2. **Fallback Mechanism**
Eğer bir key için çeviri bulunamazsa, key'in kendisi gösterilir:
```vb
langManager.GetText("UndefinedKey", "DefaultValue")
```

### 3. **Runtime Reload**
Dil dosyası değiştirildiğinde yeniden yüklemek için:
```vb
langManager.ReloadLanguages()
PopulateLanguageComboBox()
ApplyLanguage()
```

### 4. **Form Kapatma**
Program kapatılırken seçili dil otomatik olarak settings.ini'ye kaydedilir.

---

## 📖 Örnek Kullanım

### Kodda Çeviri Kullanımı:

```vb
' Basit kullanım
Dim text = langManager.GetText("MenuDosya")

' Fallback değer ile
Dim text = langManager.GetText("MenuDosya", "Dosya")

' MessageBox'larda
MessageBox.Show(
    langManager.GetText("MsgSuccess", "Başarılı"),
    langManager.GetText("MsgInfo", "Bilgi"),
    MessageBoxButtons.OK,
    MessageBoxIcon.Information
)
```

---

## ✅ Sonuç

WinLauncher v2.0 artık tam i18n (internationalization) desteklidir:
- ✅ Kullanıcı arayüzü runtime'da değiştirilebilir
- ✅ Yeni diller kolayca eklenebilir
- ✅ Dil tercihi kalıcı olarak kaydedilir
- ✅ Tüm menü ve kontroller çevrilebilir
- ✅ **TÜM MessageBox'lar çevrildi (48/48)**
- ✅ **TÜM Formlar çevrildi (AboutForm, SettingsForm, ManualSortForm, CopyMoveForm)**
- ✅ **Hata mesajları String.Format() ile dinamik içerik desteği**
- ✅ UTF-8 encoding ile tüm karakterler desteklenir
- ✅ Kullanım kılavuzu (TR/EN)

**Varsayılan Diller:**
- 🇹🇷 Türkçe (tr)
- 🇬🇧 İngilizce (en)

**Ek diller lang.ini dosyasına eklenebilir!**

---

## 📚 Kullanım Kılavuzu

Detaylı kullanım kılavuzları için:
- **Türkçe:** `assets/documentation/KullanımKlavuzu.md`
- **English:** `assets/documentation/UserManual.md`

Menü: **Yardım** → **Döküman İndir** ile aktif dile göre otomatik açılır.
