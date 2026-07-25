# WinLauncher v2.0 - User Manual

![WinLauncher Logo](../icon/logo/winLuncher128x128.png)

---

## 📖 Table of Contents

1. [Introduction](#introduction)
2. [Installation](#installation)
3. [First Launch](#first-launch)
4. [Key Features](#key-features)
5. [Menu System](#menu-system)
6. [Settings](#settings)
7. [Tips & Tricks](#tips--tricks)
8. [FAQ](#frequently-asked-questions)
9. [Support](#technical-support)

---

## 🎯 Introduction

### What is WinLauncher?

**WinLauncher** is a modern and user-friendly **application launcher** developed for Windows operating systems. It helps you keep your desktop organized and provides quick access to your frequently used programs, files, and folders.

### Why WinLauncher?

- ✅ **Keep Your Desktop Clean** - Organize all shortcuts in one place
- ✅ **Quick Access** - Launch programs with single or double click
- ✅ **Categorize** - Organize your applications in tabs
- ✅ **Multi-Language** - Turkish and English interface support
- ✅ **Portable** - No installation required, runs from USB drive
- ✅ **Free** - Completely free for personal use
- ✅ **Built-in Tools** - Quick access to system tools

### Who Is It For?

- 🎮 Gamers - Organize your game library
- 💼 Professionals - Categorize business applications
- 🎨 Designers - Keep design tools together
- 🖥️ System Administrators - Quick access to system tools
- 👥 Everyone - Use your computer more efficiently

---

## 💾 Installation

### System Requirements

- **Operating System:** Windows 7 / 8 / 10 / 11
- **Framework:** .NET Framework 4.7.2 or higher
- **Disk Space:** ~10 MB
- **RAM:** Minimum 512 MB

### Installation Steps

#### Method 1: Portable Version (Recommended)

1. Download `WinLauncher_v2.0_Portable.zip`
2. Extract the ZIP file to a folder of your choice
3. Run `winLuncher.exe`
4. Installation complete! 🎉

#### Method 2: Setup Installation

1. Download `WinLauncher_v2.0_Setup.exe`
2. Run the setup file
3. Follow the installation wizard
4. Create desktop shortcut (optional)
5. Installation complete! 🎉

### First Run

1. **When you run WinLauncher for the first time:**
   - `assets` folder is created automatically
   - `launcherdata.xml` is populated with default data
   - `settings.ini` is created with default settings
   - `lang.ini` language file is created

2. **Main window opens:**
   - Default tabs appear
   - Turkish language is active
   - Sample items are loaded

---

## 🚀 First Launch

### Main Screen Overview

```
┌─────────────────────────────────────────────────────────┐
│  File  Tabs  Sorting  Tools  Settings  Help             │  ← Menu Bar
├─────────────────────────────────────────────────────────┤
│  [Language: EN ▼]                                       │  ← Language Selector
├─────────────────────────────────────────────────────────┤
│  Search: [_____________] [Search]                       │  ← Search Panel
├─────────────────────────────────────────────────────────┤
│  ┌──────┬──────┬──────┬──────┐                          │
│  │ Games│ Work │ Tools│ Other│                          │  ← Tabs
│  └──────┴──────┴──────┴──────┘                          │
│  ┌──────┐  ┌──────┐  ┌──────┐                          │
│  │ 🎮   │  │ 💼   │  │ 🔧   │                          │
│  │ APEX │  │ Word │  │Chrome│                          │  ← Items
│  └──────┘  └──────┘  └──────┘                          │
└─────────────────────────────────────────────────────────┘
```

### Getting Started

#### 1. Creating a New Tab

1. **Menu Bar** → **Tabs** → **New Tab**
2. Enter tab name (e.g., "Games", "Work", "Design")
3. Press **Enter**
4. New tab created! 🎉

#### 2. Adding Applications

**Method 1: Drag & Drop**
- Drag a file from desktop or file explorer to WinLauncher window
- Automatically added to selected tab

**Method 2: Manual Addition**
1. Right-click on a tab
2. Select **"Add New Item"** (coming soon)
3. Select file or folder
4. Icon auto-detected

#### 3. Launching Applications

- **Single Click:** Default mode launches with single click
- **Double Click:** Can switch to double-click mode in settings

---

## ⚙️ Key Features

### 1. Tab Management

#### Creating Tab
- Menu: **Tabs** → **New Tab**
- Shortcut: `Ctrl + T` (coming soon)

#### Deleting Tab
- **Right-click** on tab → **Delete Tab**
- Menu: **Tabs** → **Delete Tab**

#### Refreshing Tab
- **Right-click** on tab → **Refresh Tab**
- Menu: **Tabs** → **Refresh Tab**

#### Renaming Tab
- **Right-click** on tab → **Rename Tab**
- Enter new name → **Enter**

### 2. Item Management

#### Item Context Menu (Right Click)

When you right-click on an item, these options appear:

```
┌────────────────────────┐
│ ▶ Launch              │  ← Run program
│ 📋 Copy/Move...       │  ← Move to another tab
│ ✏️ Rename             │  ← Change name
│ 🎨 Change Icon        │  ← Select custom icon
│ 🔄 Update Path        │  ← Update file path
│ 📁 Show in Folder     │  ← Open in Windows Explorer
│ 🗑️ Delete             │  ← Delete item
│ ℹ️ Properties         │  ← Detail information
└────────────────────────┘
```

#### Renaming
1. **Right-click** on item → **Rename**
2. Enter new name
3. Press **Enter**

#### Changing Icon
1. **Right-click** on item → **Change Icon**
2. Select `.ico`, `.png`, `.jpg` file
3. Icon updated

#### Update Path
1. **Right-click** on item → **Update Path**
2. Select new file/folder
3. Path updated

#### Copy/Move
1. **Right-click** on item → **Copy/Move**
2. Select target tab
3. Click **Copy** or **Move** button

### 3. Manual Sorting

Arrange items in your desired order:

1. Menu: **Sorting** → **Manual Sorting**
2. Select an item from list
3. Use **↑ Up** or **↓ Down** buttons
4. Click **Save** button

### 4. Search Feature

Search across all tabs:

1. Start typing in **"Search:"** box at top panel
2. Automatically filters (coming soon)
3. Click on search results

---

## 📋 Menu System

### 1. File Menu

```
File
 └─ Exit (Alt + F4)
```

- **Exit:** Closes the program

### 2. Tabs Menu

```
Tabs
 ├─ New Tab
 ├─ Rename Tab
 ├─ Delete Tab
 └─ Refresh Tab
```

- **New Tab:** Creates new category
- **Rename Tab:** Changes active tab name
- **Delete Tab:** Deletes active tab (minimum 1 tab must remain)
- **Refresh Tab:** Reloads tab from XML

### 3. Sorting Menu

```
Sorting
 └─ Manual Sorting
```

- **Manual Sorting:** Opens manual item sorting window

### 4. Tools Menu

```
Tools
 ├─ Command Prompt
 ├─ PowerShell
 ├─ Task Manager
 ├─ Services Manager
 ├─ Show Desktop
 ├─ Restore Desktop
 ├─ Control Panel
 ├─ Network and Sharing Center
 ├─ Device Manager
 ├─ Show Computer Name
 └─ Show IP Addresses
```

#### System Tools:

- **Command Prompt:** Opens CMD
- **PowerShell:** Opens Windows PowerShell
- **Task Manager:** Opens Task Manager
- **Services Manager:** Opens Services.msc
- **Control Panel:** Opens Control Panel
- **Network and Sharing Center:** Opens Network Center
- **Device Manager:** Opens Device Manager

#### Desktop Tools:

- **Show Desktop:** Minimizes all windows
- **Restore Desktop:** Restores windows

#### Information Tools:

- **Show Computer Name:** Shows PC name, copies to clipboard
- **Show IP Addresses:** Shows local and remote IP addresses

### 5. Settings Menu

```
Settings
 └─ Settings...
```

Opens settings window. See [Settings](#settings) section for details.

### 6. Help Menu

```
Help
 ├─ Help
 ├─ Download Documentation (PDF)
 ├─ License Terms
 ├─ Donate
 ├─ Home Page
 └─ About
```

- **Help:** Help page (coming soon)
- **Download Documentation:** Downloads PDF version of this manual
- **License Terms:** Shows license text
- **Donate:** Opens GitHub Sponsors page
- **Home Page:** Opens GitHub repository page
- **About:** Shows version and copyright information

---

## ⚙️ Settings

Menu: **Settings** → **Settings...**

### Launch Mode

```
○ Single Click
○ Double Click
```

- **Single Click:** Launch items with single click (default)
- **Double Click:** Launch items with double click

### View Mode

```
○ Icon + Text
○ Icon Only
```

- **Icon + Text:** Shows text below icon (default)
- **Icon Only:** Shows only icon (more compact)

### Other Settings

```
☑ Always On Top
```

- **Always On Top:** Keeps window above other windows

### Language Selection

From **Language** dropdown menu in menu bar:

- **TR** - Türkçe
- **EN** - English

Selection applies immediately and saved to `settings.ini`.

---

## 💡 Tips & Tricks

### 1. Quick Organization

**Categorize Your Tabs:**
```
📁 Games        - Steam, Epic Games, games
📁 Work         - Office, Mail, work tools
📁 Design       - Photoshop, Illustrator, Figma
📁 Development  - VS Code, Git, terminals
📁 Multimedia   - VLC, Spotify, Netflix
📁 Tools        - WinRAR, Notepad++, tools
```

### 2. Icon Tips

- `.ico` files give best results
- `128x128` or `256x256` sizes are ideal
- Store icons in `assets/icon/` folder
- PNG/JPG files auto-convert to ICO

### 3. Performance Tips

- **20-30 Items Per Tab** is ideal
- Categorize instead of too many tabs
- Delete unused items
- Refresh tabs regularly

### 4. Backup

**Important Files:**
```
📁 winLuncher/
 ├─ launcherdata.xml   ← YOUR DATA
 ├─ settings.ini       ← YOUR SETTINGS
 └─ assets/
     └─ icon/          ← YOUR CUSTOM ICONS
```

Backup these files regularly!

### 5. Portable Usage

Using on USB Drive:
1. Copy entire `winLuncher` folder to USB
2. Same settings on every computer
3. Portable - no installation required!

---

## ❓ Frequently Asked Questions

### General Questions

**Q: Is WinLauncher free?**
> A: Yes! Completely free for personal use.

**Q: Does it require installation?**
> A: No. Portable version requires no installation.

**Q: Is .NET Framework required?**
> A: Yes, .NET Framework 4.7.2 or higher is required.

**Q: What file formats are supported?**
> A: .exe, .lnk, files and folders are supported.

**Q: Icon formats?**
> A: .ico, .png, .jpg formats are supported.

### Technical Questions

**Q: Where is my data stored?**
> A: In `launcherdata.xml` file in XML format (not encrypted).

**Q: Where are my settings?**
> A: In `settings.ini` file in INI format.

**Q: Where is the language file?**
> A: In `assets/lang.ini` file.

**Q: Can I add new languages?**
> A: Yes! Add new [languagecode] section to `lang.ini` file.

### Troubleshooting

**Q: Program won't open?**
> A: Make sure .NET Framework 4.7.2 is installed.

**Q: Icons not showing?**
> A: Ensure `assets/icon/` folder exists.

**Q: XML load error?**
> A: Delete `launcherdata.xml` file, it will be created automatically.

**Q: Settings not saving?**
> A: Make sure folder has write permissions.

**Q: Application won't launch?**
> A: Check that file path is correct.

---

## 🛠️ Technical Support

### Contact

- **GitHub Issues:** [github.com/hikmetalemdaroglu/999Projects/issues](https://github.com/hikmetalemdaroglu/999Projects/issues)
- **Email:** paylas24@gmail.com
- **GitHub:** [@hikmetalemdaroglu](https://github.com/hikmetalemdaroglu)

### Donate

If you like WinLauncher and want to support development:

- **GitHub Sponsors:** [github.com/sponsors/hikmetalemdaroglu](https://github.com/sponsors/hikmetalemdaroglu)

### Contributing

If you want to contribute to the project:

1. Fork the repository
2. Create a new branch
3. Make your changes
4. Send pull request

---

## 📄 License

```
WinLauncher - Personal Use License

This software is free for personal use.

© 2024-2025 Hikmet Alp Alemdaroğlu

All rights reserved.

This software is provided "AS IS".
```

---

## 📝 Release Notes

### v2.0.0 (2025)
- ✅ Full multi-language support (TR/EN)
- ✅ All MessageBoxes translated
- ✅ Tools menu (13 system tools)
- ✅ Manual sorting feature
- ✅ Copy/Move feature
- ✅ IP address display
- ✅ Computer name display
- ✅ Advanced settings form
- ✅ About form
- ✅ User manual (TR/EN)

### v1.3.0 (2025)
- ✅ Multi-language support start
- ✅ LanguageManager added
- ✅ Menu translations

### v1.2.0 (2024)
- ✅ Manual sorting
- ✅ Copy/Move feature
- ✅ Icon changing

### v1.0.0 (2024)
- ✅ Initial release
- ✅ Basic features

---

## 🙏 Thank You

Thank you for using WinLauncher!

**Enjoy! 🚀**

---

*Last Updated: 2025*
*Version: 2.0.0*
*© 2024-2025 Hikmet Alp Alemdaroğlu*
