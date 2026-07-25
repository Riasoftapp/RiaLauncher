# 🎯 Tüm Form Kontrollerinin Dil Desteğini Tam Olarak Uygulanması

## Understanding
Uygulamanın bazı formlarında (özellikle ManualSortForm, CopyMoveForm, vb.) label, column header'lar ve başlıklar dil değiştiğinde güncellenmiyor. ApplyLanguage() metodu eksik kontrollerin localization'ını yapmıyor. Tüm formlardaki tüm UI öğelerini (labellar, kolonlar, başlıklar vb.) localize etmeliyiz.

## Assumptions
- LanguageManager'da gerekli tüm metin keyleri var (tr.lng ve en.lng'de tanımlandı)
- Form1 önceden bilgilendirilebilir (tüm formlara langManager referansı zaten veriliyor)
- Dil değiştirme event'leri Form1'de tetikleniyor
- Tüm formlar InitializeComponent()'ten sonra ApplyLanguage() çağırıyor

## Approach
1. **ManualSortForm** - Column headers ve Label1 eklenecek
2. **SettingsForm** - GroupBox'lar ve RadioButton'lar kontrol edilecek  
3. **AboutForm** - Label'lar ve başlık kontrol edilecek
4. **CopyMoveForm** - Tüm kontrollerin localization kontrol edilecek
5. **Form1.ApplyLanguage()** - Tüm menu items ve dinamik metinler kontrol edilecek

## Key Files
- ManualSortForm.vb - Column header'lar, Label1 eksik
- SettingsForm.vb - GroupBox ve kontroller kontrol gerekli
- AboutForm.vb - Tüm label'lar kontrol gerekli
- CopyMoveForm.vb - Başlık ve label'lar kontrol gerekli
- Form1.vb - Menu items, messagebox'lar kontrol gerekli

## Risks & Open Questions
- Her dil dosyasında tüm keylerin olması gerekiyor
- Dinamik messagebox'alar da localize edilmesi gerekiyor
- Menu items zaten tr.lng/en.lng'de tanımlı mı doğru mu kontrol etmeli

**Last Updated**: 2026-07-20 19:09:34

## 📝 Plan Steps
-  **ManualSortForm.ApplyLanguage() içine Column header'lar ve Label1 ekle**
-  **SettingsForm.ApplyLanguage() içine eksik kontrollerle ekle**
-  **AboutForm.ApplyLanguage() içine tüm label'ları ekle**
-  **CopyMoveForm.ApplyLanguage() kontrol et, eksik kontrollerle ekle**
-  **Form1.ApplyLanguage() içine tüm menu items'ı ekle**
-  **MessageBox'ların localization'ını kontrol ve düzelt**
-  **Tüm .lng dosyalarında eksik key'ler varsa ekle**
-  **Build ve test**

