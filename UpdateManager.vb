Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Security.Cryptography
Imports System.IO.Compression
Imports System.Diagnostics
Imports System.Web.Script.Serialization

Public Class ReleaseInfo
    Public Property Version As String = ""
    Public Property Changelog As String = ""
    Public Property DownloadUrl As String = ""
    Public Property Sha256 As String = ""
End Class

Public Class UpdateManager

    Private ReadOnly _appDir As String
    Private ReadOnly _repo As String
    Private ReadOnly _assetName As String
    Private ReadOnly _currentVersion As String
    Private ReadOnly _userAgent As String

    Public Sub New(appDir As String, repo As String, assetName As String, currentVersion As String)
        _appDir = appDir
        _repo = repo
        _assetName = assetName
        _currentVersion = currentVersion
        _userAgent = "RiaLauncher/" & currentVersion
    End Sub

    Public Function GetLatestRelease() As ReleaseInfo
        Dim url As String = "https://api.github.com/repos/" & _repo & "/releases/latest"
        Dim json As String

        Using wc As New WebClient()
            wc.Headers.Add("User-Agent", _userAgent)
            wc.Headers.Add("Accept", "application/vnd.github+json")
            wc.Encoding = Encoding.UTF8
            json = wc.DownloadString(url)
        End Using

        Dim jss As New JavaScriptSerializer()
        Dim root As Dictionary(Of String, Object) =
            DirectCast(jss.DeserializeObject(json), Dictionary(Of String, Object))

        Dim rel As New ReleaseInfo()
        If root.ContainsKey("tag_name") Then
            rel.Version = root("tag_name").ToString().TrimStart("v"c, "V"c)
        End If
        If root.ContainsKey("body") AndAlso root("body") IsNot Nothing Then
            rel.Changelog = root("body").ToString().Trim()
        End If

        If root.ContainsKey("assets") AndAlso root("assets") IsNot Nothing Then
            For Each asset As Dictionary(Of String, Object) In
                DirectCast(root("assets"), System.Collections.IEnumerable)

                If asset("name").ToString().Equals(_assetName, StringComparison.OrdinalIgnoreCase) Then
                    rel.DownloadUrl = asset("browser_download_url").ToString()
                    If asset.ContainsKey("digest") AndAlso asset("digest") IsNot Nothing Then
                        rel.Sha256 = asset("digest").ToString()
                    End If
                    Exit For
                End If
            Next
        End If

        Return rel
    End Function

    Public Function IsNewer(rel As ReleaseInfo) As Boolean
        Dim remote As Version = Nothing
        Dim local As Version = Nothing
        If Not Version.TryParse(rel.Version, remote) Then Return False
        If Not Version.TryParse(_currentVersion, local) Then Return False
        Return remote > local
    End Function

    Public Function DownloadAndStage(rel As ReleaseInfo) As String
        Dim workDir As String = Path.Combine(Path.GetTempPath(), "RiaLauncherUpdate")
        If Directory.Exists(workDir) Then Directory.Delete(workDir, True)
        Directory.CreateDirectory(workDir)

        Dim zipPath As String = Path.Combine(workDir, _assetName)
        Using wc As New WebClient()
            wc.Headers.Add("User-Agent", _userAgent)
            wc.DownloadFile(rel.DownloadUrl, zipPath)
        End Using

        Dim actualHash As String
        Using fs As FileStream = File.OpenRead(zipPath)
            Using sha As SHA256 = SHA256.Create()
                actualHash = BitConverter.ToString(sha.ComputeHash(fs)).Replace("-", "").ToLowerInvariant()
            End Using
        End Using

        If rel.Sha256 <> "" Then
            Dim expected As String = rel.Sha256
            If expected.StartsWith("sha256:", StringComparison.OrdinalIgnoreCase) Then
                expected = expected.Substring(7)
            End If
            If Not expected.Equals(actualHash, StringComparison.OrdinalIgnoreCase) Then
                File.Delete(zipPath)
                Throw New InvalidDataException("SHA-256 uyusmazligi: indirilen dosya bozuk veya degistirilmis.")
            End If
        End If

        Dim staging As String = Path.Combine(workDir, "staging")
        ZipFile.ExtractToDirectory(zipPath, staging)
        File.Delete(zipPath)
        Return staging
    End Function

    Public Function LaunchUpdater(stagingDir As String) As Boolean
        Dim updaterSrc As String = Path.Combine(_appDir, "update", "Updater.exe")
        If Not File.Exists(updaterSrc) Then Return False

        Dim workDir As String = Path.Combine(Path.GetTempPath(), "RiaLauncherUpdate")
        Dim updaterDst As String = Path.Combine(workDir, "Updater.exe")
        If File.Exists(updaterDst) Then File.Delete(updaterDst)
        File.Copy(updaterSrc, updaterDst)

        Dim psi As New ProcessStartInfo()
        psi.FileName = updaterDst
        psi.Arguments = "-target """ & _appDir & """ -staging """ & stagingDir & """ -exe RiaLauncher.exe -exclude Data;settings.ini"
        psi.WorkingDirectory = workDir
        psi.UseShellExecute = False
        psi.CreateNoWindow = True

        Process.Start(psi)
        Return True
    End Function

End Class