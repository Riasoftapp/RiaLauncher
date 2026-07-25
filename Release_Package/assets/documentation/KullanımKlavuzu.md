# WinLauncher v2.0 - Kullanım Kılavuzu

![WinLauncher Logo](../icon/logo/winLuncher128x128.png)

---

## 📖 İçindekiler

1. [Giriş](#giriş)
2. [Kurulum](#kurulum)
3. [İlk Başlatma](#ilk-başlatma)
4. [Temel Özellikler](#temel-özellikler)
5. [Menü Sistemi](#menü-sistemi)
6. [Ayarlar](#ayarlar)
7. [İpuçları ve Püf Noktaları](#ipuçları-ve-püf-noktaları)
8. [Sık Sorulan Sorular](#sık-sorulan-sorular)
9. [Teknik Destek](#teknik-destek)

---

## 🎯 Giriş

### WinLauncher Nedir?

**WinLauncher**, Windows işletim sistemi için geliştirilmiş, modern ve kullanıcı dostu bir **uygulama başlatıcı**dır. Masaüstünüzü düzenli tutmanıza ve sık kullandığınız programlara, dosyalara ve klasörlere hızlıca erişmenize yardımcı olur.

### Neden WinLauncher?

- ✅ **Masaüstünüzü Temiz Tutun** - Tüm kısayolları tek bir yerde toplayın
- ✅ **Hızlı Erişim** - Programlarınızı tek tıkla veya çift tıkla başlatın
- ✅ **Kategorize Edin** - Uygulamalarınızı sekmeler halinde düzenleyin
- ✅ **Çok Dilli** - Türkçe ve İngilizce arayüz desteği
- ✅ **Taşınabilir** - Kurulum gerektirmez, USB bellekten çalışabilir
- ✅ **Ücretsiz** - Kişisel kullanım için tamamen ücretsiz
- ✅ **Yerleşik Araçlar** - Sistem araçlarına hızlı erişim

### Kimler İçin?

- 🎮 Oyuncular - Oyun kütüphanenizi düzenleyin
- 💼 Profesyoneller - İş uygulamalarınızı kategorize edin
- 🎨 Tasarımcılar - Tasarım araçlarınızı bir arada tutun
- 🖥️ Sistem Yöneticileri - Sistem araçlarına hızlı erişim
- 👥 Herkes - Bilgisayarınızı daha verimli kullanın

---

## 💾 Kurulum

### Sistem Gereksinimleri

- **İşletim Sistemi:** Windows 7 / 8 / 10 / 11
- **Framework:** .NET Framework 4.7.2 veya üzeri
- **Disk Alanı:** ~10 MB
- **RAM:** Minimum 512 MB

### Kurulum Adımları

#### Yöntem 1: Portable Versiyon (Önerilen)

1. `WinLauncher_v2.0_Portable.zip` dosyasını indirin
2. ZIP dosyasını istediğiniz bir klasöre çıkarın
3. `winLuncher.exe` dosyasını çalıştırın
4. Kurulum tamamlandı! 🎉

#### Yöntem 2: Setup ile Kurulum

1. `WinLauncher_v2.0_Setup.exe` dosyasını indirin
2. Setup dosyasını çalıştırın
3. Kurulum sihirbazını takip edin
4. Masaüstü kısayolu oluşturun (isteğe bağlı)
5. Kurulum tamamlandı! 🎉

### İlk Çalıştırma

1. **WinLauncher**'ı ilk kez çalıştırdığınızda:
   - `assets` klasörü otomatik oluşturulur
   - `launcherdata.xml` varsayılan verilerle doldurulur
   - `settings.ini` varsayılan ayarlarla oluşturulur
   - `lang.ini` dil dosyası oluşturulur

2. **Ana pencere açılır:**
   - Varsayılan sekmeler görünür
   - Türkçe dil aktif olur
   - Örnek öğeler yüklenmiş olur

---

## 🚀 İlk Başlatma

### Ana Ekran Tanıtımı

```
┌─────────────────────────────────────────────────────────┐
│  Dosya  Sekmeler  Sıralama  Araçlar  Ayarlar  Yardım   │  ← Menü Çubuğu
├─────────────────────────────────────────────────────────┤
│  [Dil: TR ▼]                                            │  ← Dil Seçici
├─────────────────────────────────────────────────────────┤
│  Ara: [_____________] [Ara]                             │  ← Arama Paneli
├─────────────────────────────────────────────────────────┤
│  ┌──────┬──────┬──────┬──────┐                          │
│  │ Oyun │ İş   │ Araç │ Diğer│                          │  ← Sekmeler
│  └──────┴──────┴──────┴──────┘                          │
│  ┌──────┐  ┌──────┐  ┌──────┐                          │
│  │ 🎮   │  │ 💼   │  │ 🔧   │                          │
│  │ APEX │  │ Word │  │Chrome│                          │  ← Öğeler
│  └──────┘  └──────┘  └──────┘                          │
└─────────────────────────────────────────────────────────┘
```

### İlk Adımlar

#### 1. Yeni Sekme Oluşturma

1. **Menü Çubuğu** → **Sekmeler** → **Yeni Sekme**
2. Sekme adını girin (örn: "Oyunlar", "İş", "Tasarım")
3. **Enter** tuşuna basın
4. Yeni sekme oluşturuldu! 🎉

#### 2. Uygulama Ekleme

**Yöntem 1: Sürükle-Bırak**
- Masaüstünden veya dosya gezgininden bir dosyayı WinLauncher penceresine sürükleyin
- Otomatik olarak seçili sekmeye eklenir

**Yöntem 2: Manuel Ekleme**
1. Bir sekmeye sağ tıklayın
2. **"Yeni Öğe Ekle"** seçin (yakında eklenecek)
3. Dosya veya klasör seçin
4. İkon otomatik algılanır

#### 3. Uygulama Başlatma

- **Tek Tık:** Varsayılan modda tek tıklama ile başlatır
- **Çift Tık:** Ayarlardan çift tıklama moduna geçebilirsiniz

---

## ⚙️ Temel Özellikler

### 1. Sekme Yönetimi

#### Sekme Oluşturma
- Menü: **Sekmeler** → **Yeni Sekme**
- Kısayol: `Ctrl + T` (yakında)

#### Sekme Silme
- Sekmeye **sağ tıklayın** → **Sekmeyi Sil**
- Menü: **Sekmeler** → **Sekmeyi Sil**

#### Sekme Yenileme
- Sekmeye **sağ tıklayın** → **Sekmeyi Yenile**
- Menü: **Sekmeler** → **Sekmeyi Yenile**

#### Sekme Adını Değiştirme
- Sekmeye **sağ tıklayın** → **Sekme Adını Değiştir**
- Yeni adı girin → **Enter**

### 2. Öğe Yönetimi

#### Öğe Bağlam Menüsü (Sağ Tık)

Bir öğeye sağ tıkladığınızda şu seçenekler görünür:

```
┌────────────────────────┐
│ ▶ Başlat              │  ← Programı çalıştır
│ 📋 Kopyala/Taşı...    │  ← Başka sekmeye taşı
│ ✏️ Yeniden Adlandır   │  ← Adını değiştir
│ 🎨 İkonu Değiştir     │  ← Özel ikon seç
│ 🔄 Yolu Güncelle      │  ← Dosya yolunu güncelle
│ 📁 Klasörde Göster    │  ← Windows Explorer'da aç
│ 🗑️ Sil                │  ← Öğeyi sil
│ ℹ️ Özellikler         │  ← Detay bilgiler
└────────────────────────┘
```

#### Yeniden Adlandırma
1. Öğeye **sağ tık** → **Yeniden Adlandır**
2. Yeni adı girin
3. **Enter** tuşuna basın

#### İkon Değiştirme
1. Öğeye **sağ tık** → **İkonu Değiştir**
2. `.ico`, `.png`, `.jpg` dosyası seçin
3. İkon güncellenir

#### Yolu Güncelle
1. Öğeye **sağ tık** → **Yolu Güncelle**
2. Yeni dosya/klasör seçin
3. Yol güncellenir

#### Kopyala/Taşı
1. Öğeye **sağ tık** → **Kopyala/Taşı**
2. Hedef sekmeyi seçin
3. **Kopyala** veya **Taşı** düğmesine basın

### 3. Manuel Sıralama

Sekme içindeki öğeleri istediğiniz sıraya koyun:

1. Menü: **Sıralama** → **Manuel Sıralama**
2. Listeden bir öğe seçin
3. **↑ Yukarı** veya **↓ Aşağı** butonlarını kullanın
4. **Kaydet** düğmesine basın

### 4. Arama Özelliği

Tüm sekmeler arasında arama yapın:

1. Üst paneldeki **"Ara:"** kutusuna yazmaya başlayın
2. Otomatik olarak filtreler (yakında)
3. Arama sonuçlarına tıklayın

---

## 📋 Menü Sistemi

### 1. Dosya Menüsü

```
Dosya
 └─ Çıkış (Alt + F4)
```

- **Çıkış:** Programı kapatır

### 2. Sekmeler Menüsü

```
Sekmeler
 ├─ Yeni Sekme
 ├─ Sekme Adını Değiştir
 ├─ Sekmeyi Sil
 └─ Sekmeyi Yenile
```

- **Yeni Sekme:** Yeni kategori oluşturur
- **Sekme Adını Değiştir:** Aktif sekmenin adını değiştirir
- **Sekmeyi Sil:** Aktif sekmeyi siler (en az 1 sekme kalmalı)
- **Sekmeyi Yenile:** Sekmeyi XML'den yeniden yükler

### 3. Sıralama Menüsü

```
Sıralama
 └─ Manuel Sıralama
```

- **Manuel Sıralama:** Öğeleri elle sıralama penceresi açar

### 4. Araçlar Menüsü

```
Araçlar
 ├─ Komut İstemi
 ├─ PowerShell
 ├─ Görev Yöneticisi
 ├─ Hizmet Yöneticisi
 ├─ Masaüstünü Göster
 ├─ Masaüstünü Geri Yükle
 ├─ Denetim Masası
 ├─ Ağ ve Paylaşım Merkezi
 ├─ Aygıt Yöneticisi
 ├─ Bilgisayar Adını Göster
 └─ IP Adreslerini Göster
```

#### Sistem Araçları:

- **Komut İstemi:** CMD açar
- **PowerShell:** Windows PowerShell açar
- **Görev Yöneticisi:** Task Manager açar
- **Hizmet Yöneticisi:** Services.msc açar
- **Denetim Masası:** Control Panel açar
- **Ağ ve Paylaşım Merkezi:** Network Center açar
- **Aygıt Yöneticisi:** Device Manager açar

#### Masaüstü Araçları:

- **Masaüstünü Göster:** Tüm pencereleri minimize eder
- **Masaüstünü Geri Yükle:** Pencereleri geri getirir

#### Bilgi Araçları:

- **Bilgisayar Adını Göster:** PC adını gösterir, panoya kopyalar
- **IP Adreslerini Göster:** Yerel ve uzak IP adreslerini gösterir

### 5. Ayarlar Menüsü

```
Ayarlar
 └─ Ayarlar...
```

Ayarlar penceresini açar. Detaylar için [Ayarlar](#ayarlar) bölümüne bakın.

### 6. Yardım Menüsü

```
Yardım
 ├─ Yardım
 ├─ Döküman İndir (PDF)
 ├─ Lisans Koşulları
 ├─ Bağış Yap
 ├─ Ana Sayfa
 └─ Hakkında
```

- **Yardım:** Yardım sayfası (yakında)
- **Döküman İndir:** Bu kılavuzun PDF versiyonunu indirir
- **Lisans Koşulları:** Lisans metnini gösterir
- **Bağış Yap:** GitHub Sponsors sayfasını açar
- **Ana Sayfa:** GitHub repository sayfasını açar
- **Hakkında:** Versiyon ve telif hakkı bilgilerini gösterir

---

## ⚙️ Ayarlar

Menü: **Ayarlar** → **Ayarlar...**

### Başlatma Modu

```
○ Tek Tık
○ Çift Tık
```

- **Tek Tık:** Öğelere bir kez tıklayarak başlatır (varsayılan)
- **Çift Tık:** Öğelere çift tıklayarak başlatır

### Görünüm Modu

```
○ İkon + Metin
○ Sadece İkon
```

- **İkon + Metin:** İkon altında metin gösterir (varsayılan)
- **Sadece İkon:** Sadece ikonu gösterir (daha kompakt)

### Diğer Ayarlar

```
☑ Her Zaman Üstte
```

- **Her Zaman Üstte:** Pencereyi diğer pencerelerin üstünde tutar

### Dil Seçimi

Menü çubuğundaki **Dil** açılır menüsünden:

- **TR** - Türkçe
- **EN** - English

Seçim anında uygulanır ve `settings.ini`'ye kaydedilir.

---

## 💡 İpuçları ve Püf Noktaları

### 1. Hızlı Organizasyon

**Sekmelerinizi Kategorize Edin:**
```
📁 Oyunlar      - Steam, Epic Games, oyunlar
📁 İş           - Office, Mail, iş araçları
📁 Tasarım      - Photoshop, Illustrator, Figma
📁 Geliştirme   - VS Code, Git, terminaller
📁 Multimedya   - VLC, Spotify, Netflix
📁 Araçlar      - WinRAR, Notepad++, araçlar
```

### 2. İkon İpuçları

- `.ico` dosyaları en iyi sonucu verir
- `128x128` veya `256x256` boyutlar idealdir
- İkonları `assets/icon/` klasöründe saklayın
- PNG/JPG dosyaları otomatik olarak ICO'ya dönüştürülür

### 3. Performans İpuçları

- **Sekme Başına 20-30 Öğe** idealdir
- Çok fazla sekme yerine kategorize edin
- Kullanılmayan öğeleri silin
- Düzenli olarak sekmeleri yenileyin

### 4. Yedekleme

**Önemli Dosyalar:**
```
📁 winLuncher/
 ├─ launcherdata.xml   ← VERİLERİNİZ
 ├─ settings.ini       ← AYARLARINIZ
 └─ assets/
     └─ icon/          ← ÖZEL İKONLARINIZ
```

Bu dosyaları düzenli yedekleyin!

### 5. Taşınabilir Kullanım

USB Bellekte Kullanım:
1. Tüm `winLuncher` klasörünü USB'ye kopyalayın
2. Her bilgisayarda aynı ayarlarınız olacak
3. Portable - kurulum gerektirmez!

---

## ❓ Sık Sorulan Sorular

### Genel Sorular

**S: WinLauncher ücretsiz mi?**
> A: Evet! Kişisel kullanım için tamamen ücretsizdir.

**S: Kurulum gerekiyor mu?**
> A: Hayır. Portable versiyonu herhangi bir kurulum gerektirmez.

**S: .NET Framework gerekli mi?**
> A: Evet, .NET Framework 4.7.2 veya üzeri gereklidir.

**S: Hangi dosya formatları desteklenir?**
> A: .exe, .lnk, dosyalar ve klasörler desteklenir.

**S: İkon formatları?**
> A: .ico, .png, .jpg formatları desteklenir.

### Teknik Sorular

**S: Verilerim nerede saklanıyor?**
> A: `launcherdata.xml` dosyasında şifreli değil, XML formatında.

**S: Ayarlarım nerede?**
> A: `settings.ini` dosyasında INI formatında.

**S: Dil dosyası nerede?**
> A: `assets/lang.ini` dosyasında.

**S: Yeni dil ekleyebilir miyim?**
> A: Evet! `lang.ini` dosyasına yeni [dilkodu] section'ı ekleyin.

### Sorun Giderme

**S: Program açılmıyor?**
> A: .NET Framework 4.7.2 kurulu olduğundan emin olun.

**S: İkonlar görünmüyor?**
> A: `assets/icon/` klasörünün var olduğundan emin olun.

**S: XML yüklenemedi hatası?**
> A: `launcherdata.xml` dosyasını silin, otomatik oluşacaktır.

**S: Ayarlar kaydedilmiyor?**
> A: Klasör yazma iznine sahip olduğundan emin olun.

**S: Uygulama başlatılamıyor?**
> A: Dosya yolunun doğru olduğunu kontrol edin.

---

## 🛠️ Teknik Destek

### İletişim

- **GitHub Issues:** [github.com/hikmetalemdaroglu/999Projects/issues](https://github.com/hikmetalemdaroglu/999Projects/issues)
- **E-posta:** paylas24@gmail.com
- **GitHub:** [@hikmetalemdaroglu](https://github.com/hikmetalemdaroglu)

### Bağış

WinLauncher'ı beğendiyseniz ve geliştirmeye destek olmak isterseniz:

- **GitHub Sponsors:** [github.com/sponsors/hikmetalemdaroglu](https://github.com/sponsors/hikmetalemdaroglu)

### Katkıda Bulunma

Projeye katkıda bulunmak isterseniz:

1. Repository'yi fork edin
2. Yeni bir branch oluşturun
3. Değişikliklerinizi yapın
4. Pull request gönderin

---

## 📄 Lisans

```
WinLauncher - Kişisel Kullanım Lisansı

Bu yazılım kişisel kullanım için ücretsizdir.

© 2024-2025 Hikmet Alp Alemdaroğlu

Tüm hakları saklıdır.

Bu yazılım "OLDUĞU GİBİ" sağlanmaktadır.
```

---

## 📝 Sürüm Notları

### v2.0.0 (2025)
- ✅ Tam çok dil desteği (TR/EN)
- ✅ Tüm MessageBox'lar çevrildi
- ✅ Araçlar menüsü (13 sistem aracı)
- ✅ Manuel sıralama özelliği
- ✅ Kopyala/Taşı özelliği
- ✅ IP adresi gösterici
- ✅ Bilgisayar adı gösterici
- ✅ Gelişmiş ayarlar formu
- ✅ Hakkında formu
- ✅ Kullanım kılavuzu (TR/EN)

### v1.3.0 (2025)
- ✅ Çok dil desteği başlangıç
- ✅ LanguageManager eklendi
- ✅ Menü çevirileri

### v1.2.0 (2024)
- ✅ Manuel sıralama
- ✅ Kopyala/Taşı özelliği
- ✅ İkon değiştirme

### v1.0.0 (2024)
- ✅ İlk sürüm
- ✅ Temel özellikler

---

## 🙏 Teşekkürler

WinLauncher'ı kullandığınız için teşekkür ederiz!

**Keyifli kullanımlar! 🚀**

---

*Son Güncelleme: 2025*
*Versiyon: 2.0.0*
*© 2024-2025 Hikmet Alp Alemdaroğlu*
