param(
	[Parameter(Mandatory=$true)]
	[string]$ExePath,
	[string]$RootDir = "$(Split-Path -Parent $MyInvocation.MyCommand.Path)/..",
	[string]$ScreenshotsDir = "$(Split-Path -Parent $MyInvocation.MyCommand.Path)/screenshots"
)

Set-StrictMode -Version Latest

# Ensure screenshots folder exists
$screenshotsFull = Resolve-Path -Path $ScreenshotsDir -ErrorAction SilentlyContinue
if (-not $screenshotsFull) {
	New-Item -ItemType Directory -Path $ScreenshotsDir -Force | Out-Null
	$screenshotsFull = Resolve-Path -Path $ScreenshotsDir
}

Add-Type -TypeDefinition @"
using System;
using System.Runtime.InteropServices;
public class Win32 {
	[StructLayout(LayoutKind.Sequential)]
	public struct RECT { public int Left; public int Top; public int Right; public int Bottom; }

	[DllImport("user32.dll")]
	public static extern IntPtr GetForegroundWindow();

	[DllImport("user32.dll")]
	public static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);

	[DllImport("user32.dll")]
	public static extern bool SetForegroundWindow(IntPtr hWnd);

	[DllImport("user32.dll")]
	public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
}
"@ -PassThru | Out-Null

function Capture-ForegroundWindow {
	param([string]$OutPath)
	$h = [Win32]::GetForegroundWindow()
	if ($h -eq [IntPtr]::Zero) { throw "No foreground window." }
	$rect = New-Object Win32+RECT
	[Win32]::GetWindowRect($h,[ref]$rect) | Out-Null
	$w = $rect.Right - $rect.Left
	$hgt = $rect.Bottom - $rect.Top
	if ($w -le 0 -or $hgt -le 0) { throw "Invalid window size: $w x $hgt" }

	$bmp = New-Object System.Drawing.Bitmap $w, $hgt
	$g = [System.Drawing.Graphics]::FromImage($bmp)
	$g.CopyFromScreen($rect.Left, $rect.Top, 0, 0, ([System.Drawing.Size]::new($w,$hgt)))
	$bmp.Save($OutPath, [System.Drawing.Imaging.ImageFormat]::Png)
	$g.Dispose()
	$bmp.Dispose()
}

# Screenshot names to capture (order will prompt you)
$screenshotNames = @(
	'ana_ekran', 'baslik_cubugu', 'menu_cubugu', 'arac_paneli', 'sekme_alani', 'oge_alani',
	'yeni_sekme_dialog', 'sekme_sag_tik', 'surukle_birak', 'yeniden_adlandir', 'ikon_degistir',
	'ozellikler', 'arama', 'manuel_siralama1', 'manuel_siralama2', 'manuel_siralama_secili',
	'kopyala_tasi', 'ayarlar', 'ayarlar_baslama_modu', 'ayarlar_kaydedildi', 'araclar_menu',
	'ip_adresi', 'yardim_menu', 'hakkinda', 'lisans', 'dil_secici', 'ayarlar_kaydedildi'
)

# Languages to capture (settings.ini will be updated)
$languages = @('tr','en')

# Path to settings.ini (relative to project root)
$settingsPath = Join-Path -Path (Resolve-Path -Path (Join-Path -Path $RootDir -ChildPath 'RiaLauncher') -ErrorAction SilentlyContinue) -ChildPath 'assets\settings.ini'
if (-not (Test-Path $settingsPath)) {
	# try fallback: RootDir\assets\settings.ini
	$settingsPath = Join-Path -Path (Resolve-Path -Path $RootDir -ErrorAction SilentlyContinue) -ChildPath 'assets\settings.ini'
}

Write-Host "Screenshots will be saved to: $($screenshotsFull)"
Write-Host "Settings.ini path assumed: $settingsPath"

foreach ($lang in $languages) {
	Write-Host "\n--- Preparing language: $lang ---\n"

	if (Test-Path $settingsPath) {
		(Get-Content $settingsPath) -replace '(?m)^Language=.*$', "Language=$lang" | Set-Content $settingsPath -Encoding UTF8
		Write-Host "Updated settings.ini -> Language=$lang"
	} else {
		Write-Host "settings.ini not found at expected path. Make sure application reads language from settings.ini or change language manually in the app." -ForegroundColor Yellow
	}

	Write-Host "Starting application: $ExePath"
	$proc = Start-Process -FilePath $ExePath -PassThru

	Write-Host "Waiting for main window..."
	for ($i=0; $i -lt 60; $i++) {
		$proc.Refresh()
		if ($proc.MainWindowHandle -ne 0) { break }
		Start-Sleep -Milliseconds 500
	}
	if ($proc.MainWindowHandle -eq 0) { Write-Host "Warning: MainWindowHandle not available. You can still switch to the app window manually." }

	# Bring to front
	try {
		if ($proc.MainWindowHandle -ne 0) { [Win32]::SetForegroundWindow($proc.MainWindowHandle) | Out-Null }
	} catch { }

	foreach ($name in $screenshotNames) {
		Read-Host "Prepare the app to show '$name' for language '$lang' then press Enter to capture"
		# Small delay to allow UI to update
		Start-Sleep -Milliseconds 300
		$outFile = Join-Path -Path $screenshotsFull -ChildPath "${name}_$lang.png"
		try {
			Capture-ForegroundWindow -OutPath $outFile
			Write-Host "Saved: $outFile"
		} catch {
			Write-Host "Capture failed: $_" -ForegroundColor Red
		}
	}

	# Close the app
	if ($proc -and -not $proc.HasExited) {
		Write-Host "Closing application..."
		$proc.CloseMainWindow() | Out-Null
		Start-Sleep -Seconds 1
		if (-not $proc.HasExited) { $proc.Kill() }
	}
}

Write-Host "All done. Check the screenshots folder and update the markdown if needed."
