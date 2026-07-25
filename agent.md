**Tercih:** Bu yönergeler hızdan çok ihtiyatı önceliklendirir. Basit görevlerde kendi muhakemeni kullan.

---

## 1. Kodlamadan Önce Düşün

**Varsayım yapma. Kafa karışıklığını gizleme. Tercihleri görünür kıl.**

Uygulamadan önce:
- Varsayımlarını açıkça belirt. Emin değilsen sor.
- Birden fazla yorum varsa hepsini sun, sessizce seçme.
- Daha basit bir yaklaşım varsa söyle. Gerektiğinde geri adım at.
- Bir şey belirsizse dur. Neyin kafa karıştırıcı olduğunu belirt. Sor.

---

## 2. Önce Basitlik

**Problemi çözen minimum kod. Hiçbir spekülasyon yok.**

- İstenenden fazlasını ekleme.
- Tek kullanımlık kod için soyutlama yapma.
- İstenmedikçe esneklik veya yapılandırılabilirlik ekleme.
- İmkânsız senaryolar için hata yakalama yazma.
- 200 satır yazıp 50 satırda çözülebiliyorsa yeniden yaz.

Kendine sor: “Kıdemli bir mühendis bunu fazla karmaşık bulur mu?”  
Eğer evet → basitleştir.

---

## 3. Cerrahi Değişiklikler

**Sadece gerekli olanı dokun. Sadece kendi hatanı temizle.**

Mevcut kodu düzenlerken:
- Yan kodları, yorumları veya biçimlendirmeyi “iyileştirme”.
- Bozuk olmayan şeyleri refaktör etme.
- Mevcut stile uy, farklı olsa bile.
- İlgisiz ölü kod görürsen belirt ama silme.

Değişikliklerin kullanılmaz hale getirdiği öğeler:
- Senin değişikliklerinle kullanılmaz hale gelen import/variable/fonksiyonları kaldır.
- Önceden var olan ölü kodu silme (istenmedikçe).

Test: Her değişiklik doğrudan kullanıcının isteğine bağlanmalı.

---

## 4. Hedef Odaklı Çalışma

**Başarı kriterlerini tanımla. Doğrulanana kadar yinele.**

Görevleri doğrulanabilir hedeflere dönüştür:
- “Doğrulama ekle” → “Geçersiz girdiler için test yaz, sonra geçmesini sağla”
- “Hata düzelt” → “Hatayı yeniden üreten test yaz, sonra geçmesini sağla”
- “Refaktör X” → “Önce ve sonra testlerin geçmesini sağla”

Çok adımlı görevlerde kısa plan yap.

---

## 5. Yazılım Geliştirme ve Mimari Kuralları

### a. Modüler Kodlama ve İsimlendirme Standartları
Kodun yönetilebilir ve sürdürülebilir olması için modüler yapı kesinlikle uygulanacaktır:
- [cite_start]Tüm modüller `md_` ön eki ile başlamalıdır[cite: 3].
- [cite_start]Modüller içerisindeki tüm prosedürler `pr_` ön eki ile başlamalıdır[cite: 3].
- [cite_start]Modüller içerisindeki tüm fonksiyonlar `fn_` ön eki ile başlamalıdır[cite: 3].

### b. Hata Yönetimi ve Loglama Mimarisi
[cite_start]Yazılım genelinde try-catch (Tray-cache) hata kontrolleri zorunludur[cite: 4, 45].
- [cite_start]Belirli bir işlevi tamamlayan tüm fonksiyonlar işlem durumunu bildiren standart bir `return` mesajı döndürür[cite: 6].
- [cite_start]**Dönen Mesaj Formatı:** Kolonlar mutlaka pipe (`|`) işareti ile bölünmelidir[cite: 7].
  Format: `statu | Modul Name | Procedure Name | MsgType | return value | Title | [cite_start]Description` [cite: 7, 8]
  * [cite_start]*Statu:* Başarı için `100`, hata için `900`[cite: 8].
  * [cite_start]*MsgType:* `Error` (veri/işlev kaybı) [cite: 9][cite_start], `Warning` (olasılıklar/kurtarılabilir durumlar) [cite: 11, 13][cite_start], `info` (başarılı çalışma açıklaması) [cite: 14][cite_start], `SuccessAudit` (başarılı güvenlik erişimi) [cite: 17][cite_start], `ErrorAudit` (başarısız güvenlik erişimi)[cite: 19].
- [cite_start]**MessageBox Fonksiyonu:** Hata ve bildirim mesajları ayrı bir fonksiyon üzerinden merkezi olarak yönetilir[cite: 24]. [cite_start]Tam olarak 4 parametre alır[cite: 25]:
  1. [cite_start]`returnParam`: Pipe ile bölünmüş dönüş değerinin tamamı[cite: 25].
  2. [cite_start]`msgdisplay`: `Y` (göster) / `N` (gösterme)[cite: 25, 26].
  3. [cite_start]`LogType`: `Y` (loga yaz) / `N` (yazma)[cite: 26, 27].
- [cite_start]**Log Tablosu Yapısı:** `LogType = Y` olduğunda [cite: 27] [cite_start]kayıt uluslararası standartta (`yyyy-aa-gg saat`) [cite: 28][cite_start], ilgili modül/prosedür adları, hata tipi, başlık, açıklama ve aktif kullanıcının `userid` değeri ile tutulur[cite: 28].

### c. Veritabanı ve Transaction Yönetimi
[cite_start]Tüm kayıt işlemleri transaction mantığında yürütülmelidir[cite: 29]. [cite_start]Aynı anda birden fazla tabloya yazılıyorsa (Master-Detail vb.) mutlaka büyük (`large transaction`) blokları kurulmalıdır[cite: 30].
- [cite_start]**OpenEdge ABL Transaction Yapısı:** Açık transaction yapıları için `DO TRANSACTION` veya `REPEAT TRANSACTION` blokları kullanılmalıdır[cite: 31, 42].
- [cite_start]**Geri Alma (Rollback):** Hata durumlarında kontrol `UNDO` ifadesi ile sağlanır[cite: 35, 43]. 
  * [cite_start]`UNDO, LEAVE` bloğu terk eder ve değişiklikleri geri alır[cite: 36].
  * [cite_start]`UNDO, RETRY` transaction'ı yeniden başlatır[cite: 36].
- [cite_start]**Subtransaction Kuralları:** İç içe transaction yapılarında içteki subtransaction geri alınsa dahi dış transaction devam edebilir[cite: 40, 44]. [cite_start]Ancak dış transaction geri alınırsa, ona bağlı tüm iç subtransaction'lar da istisnasız geri alınır[cite: 44].