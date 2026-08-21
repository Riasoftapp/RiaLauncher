Public Class SettingsForm
    ' Properties for settings
    Public Property LaunchMode As String = "DoubleClick"
    Public Property ViewMode As String = "IconText"
    Public Property AlwaysOnTop As Boolean = False
    Public Property CurrentLanguage As String = "en"
    Public Property LastActiveTab As Integer = 0
    Public Property AutoUpdateEnabled As Boolean = True
    Public Property AvailableTabs As String() = Nothing
    Public Property LanguageManager As LanguageManager = Nothing

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub SettingsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateLanguages()
        PopulateTabs()
        LoadSettings()

        ' Dili langManager'a set et
        If Not String.IsNullOrEmpty(CurrentLanguage) Then
            Form1.langManager.SetLanguage(CurrentLanguage)
        End If

        ' Debug message removed: previously used custom MBox helper which has been deleted

        ApplyLanguage()
    End Sub

    Private Sub PopulateLanguages()
        CmbLanguage.Items.Clear()

        ' Form1'den langManager'ı direkt kullan
        If Form1.langManager IsNot Nothing Then
            ' LanguageManager'dan tüm dilleri al ve combobox'a ekle
            For Each langCode In Form1.langManager.GetAvailableLanguages()
                CmbLanguage.Items.Add(langCode)
            Next
        Else
            ' Fallback: sadece İngilizce
            CmbLanguage.Items.Add("en")
        End If

        ' CurrentLanguage değerine göre seç (settings.ini'deki dil değeri)
        If Not String.IsNullOrEmpty(CurrentLanguage) Then
            For i As Integer = 0 To CmbLanguage.Items.Count - 1
                If CmbLanguage.Items(i).ToString().ToLower() = CurrentLanguage.ToLower() Then
                    CmbLanguage.SelectedIndex = i
                    Exit For
                End If
            Next
        End If

        ' Hiçbiri seçilmemişse ilkini seç
        If CmbLanguage.SelectedIndex < 0 AndAlso CmbLanguage.Items.Count > 0 Then
            CmbLanguage.SelectedIndex = 0
        End If
    End Sub

    Private Sub PopulateTabs()
        CmbLastTab.Items.Clear()

        If AvailableTabs IsNot Nothing AndAlso AvailableTabs.Length > 0 Then
            ' Tab'ları AvailableTabs sırasıyla ekle (index korunur)
            For Each tabName In AvailableTabs
                CmbLastTab.Items.Add(tabName)
            Next

            ' Aktif tab'ı seçili yap
            If LastActiveTab >= 0 AndAlso LastActiveTab < CmbLastTab.Items.Count Then
                CmbLastTab.SelectedIndex = LastActiveTab
            Else
                CmbLastTab.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub LoadSettings()
        ' Launch Mode
        If LaunchMode = "DoubleClick" Then
            RdbLaunchDoubleClick.Checked = True
        Else
            RdbLaunchSingleClick.Checked = True
        End If

        ' View Mode
        Select Case ViewMode
            Case "Icon"
                RdbViewIcon.Checked = True
            Case "List"
                RdbViewList.Checked = True
            Case "Tile"
                RdbViewTile.Checked = True
            Case Else
                RdbViewIconText.Checked = True
        End Select

        ' Always On Top
        ChkAlwaysOnTop.Checked = AlwaysOnTop

        ' Auto Update
        ChkAutoUpdate.Checked = AutoUpdateEnabled
    End Sub

    Private Sub ApplyLanguage()
        ' Form başlığı
        Me.Text = Form1.langManager.GetText("Settings", "Settings")



        ' GroupBox başlıkları
        GrpLaunchMode.Text = Form1.langManager.GetText("LaunchMode", "Launch Mode")
        GrpViewMode.Text = Form1.langManager.GetText("ViewMode", "View Mode")
        GrpLanguage.Text = Form1.langManager.GetText("Language", "Language")
        GrpLastTab.Text = Form1.langManager.GetText("LastActiveTab", "Last Active Tab")

        ' Labels
        LblLanguage.Text = Form1.langManager.GetText("SelectLanguage", "Select Language:")
        LblLastTab.Text = Form1.langManager.GetText("SelectTab", "Select Tab:")

        ' Radio buttons - Launch Mode
        RdbLaunchDoubleClick.Text = Form1.langManager.GetText("LaunchDoubleClick", "Double Click")
        RdbLaunchSingleClick.Text = Form1.langManager.GetText("LaunchSingleClick", "Single Click")

        ' Radio buttons - View Mode
        RdbViewIconText.Text = Form1.langManager.GetText("ViewIconText", "Icon+Text")
        RdbViewIcon.Text = Form1.langManager.GetText("ViewIcon", "Icon")
        RdbViewList.Text = Form1.langManager.GetText("ViewList", "List")
        RdbViewTile.Text = Form1.langManager.GetText("ViewTile", "Tile")

        ' Checkbox
        ChkAlwaysOnTop.Text = Form1.langManager.GetText("AlwaysOnTop", "Always On Top")
        ChkAutoUpdate.Text = Form1.langManager.GetText("AutoUpdate", "Enable automatic update check")

        ' Buttons
        BtnOK.Text = Form1.langManager.GetText("OK", "OK")
        BtnCancel.Text = Form1.langManager.GetText("Cancel", "Cancel")
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        ' Get Launch Mode
        If RdbLaunchSingleClick.Checked Then
            LaunchMode = "SingleClick"
        Else
            LaunchMode = "DoubleClick"
        End If

        ' Get View Mode
        If RdbViewIcon.Checked Then
            ViewMode = "Icon"
        ElseIf RdbViewList.Checked Then
            ViewMode = "List"
        ElseIf RdbViewTile.Checked Then
            ViewMode = "Tile"
        Else
            ViewMode = "IconText"
        End If

        ' Get Always On Top
        AlwaysOnTop = ChkAlwaysOnTop.Checked

        ' Get Auto Update
        AutoUpdateEnabled = ChkAutoUpdate.Checked

        ' Get Language
        If CmbLanguage.SelectedItem IsNot Nothing Then
            CurrentLanguage = CmbLanguage.SelectedItem.ToString()
        End If

        ' Get Last Active Tab - CmbLastTab AvailableTabs ile aynı sırada doldu
        If CmbLastTab.SelectedIndex >= 0 Then
            LastActiveTab = CmbLastTab.SelectedIndex
        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class
