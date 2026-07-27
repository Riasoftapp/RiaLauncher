<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ComboLang = New System.Windows.Forms.ComboBox()
        Me.lbl_logo = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.ContextMenuStripItem = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MenuItemLaunch = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuItemCopyMove = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuItemRename = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemChangeIcon = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemUpdatePath = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemOpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuItemDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuItemProperties = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MenuDosya = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuDosyaCikis = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuSekmeler = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuSekmelerYeni = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuSekmelerAdDegistir = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuSekmelerSil = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuSekmelerYenile = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuSiralama = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuManuelSiralama = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuToolsCmd = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuToolsPowershell = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuToolsTaskMgr = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuToolsServices = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuToolsShowDesktop = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuToolsRestoreDesktop = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuToolsControlPanel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuToolsNetworkCenter = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuToolsDeviceManager = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuToolsComputerName = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuToolsIPAddress = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuYardim = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuYardimDokumanlar = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuYardimDokumanIndir = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuYardimLisans = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuYardimBagis = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuYardimAnaSayfa = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuYardimHakkinda = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStripTab = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MenuTabYeni = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuTabAdDegistir = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuTabSil = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuPanel = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btn_setup = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStripItem.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.ContextMenuStripTab.SuspendLayout()
        Me.MenuPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TabControl1.ItemSize = New System.Drawing.Size(100, 26)
        Me.TabControl1.Location = New System.Drawing.Point(4, 94)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(823, 366)
        Me.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TabControl1.TabIndex = 1
        Me.TabControl1.AllowDrop = False
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.Transparent
        Me.TabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPage1.Controls.Add(Me.FlowLayoutPanel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 30)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(815, 332)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Development"
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.AllowDrop = True
        Me.FlowLayoutPanel1.AutoScroll = True
        Me.FlowLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(807, 324)
        Me.FlowLayoutPanel1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(72, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.Panel1.Controls.Add(Me.ComboLang)
        Me.Panel1.Controls.Add(Me.lbl_logo)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Location = New System.Drawing.Point(-2, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(841, 56)
        Me.Panel1.TabIndex = 0
        '
        'ComboLang
        '
        Me.ComboLang.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboLang.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboLang.FormattingEnabled = True
        Me.ComboLang.Items.AddRange(New Object() {"En", "Tr"})
        Me.ComboLang.Location = New System.Drawing.Point(787, 18)
        Me.ComboLang.Name = "ComboLang"
        Me.ComboLang.Size = New System.Drawing.Size(42, 21)
        Me.ComboLang.TabIndex = 7
        Me.ComboLang.TabStop = False
        Me.ComboLang.Text = "En"
        '
        'lbl_logo
        '
        Me.lbl_logo.AutoSize = True
        Me.lbl_logo.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lbl_logo.ForeColor = System.Drawing.SystemColors.Window
        Me.lbl_logo.Location = New System.Drawing.Point(58, 13)
        Me.lbl_logo.Name = "lbl_logo"
        Me.lbl_logo.Size = New System.Drawing.Size(149, 30)
        Me.lbl_logo.TabIndex = 6
        Me.lbl_logo.Text = "RiaLAUNCHER"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(6, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(49, 50)
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.BackColor = System.Drawing.Color.Transparent
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.Navy
        Me.btnSearch.FlatAppearance.BorderSize = 0
        Me.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Linen
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.ForeColor = System.Drawing.SystemColors.Window
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(750, 4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(30, 23)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.BackColor = System.Drawing.SystemColors.Window
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.txtSearch.Location = New System.Drawing.Point(574, 5)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(172, 20)
        Me.txtSearch.TabIndex = 3
        '
        'ContextMenuStripItem
        '
        Me.ContextMenuStripItem.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItemLaunch, Me.MenuItemSeparator1, Me.MenuItemCopyMove, Me.MenuItemSeparator4, Me.MenuItemRename, Me.MenuItemChangeIcon, Me.MenuItemUpdatePath, Me.MenuItemOpenFolder, Me.MenuItemSeparator2, Me.MenuItemDelete, Me.MenuItemSeparator3, Me.MenuItemProperties})
        Me.ContextMenuStripItem.Name = "ContextMenuStripItem"
        Me.ContextMenuStripItem.Size = New System.Drawing.Size(174, 204)
        '
        'MenuItemLaunch
        '
        Me.MenuItemLaunch.Name = "MenuItemLaunch"
        Me.MenuItemLaunch.Size = New System.Drawing.Size(173, 22)
        Me.MenuItemLaunch.Text = "Launch"
        '
        'MenuItemSeparator1
        '
        Me.MenuItemSeparator1.Name = "MenuItemSeparator1"
        Me.MenuItemSeparator1.Size = New System.Drawing.Size(170, 6)
        '
        'MenuItemCopyMove
        '
        Me.MenuItemCopyMove.Name = "MenuItemCopyMove"
        Me.MenuItemCopyMove.Size = New System.Drawing.Size(173, 22)
        Me.MenuItemCopyMove.Text = "Copy/Move..."
        '
        'MenuItemSeparator4
        '
        Me.MenuItemSeparator4.Name = "MenuItemSeparator4"
        Me.MenuItemSeparator4.Size = New System.Drawing.Size(170, 6)
        '
        'MenuItemRename
        '
        Me.MenuItemRename.Name = "MenuItemRename"
        Me.MenuItemRename.Size = New System.Drawing.Size(173, 22)
        Me.MenuItemRename.Text = "Rename"
        '
        'MenuItemChangeIcon
        '
        Me.MenuItemChangeIcon.Name = "MenuItemChangeIcon"
        Me.MenuItemChangeIcon.Size = New System.Drawing.Size(173, 22)
        Me.MenuItemChangeIcon.Text = "Change Icon..."
        '
        'MenuItemUpdatePath
        '
        Me.MenuItemUpdatePath.Name = "MenuItemUpdatePath"
        Me.MenuItemUpdatePath.Size = New System.Drawing.Size(173, 22)
        Me.MenuItemUpdatePath.Text = "Update Path..."
        '
        'MenuItemOpenFolder
        '
        Me.MenuItemOpenFolder.Name = "MenuItemOpenFolder"
        Me.MenuItemOpenFolder.Size = New System.Drawing.Size(173, 22)
        Me.MenuItemOpenFolder.Text = "Open File Location"
        '
        'MenuItemSeparator2
        '
        Me.MenuItemSeparator2.Name = "MenuItemSeparator2"
        Me.MenuItemSeparator2.Size = New System.Drawing.Size(170, 6)
        '
        'MenuItemDelete
        '
        Me.MenuItemDelete.Name = "MenuItemDelete"
        Me.MenuItemDelete.Size = New System.Drawing.Size(173, 22)
        Me.MenuItemDelete.Text = "Delete"
        '
        'MenuItemSeparator3
        '
        Me.MenuItemSeparator3.Name = "MenuItemSeparator3"
        Me.MenuItemSeparator3.Size = New System.Drawing.Size(170, 6)
        '
        'MenuItemProperties
        '
        Me.MenuItemProperties.Name = "MenuItemProperties"
        Me.MenuItemProperties.Size = New System.Drawing.Size(173, 22)
        Me.MenuItemProperties.Text = "Properties"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.AutoSize = False
        Me.MenuStrip1.BackColor = System.Drawing.Color.Transparent
        Me.MenuStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuDosya, Me.MenuSekmeler, Me.MenuSiralama, Me.MenuTools, Me.MenuYardim})
        Me.MenuStrip1.Location = New System.Drawing.Point(1, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(478, 27)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'MenuDosya
        '
        Me.MenuDosya.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuDosyaCikis})
        Me.MenuDosya.Name = "MenuDosya"
        Me.MenuDosya.Size = New System.Drawing.Size(37, 23)
        Me.MenuDosya.Text = "&File"
        '
        'MenuDosyaCikis
        '
        Me.MenuDosyaCikis.Name = "MenuDosyaCikis"
        Me.MenuDosyaCikis.Size = New System.Drawing.Size(93, 22)
        Me.MenuDosyaCikis.Text = "E&xit"
        '
        'MenuSekmeler
        '
        Me.MenuSekmeler.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuSekmelerYeni, Me.MenuSekmelerAdDegistir, Me.MenuSekmelerSil, Me.ToolStripSeparator1, Me.MenuSekmelerYenile})
        Me.MenuSekmeler.Name = "MenuSekmeler"
        Me.MenuSekmeler.Size = New System.Drawing.Size(42, 23)
        Me.MenuSekmeler.Text = "&Tabs"
        '
        'MenuSekmelerYeni
        '
        Me.MenuSekmelerYeni.Name = "MenuSekmelerYeni"
        Me.MenuSekmelerYeni.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.MenuSekmelerYeni.Size = New System.Drawing.Size(173, 22)
        Me.MenuSekmelerYeni.Text = "&New Tab"
        '
        'MenuSekmelerAdDegistir
        '
        Me.MenuSekmelerAdDegistir.Name = "MenuSekmelerAdDegistir"
        Me.MenuSekmelerAdDegistir.Size = New System.Drawing.Size(173, 22)
        Me.MenuSekmelerAdDegistir.Text = "Rename &Tab"
        '
        'MenuSekmelerSil
        '
        Me.MenuSekmelerSil.Name = "MenuSekmelerSil"
        Me.MenuSekmelerSil.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.MenuSekmelerSil.Size = New System.Drawing.Size(173, 22)
        Me.MenuSekmelerSil.Text = "&Delete Tab"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(170, 6)
        '
        'MenuSekmelerYenile
        '
        Me.MenuSekmelerYenile.Name = "MenuSekmelerYenile"
        Me.MenuSekmelerYenile.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.MenuSekmelerYenile.Size = New System.Drawing.Size(173, 22)
        Me.MenuSekmelerYenile.Text = "&Refresh Tab"
        '
        'MenuSiralama
        '
        Me.MenuSiralama.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuManuelSiralama})
        Me.MenuSiralama.Name = "MenuSiralama"
        Me.MenuSiralama.Size = New System.Drawing.Size(40, 23)
        Me.MenuSiralama.Text = "&Sort"
        '
        'MenuManuelSiralama
        '
        Me.MenuManuelSiralama.Name = "MenuManuelSiralama"
        Me.MenuManuelSiralama.Size = New System.Drawing.Size(147, 22)
        Me.MenuManuelSiralama.Text = "&Manual Sort..."
        '
        'MenuTools
        '
        Me.MenuTools.BackColor = System.Drawing.Color.Transparent
        Me.MenuTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuToolsCmd, Me.MenuToolsPowershell, Me.MenuToolsTaskMgr, Me.MenuToolsServices, Me.ToolStripSeparator4, Me.MenuToolsShowDesktop, Me.MenuToolsRestoreDesktop, Me.ToolStripSeparator5, Me.MenuToolsControlPanel, Me.MenuToolsNetworkCenter, Me.MenuToolsDeviceManager, Me.ToolStripSeparator6, Me.MenuToolsComputerName, Me.MenuToolsIPAddress})
        Me.MenuTools.Name = "MenuTools"
        Me.MenuTools.Size = New System.Drawing.Size(46, 23)
        Me.MenuTools.Text = "&Tools"
        '
        'MenuToolsCmd
        '
        Me.MenuToolsCmd.Name = "MenuToolsCmd"
        Me.MenuToolsCmd.Size = New System.Drawing.Size(223, 22)
        Me.MenuToolsCmd.Text = "Command Prompt"
        '
        'MenuToolsPowershell
        '
        Me.MenuToolsPowershell.Name = "MenuToolsPowershell"
        Me.MenuToolsPowershell.Size = New System.Drawing.Size(223, 22)
        Me.MenuToolsPowershell.Text = "PowerShell"
        '
        'MenuToolsTaskMgr
        '
        Me.MenuToolsTaskMgr.Name = "MenuToolsTaskMgr"
        Me.MenuToolsTaskMgr.Size = New System.Drawing.Size(223, 22)
        Me.MenuToolsTaskMgr.Text = "Task Manager"
        '
        'MenuToolsServices
        '
        Me.MenuToolsServices.Name = "MenuToolsServices"
        Me.MenuToolsServices.Size = New System.Drawing.Size(223, 22)
        Me.MenuToolsServices.Text = "Services Manager"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(220, 6)
        '
        'MenuToolsShowDesktop
        '
        Me.MenuToolsShowDesktop.Name = "MenuToolsShowDesktop"
        Me.MenuToolsShowDesktop.Size = New System.Drawing.Size(223, 22)
        Me.MenuToolsShowDesktop.Text = "Show Desktop"
        '
        'MenuToolsRestoreDesktop
        '
        Me.MenuToolsRestoreDesktop.Name = "MenuToolsRestoreDesktop"
        Me.MenuToolsRestoreDesktop.Size = New System.Drawing.Size(223, 22)
        Me.MenuToolsRestoreDesktop.Text = "Restore Desktop"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(220, 6)
        '
        'MenuToolsControlPanel
        '
        Me.MenuToolsControlPanel.Name = "MenuToolsControlPanel"
        Me.MenuToolsControlPanel.Size = New System.Drawing.Size(223, 22)
        Me.MenuToolsControlPanel.Text = "Control Panel"
        '
        'MenuToolsNetworkCenter
        '
        Me.MenuToolsNetworkCenter.Name = "MenuToolsNetworkCenter"
        Me.MenuToolsNetworkCenter.Size = New System.Drawing.Size(223, 22)
        Me.MenuToolsNetworkCenter.Text = "Network and Sharing Center"
        '
        'MenuToolsDeviceManager
        '
        Me.MenuToolsDeviceManager.Name = "MenuToolsDeviceManager"
        Me.MenuToolsDeviceManager.Size = New System.Drawing.Size(223, 22)
        Me.MenuToolsDeviceManager.Text = "Device Manager"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(220, 6)
        '
        'MenuToolsComputerName
        '
        Me.MenuToolsComputerName.Name = "MenuToolsComputerName"
        Me.MenuToolsComputerName.Size = New System.Drawing.Size(223, 22)
        Me.MenuToolsComputerName.Text = "Show Computer Name"
        '
        'MenuToolsIPAddress
        '
        Me.MenuToolsIPAddress.Name = "MenuToolsIPAddress"
        Me.MenuToolsIPAddress.Size = New System.Drawing.Size(223, 22)
        Me.MenuToolsIPAddress.Text = "Show IP Addresses"
        '
        'MenuYardim
        '
        Me.MenuYardim.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuYardimDokumanlar, Me.MenuYardimDokumanIndir, Me.MenuYardimLisans, Me.ToolStripSeparator2, Me.MenuYardimBagis, Me.MenuYardimAnaSayfa, Me.ToolStripSeparator3, Me.MenuYardimHakkinda})
        Me.MenuYardim.Name = "MenuYardim"
        Me.MenuYardim.Size = New System.Drawing.Size(44, 23)
        Me.MenuYardim.Text = "&Help"
        '
        'MenuYardimDokumanlar
        '
        Me.MenuYardimDokumanlar.Name = "MenuYardimDokumanlar"
        Me.MenuYardimDokumanlar.Size = New System.Drawing.Size(157, 22)
        Me.MenuYardimDokumanlar.Text = "&Help"
        '
        'MenuYardimDokumanIndir
        '
        Me.MenuYardimDokumanIndir.Name = "MenuYardimDokumanIndir"
        Me.MenuYardimDokumanIndir.Size = New System.Drawing.Size(157, 22)
        Me.MenuYardimDokumanIndir.Text = "&Download Docs"
        '
        'MenuYardimLisans
        '
        Me.MenuYardimLisans.Name = "MenuYardimLisans"
        Me.MenuYardimLisans.Size = New System.Drawing.Size(157, 22)
        Me.MenuYardimLisans.Text = "&License Terms"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(154, 6)
        '
        'MenuYardimBagis
        '
        Me.MenuYardimBagis.Name = "MenuYardimBagis"
        Me.MenuYardimBagis.Size = New System.Drawing.Size(157, 22)
        Me.MenuYardimBagis.Text = "&Donate"
        '
        'MenuYardimAnaSayfa
        '
        Me.MenuYardimAnaSayfa.Name = "MenuYardimAnaSayfa"
        Me.MenuYardimAnaSayfa.Size = New System.Drawing.Size(157, 22)
        Me.MenuYardimAnaSayfa.Text = "Home &Page"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(154, 6)
        '
        'MenuYardimHakkinda
        '
        Me.MenuYardimHakkinda.Name = "MenuYardimHakkinda"
        Me.MenuYardimHakkinda.Size = New System.Drawing.Size(157, 22)
        Me.MenuYardimHakkinda.Text = "&About..."
        '
        'ContextMenuStripTab
        '
        Me.ContextMenuStripTab.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuTabYeni, Me.MenuTabAdDegistir, Me.MenuTabSil})
        Me.ContextMenuStripTab.Name = "ContextMenuStripTab"
        Me.ContextMenuStripTab.Size = New System.Drawing.Size(120, 70)
        '
        'MenuTabYeni
        '
        Me.MenuTabYeni.Name = "MenuTabYeni"
        Me.MenuTabYeni.Size = New System.Drawing.Size(119, 22)
        Me.MenuTabYeni.Text = "New Tab"
        '
        'MenuTabAdDegistir
        '
        Me.MenuTabAdDegistir.Name = "MenuTabAdDegistir"
        Me.MenuTabAdDegistir.Size = New System.Drawing.Size(119, 22)
        Me.MenuTabAdDegistir.Text = "Rename"
        '
        'MenuTabSil
        '
        Me.MenuTabSil.Name = "MenuTabSil"
        Me.MenuTabSil.Size = New System.Drawing.Size(119, 22)
        Me.MenuTabSil.Text = "Delete"
        '
        'MenuPanel
        '
        Me.MenuPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MenuPanel.BackColor = System.Drawing.Color.White
        Me.MenuPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MenuPanel.Controls.Add(Me.Label1)
        Me.MenuPanel.Controls.Add(Me.btn_setup)
        Me.MenuPanel.Controls.Add(Me.MenuStrip1)
        Me.MenuPanel.Controls.Add(Me.txtSearch)
        Me.MenuPanel.Controls.Add(Me.btnSearch)
        Me.MenuPanel.Location = New System.Drawing.Point(1, 56)
        Me.MenuPanel.Name = "MenuPanel"
        Me.MenuPanel.Size = New System.Drawing.Size(835, 32)
        Me.MenuPanel.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(507, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Search"
        '
        'btn_setup
        '
        Me.btn_setup.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_setup.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(72, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.btn_setup.FlatAppearance.BorderSize = 0
        Me.btn_setup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_setup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(72, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.btn_setup.Image = CType(resources.GetObject("btn_setup.Image"), System.Drawing.Image)
        Me.btn_setup.Location = New System.Drawing.Point(795, 4)
        Me.btn_setup.Name = "btn_setup"
        Me.btn_setup.Size = New System.Drawing.Size(30, 23)
        Me.btn_setup.TabIndex = 5
        Me.btn_setup.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(834, 460)
        Me.Controls.Add(Me.MenuPanel)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "WinLauncher - Custom Windows Launcher"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStripItem.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ContextMenuStripTab.ResumeLayout(False)
        Me.MenuPanel.ResumeLayout(False)
        Me.MenuPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ContextMenuStripItem As ContextMenuStrip
    Friend WithEvents MenuItemLaunch As ToolStripMenuItem
    Friend WithEvents MenuItemSeparator1 As ToolStripSeparator
    Friend WithEvents MenuItemCopyMove As ToolStripMenuItem
    Friend WithEvents MenuItemSeparator4 As ToolStripSeparator
    Friend WithEvents MenuItemRename As ToolStripMenuItem
    Friend WithEvents MenuItemChangeIcon As ToolStripMenuItem
    Friend WithEvents MenuItemUpdatePath As ToolStripMenuItem
    Friend WithEvents MenuItemOpenFolder As ToolStripMenuItem
    Friend WithEvents MenuItemSeparator2 As ToolStripSeparator
    Friend WithEvents MenuItemDelete As ToolStripMenuItem
    Friend WithEvents MenuItemSeparator3 As ToolStripSeparator
    Friend WithEvents MenuItemProperties As ToolStripMenuItem
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents MenuDosya As ToolStripMenuItem
    Friend WithEvents MenuDosyaCikis As ToolStripMenuItem
    Friend WithEvents MenuSekmeler As ToolStripMenuItem
    Friend WithEvents MenuSekmelerYeni As ToolStripMenuItem
    Friend WithEvents MenuSekmelerAdDegistir As ToolStripMenuItem
    Friend WithEvents MenuSekmelerSil As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents MenuSekmelerYenile As ToolStripMenuItem
    Friend WithEvents MenuSiralama As ToolStripMenuItem
    Friend WithEvents MenuManuelSiralama As ToolStripMenuItem
    Friend WithEvents MenuTools As ToolStripMenuItem
    Friend WithEvents MenuToolsCmd As ToolStripMenuItem
    Friend WithEvents MenuToolsPowershell As ToolStripMenuItem
    Friend WithEvents MenuToolsTaskMgr As ToolStripMenuItem
    Friend WithEvents MenuToolsServices As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents MenuToolsShowDesktop As ToolStripMenuItem
    Friend WithEvents MenuToolsRestoreDesktop As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents MenuToolsControlPanel As ToolStripMenuItem
    Friend WithEvents MenuToolsNetworkCenter As ToolStripMenuItem
    Friend WithEvents MenuToolsDeviceManager As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents MenuToolsComputerName As ToolStripMenuItem
    Friend WithEvents MenuToolsIPAddress As ToolStripMenuItem
    Friend WithEvents MenuYardim As ToolStripMenuItem
    Friend WithEvents MenuYardimDokumanlar As ToolStripMenuItem
    Friend WithEvents MenuYardimDokumanIndir As ToolStripMenuItem
    Friend WithEvents MenuYardimLisans As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents MenuYardimBagis As ToolStripMenuItem
    Friend WithEvents MenuYardimAnaSayfa As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents MenuYardimHakkinda As ToolStripMenuItem
    Friend WithEvents ContextMenuStripTab As ContextMenuStrip
    Friend WithEvents MenuTabYeni As ToolStripMenuItem
    Friend WithEvents MenuTabAdDegistir As ToolStripMenuItem
    Friend WithEvents MenuTabSil As ToolStripMenuItem
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents MenuPanel As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lbl_logo As Label
    Friend WithEvents ComboLang As ComboBox
    Friend WithEvents btn_setup As Button
    Friend WithEvents Label1 As Label
End Class
