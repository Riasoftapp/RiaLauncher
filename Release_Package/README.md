# WinLauncher v2.0 - Windows Application Launcher

![WinLauncher](assets/icon/logo/winLuncher128x128.png)

**Modern ve kullanıcı dostu Windows uygulama başlatıcısı**

---

## 🎯 Özellikler

### ✨ Temel Özellikler
- 🚀 **Hızlı Başlatma** - Tek veya çift tıkla uygulamaları başlatın
- 📂 **Sekme Sistemi** - Uygulamalarınızı kategorilere ayırın
- 🎨 **Özel İkonlar** - Her öğe için özel ikon seçin
- 🔄 **Sürükle-Bırak** - Kolay ekleme için sürükle-bırak desteği
- 🔍 **Arama** - Uygulamalar arasında hızlı arama
- ⚡ **Hafif** - Düşük sistem kaynağı kullanımı
- 💾 **Taşınabilir** - USB bellekten çalışabilir

### 🌍 Çok Dil Desteği (v2.0)
- 🇹🇷 **Türkçe** - Tam destek
- 🇬🇧 **English** - Full support
- ✅ **48/48 MessageBox** çevrildi
- ✅ **Tüm formlar** çok dilli
- ✅ **Dinamik dil değiştirme** - Runtime'da dil değişimi
- 📝 **Kullanım kılavuzu** - TR/EN

### 🛠️ Araçlar Menüsü
- ⌨️ Komut İstemi
- 💻 PowerShell
- 📊 Görev Yöneticisi
- ⚙️ Hizmet Yöneticisi
- 🖥️ Masaüstü Göster/Geri Yükle
- 🎛️ Denetim Masası
- 🌐 Ağ ve Paylaşım Merkezi
- 🔌 Aygıt Yöneticisi
- 💻 Bilgisayar Adı Göster
- 🌍 IP Adresleri (Yerel/Uzak)

### 🎨 Gelişmiş Özellikler
- 📋 **Kopyala/Taşı** - Öğeleri sekmeler arasında taşıyın
- 🔄 **Manuel Sıralama** - İkonları istediğiniz gibi sıralayın
- ⚙️ **Ayarlar** - Başlatma ve görünüm modları
- 🎯 **Her Zaman Üstte** - Pencereyi üstte tutun
- 💾 **XML Veri Saklama** - Güvenli veri saklama

---

## 💾 Kurulum

### Sistem Gereksinimleri
- **OS:** Windows 7/8/10/11
- **.NET:** Framework 4.7.2+
- **Disk:** ~10 MB
- **RAM:** 512 MB

### Portable Versiyon
1. `WinLauncher_v2.0_Portable.zip` indirin
2. İstediğiniz klasöre çıkarın
3. `winLuncher.exe` çalıştırın

### Kurulum Sihirbazı
1. `WinLauncher_v2.0_Setup.exe` indirin
2. Kurulum sihirbazını takip edin
3. Masaüstü kısayolu oluşturun (opsiyonel)

---

## 📖 Kullanım

### Yeni Sekme Oluşturma
1. Menü → **Sekmeler** → **Yeni Sekme**
2. Sekme adı girin
3. Enter tuşuna basın

### Uygulama Ekleme
**Sürükle-Bırak:**
- Dosyayı/klasörü WinLauncher penceresine sürükleyin

**Bağlam Menüsü:**
1. Öğeye sağ tıklayın
2. İstediğiniz işlemi seçin

### Öğe İşlemleri
- ▶️ **Başlat** - Uygulamayı çalıştır
- ✏️ **Yeniden Adlandır** - Adını değiştir
- 🎨 **İkonu Değiştir** - Özel ikon seç
- 📋 **Kopyala/Taşı** - Başka sekmeye taşı
- 🗑️ **Sil** - Öğeyi kaldır
- ℹ️ **Özellikler** - Detay bilgi

### Manuel Sıralama
1. Menü → **Sıralama** → **Manuel Sıralama**
2. Öğe seç
3. ↑ Yukarı / ↓ Aşağı
4. Kaydet

### Dil Değiştirme
- Menü çubuğundan **Dil** seç
- **TR** (Türkçe) veya **EN** (English)
- Anında uygulanır

---

## 📚 Dokümantasyon

### Kullanım Kılavuzları
- 🇹🇷 [Türkçe Kullanım Kılavuzu](assets/documentation/KullanımKlavuzu.md)
- 🇬🇧 [English User Manual](assets/documentation/UserManual.md)

