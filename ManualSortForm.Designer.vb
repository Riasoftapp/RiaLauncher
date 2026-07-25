<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ManualSortForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.lvItems = New System.Windows.Forms.ListView()
        Me.colSira = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colIcon = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.imageList = New System.Windows.Forms.ImageList(Me.components)
        Me.btnUp = New System.Windows.Forms.Button()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lvItems
        '
        Me.lvItems.CheckBoxes = True
        Me.lvItems.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colSira, Me.colIcon, Me.colName, Me.colPath})
        Me.lvItems.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lvItems.FullRowSelect = True
        Me.lvItems.HideSelection = False
        Me.lvItems.Location = New System.Drawing.Point(12, 45)
        Me.lvItems.MultiSelect = False
        Me.lvItems.Name = "lvItems"
        Me.lvItems.Size = New System.Drawing.Size(650, 310)
        Me.lvItems.SmallImageList = Me.imageList
        Me.lvItems.TabIndex = 0
        Me.lvItems.UseCompatibleStateImageBehavior = False
        Me.lvItems.View = System.Windows.Forms.View.Details
        '
        'colSira
        '
        Me.colSira.Text = "Order"
        Me.colSira.Width = 50
        '
        'colIcon
        '
        Me.colIcon.Text = "Icon"
        Me.colIcon.Width = 70
        '
        'colName
        '
        Me.colName.Text = "Item Name"
        Me.colName.Width = 200
        '
        'colPath
        '
        Me.colPath.Text = "Program / Path"
        Me.colPath.Width = 320
        '
        'imageList
        '
        Me.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.imageList.ImageSize = New System.Drawing.Size(32, 32)
        Me.imageList.TransparentColor = System.Drawing.Color.Transparent
        '
        'btnUp
        '
        Me.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUp.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnUp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnUp.Location = New System.Drawing.Point(678, 45)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(90, 35)
        Me.btnUp.TabIndex = 1
        Me.btnUp.Text = "▲ Up"
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'btnDown
        '
        Me.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDown.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnDown.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnDown.Location = New System.Drawing.Point(678, 90)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(90, 35)
        Me.btnDown.TabIndex = 2
        Me.btnDown.Text = "▼ Down"
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnSave.Location = New System.Drawing.Point(12, 365)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 35)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save and Exit"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnCancel.Location = New System.Drawing.Point(118, 365)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(90, 35)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "✗ Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(430, 15)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Select the items (check the checkbox) and sort them using the Up/Down buttons:" &
    ""
        '
        'ManualSortForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 411)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.lvItems)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ManualSortForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Manual Sort"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lvItems As ListView
    Friend WithEvents btnUp As Button
    Friend WithEvents btnDown As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents imageList As ImageList
    Friend WithEvents colSira As ColumnHeader
    Friend WithEvents colIcon As ColumnHeader
    Friend WithEvents colName As ColumnHeader
    Friend WithEvents colPath As ColumnHeader
End Class
