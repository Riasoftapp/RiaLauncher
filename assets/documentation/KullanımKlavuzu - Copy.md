# WinLauncher v2.0 - Kullanim Kilavuzu

---

## Icindekiler

1. [Giris](#giris)
2. [Kurulum](#kurulum)
3. [Ana Ekran](#ana-ekran)
4. [Sekme Yonetimi](#sekme-yonetimi)
5. [Oge Yonetimi](#oge-yonetimi)
6. [Manuel Siralama Ekrani](#manuel-siralama-ekrani)
7. [Kopyala-Tasi Ekrani](#kopyala-tasi-ekrani)
8. [Ayarlar Ekrani](#ayarlar-ekrani)
9. [Araclar Menusu](#araclar-menusu)
10. [Yardim Menusu](#yardim-menusu)
11. [Hakkinda Ekrani](#hakkinda-ekrani)
12. [Lisans Ekrani](#lisans-ekrani)
13. [Dil Destegi](#dil-destegi)
14. [Ipuclari ve Yedekleme](#ipuclari-ve-yedekleme)
15. [SSS](#sss)
16. [Teknik Destek](#teknik-destek)

---

## Giris

**WinLauncher**, Windows icin gelistirilmis modern ve kullanici dostu bir **uygulama baslatici**dir.
Masaustunuzu duzenli tutmaniza ve sik kullandiginiz programlara, dosyalara ve klasorlere hizlica erismenize yardimci olur.

### Ozellikler

| Ozellik | Aciklama |
|---|---|
| Sekme Yonetimi | Uygulamalarinizi kategorilere ayirin |
| Tek / Cift Tik | Baslatma modunu secin |
| Arama | Ogelerinizde hizla arama yapin |
| Cok Dilli | Turkce ve Ingilizce arayuz |
| Sistem Araclari | 13 adet yerlesik sistem araci |
| Tasinilebilir | Kurulum gerektirmez, USB den calisir |
| Manuel Siralama | Oge sirasini kendiniz belirleyin |
| Kopyala / Tasi | Ogeleri sekmeler arasinda tasiyin |

---

## Kurulum

### Sistem Gereksinimleri

| Bilesen | Minimum |
|---|---|
| Isletim Sistemi | Windows 7 / 8 / 10 / 11 |
| .NET Framework | 4.7.2 veya uzeri |
| Disk Alani | ~10 MB |
| RAM | 512 MB |

### Kurulum Adimlari (Portable)

1. WinLauncher_v2.0_Portable.zip dosyasini indirin
2. ZIP dosyasini istediginiz bir klasore cikarin
3. winLuncher.exe dosyasini calistirin

> Kurulum gerekmez. Program ilk calismada tum gerekli dosyalari otomatik olusturur.

### Ilk Calistirmada Olusturulan Dosyalar

```
winLuncher/
  winLuncher.exe
  Data/
    WinLauncher.xml       <- Oge ve sekme verileri
  settings.ini            <- Uygulama ayarlari
  assets/
    lang.ini              <- Dil dosyasi
    icon/                 <- Ikon kutuphanesi
    documentation/        <- Kilavuzlar
```

---

## Ana Ekran

Ana ekran, WinLauncher in merkezi yonetim alanidir.

![Ana Ekran](screenshots/ana_ekran.png)

### Baslik Cubugu

![Baslik Cubugu](screenshots/baslik_cubugu.png)

Baslik cubugu en ustte yer alir:

| Oge | Aciklama |
|---|---|
| WinLauncher Logosu | Sol ustteki uygulama ikonu |
| Uygulama Basligi | "WinLauncher - Custom Windows Launcher" |
| Dil Menusu (TR / EN) | Anlik dil degistirme |
| Minimize (-) | Pencereyi kuculttur |
| Maximize (kare) | Pencereyi buyutur |
| Kapat (X) | Programi kapatir |

> Ipucu: Baslik cubugundan tutarak pencereyi istediginiz yere surukleyebilirsiniz.

### Menu Cubugu

![Menu Cubugu](screenshots/menu_cubugu.png)

```
Dosya | Sekmeler | Siralama | Araclar | Ayarlar | Yardim
```

### Arac Paneli

![Arac Paneli](screenshots/arac_paneli.png)

| Oge | Aciklama |
|---|---|
| Yeni Sekme butonu | Yeni sekme olusturur |
| Sekmeyi Sil butonu | Secili sekmeyi siler |
| Ara metin kutusu | Ogelerde arama yapar |
| Arama butonu | Aramayi baslatir |

### Sekme Alani

![Sekme Alani](screenshots/sekme_alani.png)

Her sekme bir kategoriyi temsil eder (ornegin: Gelistirme, Oyunlar, Is).

### Oge Alani (Ikon Paneli)

![Oge Alani](screenshots/oge_alani.png)

Her oge sunlardan olusur:
- Ikon (ustte, 48x48 px)
- Ad (altta, metin)

---

## Sekme Yonetimi

### Yeni Sekme Olusturma

Yontem 1 - Menudem:
1. Sekmeler menusu -> Yeni Sekme
2. Acilan pencereye sekme adini yazin (orn: "Oyunlar")
3. Tamam a tiklayin

Yontem 2 - Arac Panelinden:
1. Arac panelindeki Yeni Sekme butonuna tiklayin
2. Sekme adini yazin -> Tamam

Yontem 3 - Sag Tik:
1. Sekme cubugunma sag tiklayin -> Yeni Sekme

![Yeni Sekme Dialog](screenshots/yeni_sekme_dialog.png)

### Sekme Adini Degistirme

1. Sekmeye sag tiklayin -> Sekme Adini Degistir
2. Yeni adi yazin -> Tamam

![Sekme Sag Tik](screenshots/sekme_sag_tik.png)

### Sekme Silme

1. Sekmeye sag tiklayin -> Sekmeyi Sil
2. Ya da arac panelindeki Sekmeyi Sil butonuna tiklayin
3. Onay penceresinde Evet e tiklayin

> UYARI: En az bir sekme her zaman kalmalidir. Son sekme silinemez.

### Sekme Yenileme

Sekme icerigini XML dosyasindan yeniden yukler:
- Sekmeye sag tiklayin -> Sekmeyi Yenile
- Ya da menuden Sekmeler -> Sekmeyi Yenile

### Sekmeler Arasi Gecis

| Yontem | Aciklama |
|---|---|
| Tiklama | Sekme adina tiklayin |
| Tab tusu | Sagdaki sekmeye gecer |
| Shift + < | Soldaki sekmeye gecer |
| Shift + > | Sagdaki sekmeye gecer |

---

## Oge Yonetimi

### Oge Ekleme (Surukle-Birak)

![Surukle Birak](screenshots/surukle_birak.png)

1. Dosya Gezgini veya Masaustunden bir dosya / klasor / kisayol secin
2. WinLauncher daki ilgili sekmeye surukleyip birakin
3. Oge otomatik eklenir, ikon otomatik algilanir

> Ipucu: .lnk (kisayol) dosyalari suruklendiginde WinLauncher hedef uygulamayi otomatik algilar.

### Oge Baslatma

| Mod | Nasil? | Ayar |
|---|---|---|
| Tek Tik | Ikona veya ada bir kez tiklayin | Ayarlar -> Tek Tik |
| Cift Tik | Ikona veya ada cift tiklayin | Ayarlar -> Cift Tik (varsayilan) |

### Oge Sag Tik Menusu

![Oge Sag Tik Menu](screenshots/oge_sag_tik_menu.png)

Herhangi bir ogeye sag tikladiginizdaki menu:

```
  Baslat
  ---
  Kopyala/Tasi...
  ---
  Yeniden Adlandir
  Ikonu Degistir
  Yolu Guncelle
  Klasorde Goster
  ---
  Sil
  ---
  Ozellikler
```

#### Baslat
Secili uygulamayi / dosyayi / klasoru baslatir veya acar.

#### Kopyala / Tasi
Ogeyi baska bir sekmeye kopyalar veya tasir. Bkz. Kopyala/Tasi Ekrani bolumu.

#### Yeniden Adlandir

![Yeniden Adlandir](screenshots/yeniden_adlandir.png)

1. Yeniden Adlandir a tiklayin
2. Yeni adi girin -> Tamam

#### Ikonu Degistir

![Ikon Degistir](screenshots/ikon_degistir.png)

1. Ikonu Degistir e tiklayin
2. Desteklenen formatlarda dosya secin: .ico, .png, .jpg, .bmp
3. Ikon aninda guncellenir

> Ipucu: assets/icon/ klasorudeki hazir ikonlari kullanabilirsiniz.

#### Yolu Guncelle
Uygulama tasindiysa dosya yolunu guncelleyin:
1. Yolu Guncelle ye tiklayin
2. Yeni dosya konumunu secin
3. Yol ve ikon otomatik yenilenir

#### Klasorde Goster
Ogunun bulundugu klasoru Windows Gezgininde acar ve dosyayi secili gosterir.

#### Sil
Ogeyi WinLauncher listesinden kaldirir (diskten silinmez):
1. Sil e tiklayin -> Onay penceresinde Evet

#### Ozellikler

![Ozellikler](screenshots/ozellikler.png)

```
Isim     : Visual Studio Code
Yol      : C:\Program Files\VSCode\Code.exe
Mevcut   : Evet (Dosya)
Ozel Ikon: Evet
```

### Arama

![Arama](screenshots/arama.png)

1. Arac panelindeki Ara kutusuna yazmayi baslayin
2. Ara butonuna tiklayin veya Enter a basin
3. Ogeler ada gore filtrelenir
4. Kutuyu temizleyip tekrar arattiginizda tum ogeler geri doner

---

## Manuel Siralama Ekrani

Sekmedeki ogelerin sirasini elle belirleyin.

Acmak icin: Menu -> Siralama -> Manuel Siralama...

![Manuel Siralama 1](screenshots/manuel_siralama1.png)

### Ekran Bilesenleri

	![Manuel Siralama 2](screenshots/manuel_siralama2.png)

| Bilesen | Aciklama |
|---|---|
| Oge Listesi | Sekmedeki tum ogeler sirayla listelenir |
| Checkbox | Tasinacak ogeyi secmek icin isaretleyin |
| Yukari | Secili ogeyi bir ust siraya tasir |
| Asagi | Secili ogeyi bir alt siraya tasir |
| Kaydet ve Cik | Yeni siralami kaydeder, pencereyi kapatir |
| Iptal | Degisiklikleri iptal eder, pencereyi kapatir |

### Liste Sutunlari

| Sutun | Aciklama |
|---|---|
| Sira | Mevcut sira numarasi |
| Simge | Ogunun ikonu |
| Simge Adi | Ogunun adi |
| Program / Yol | Dosya veya uygulamanin tam yolu |

### Kullanim Adimlari

1. Siralama -> Manuel Siralama... yi acin
2. Tasmak istediginiz ogunun checkbox ini isaretleyin
3. Yukari veya Asagi ile istediginiz konuma tasiyin
4. Kaydet ve Cik a tiklayin

![Manuel Siralama Secili](screenshots/manuel_siralama_secili.png)

> NOT: Iptal e basarsaniz yaptipiniz degisiklikler kaydedilmez.

---

## Kopyala-Tasi Ekrani

Ogeleri sekmeler arasinda kopyalayin veya tasiyin.

Acmak icin: Ogeye sag tiklayin -> Kopyala/Tasi...

![Kopyala Tasi](screenshots/kopyala_tasi.png)

### Ekran Bilesenleri

| Bilesen | Aciklama |
|---|---|
| Baslik | "Kopyala/Tasi - [Oge Adi]" |
| Kaynak Bilgisi | "Kaynak Sekme: X / Oge Adi: Y" |
| Hedef Sekme | Acilir listeden hedef sekme secin |
| Kopyala | Ogeyi kopyalar (kaynakta kalir) |
| Tasi | Ogeyi tasir (kaynaktan silinir) |
| Iptal | Islemi iptal eder |

### Kopyalama

1. Ogeye sag tiklayin -> Kopyala/Tasi...
2. Hedef sekmeyi secin
3. Kopyala ya tiklayin -> Oge her iki sekmede de gorunur

### Tasima

1. Ogeye sag tiklayin -> Kopyala/Tasi...
2. Hedef sekmeyi secin
3. Tasi ya tiklayin -> Oge kaynak sekmeden kaldirilir, hedefe eklenir

> NOT: Ogeyi ayni sekmeye tasimaya calisirsaniz uyari mesaji alirsiniz.

---

## Ayarlar Ekrani

Acmak icin: Menu -> Ayarlar

![Ayarlar](screenshots/ayarlar.png)

### Baslatma Modu

![Ayarlar Baslama Modu](screenshots/ayarlar_baslama_modu.png)

```
Baslatma Modu:
  o Tek Tik
  * Cift Tik   <- Varsayilan
```

| Secenek | Davranis |
|---|---|
| Tek Tik | Bir kez tiklamak uygulamayi baslatir |
| Cift Tik | Cift tiklamak uygulamayi baslatir |

### Gorunum Modu

```
Gorunum Modu:
  * Ikon + Metin   <- Varsayilan
  o Sadece Ikon
```

| Secenek | Davranis |
|---|---|
| Ikon + Metin | Ikonun altinda oge adi gorunur |
| Sadece Ikon | Yalnizca ikon gorunur, daha kompakt |

### Her Zaman Ustte

```
[x] Her Zaman Ustte
```

Isaretlendiginde WinLauncher diger tum pencerelerin uzerinde gorunur.

### Kaydetme

Kaydet e tiklayin. Ayarlar settings.ini dosyasina kaydedilir.

![Ayarlar Kaydedildi](screenshots/ayarlar_kaydedildi.png)

---

## Araclar Menusu

Acmak icin: Menu -> Araclar

![Araclar Menu](screenshots/araclar_menu.png)

### Sistem Araclari

| Arac | Aciklama |
|---|---|
| Komut Istemi | cmd.exe acar |
| PowerShell | powershell.exe acar |
| Gorev Yoneticisi | taskmgr.exe acar |
| Hizmet Yoneticisi | services.msc acar |
| Denetim Masasi | control.exe acar |
| Ag ve Paylasim Merkezi | Windows Ag Merkezi acar |
| Aygit Yoneticisi | devmgmt.msc acar |

### Masaustu Araclari

| Arac | Aciklama |
|---|---|
| Masaustunu Goster | Tum pencereleri minimize eder |
| Masaustunu Geri Yukle | Explorer yeniden baslatilir |

### Bilgi Araclari

| Arac | Aciklama |
|---|---|
| Bilgisayar Adini Goster | PC adini gosterir, panoya kopyalar |
| IP Adreslerini Goster | Yerel ve uzak IP adreslerini gosterir, panoya kopyalar |

![IP Adresi](screenshots/ip_adresi.png)

---

## Yardim Menusu

Acmak icin: Menu -> Yardim

![Yardim Menu](screenshots/yardim_menu.png)

| Alt Menu | Aciklama |
|---|---|
| Yardim | Yardim bilgisi |
| Dokuman Indir | Aktif dile gore kilavuzu acar |
| Lisans Kosullari | Lisans detay ekranini acar |
| Bagis Yap | GitHub Sponsors sayfasina yonlendirir |
| Ana Sayfa | Projenin GitHub sayfasini acar |
| Hakkinda... | Hakkinda ekranini acar |

---

## Hakkinda Ekrani

Acmak icin: Menu -> Yardim -> Hakkinda...

![Hakkinda](screenshots/hakkinda.png)

| Alan | Icerik |
|---|---|
| Uygulama Adi | WinLauncher - Windows Launcher |
| Surum | Version 2.0 |
| Lisans Durumu | Ticari Kullanim Icin Henuz Lisanslanmamistir |
| Kullanim | Kisisel Kullanim Icin Ucretsizdir |
| Telif Hakki | 2024-2025 Hikmet Alp Alemdaro&#287;lu |
| Web Sitesi | Tiklanabilir baglanti |
| Destek E-posta | Tiklanabilir e-posta adresi |

### Butonlar

| Buton | Islev |
|---|---|
| Ana Sayfa | GitHub proje sayfasini tarayicida acar |
| Lisans Kosullari | Lisans detay ekranini acar |
| Kapat | Pencereyi kapatir |

---

## Lisans Ekrani

Acmak icin:
- Menu -> Yardim -> Lisans Kosullari
- Hakkinda ekrani -> Lisans Kosullari butonu

![Lisans](screenshots/lisans.png)

- Aktif dile gore otomatik yuklenir (TR -> license_tr.txt, EN -> license_en.txt)
- Salt okunur - duzenlenemez
- Dikey kaydirma ile tum metin okunabilir
- Kapat butonu ile pencere kapatilir

---

## Dil Destegi

### Dil Degistirme

![Dil Secici](screenshots/dil_secici.png)

1. Baslik cubukundaki dil acilir menuSunden (TR / EN) dili secin
2. Tum menular, butonlar ve mesajlar aninda degisir
3. Secim settings.ini dosyasina otomatik kaydedilir
4. Program yeniden baslatildiginda ayni dil aktif kalir

### Dil Dosyasi Ozellestirme

assets/lang.ini dosyasini bir metin editoruyle duzunleyerek:
- Mevcut cevirileri degistirebilirsiniz
- Yeni bir dil ekleyebilirsiniz (orn: [de], [fr])

---

## Ipuclari ve Yedekleme

### Sekme Organizasyonu Onerileri

```
Gelistirme   - VS Code, Git, Terminal, Postman
Oyunlar      - Steam, Epic Games, oyun kisayollari
Is           - Office, e-posta, is uygulamalari
Tasarim      - Photoshop, Figma, Illustrator
Sistem       - Disk temizleme, antivirus, araclar
Multimedya   - VLC, Spotify, fotograf goruntuleyici
```

### Ikon Ipuclari

- .ico dosyalari en iyi kaliteyi verir
- 128x128 veya 256x256 piksel boyut idealdir
- Hazir ikonlar assets/icon/ klasorunde bulunur
- Son kullanilan ikon klasoru otomatik hatirlanir

### Onemli Dosyalari Yedekleyin

```
winLuncher/
  Data/WinLauncher.xml    <- TUM VERILERINIZ
  settings.ini            <- TUM AYARLARINIZ
  assets/icon/            <- OZEL IKONLARINIZ
```

> Bu uc konumu duzenli araliklarla yedekleyin!

### Tasinabilir Kullanim (USB)

1. Tum winLuncher/ klasorunu USB bellege kopyalayin
2. winLuncher.exe yi dogrudan USB den calistirin
3. Tum ayarlar ve veriler USB de saklanir
4. Farkli bilgisayarlarda ayni deneyim

---

## SSS

**S: Program acilmiyor, ne yapmaliyim?**
.NET Framework 4.7.2 veya uzerinin kurulu oldugunden emin olun.

**S: Oge ekledim ama ikonlar gorunmuyor?**
assets/icon/ klasorununun var oldugunu kontrol edin. Ogeye sag tiklayip Ikonu Degistir ile manuel ikon atayabilirsiniz.

**S: XML yukleme hatasi aliyorum?**
Data/WinLauncher.xml dosyasini silin; program bir sonraki acilista yeni bir tane olusturur. NOT: Mevcut verileriniz kaybolur, once yedekleyin.

**S: Dil degistirdim ama bazi ogeler eski dilde?**
Programi kapatip yeniden acin.

**S: Yanillikla oge sildim, geri alabilir miyim?**
Hayir. Bu yuzden WinLauncher.xml dosyasini duzenli yedekleyin.

**S: Ayni uygulamayi birden fazla sekmeye ekleyebilir miyim?**
Evet. Ogeye sag tiklayin -> Kopyala/Tasi -> Kopyala.

**S: Ayarlarim kaydedilmiyor?**
Programin bulundugu klasore yazma izniniz oldugunden emin olun.

**S: Yeni bir dil ekleyebilir miyim?**
Evet. assets/lang.ini dosyasina yeni bir [dilkodu] bolumu ekleyin (orn: [de]).

---

## Teknik Destek

| Kanal | Adres |
|---|---|
| Hata Bildirimi | https://github.com/hikmetalemdaroglu/999Projects/issues |
| Proje Sayfasi | https://github.com/hikmetalemdaroglu/999Projects |
| E-posta | paylas24@gmail.com |

Bagis: https://github.com/sponsors/hikmetalemdaroglu

---

## Surum Notlari

### v2.0 (2025)
- Tam cok dil destegi (TR / EN) - tum ekranlar, menular, mesajlar
- Lisans Detay Ekrani (LicenseDetForm)
- 13 adet sistem araci (Araclar menusu)
- Manuel Siralama ekrani
- Kopyala / Tasi ekrani
- IP ve bilgisayar adi gosterici
- Hakkinda ekrani
- Gelismis Ayarlar formu
- Dil degistiginde tum butonlar aninda guncellenir

### v1.3 (2025)
- LanguageManager altyapisi
- TR / EN menu cevirileri

### v1.2 (2024)
- Manuel siralama, Kopyala/Tasi, Ikon degistirme

### v1.0 (2024)
- Ilk surum, temel ozellikler

---

Son Guncelleme: 2025 | Versiyon: 2.0 | 2024-2025 Hikmet Alemdaroğlu
