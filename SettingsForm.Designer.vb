<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingsForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GrpLaunchMode = New System.Windows.Forms.GroupBox()
        Me.RdbLaunchSingleClick = New System.Windows.Forms.RadioButton()
        Me.RdbLaunchDoubleClick = New System.Windows.Forms.RadioButton()
        Me.GrpViewMode = New System.Windows.Forms.GroupBox()
        Me.RdbViewTile = New System.Windows.Forms.RadioButton()
        Me.RdbViewList = New System.Windows.Forms.RadioButton()
        Me.RdbViewIcon = New System.Windows.Forms.RadioButton()
        Me.RdbViewIconText = New System.Windows.Forms.RadioButton()
        Me.GrpLanguage = New System.Windows.Forms.GroupBox()
        Me.LblLanguage = New System.Windows.Forms.Label()
        Me.CmbLanguage = New System.Windows.Forms.ComboBox()
        Me.GrpLastTab = New System.Windows.Forms.GroupBox()
        Me.LblLastTab = New System.Windows.Forms.Label()
        Me.CmbLastTab = New System.Windows.Forms.ComboBox()
        Me.ChkAlwaysOnTop = New System.Windows.Forms.CheckBox()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.GrpLaunchMode.SuspendLayout()
        Me.GrpViewMode.SuspendLayout()
        Me.GrpLanguage.SuspendLayout()
        Me.GrpLastTab.SuspendLayout()
        Me.SuspendLayout()
        '
        'GrpLaunchMode
        '
        Me.GrpLaunchMode.Controls.Add(Me.RdbLaunchSingleClick)
        Me.GrpLaunchMode.Controls.Add(Me.RdbLaunchDoubleClick)
        Me.GrpLaunchMode.Location = New System.Drawing.Point(12, 12)
        Me.GrpLaunchMode.Name = "GrpLaunchMode"
        Me.GrpLaunchMode.Size = New System.Drawing.Size(300, 80)
        Me.GrpLaunchMode.TabIndex = 0
        Me.GrpLaunchMode.TabStop = False
        Me.GrpLaunchMode.Text = "Launch Mode"
        '
        'RdbLaunchSingleClick
        '
        Me.RdbLaunchSingleClick.AutoSize = True
        Me.RdbLaunchSingleClick.Location = New System.Drawing.Point(15, 50)
        Me.RdbLaunchSingleClick.Name = "RdbLaunchSingleClick"
        Me.RdbLaunchSingleClick.Size = New System.Drawing.Size(80, 17)
        Me.RdbLaunchSingleClick.TabIndex = 1
        Me.RdbLaunchSingleClick.Text = "Single Click"
        Me.RdbLaunchSingleClick.UseVisualStyleBackColor = True
        '
        'RdbLaunchDoubleClick
        '
        Me.RdbLaunchDoubleClick.AutoSize = True
        Me.RdbLaunchDoubleClick.Location = New System.Drawing.Point(15, 25)
        Me.RdbLaunchDoubleClick.Name = "RdbLaunchDoubleClick"
        Me.RdbLaunchDoubleClick.Size = New System.Drawing.Size(87, 17)
        Me.RdbLaunchDoubleClick.TabIndex = 0
        Me.RdbLaunchDoubleClick.Text = "Double Click"
        Me.RdbLaunchDoubleClick.UseVisualStyleBackColor = True
        '
        'GrpViewMode
        '
        Me.GrpViewMode.Controls.Add(Me.RdbViewTile)
        Me.GrpViewMode.Controls.Add(Me.RdbViewList)
        Me.GrpViewMode.Controls.Add(Me.RdbViewIcon)
        Me.GrpViewMode.Controls.Add(Me.RdbViewIconText)
        Me.GrpViewMode.Location = New System.Drawing.Point(320, 12)
        Me.GrpViewMode.Name = "GrpViewMode"
        Me.GrpViewMode.Size = New System.Drawing.Size(300, 130)
        Me.GrpViewMode.TabIndex = 1
        Me.GrpViewMode.TabStop = False
        Me.GrpViewMode.Text = "View Mode"
        '
        'RdbViewTile
        '
        Me.RdbViewTile.AutoSize = True
        Me.RdbViewTile.Location = New System.Drawing.Point(15, 100)
        Me.RdbViewTile.Name = "RdbViewTile"
        Me.RdbViewTile.Size = New System.Drawing.Size(46, 17)
        Me.RdbViewTile.TabIndex = 3
        Me.RdbViewTile.Text = "Tile"
        Me.RdbViewTile.UseVisualStyleBackColor = True
        '
        'RdbViewList
        '
        Me.RdbViewList.AutoSize = True
        Me.RdbViewList.Location = New System.Drawing.Point(15, 75)
        Me.RdbViewList.Name = "RdbViewList"
        Me.RdbViewList.Size = New System.Drawing.Size(43, 17)
        Me.RdbViewList.TabIndex = 2
        Me.RdbViewList.Text = "List"
        Me.RdbViewList.UseVisualStyleBackColor = True
        '
        'RdbViewIcon
        '
        Me.RdbViewIcon.AutoSize = True
        Me.RdbViewIcon.Location = New System.Drawing.Point(15, 50)
        Me.RdbViewIcon.Name = "RdbViewIcon"
        Me.RdbViewIcon.Size = New System.Drawing.Size(46, 17)
        Me.RdbViewIcon.TabIndex = 1
        Me.RdbViewIcon.Text = "Icon"
        Me.RdbViewIcon.UseVisualStyleBackColor = True
        '
        'RdbViewIconText
        '
        Me.RdbViewIconText.AutoSize = True
        Me.RdbViewIconText.Location = New System.Drawing.Point(15, 25)
        Me.RdbViewIconText.Name = "RdbViewIconText"
        Me.RdbViewIconText.Size = New System.Drawing.Size(70, 17)
        Me.RdbViewIconText.TabIndex = 0
        Me.RdbViewIconText.Text = "Icon+Text"
        Me.RdbViewIconText.UseVisualStyleBackColor = True
        '
        'GrpLanguage
        '
        Me.GrpLanguage.Controls.Add(Me.LblLanguage)
        Me.GrpLanguage.Controls.Add(Me.CmbLanguage)
        Me.GrpLanguage.Location = New System.Drawing.Point(12, 150)
        Me.GrpLanguage.Name = "GrpLanguage"
        Me.GrpLanguage.Size = New System.Drawing.Size(300, 80)
        Me.GrpLanguage.TabIndex = 2
        Me.GrpLanguage.TabStop = False
        Me.GrpLanguage.Text = "Language"
        '
        'LblLanguage
        '
        Me.LblLanguage.AutoSize = True
        Me.LblLanguage.Location = New System.Drawing.Point(15, 25)
        Me.LblLanguage.Name = "LblLanguage"
        Me.LblLanguage.Size = New System.Drawing.Size(58, 13)
        Me.LblLanguage.TabIndex = 0
        Me.LblLanguage.Text = "Select Language:"
        '
        'CmbLanguage
        '
        Me.CmbLanguage.FormattingEnabled = True
        Me.CmbLanguage.Location = New System.Drawing.Point(15, 45)
        Me.CmbLanguage.Name = "CmbLanguage"
        Me.CmbLanguage.Size = New System.Drawing.Size(270, 21)
        Me.CmbLanguage.TabIndex = 1
        '
        'GrpLastTab
        '
        Me.GrpLastTab.Controls.Add(Me.LblLastTab)
        Me.GrpLastTab.Controls.Add(Me.CmbLastTab)
        Me.GrpLastTab.Location = New System.Drawing.Point(320, 150)
        Me.GrpLastTab.Name = "GrpLastTab"
        Me.GrpLastTab.Size = New System.Drawing.Size(300, 80)
        Me.GrpLastTab.TabIndex = 3
        Me.GrpLastTab.TabStop = False
        Me.GrpLastTab.Text = "Last Active Tab"
        '
        'LblLastTab
        '
        Me.LblLastTab.AutoSize = True
        Me.LblLastTab.Location = New System.Drawing.Point(15, 25)
        Me.LblLastTab.Name = "LblLastTab"
        Me.LblLastTab.Size = New System.Drawing.Size(58, 13)
        Me.LblLastTab.TabIndex = 0
        Me.LblLastTab.Text = "Select Tab:"
        '
        'CmbLastTab
        '
        Me.CmbLastTab.FormattingEnabled = True
        Me.CmbLastTab.Location = New System.Drawing.Point(15, 45)
        Me.CmbLastTab.Name = "CmbLastTab"
        Me.CmbLastTab.Size = New System.Drawing.Size(270, 21)
        Me.CmbLastTab.TabIndex = 1
        '
        'ChkAlwaysOnTop
        '
        Me.ChkAlwaysOnTop.AutoSize = True
        Me.ChkAlwaysOnTop.Location = New System.Drawing.Point(12, 250)
        Me.ChkAlwaysOnTop.Name = "ChkAlwaysOnTop"
        Me.ChkAlwaysOnTop.Size = New System.Drawing.Size(106, 17)
        Me.ChkAlwaysOnTop.TabIndex = 4
        Me.ChkAlwaysOnTop.Text = "Always On Top"
        Me.ChkAlwaysOnTop.UseVisualStyleBackColor = True
        '
        'BtnOK
        '
        Me.BtnOK.Location = New System.Drawing.Point(480, 250)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(75, 23)
        Me.BtnOK.TabIndex = 5
        Me.BtnOK.Text = "OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Location = New System.Drawing.Point(560, 250)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(75, 23)
        Me.BtnCancel.TabIndex = 6
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'SettingsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(645, 290)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.ChkAlwaysOnTop)
        Me.Controls.Add(Me.GrpLastTab)
        Me.Controls.Add(Me.GrpLanguage)
        Me.Controls.Add(Me.GrpViewMode)
        Me.Controls.Add(Me.GrpLaunchMode)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SettingsForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settings"
        Me.GrpLaunchMode.ResumeLayout(False)
        Me.GrpLaunchMode.PerformLayout()
        Me.GrpViewMode.ResumeLayout(False)
        Me.GrpViewMode.PerformLayout()
        Me.GrpLanguage.ResumeLayout(False)
        Me.GrpLanguage.PerformLayout()
        Me.GrpLastTab.ResumeLayout(False)
        Me.GrpLastTab.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GrpLaunchMode As GroupBox
    Friend WithEvents RdbLaunchSingleClick As RadioButton
    Friend WithEvents RdbLaunchDoubleClick As RadioButton
    Friend WithEvents GrpViewMode As GroupBox
    Friend WithEvents RdbViewTile As RadioButton
    Friend WithEvents RdbViewList As RadioButton
    Friend WithEvents RdbViewIcon As RadioButton
    Friend WithEvents RdbViewIconText As RadioButton
    Friend WithEvents GrpLanguage As GroupBox
    Friend WithEvents LblLanguage As Label
    Friend WithEvents CmbLanguage As ComboBox
    Friend WithEvents GrpLastTab As GroupBox
    Friend WithEvents LblLastTab As Label
    Friend WithEvents CmbLastTab As ComboBox
    Friend WithEvents ChkAlwaysOnTop As CheckBox
    Friend WithEvents BtnOK As Button
    Friend WithEvents BtnCancel As Button

End Class
