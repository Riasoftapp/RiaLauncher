Imports System.Diagnostics
Imports System.IO
Imports System.Windows.Forms

Public Class PropertiesForm
    Private itemName As String
    Private itemPath As String
    Private itemIconPath As String

    Public Sub New(name As String, path As String, iconPath As String)
        InitializeComponent()
        itemName = name
        itemPath = path
        itemIconPath = iconPath
    End Sub

    Private Sub PropertiesForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFormIcon()
        ApplyLanguage()
        FillInfo()
    End Sub

    Private Sub ApplyLanguage()
        If Form1.langManager Is Nothing Then Return

        Me.Text = Form1.langManager.GetText("MsgItemPropertiesTitle", "Item Properties")
        GroupBox1.Text = Form1.langManager.GetText("PropertiesInfoGroup", "Item Info")
        lblNameLabel.Text = Form1.langManager.GetText("PropertiesLblName", "Name:")
        lblPathLabel.Text = Form1.langManager.GetText("PropertiesLblPath", "Full path:")
        lblExistsLabel.Text = Form1.langManager.GetText("PropertiesLblExists", "Exists:")
        lblIconLabel.Text = Form1.langManager.GetText("PropertiesLblIcon", "Custom icon:")
        btnCopyName.Text = Form1.langManager.GetText("PropertiesBtnCopyName", "Copy Name")
        btnCopyPath.Text = Form1.langManager.GetText("PropertiesBtnCopyPath", "Copy Path")
        btnCopyFullPath.Text = Form1.langManager.GetText("PropertiesBtnCopyFullPath", "Copy Full Path")
        btnOpenFolder.Text = Form1.langManager.GetText("PropertiesBtnOpenFolder", "Open in Explorer")
        btnClose.Text = Form1.langManager.GetText("PropertiesBtnClose", "Close")
    End Sub

    Private Sub FillInfo()
        lblName.Text = itemName

        Dim fileExists As String = Form1.langManager.GetText("PropertiesExistsNo", "No")
        If File.Exists(itemPath) Then
            fileExists = Form1.langManager.GetText("PropertiesExistsFile", "Yes (File)")
        ElseIf Directory.Exists(itemPath) Then
            fileExists = Form1.langManager.GetText("PropertiesExistsFolder", "Yes (Folder)")
        End If
        lblExists.Text = fileExists

        lblIcon.Text = If(String.IsNullOrEmpty(itemIconPath),
                          Form1.langManager.GetText("PropertiesIconNone", "None"),
                          Form1.langManager.GetText("PropertiesIconYes", "Yes"))

        lblPath.Text = itemPath
    End Sub

    Private Sub btnCopyName_Click(sender As Object, e As EventArgs) Handles btnCopyName.Click
        Clipboard.SetText(itemName)
    End Sub

    Private Sub btnCopyPath_Click(sender As Object, e As EventArgs) Handles btnCopyPath.Click
        Dim dir As String = Path.GetDirectoryName(itemPath)
        If dir <> "" Then Clipboard.SetText(dir)
    End Sub

    Private Sub btnCopyFullPath_Click(sender As Object, e As EventArgs) Handles btnCopyFullPath.Click
        Clipboard.SetText(itemPath)
    End Sub

    Private Sub btnOpenFolder_Click(sender As Object, e As EventArgs) Handles btnOpenFolder.Click
        Try
            If File.Exists(itemPath) Then
                Process.Start("explorer.exe", "/select,""" & itemPath & """")
            ElseIf Directory.Exists(itemPath) Then
                Process.Start("explorer.exe", itemPath)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
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
End Class