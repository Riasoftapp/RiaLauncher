<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CopyMoveForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CopyMoveForm))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstTabs = New System.Windows.Forms.ListBox()
        Me.lblSourceInfo = New System.Windows.Forms.Label()
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.btnMove = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lstTabs)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 50)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(360, 200)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Target Tab"
        '
        'lstTabs
        '
        Me.lstTabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstTabs.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lstTabs.FormattingEnabled = True
        Me.lstTabs.ItemHeight = 17
        Me.lstTabs.Location = New System.Drawing.Point(3, 16)
        Me.lstTabs.Name = "lstTabs"
        Me.lstTabs.Size = New System.Drawing.Size(354, 181)
        Me.lstTabs.TabIndex = 0
        '
        'lblSourceInfo
        '
        Me.lblSourceInfo.AutoSize = True
        Me.lblSourceInfo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblSourceInfo.Location = New System.Drawing.Point(12, 15)
        Me.lblSourceInfo.Name = "lblSourceInfo"
        Me.lblSourceInfo.Size = New System.Drawing.Size(50, 15)
        Me.lblSourceInfo.TabIndex = 1
        Me.lblSourceInfo.Text = "Source:"
        '
        'btnCopy
        '
        Me.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCopy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnCopy.Image = CType(resources.GetObject("btnCopy.Image"), System.Drawing.Image)
        Me.btnCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCopy.Location = New System.Drawing.Point(121, 265)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(81, 30)
        Me.btnCopy.TabIndex = 2
        Me.btnCopy.Text = "Copy"
        Me.btnCopy.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'btnMove
        '
        Me.btnMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMove.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnMove.Image = CType(resources.GetObject("btnMove.Image"), System.Drawing.Image)
        Me.btnMove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMove.Location = New System.Drawing.Point(207, 265)
        Me.btnMove.Name = "btnMove"
        Me.btnMove.Size = New System.Drawing.Size(67, 30)
        Me.btnMove.TabIndex = 3
        Me.btnMove.Text = "Move"
        Me.btnMove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMove.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(299, 266)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(70, 28)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'CopyMoveForm
        '
        Me.AcceptButton = Me.btnCopy
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(384, 305)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnMove)
        Me.Controls.Add(Me.btnCopy)
        Me.Controls.Add(Me.lblSourceInfo)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CopyMoveForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Copy/Move Item"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lstTabs As ListBox
    Friend WithEvents lblSourceInfo As Label
    Friend WithEvents btnCopy As Button
    Friend WithEvents btnMove As Button
    Friend WithEvents btnCancel As Button
End Class
