Imports System.Windows.Forms

Public Class LicenseForm
    Private Sub LicenseForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFormIcon()
        ApplyLanguage()
        LoadLicenseText()
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

    Private Sub ApplyLanguage()
        If Form1.langManager Is Nothing Then Return

        Me.Text = Form1.langManager.GetText("MenuYardimLisans", "License Terms")
        btnClose.Text = Form1.langManager.GetText("BtnClose", "Close")
    End Sub

    Private Sub LoadLicenseText()
        Dim freeLine As String = Form1.langManager.GetText("LicenseFree", "This software is free for personal and commercial use.")

        Dim apache As String = ""
        Try
            Dim path As String = IO.Path.Combine(Form1.sRootDir, "assets", "license", "Apache-2.0.txt")
            If IO.File.Exists(path) Then
                apache = IO.File.ReadAllText(path, System.Text.Encoding.UTF8)
            End If
        Catch ex As Exception
        End Try

        If apache.Trim() = "" Then
            apache = Form1.langManager.GetText("MsgLicenseFileError", "Apache License 2.0 file could not be loaded.")
        End If

        txtLicense.Text = freeLine & vbCrLf & vbCrLf & apache
        txtLicense.SelectionStart = 0
        txtLicense.ScrollToCaret()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
