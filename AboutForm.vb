Imports System.Windows.Forms

Public Class AboutForm
    Private Sub AboutForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFormIcon()
        ApplyLanguage()
    End Sub

    Private Sub ApplyLanguage()
        If Form1.langManager Is Nothing Then Return

        ' Form title
        Me.Text = Form1.langManager.GetText("AboutTitle", "About")

        ' Labels
        lblAppName.Text = Form1.langManager.GetText("AboutAppName", "WinLauncher - Windows Launcher")
        lblVersion.Text = Form1.langManager.GetText("AboutVersion", "Version 1.3")
        lblLicenseStatus.Text = Form1.langManager.GetText("AboutLicenseStatus", "Not yet licensed for commercial use")
        lblFreeUse.Text = Form1.langManager.GetText("AboutFreeUse", "Free for personal use")
        lblCopyright.Text = Form1.langManager.GetText("AboutCopyright", "© 2024-2025 Hikmet Alp Alemdaroğlu")
        lblWebSiteLabel.Text = Form1.langManager.GetText("AboutWebSiteLabel", "Web Site:")
        lblEmailLabel.Text = Form1.langManager.GetText("AboutEmailLabel", "Support Email:")

        ' Buttons
        btnAnaSayfa.Text = Form1.langManager.GetText("AboutBtnAnaSayfa", "Home Page")
        btnLisans.Text = Form1.langManager.GetText("AboutBtnLisans", "License Terms")
        btnKapat.Text = Form1.langManager.GetText("AboutBtnKapat", "Close")
    End Sub

    Private Sub SetFormIcon()
        Try
            Dim iconPath As String = IO.Path.Combine(Form1.sLogoDir, "winLuncher32x32.ico")
            If IO.File.Exists(iconPath) Then
                Me.Icon = New Drawing.Icon(iconPath)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnAnaSayfa_Click(sender As Object, e As EventArgs) Handles btnAnaSayfa.Click
        Try
            Process.Start("https://github.com/hikmetalemdaroglu/999Projects/tree/winluncher-v1.2-release/ProjectVs/ProjectVb.net/winLuncher")
        Catch ex As Exception
            Dim msg As String = String.Format(Form1.langManager.GetText("MsgHomePageError", "Home page could not be opened: {0}"), ex.Message)
            MessageBox.Show(msg, Form1.langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnLisans_Click(sender As Object, e As EventArgs) Handles btnLisans.Click
        Dim lisansBaslik As String = Form1.langManager.GetText("LicenseTitle", "WinLauncher - Personal Use License")
        Dim lisansFree As String = Form1.langManager.GetText("LicenseFree", "This software is free for personal use.")
        Dim lisansCopyright As String = Form1.langManager.GetText("AboutCopyright", "© 2024-2025 Hikmet Alp Alemdaroğlu")
        Dim lisansRights As String = Form1.langManager.GetText("LicenseRights", "All rights reserved.")
        Dim lisansAsIs As String = Form1.langManager.GetText("LicenseAsIs", "This software is provided ""AS IS"".")

        Dim lisansMetni As String = lisansBaslik & vbCrLf & vbCrLf &
                                    lisansFree & vbCrLf & vbCrLf &
                                    lisansCopyright & vbCrLf & vbCrLf &
                                    lisansRights & vbCrLf & vbCrLf &
                                    lisansAsIs

        MessageBox.Show(lisansMetni, Form1.langManager.GetText("MenuYardimLisans", "License Terms"), MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnKapat_Click(sender As Object, e As EventArgs) Handles btnKapat.Click
        Me.Close()
    End Sub

    Private Sub lblWebSite_Click(sender As Object, e As EventArgs) Handles lblWebSite.Click
        Try
            Process.Start("https://github.com/hikmetalemdaroglu/999Projects/tree/winluncher-v1.2-release")
        Catch ex As Exception
            Dim msg As String = String.Format(Form1.langManager.GetText("MsgWebSiteError", "Website could not be opened: {0}"), ex.Message)
            MessageBox.Show(msg, Form1.langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub lblEmail_Click(sender As Object, e As EventArgs) Handles lblEmail.Click
        Try
            Process.Start("mailto:paylas24@gmail.com")
        Catch ex As Exception
            Dim msg As String = String.Format(Form1.langManager.GetText("MsgEmailError", "Email application could not be opened: {0}"), ex.Message)
            MessageBox.Show(msg, Form1.langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
