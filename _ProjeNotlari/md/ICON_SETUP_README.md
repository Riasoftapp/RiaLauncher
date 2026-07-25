# Icon Kurulum Talimatları

## ✅ Yapılanlar:
1. Icon klasörü uygulama çalıştırıldığında otomatik olarak `bin\Debug\icon` ve `bin\Release\icon` klasörlerine kopyalanacak
2. Form1 ve SettingsForm'a runtime'da icon yükleme eklendi
3. Logo dosyaları `icon\logo\` klasöründe hazır

## 🔧 Manuel Yapılması Gerekenler:

### Application Icon Ekleme (EXE Icon):

**Yöntem 1 - Visual Studio'dan:**
1. Solution Explorer'da `winLuncher` projesine sağ tıklayın
2. **Properties** (Özellikler) seçin
3. **Application** sekmesine gidin
4. **Icon and manifest** bölümünde **Icon:** dropdown'ını açın
5. **Browse...** (Gözat) tıklayın
6. `icon\logo\winLuncher48x48.ico` dosyasını seçin
7. **Ctrl+S** ile kaydedin

**Yöntem 2 - Proje Dosyasını Manuel Düzenleme:**
1. Visual Studio'yu kapatın
2. `winLuncher.vbproj` dosyasını text editör ile açın
3. `<PropertyGroup>` etiketlerinden birinin içine şunu ekleyin:
   ```xml
   <ApplicationIcon>icon\logo\winLuncher48x48.ico</ApplicationIcon>
   ```
4. Kaydedin ve Visual Studio'yu tekrar açın

### Post-Build Event Ekleme (Opsiyonel - Icon klasörünü her build'de otomatik kopyala):

1. Solution Explorer'da `winLuncher` projesine sağ tıklayın
2. **Properties** seçin
3. **Compile** sekmesine gidin
4. **Build Events** butonuna tıklayın
5. **Post-build event command line** alanına şunu yazın:
   ```
   xcopy "$(ProjectDir)icon" "$(TargetDir)icon" /E /I /Y
   ```
6. Kaydedin

## 📁 Icon Dosyaları:

- **winLuncher16x16.ico** - Küçük icon (system tray vb.)
- **winLuncher24x24.ico** - Orta icon
- **winLuncher32x32.ico** - Standart icon (Form icon)
- **winLuncher48x48.ico** - Büyük icon (Application/EXE icon) ⭐ **ÖNERİLEN**
- **winLuncher64x64.ico** - Extra büyük icon
- **winLuncher256x256.ico** - HD icon

## 🎨 Icon Kullanım Alanları:

- **Form1**: `winLuncher48x48.ico` - Ana form
- **SettingsForm**: `winLuncher32x32.ico` - Ayarlar formu
- **EXE**: `winLuncher48x48.ico` - Uygulama çalıştırılabilir dosyası

## ✅ Test:

1. Uygulamayı derleyin (F5)
2. `bin\Debug\icon` klasörünün oluştuğunu kontrol edin
3. Form başlıklarında icon görünmeli
4. EXE dosyasının icon'u değişmeli (Application Icon eklediyseniz)