### Teknik Dokümantasyon
- 🌍 [Multi-Language System](MULTI_LANGUAGE_README.md)
- 🎨 [Icon Setup Guide](ICON_SETUP_README.md)

### Menüden Erişim
**Yardım** → **Döküman İndir** (Aktif dile göre otomatik açılır)

---

## 🎨 Ekran Görüntüleri

*(Ekran görüntüleri eklenecek)*

---

## 🔧 Geliştirme

### Teknoloji Stack
- **Dil:** Visual Basic .NET
- **Framework:** .NET Framework 4.7.2
- **IDE:** Visual Studio 2022
- **Veri:** XML
- **Ayarlar:** INI

### Proje Yapısı
```
winLuncher/
├── assets/
│   ├── icon/          # İkonlar
│   ├── documentation/ # Kullanım kılavuzları
│   └── lang.ini       # Dil dosyası
├── Form1.vb           # Ana form
├── AboutForm.vb       # Hakkında formu
├── SettingsForm.vb    # Ayarlar formu
├── ManualSortForm.vb  # Sıralama formu
├── CopyMoveForm.vb    # Kopyala/Taşı formu
├── LanguageManager.vb # Dil yöneticisi
├── IniManager.vb      # INI yöneticisi
└── launcherdata.xml   # Veri dosyası
```

### Build
```powershell
# Debug
msbuild winLuncher.vbproj /p:Configuration=Debug

# Release
msbuild winLuncher.vbproj /p:Configuration=Release
```

---

## 📝 Sürüm Notları

### v2.0.0 (2025-01-XX)
**Yeni Özellikler:**
- ✅ Tam çok dil desteği (TR/EN)
- ✅ 48 MessageBox çevirisi
- ✅ Tüm formlar çok dilli
- ✅ Araçlar menüsü (13 sistem aracı)
- ✅ IP adresi gösterici
- ✅ Bilgisayar adı gösterici
- ✅ Kullanım kılavuzları (TR/EN)
- ✅ Döküman indirme özelliği

**İyileştirmeler:**
- ✅ String.Format() ile dinamik mesajlar
- ✅ GetCurrentLanguage() metodu
- ✅ Otomatik dil bazlı döküman açma

### v1.3.0 (2025-01-XX)
- ✅ Çok dil sistemi başlangıcı
- ✅ LanguageManager eklendi
- ✅ Menü çevirileri

### v1.2.0 (2024-12-XX)
- ✅ Manuel sıralama
- ✅ Kopyala/Taşı özelliği
- ✅ İkon değiştirme

### v1.0.0 (2024-11-XX)
- ✅ İlk sürüm
- ✅ Temel özellikler

---

## 🤝 Katkıda Bulunma

Projeye katkıda bulunmak isterseniz:

1. **Fork** edin
2. **Branch** oluşturun (`git checkout -b feature/amazing-feature`)
3. **Commit** edin (`git commit -m 'Add amazing feature'`)
4. **Push** edin (`git push origin feature/amazing-feature`)
5. **Pull Request** açın

---

## 📄 Lisans

```
WinLauncher - Kişisel Kullanım Lisansı

Bu yazılım kişisel kullanım için ücretsizdir.

© 2024-2025 Hikmet Alp Alemdaroğlu
Tüm hakları saklıdır.

Bu yazılım "OLDUĞU GİBİ" sağlanmaktadır.
```

**Ticari kullanım için lisans gereklidir.**

---

## 💬 İletişim

- **GitHub:** [@hikmetalemdaroglu](https://github.com/hikmetalemdaroglu)
- **Email:** paylas24@gmail.com
- **Issues:** [GitHub Issues](https://github.com/hikmetalemdaroglu/999Projects/issues)

---

## 🙏 Teşekkürler

WinLauncher'ı kullandığınız için teşekkür ederiz!

### Bağış

Projeyi beğendiyseniz ve geliştirmeye destek olmak isterseniz:
- **GitHub Sponsors:** [github.com/sponsors/hikmetalemdaroglu](https://github.com/sponsors/hikmetalemdaroglu)

---

## ⭐ Yıldız

Projeyi beğendiyseniz lütfen ⭐ verin!

---

*Son Güncelleme: 2025*  
*Versiyon: 2.0.0*  
*© 2024-2025 Hikmet Alp Alemdaroğlu*
