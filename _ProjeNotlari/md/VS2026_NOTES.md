# Visual Studio 2026 notları

## Snippet hatası
Visual Studio 2026 açılırken bazı HTML/ASP.NET snippet dosyalarında şu tür bir hata görülebilir:

- `Missing or unspecified Language attribute`

Bunun nedeni, `*.snippet` dosyalarındaki `<Code>` öğesinde `Language="html"` gibi bir değer bulunmasıdır. Bu değer `Language="HTML"` olarak düzeltilmelidir.

## Geçici düzeltme
Aşağıdaki klasördeki `.snippet` dosyaları kontrol edilip gerekli düzenleme yapılmalıdır:

- `C:\Program Files\Microsoft Visual Studio\18\Community\Web\Snippets\HTML\1033\HTML`
- `C:\Program Files\Microsoft Visual Studio\18\Community\Web\Snippets\HTML\1033\ASP.NET`

## Güncelleme davranışı
Visual Studio 2026 bazen güncelleme sonrası bu dosyaları yeniden kurup değiştirebilir. Bu nedenle sorun tekrar ortaya çıkabilir.

## Güncellemeleri kapatma
Visual Studio Installer tarafında bazı sürümlerde "Update settings" gibi seçenek görünmeyebilir. Bu durumda:

1. Visual Studio Installer'ı kapatın.
2. Yönetici olarak çalıştırın.
3. Windows Update ayarlarından otomatik güncellemeleri kontrol edin.
4. Gerekirse `vs_installer.exe` sürecini kapatıp tekrar deneyin.

Not: Bazı sürümlerde Visual Studio güncellemelerini tamamen kapatmak mümkün olmayabilir.
