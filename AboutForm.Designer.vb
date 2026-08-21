<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AboutForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutForm))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblAppName = New System.Windows.Forms.Label()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblLicenseStatus = New System.Windows.Forms.Label()
        Me.lblFreeUse = New System.Windows.Forms.Label()
        Me.lblCopyright = New System.Windows.Forms.Label()
        Me.lblWebSiteLabel = New System.Windows.Forms.Label()
        Me.lblWebSite = New System.Windows.Forms.Label()
        Me.lblEmailLabel = New System.Windows.Forms.Label()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.btnAnaSayfa = New System.Windows.Forms.Button()
        Me.btnLisans = New System.Windows.Forms.Button()
        Me.btnKapat = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.lblAppName)
        Me.Panel1.Controls.Add(Me.lblVersion)
        Me.Panel1.Controls.Add(Me.lblLicenseStatus)
        Me.Panel1.Controls.Add(Me.lblFreeUse)
        Me.Panel1.Location = New System.Drawing.Point(15, 40)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(360, 110)
        Me.Panel1.TabIndex = 0
        '
        'lblAppName
        '
        Me.lblAppName.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblAppName.Location = New System.Drawing.Point(10, 15)
        Me.lblAppName.Name = "lblAppName"
        Me.lblAppName.Size = New System.Drawing.Size(340, 25)
        Me.lblAppName.TabIndex = 0
        Me.lblAppName.Text = "RiaLauncher - Windows Launcher"
        Me.lblAppName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblVersion
        '
        Me.lblVersion.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(10, 40)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(340, 18)
        Me.lblVersion.TabIndex = 1
        Me.lblVersion.Text = "Version 1.0"
        Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLicenseStatus
        '
        Me.lblLicenseStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblLicenseStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblLicenseStatus.Location = New System.Drawing.Point(10, 58)
        Me.lblLicenseStatus.Name = "lblLicenseStatus"
        Me.lblLicenseStatus.Size = New System.Drawing.Size(340, 18)
        Me.lblLicenseStatus.TabIndex = 2
        Me.lblLicenseStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFreeUse
        '
        Me.lblFreeUse.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblFreeUse.Location = New System.Drawing.Point(10, 76)
        Me.lblFreeUse.Name = "lblFreeUse"
        Me.lblFreeUse.Size = New System.Drawing.Size(340, 18)
        Me.lblFreeUse.TabIndex = 3
        Me.lblFreeUse.Text = "Free for personal use"
        Me.lblFreeUse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCopyright
        '
        Me.lblCopyright.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblCopyright.Location = New System.Drawing.Point(15, 160)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(360, 18)
        Me.lblCopyright.TabIndex = 1
        Me.lblCopyright.Text = "Copyright © 2024-2025 Hikmet Alp Alemdaroğlu"
        Me.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblWebSiteLabel
        '
        Me.lblWebSiteLabel.AutoSize = True
        Me.lblWebSiteLabel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblWebSiteLabel.Location = New System.Drawing.Point(80, 188)
        Me.lblWebSiteLabel.Name = "lblWebSiteLabel"
        Me.lblWebSiteLabel.Size = New System.Drawing.Size(56, 13)
        Me.lblWebSiteLabel.TabIndex = 2
        Me.lblWebSiteLabel.Text = "Web Site:"
        '
        'lblWebSite
        '
        Me.lblWebSite.AutoSize = True
        Me.lblWebSite.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblWebSite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblWebSite.ForeColor = System.Drawing.Color.Blue
        Me.lblWebSite.Location = New System.Drawing.Point(142, 188)
        Me.lblWebSite.Name = "lblWebSite"
        Me.lblWebSite.Size = New System.Drawing.Size(203, 13)
        Me.lblWebSite.TabIndex = 3
        Me.lblWebSite.Text = "https://riasoft.net/en/rialauncher.html"
        '
        'lblEmailLabel
        '
        Me.lblEmailLabel.AutoSize = True
        Me.lblEmailLabel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblEmailLabel.Location = New System.Drawing.Point(50, 208)
        Me.lblEmailLabel.Name = "lblEmailLabel"
        Me.lblEmailLabel.Size = New System.Drawing.Size(82, 13)
        Me.lblEmailLabel.TabIndex = 4
        Me.lblEmailLabel.Text = "Support Email:"
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblEmail.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblEmail.ForeColor = System.Drawing.Color.Blue
        Me.lblEmail.Location = New System.Drawing.Point(142, 208)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(141, 13)
        Me.lblEmail.TabIndex = 5
        Me.lblEmail.Text = "riasoft.official@gmail.com"
        '
        'btnAnaSayfa
        '
        Me.btnAnaSayfa.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAnaSayfa.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnAnaSayfa.Image = CType(resources.GetObject("btnAnaSayfa.Image"), System.Drawing.Image)
        Me.btnAnaSayfa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAnaSayfa.Location = New System.Drawing.Point(12, 240)
        Me.btnAnaSayfa.Name = "btnAnaSayfa"
        Me.btnAnaSayfa.Size = New System.Drawing.Size(103, 34)
        Me.btnAnaSayfa.TabIndex = 6
        Me.btnAnaSayfa.Text = "Home Page"
        Me.btnAnaSayfa.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAnaSayfa.UseVisualStyleBackColor = True
        '
        'btnLisans
        '
        Me.btnLisans.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLisans.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.btnLisans.Image = CType(resources.GetObject("btnLisans.Image"), System.Drawing.Image)
        Me.btnLisans.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLisans.Location = New System.Drawing.Point(117, 240)
        Me.btnLisans.Name = "btnLisans"
        Me.btnLisans.Size = New System.Drawing.Size(116, 34)
        Me.btnLisans.TabIndex = 7
        Me.btnLisans.Text = "License Terms"
        Me.btnLisans.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnLisans.UseVisualStyleBackColor = True
        '
        'btnKapat
        '
        Me.btnKapat.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnKapat.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnKapat.Image = CType(resources.GetObject("btnKapat.Image"), System.Drawing.Image)
        Me.btnKapat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnKapat.Location = New System.Drawing.Point(306, 240)
        Me.btnKapat.Name = "btnKapat"
        Me.btnKapat.Size = New System.Drawing.Size(69, 34)
        Me.btnKapat.TabIndex = 8
        Me.btnKapat.Text = "Close"
        Me.btnKapat.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnKapat.UseVisualStyleBackColor = True
        '
        'AboutForm
        '
        Me.AcceptButton = Me.btnKapat
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnKapat
        Me.ClientSize = New System.Drawing.Size(390, 280)
        Me.Controls.Add(Me.btnKapat)
        Me.Controls.Add(Me.btnLisans)
        Me.Controls.Add(Me.btnAnaSayfa)
        Me.Controls.Add(Me.lblEmail)
        Me.Controls.Add(Me.lblEmailLabel)
        Me.Controls.Add(Me.lblWebSite)
        Me.Controls.Add(Me.lblWebSiteLabel)
        Me.Controls.Add(Me.lblCopyright)
        Me.Controls.Add(Me.Panel1)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AboutForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "About"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblAppName As Label
    Friend WithEvents lblVersion As Label
    Friend WithEvents lblLicenseStatus As Label
    Friend WithEvents lblFreeUse As Label
    Friend WithEvents lblCopyright As Label
    Friend WithEvents lblWebSiteLabel As Label
    Friend WithEvents lblWebSite As Label
    Friend WithEvents lblEmailLabel As Label
    Friend WithEvents lblEmail As Label
    Friend WithEvents btnAnaSayfa As Button
    Friend WithEvents btnLisans As Button
    Friend WithEvents btnKapat As Button
End Class
