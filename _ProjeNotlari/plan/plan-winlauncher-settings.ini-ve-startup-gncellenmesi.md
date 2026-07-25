# 🎯 WinLauncher Settings.ini ve Startup Güncellenmesi

## Understanding
WinLauncher uygulamasında settings.ini ve startup işlemlerini güncellemek. Uygulama açılırken:
1. Settings.ini dosyasından ayarları yüklemek
2. WinLauncher.xml dosyasını kontrol etmek
3. Gerekirse varsayılan dosyaları oluşturmak

## Assumptions
- IniManager.vb dosyası zaten mevcut olup, burada fonksiyonlar yazılacak
- Form1.vb dosyasında Form_Load olayında startup mantığı yazılacak
- GetRootDir() fonksiyonu zaten mevcut veya yazılacak
- sAssetDir ve sDataDir değişkenleri zaten tanımlı

## Approach
1. IniManager.vb dosyasını gözden geçir ve mevcut fonksiyonları kontrol et
2. GetRootDir() fonksiyonunun güncellenmesi (development ve production modları için)
3. İki kontrol fonksiyonu yaz: isSettingsIniExist() ve isWinLauncherXmlExist()
4. CreateDefaultIni() fonksiyonu: Varsayılan settings.ini dosyası oluştur
5. LoadIni() fonksiyonu: Settings.ini dosyasını oku kaydedilen değişkenlere yükle
6. CreateXmlData() fonksiyonu: Boş WinLauncher.xml dosyası oluştur
7. Form1.vb'de Form_Load yapısını güncelle
8. Proje derle ve test et

## Key Files
- IniManager.vb - Ayar yönetimi fonksiyonları buraya yazılacak
- Form1.vb - Startup akışı burada uygulanacak

## Risks & Open Questions
- sAssetDir ve sDataDir değişkenleri tanımlanmış mı?
- CreateDefaultIni hata durumunda Application.Exit() ile çıkmalı mı veya hata mesajı göstermeli mi?
- WinLauncher.xml yapısı ne olmalı?

**Last Updated**: 2026-07-25 04:36:52

## 📝 Plan Steps
-  **IniManager.vb dosyasını oku ve mevcut yapısını anlama**
-  **GetRootDir() fonksiyonunu güncelle (dev/product untuk)**
-  **isSettingsIniExist() fonksiyonunu yaz**
-  **isWinLauncherXmlExist() fonksiyonunu yaz**
-  **CreateDefaultIni() fonksiyonunu yaz (hata-trap ile)**
-  **LoadIni() fonksiyonunu yaz**
-  **CreateXmlData() fonksiyonunu yaz**
-  **Form1.vb'de Form_Load'u güncelle**
-  **Proje derle ve hataları kontrol et**

