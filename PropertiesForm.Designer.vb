<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PropertiesForm
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblIcon = New System.Windows.Forms.Label()
        Me.lblExists = New System.Windows.Forms.Label()
        Me.lblPath = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblIconLabel = New System.Windows.Forms.Label()
        Me.lblExistsLabel = New System.Windows.Forms.Label()
        Me.lblPathLabel = New System.Windows.Forms.Label()
        Me.lblNameLabel = New System.Windows.Forms.Label()
        Me.btnCopyName = New System.Windows.Forms.Button()
        Me.btnCopyPath = New System.Windows.Forms.Button()
        Me.btnCopyFullPath = New System.Windows.Forms.Button()
        Me.btnOpenFolder = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblIcon)
        Me.GroupBox1.Controls.Add(Me.lblExists)
        Me.GroupBox1.Controls.Add(Me.lblPath)
        Me.GroupBox1.Controls.Add(Me.lblName)
        Me.GroupBox1.Controls.Add(Me.lblIconLabel)
        Me.GroupBox1.Controls.Add(Me.lblExistsLabel)
        Me.GroupBox1.Controls.Add(Me.lblPathLabel)
        Me.GroupBox1.Controls.Add(Me.lblNameLabel)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(449, 132)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Item Info"
        '
        'lblIcon
        '
        Me.lblIcon.AutoSize = True
        Me.lblIcon.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblIcon.Location = New System.Drawing.Point(110, 102)
        Me.lblIcon.Name = "lblIcon"
        Me.lblIcon.Size = New System.Drawing.Size(36, 15)
        Me.lblIcon.TabIndex = 7
        Me.lblIcon.Text = "None"
        '
        'lblExists
        '
        Me.lblExists.AutoSize = True
        Me.lblExists.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblExists.Location = New System.Drawing.Point(110, 76)
        Me.lblExists.Name = "lblExists"
        Me.lblExists.Size = New System.Drawing.Size(23, 15)
        Me.lblExists.TabIndex = 6
        Me.lblExists.Text = "No"
        '
        'lblPath
        '
        Me.lblPath.AutoEllipsis = True
        Me.lblPath.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblPath.Location = New System.Drawing.Point(110, 50)
        Me.lblPath.Name = "lblPath"
        Me.lblPath.Size = New System.Drawing.Size(320, 18)
        Me.lblPath.TabIndex = 5
        Me.lblPath.Text = "-"
        '
        'lblName
        '
        Me.lblName.AutoEllipsis = True
        Me.lblName.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblName.Location = New System.Drawing.Point(110, 24)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(320, 18)
        Me.lblName.TabIndex = 4
        Me.lblName.Text = "-"
        '
        'lblIconLabel
        '
        Me.lblIconLabel.AutoSize = True
        Me.lblIconLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblIconLabel.Location = New System.Drawing.Point(12, 102)
        Me.lblIconLabel.Name = "lblIconLabel"
        Me.lblIconLabel.Size = New System.Drawing.Size(78, 15)
        Me.lblIconLabel.TabIndex = 3
        Me.lblIconLabel.Text = "Custom icon:"
        '
        'lblExistsLabel
        '
        Me.lblExistsLabel.AutoSize = True
        Me.lblExistsLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblExistsLabel.Location = New System.Drawing.Point(12, 76)
        Me.lblExistsLabel.Name = "lblExistsLabel"
        Me.lblExistsLabel.Size = New System.Drawing.Size(41, 15)
        Me.lblExistsLabel.TabIndex = 2
        Me.lblExistsLabel.Text = "Exists:"
        '
        'lblPathLabel
        '
        Me.lblPathLabel.AutoSize = True
        Me.lblPathLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblPathLabel.Location = New System.Drawing.Point(12, 50)
        Me.lblPathLabel.Name = "lblPathLabel"
        Me.lblPathLabel.Size = New System.Drawing.Size(57, 15)
        Me.lblPathLabel.TabIndex = 1
        Me.lblPathLabel.Text = "Full path:"
        '
        'lblNameLabel
        '
        Me.lblNameLabel.AutoSize = True
        Me.lblNameLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblNameLabel.Location = New System.Drawing.Point(12, 24)
        Me.lblNameLabel.Name = "lblNameLabel"
        Me.lblNameLabel.Size = New System.Drawing.Size(43, 15)
        Me.lblNameLabel.TabIndex = 0
        Me.lblNameLabel.Text = "Name:"
        '
        'btnCopyName
        '
        Me.btnCopyName.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCopyName.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnCopyName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnCopyName.Location = New System.Drawing.Point(12, 158)
        Me.btnCopyName.Name = "btnCopyName"
        Me.btnCopyName.Size = New System.Drawing.Size(90, 32)
        Me.btnCopyName.TabIndex = 1
        Me.btnCopyName.Text = "Copy Name"
        Me.btnCopyName.UseVisualStyleBackColor = True
        '
        'btnCopyPath
        '
        Me.btnCopyPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCopyPath.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnCopyPath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnCopyPath.Location = New System.Drawing.Point(108, 158)
        Me.btnCopyPath.Name = "btnCopyPath"
        Me.btnCopyPath.Size = New System.Drawing.Size(72, 32)
        Me.btnCopyPath.TabIndex = 2
        Me.btnCopyPath.Text = "Copy Path"
        Me.btnCopyPath.UseVisualStyleBackColor = True
        '
        'btnCopyFullPath
        '
        Me.btnCopyFullPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCopyFullPath.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnCopyFullPath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnCopyFullPath.Location = New System.Drawing.Point(186, 158)
        Me.btnCopyFullPath.Name = "btnCopyFullPath"
        Me.btnCopyFullPath.Size = New System.Drawing.Size(91, 32)
        Me.btnCopyFullPath.TabIndex = 3
        Me.btnCopyFullPath.Text = "Copy Full Path"
        Me.btnCopyFullPath.UseVisualStyleBackColor = True
        '
        'btnOpenFolder
        '
        Me.btnOpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenFolder.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnOpenFolder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnOpenFolder.Location = New System.Drawing.Point(283, 158)
        Me.btnOpenFolder.Name = "btnOpenFolder"
        Me.btnOpenFolder.Size = New System.Drawing.Size(108, 32)
        Me.btnOpenFolder.TabIndex = 4
        Me.btnOpenFolder.Text = "Open in Explorer"
        Me.btnOpenFolder.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(397, 158)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(64, 32)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'PropertiesForm
        '
        Me.AcceptButton = Me.btnClose
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(472, 201)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnOpenFolder)
        Me.Controls.Add(Me.btnCopyFullPath)
        Me.Controls.Add(Me.btnCopyPath)
        Me.Controls.Add(Me.btnCopyName)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PropertiesForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Item Properties"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblIconLabel As Label
    Friend WithEvents lblExistsLabel As Label
    Friend WithEvents lblPathLabel As Label
    Friend WithEvents lblNameLabel As Label
    Friend WithEvents lblIcon As Label
    Friend WithEvents lblExists As Label
    Friend WithEvents lblPath As Label
    Friend WithEvents lblName As Label
    Friend WithEvents btnCopyName As Button
    Friend WithEvents btnCopyPath As Button
    Friend WithEvents btnCopyFullPath As Button
    Friend WithEvents btnOpenFolder As Button
    Friend WithEvents btnClose As Button
End Class