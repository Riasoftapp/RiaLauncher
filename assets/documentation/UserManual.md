# WinLauncher v2.0 - User Manual

![WinLauncher Logo](../icon/logo/winLuncher128x128.png)

---

## Table of Contents

1. [Introduction](#introduction)
2. [Installation](#installation)
3. [Main Screen](#main-screen)
4. [Tab Management](#tab-management)
5. [Item Management](#item-management)
6. [Manual Sort Screen](#manual-sort-screen)
7. [Copy-Move Screen](#copy-move-screen)
8. [Settings Screen](#settings-screen)
9. [Tools Menu](#tools-menu)
10. [Help Menu](#help-menu)
11. [About Screen](#about-screen)
12. [License Screen](#license-screen)
13. [Language Support](#language-support)
14. [Tips and Backup](#tips-and-backup)
15. [FAQ](#faq)
16. [Technical Support](#technical-support)

---

## Introduction

**WinLauncher** is a modern and user-friendly **application launcher** developed for Windows.
It helps you keep your desktop organized and provides quick access to frequently used programs, files and folders.

### Features

| Feature | Description |
|---|---|
| Tab Management | Categorize your applications |
| Single / Double Click | Choose launch mode |
| Search | Quickly search through your items |
| Multi-Language | Turkish and English interface |
| System Tools | 13 built-in system tools |
| Portable | No installation required, runs from USB |
| Manual Sorting | Arrange item order yourself |
| Copy / Move | Move items between tabs |

---

## Installation

### System Requirements

| Component | Minimum |
|---|---|
| Operating System | Windows 7 / 8 / 10 / 11 |
| .NET Framework | 4.7.2 or higher |
| Disk Space | ~10 MB |
| RAM | 512 MB |

### Installation Steps (Portable)

1. Download WinLauncher_v2.0_Portable.zip
2. Extract the ZIP to any folder
3. Run winLuncher.exe

> No installation required. The program automatically creates all necessary files on first run.

### Files Created on First Run

```
winLuncher/
  winLuncher.exe
  Data/
    WinLauncher.xml       <- Item and tab data
  settings.ini            <- Application settings
  assets/
    lang.ini              <- Language file
    icon/                 <- Icon library
    documentation/        <- Manuals
```

---

## Main Screen

The main screen is the central management area of WinLauncher.

> [screenshots/main_screen.png]

### Title Bar

> [screenshots/title_bar.png]

The title bar is at the very top:

| Element | Description |
|---|---|
| WinLauncher Logo | Application icon at top left |
| Application Title | "WinLauncher - Custom Windows Launcher" |
| Language Menu (TR / EN) | Instant language switching |
| Minimize (-) | Minimizes the window |
| Maximize (square) | Maximizes the window |
| Close (X) | Closes the program |

> Tip: You can drag the window by holding the title bar.

### Menu Bar

> [screenshots/menu_bar.png]

Located just below the title bar:

```
File | Tabs | Sorting | Tools | Settings | Help
```

### Toolbar Panel

> [screenshots/toolbar.png]

| Element | Description |
|---|---|
| New Tab button | Creates a new tab |
| Delete Tab button | Deletes the selected tab |
| Search text box | Searches through items |
| Search button | Initiates search |

### Tab Area

> [screenshots/tab_area.png]

Each tab represents a category (e.g.: Development, Games, Work).

### Item Area (Icon Panel)

> [screenshots/item_area.png]

Shows the content of the selected tab. Each item consists of:
- Icon (top, 48x48 px)
- Name (bottom, text)

---

## Tab Management

### Creating a New Tab

Method 1 - From Menu:
1. Tabs menu -> New Tab
2. Type the tab name (e.g. "Games")
3. Click OK

Method 2 - From Toolbar:
1. Click the New Tab button in the toolbar
2. Type the tab name -> OK

Method 3 - Right Click:
1. Right-click on the tab bar -> New Tab

> [screenshots/new_tab_dialog.png]

### Renaming a Tab

1. Right-click on the tab -> Rename Tab
2. Type the new name -> OK

> [screenshots/tab_right_click.png]

### Deleting a Tab

1. Right-click on the tab -> Delete Tab
2. Or click the Delete Tab button in the toolbar
3. Click Yes in the confirmation dialog

> WARNING: At least one tab must remain. The last tab cannot be deleted.

### Refreshing a Tab

Reloads tab content from the XML file:
- Right-click the tab -> Refresh Tab
- Or from menu: Tabs -> Refresh Tab

### Switching Between Tabs

| Method | Description |
|---|---|
| Click | Click on the tab name |
| Tab key | Moves to the next tab |
| Shift + < | Moves to the previous tab |
| Shift + > | Moves to the next tab |

---

## Item Management

### Adding Items (Drag and Drop)

> [screenshots/drag_drop.png]

1. Select a file / folder / shortcut from File Explorer or Desktop
2. Drag and drop it onto the desired tab in WinLauncher
3. Item is automatically added with auto-detected icon

> Tip: When .lnk (shortcut) files are dragged, WinLauncher automatically detects the target application.

### Launching Items

| Mode | How? | Setting |
|---|---|---|
| Single Click | Click the icon or name once | Settings -> Single Click |
| Double Click | Double-click the icon or name | Settings -> Double Click (default) |

### Item Right-Click Menu

> [screenshots/item_right_click.png]

When you right-click any item:

```
  Launch
  ---
  Copy/Move...
  ---
  Rename
  Change Icon
  Update Path
  Show in Folder
  ---
  Delete
  ---
  Properties
```

#### Launch
Launches or opens the selected application / file / folder.

#### Copy / Move
Copies or moves the item to another tab. See Copy/Move Screen section.

#### Rename

> [screenshots/rename.png]

1. Click Rename
2. Enter the new name -> OK

#### Change Icon

> [screenshots/change_icon.png]

1. Click Change Icon
2. Select a file in a supported format: .ico, .png, .jpg, .bmp
3. Icon is instantly updated

> Tip: You can use ready-made icons in the assets/icon/ folder.

#### Update Path
Update the file path if an application has been moved:
1. Click Update Path
2. Select the new file location
3. Path and icon are automatically refreshed

#### Show in Folder
Opens the folder containing the item in Windows Explorer with the file selected.

#### Delete
Removes the item from WinLauncher (NOT deleted from disk):
1. Click Delete -> Yes in the confirmation dialog

#### Properties

> [screenshots/properties.png]

```
Name      : Visual Studio Code
Path      : C:\Program Files\VSCode\Code.exe
Exists    : Yes (File)
Custom Icon: Yes
```

### Search

> [screenshots/search.png]

1. Start typing in the Search box in the toolbar
2. Click the Search button or press Enter
3. Items are filtered by name
4. Clear the box and search again to show all items

---

## Manual Sort Screen

Manually arrange the order of items in a tab.

Open: Menu -> Sorting -> Manual Sorting...

> [screenshots/manual_sort.png]

### Screen Components

| Component | Description |
|---|---|
| Item List | All items in the tab listed in order |
| Checkbox | Check to select the item to move |
| Up | Moves the selected item one position up |
| Down | Moves the selected item one position down |
| Save and Exit | Saves the new order and closes the window |
| Cancel | Discards changes and closes the window |

### List Columns

| Column | Description |
|---|---|
| Order | Current order number |
| Icon | Item's icon |
| Icon Name | Item's name |
| Program / Path | Full path of the file or application |

### How to Use

1. Open Sorting -> Manual Sorting...
2. Check the checkbox of the item you want to move
3. Use Up or Down to move it to the desired position
4. Click Save and Exit

> [screenshots/manual_sort_selected.png]

> NOTE: If you click Cancel, your changes will not be saved.

---

## Copy-Move Screen

Copy or move items between tabs.

Open: Right-click an item -> Copy/Move...

> [screenshots/copy_move.png]

### Screen Components

| Component | Description |
|---|---|
| Title | "Copy/Move - [Item Name]" |
| Source Info | "Source Tab: X / Item Name: Y" |
| Target Tab | Select the target tab from dropdown |
| Copy | Copies the item (remains in source) |
| Move | Moves the item (removed from source) |
| Cancel | Cancels the operation |

### Copying

1. Right-click item -> Copy/Move...
2. Select the target tab
3. Click Copy -> Item appears in both tabs

### Moving

1. Right-click item -> Copy/Move...
2. Select the target tab
3. Click Move -> Item is removed from source, added to target

> NOTE: If you try to move an item to the same tab, you will get a warning message.

---

## Settings Screen

Open: Menu -> Settings

> [screenshots/settings.png]

### Launch Mode

> [screenshots/settings_launch_mode.png]

```
Launch Mode:
  o Single Click
  * Double Click   <- Default
```

| Option | Behavior |
|---|---|
| Single Click | One click launches the application |
| Double Click | Double click launches the application |

### View Mode

```
View Mode:
  * Icon + Text   <- Default
  o Icon Only
```

| Option | Behavior |
|---|---|
| Icon + Text | Item name is shown below the icon |
| Icon Only | Only the icon is shown, more compact |

### Always On Top

```
[x] Always On Top
```

When checked, WinLauncher stays above all other windows.

### Saving

Click Save. Settings are saved to settings.ini.

> [screenshots/settings_saved.png]

---

## Tools Menu

Open: Menu -> Tools

> [screenshots/tools_menu.png]

### System Tools

| Tool | Description |
|---|---|
| Command Prompt | Opens cmd.exe |
| PowerShell | Opens powershell.exe |
| Task Manager | Opens taskmgr.exe |
| Services Manager | Opens services.msc |
| Control Panel | Opens control.exe |
| Network and Sharing Center | Opens Windows Network Center |
| Device Manager | Opens devmgmt.msc |

### Desktop Tools

| Tool | Description |
|---|---|
| Show Desktop | Minimizes all windows |
| Restore Desktop | Restarts Explorer |

### Information Tools

| Tool | Description |
|---|---|
| Show Computer Name | Shows PC name, copies to clipboard |
| Show IP Addresses | Shows local and public IP, copies to clipboard |

> [screenshots/ip_address.png]

---

## Help Menu

Open: Menu -> Help

> [screenshots/help_menu.png]

| Sub-Menu | Description |
|---|---|
| Help | Help information |
| Download Documentation | Opens manual in active language |
| License Terms | Opens the license detail screen |
| Donate | Redirects to GitHub Sponsors page |
| Home Page | Opens the project GitHub page |
| About... | Opens the About screen |

---

## About Screen

Open: Menu -> Help -> About...

> [screenshots/about.png]

| Field | Content |
|---|---|
| Application Name | WinLauncher - Windows Launcher |
| Version | Version 2.0 |
| License Status | Not Yet Licensed for Commercial Use |
| Usage | Free for Personal Use |
| Copyright | 2024-2025 Hikmet Alp Alemdaro&#287;lu |
| Web Site | Clickable link |
| Support Email | Clickable email address |

### Buttons

| Button | Function |
|---|---|
| Home Page | Opens GitHub project page in browser |
| License Terms | Opens the license detail screen |
| Close | Closes the window |

---

## License Screen

Open:
- Menu -> Help -> License Terms
- About screen -> License Terms button

> [screenshots/license.png]

- Automatically loaded based on active language (TR -> license_tr.txt, EN -> license_en.txt)
- Read-only - cannot be edited
- Full text readable with vertical scrollbar
- Closed with the Close button

---

## Language Support

### Changing Language

> [screenshots/language_selector.png]

1. Select your language from the language dropdown (TR / EN) in the title bar
2. All menus, buttons and messages change instantly
3. Selection is automatically saved to settings.ini
4. The same language remains active when the program is restarted

### Customizing the Language File

By editing assets/lang.ini with a text editor:
- You can change existing translations
- You can add a new language (e.g. [de], [fr])

---

## Tips and Backup

### Tab Organization Suggestions

```
Development  - VS Code, Git, Terminal, Postman
Games        - Steam, Epic Games, game shortcuts
Work         - Office, email, work applications
Design       - Photoshop, Figma, Illustrator
System       - Disk cleanup, antivirus, tools
Multimedia   - VLC, Spotify, photo viewer
```

### Icon Tips

- .ico files give the best quality
- 128x128 or 256x256 pixel size is ideal
- Ready-made icons are in the assets/icon/ folder
- Last used icon folder is automatically remembered

### Backup Important Files

```
winLuncher/
  Data/WinLauncher.xml    <- ALL YOUR DATA
  settings.ini            <- ALL YOUR SETTINGS
  assets/icon/            <- YOUR CUSTOM ICONS
```

> Back up these three locations regularly!

### Portable Usage (USB)

1. Copy the entire winLuncher/ folder to a USB drive
2. Run winLuncher.exe directly from the USB
3. All settings and data are stored on USB
4. Same experience on different computers

---

## FAQ

**Q: The program won't open, what should I do?**
Make sure .NET Framework 4.7.2 or higher is installed.

**Q: I added an item but icons are not showing?**
Check that the assets/icon/ folder exists. You can assign an icon manually by right-clicking the item and selecting Change Icon.

**Q: I'm getting an XML load error?**
Delete the Data/WinLauncher.xml file; the program will create a new one on next startup. NOTE: Existing data will be lost - back up first.

**Q: I changed the language but some items are still in the old language?**
Close and reopen the program.

**Q: I accidentally deleted an item, can I recover it?**
No. That is why you should back up WinLauncher.xml regularly.

**Q: Can I add the same application to multiple tabs?**
Yes. Right-click the item -> Copy/Move -> Copy.

**Q: My settings are not being saved?**
Make sure you have write permission to the folder where the program is located.

**Q: Can I add a new language?**
Yes. Add a new [languagecode] section to assets/lang.ini (e.g. [de]).

---

## Technical Support

| Channel | Address |
|---|---|
| Bug Report | https://github.com/hikmetalemdaroglu/999Projects/issues |
| Project Page | https://github.com/hikmetalemdaroglu/999Projects |
| Email | paylas24@gmail.com |

Donate: https://github.com/sponsors/hikmetalemdaroglu

---

## Release Notes

### v2.0 (2025)
- Full multi-language support (TR / EN) - all screens, menus, messages
- License Detail Screen (LicenseDetForm)
- 13 system tools (Tools menu)
- Manual Sort screen
- Copy / Move screen
- IP address and computer name display
- About screen
- Advanced Settings form
- All buttons update instantly when language changes

### v1.3 (2025)
- LanguageManager infrastructure
- TR / EN menu translations

### v1.2 (2024)
- Manual sorting, Copy/Move, Icon changing

### v1.0 (2024)
- Initial release, basic features

---

Last Updated: 2025 | Version: 2.0 | 2024-2025 Hikmet Alp Alemdaro&#287;lu
