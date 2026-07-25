Imports System.Windows.Forms

Public Class ManualSortForm
    Private tabName As String
    Private itemList As New List(Of ItemData)

    Public Class ItemData
        Public Property ItemId As Integer
        Public Property Name As String
        Public Property Path As String
        Public Property IconPath As String
        Public Property Icon As Icon
        Public Property OriginalIndex As Integer
    End Class

    Public Sub New(currentTabName As String)
        InitializeComponent()
        tabName = currentTabName
    End Sub

    Private Sub ManualSortForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFormIcon()
        ApplyLanguage()
        LoadItems()
    End Sub

    Private Sub ApplyLanguage()
        If Form1.langManager Is Nothing Then Return

        ' Form title
        Dim titlePrefix As String = Form1.langManager.GetText("ManualSortTitle", "Manual Sort")
        Me.Text = $"{titlePrefix} - {tabName}"

        ' Column headers
        colSira.Text = Form1.langManager.GetText("ManualSortColOrder", "Order")
        colIcon.Text = Form1.langManager.GetText("ManualSortColIcon", "Icon")
        colName.Text = Form1.langManager.GetText("ManualSortColName", "Item Name")
        colPath.Text = Form1.langManager.GetText("ManualSortColPath", "Program / Path")

        ' Buttons
        btnUp.Text = Form1.langManager.GetText("ManualSortBtnUp", "▲ Up")
        btnDown.Text = Form1.langManager.GetText("ManualSortBtnDown", "▼ Down")
        btnSave.Text = Form1.langManager.GetText("ManualSortBtnSave", "Save and Exit")
        btnCancel.Text = Form1.langManager.GetText("ManualSortBtnCancel", "✗ Cancel")

        ' Label explanation
        Label1.Text = Form1.langManager.GetText("ManualSortLabel1", "Select the items (check the checkbox) and sort them using the Up/Down buttons:")
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

    Public Sub LoadItems()
        itemList.Clear()
        lvItems.Items.Clear()
        imageList.Images.Clear()

        ' Form1'den aktif sekmenin öğelerini al
        Dim mainForm As Form1 = TryCast(Me.Owner, Form1)
        If mainForm IsNot Nothing Then
            Dim items = mainForm.GetCurrentTabItems()
            Dim index As Integer = 0
            For Each item In items
                Dim itemData As New ItemData With {
                    .ItemId = index,
                    .Name = item.Name,
                    .Path = item.Path,
                    .IconPath = item.IconPath,
                    .Icon = item.Icon,
                    .OriginalIndex = index
                }
                itemList.Add(itemData)

                ' Icon'u ImageList'e ekle
                If itemData.Icon IsNot Nothing Then
                    imageList.Images.Add(index.ToString(), itemData.Icon.ToBitmap())
                End If

                index += 1
            Next
        End If

        RefreshListView()

        ' İlk öğeyi seç
        If lvItems.Items.Count > 0 Then
            lvItems.Items(0).Selected = True
            lvItems.Items(0).Focused = True
        End If
    End Sub

    Private Sub RefreshListView()
        ' Önce tüm seçimleri temizle
        lvItems.SelectedItems.Clear()
        lvItems.Items.Clear()
        imageList.Images.Clear()

        For Each item In itemList
            ' Icon'u ImageList'e ekle
            Dim imageKey As String = item.ItemId.ToString()
            If item.Icon IsNot Nothing Then
                Try
                    imageList.Images.Add(imageKey, item.Icon.ToBitmap())
                Catch
                    ' Icon eklenemezse boş geç
                End Try
            End If

            ' Program adını/yolunu al
            Dim programInfo As String = ""
            Try
                If Not String.IsNullOrEmpty(item.Path) Then
                    programInfo = item.Path
                End If
            Catch
                programInfo = "Unknown"
            End Try

            ' Create list view item
            Dim lvItem As New ListViewItem()
            lvItem.Text = (item.ItemId + 1).ToString() ' Order
            lvItem.SubItems.Add("") ' Icon column (empty - we use imageKey only)
            lvItem.SubItems.Add(item.Name) ' Item Name
            lvItem.SubItems.Add(programInfo) ' Program/Path
            lvItem.ImageKey = imageKey
            lvItem.Tag = item

            lvItems.Items.Add(lvItem)
        Next

        UpdateButtons()
    End Sub

    Private Sub UpdateButtons()
        Dim hasSelection As Boolean = lvItems.SelectedIndices.Count > 0
        Dim selectedIndex As Integer = If(hasSelection, lvItems.SelectedIndices(0), -1)

        btnUp.Enabled = selectedIndex > 0
        btnDown.Enabled = selectedIndex >= 0 AndAlso selectedIndex < lvItems.Items.Count - 1
    End Sub

    Private Sub lvItems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvItems.SelectedIndexChanged
        UpdateButtons()
    End Sub

    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        If lvItems.SelectedIndices.Count = 0 Then Return
        Dim selectedIndex As Integer = lvItems.SelectedIndices(0)

        If selectedIndex > 0 Then
            ' Swap with previous item
            Dim temp = itemList(selectedIndex)
            itemList(selectedIndex) = itemList(selectedIndex - 1)
            itemList(selectedIndex - 1) = temp

            ' Update IDs
            For i As Integer = 0 To itemList.Count - 1
                itemList(i).ItemId = i
            Next

            ' Refresh ve yeni pozisyonu seç
            RefreshListView()

            ' Tüm seçimleri temizle
            lvItems.SelectedItems.Clear()

            ' Sadece taşınan öğeyi seç
            lvItems.Items(selectedIndex - 1).Selected = True
            lvItems.Items(selectedIndex - 1).Focused = True
            lvItems.Items(selectedIndex - 1).EnsureVisible()
        End If
    End Sub

    Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click
        If lvItems.SelectedIndices.Count = 0 Then Return
        Dim selectedIndex As Integer = lvItems.SelectedIndices(0)

        If selectedIndex >= 0 AndAlso selectedIndex < lvItems.Items.Count - 1 Then
            ' Swap with next item
            Dim temp = itemList(selectedIndex)
            itemList(selectedIndex) = itemList(selectedIndex + 1)
            itemList(selectedIndex + 1) = temp

            ' Update IDs
            For i As Integer = 0 To itemList.Count - 1
                itemList(i).ItemId = i
            Next

            ' Refresh ve yeni pozisyonu seç
            RefreshListView()

            ' Tüm seçimleri temizle
            lvItems.SelectedItems.Clear()

            ' Sadece taşınan öğeyi seç
            lvItems.Items(selectedIndex + 1).Selected = True
            lvItems.Items(selectedIndex + 1).Focused = True
            lvItems.Items(selectedIndex + 1).EnsureVisible()
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' Yeni sıralamayı ana forma geri gönder
        Dim mainForm As Form1 = TryCast(Me.Owner, Form1)
        If mainForm IsNot Nothing Then
            mainForm.ApplyNewItemOrder(itemList)
        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class
