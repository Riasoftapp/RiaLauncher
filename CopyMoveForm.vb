Imports System.Windows.Forms

Public Class CopyMoveForm
    Private sourceTabName As String
    Private itemName As String
    Private itemPath As String
    Private itemIconPath As String
    Private itemIcon As Icon
    Private operationType As String ' "copy" veya "move"

    Public Sub New(srcTab As String, name As String, path As String, iconPath As String, icon As Icon)
        InitializeComponent()
        sourceTabName = srcTab
        itemName = name
        itemPath = path
        itemIconPath = iconPath
        itemIcon = icon
    End Sub

    Private Sub CopyMoveForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFormIcon()
        ApplyLanguage()
        LoadTabs()
    End Sub

    Private Sub ApplyLanguage()
        If Form1.langManager Is Nothing Then Return

        ' Form title
        Dim titlePrefix As String = Form1.langManager.GetText("CopyMoveTitle", "Copy/Move")
        Me.Text = $"{titlePrefix} - {itemName}"

        ' Labels
        Dim sourceTabLabel As String = Form1.langManager.GetText("CopyMoveSourceTab", "Source tab:")
        Dim itemNameLabel As String = Form1.langManager.GetText("CopyMoveItemName", "Item name:")
        lblSourceInfo.Text = $"{sourceTabLabel} {sourceTabName} / {itemNameLabel} {itemName}"

        GroupBox1.Text = Form1.langManager.GetText("CopyMoveTargetTab", "Select target tab")

        ' Buttons
        btnCopy.Text = Form1.langManager.GetText("CopyMoveBtnCopy", "Copy")
        btnMove.Text = Form1.langManager.GetText("CopyMoveBtnMove", "Move")
        btnCancel.Text = Form1.langManager.GetText("CopyMoveBtnCancel", "Cancel")
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

    Private Sub LoadTabs()
        lstTabs.Items.Clear()

        ' Form1'den tüm tab isimlerini al
        Dim mainForm As Form1 = TryCast(Me.Owner, Form1)
        If mainForm IsNot Nothing Then
            For Each tabPage As TabPage In mainForm.TabControl1.TabPages
                ' Kaynak tab'ı da göster ama seçili olmasın
                lstTabs.Items.Add(tabPage.Text)
            Next

            ' İlk farklı tab'ı seç
            If lstTabs.Items.Count > 0 Then
                For i As Integer = 0 To lstTabs.Items.Count - 1
                    If lstTabs.Items(i).ToString() <> sourceTabName Then
                        lstTabs.SelectedIndex = i
                        Exit For
                    End If
                Next
            End If
        End If

        UpdateButtons()
    End Sub

    Private Sub lstTabs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTabs.SelectedIndexChanged
        UpdateButtons()
    End Sub

    Private Sub UpdateButtons()
        Dim hasSelection As Boolean = lstTabs.SelectedIndex >= 0
        btnCopy.Enabled = hasSelection
        btnMove.Enabled = hasSelection
    End Sub

    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        If lstTabs.SelectedIndex < 0 Then Return

        Dim targetTabName As String = lstTabs.SelectedItem.ToString()

        ' Ana forma kopyalama işlemini yaptır
        Dim mainForm As Form1 = TryCast(Me.Owner, Form1)
        If mainForm IsNot Nothing Then
            If mainForm.CopyItemToTab(sourceTabName, targetTabName, itemName, itemPath, itemIconPath, itemIcon) Then
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show(Form1.langManager.GetText("MsgCopyFailed", "The copy operation failed."), Form1.langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub btnMove_Click(sender As Object, e As EventArgs) Handles btnMove.Click
        If lstTabs.SelectedIndex < 0 Then Return

        Dim targetTabName As String = lstTabs.SelectedItem.ToString()

        ' Aynı sekmeye taşınamaz
        If targetTabName = sourceTabName Then
            MessageBox.Show(Form1.langManager.GetText("MsgCannotMoveSameTab", "An item cannot be moved within the same tab."), Form1.langManager.GetText("MsgWarning", "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Ana forma taşıma işlemini yaptır
        Dim mainForm As Form1 = TryCast(Me.Owner, Form1)
        If mainForm IsNot Nothing Then
            If mainForm.MoveItemToTab(sourceTabName, targetTabName, itemName, itemPath, itemIconPath, itemIcon) Then
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show(Form1.langManager.GetText("MsgMoveFailed", "The move operation failed."), Form1.langManager.GetText("MsgError", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class
