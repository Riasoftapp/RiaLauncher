# RiaLauncher

A modern, feature-rich application launcher for Windows built with VB.NET and Windows Forms. RiaLauncher allows you to organize, launch, and manage your favorite applications with ease.

## 🎯 Features

- **Tabbed Organization**: Organize applications into custom tabs for better categorization
- **Drag & Drop Support**: Simply drag and drop executable files from Windows Explorer to add them to your launcher
- **Multi-Language Support**: Full support for Turkish (TR) and English (EN)
- **Application Management**:
  - Launch applications with a single click or double-click
  - Rename applications
  - Change application icons
  - Update application paths
  - Delete applications
  - Manual sorting of applications
- **Search Functionality**: Quick search to find applications by name
- **Persistent Storage**: Application data is saved to XML and persists between sessions
- **Context Menus**: Right-click context menus for quick access to application management features
- **Built-in Tools**: Access to system utilities like Command Prompt, PowerShell, Task Manager, Device Manager, Control Panel, and more
- **Customizable Settings**: Configure launch mode, view mode, language preferences, and auto-launch behavior
- **Help Documentation**: Built-in HTML help documentation in Turkish and English
- **Auto-save**: Automatically saves your launcher configuration when changes are made

## 🚀 Quick Start

### Prerequisites
- Windows 10 or later
- .NET Framework 4.7.2 or higher

### Installation

Installer (recommended):
1. Download `RiaLauncher-<version>-Setup.exe` from the [Releases](https://github.com/Riasoftapp/RiaLauncher/releases) page
2. Run it and pick the folder you want. The installer needs no administrator rights,
   can create desktop and Start Menu shortcuts, and can be removed from Windows
   "Apps & features".

Portable:
1. Download `RiaLauncher.zip` from the same release page
2. Extract the files to the location of your choice
3. Run `RiaLauncher.exe`

Every release lists the SHA-256 checksum of the installer. Because the installer is
not digitally signed yet, Windows SmartScreen may show a "Windows protected your PC"
prompt: choose **More info** and then **Run anyway**.

### Building from source

- Visual Studio 2019/2022 with the .NET Framework 4.7.2 SDK, or on the command line:
  ```
  nuget restore RiaLauncher.vbproj -PackagesDirectory ..\packages
  msbuild RiaLauncher.vbproj /p:Configuration=Release /p:Platform=AnyCPU
  iscc setup\RiaLauncher.iss
  ```
- The GitHub Actions workflow in `.github/workflows/build.yml` builds the application,
  creates the installer and the portable zip, and attaches both to tagged releases.

### Privacy policy

See [PRIVACY.md](PRIVACY.md). RiaLauncher stores all data locally and sends no
telemetry; the only network request is the update check against this repository.

### Code signing policy

See [CODE_SIGNING_POLICY.md](CODE_SIGNING_POLICY.md).

Free code signing provided by SignPath.io, certificate by SignPath Foundation.

### First Use
1. Create a new tab by right-clicking on the tab area
2. Drag and drop executable files from Windows Explorer into the launcher
3. Double-click any application to launch it
4. Customize your settings through the File menu

## 📁 Project Structure

```
RiaLauncher/
├── RiaLauncher/                 # Main application project
│   ├── Form1.vb                 # Main launcher form
│   ├── Form1.Designer.vb        # Form designer file
│   ├── SettingsForm.vb          # Settings dialog
│   ├── ManualSortForm.vb        # Manual sorting interface
│   ├── AboutForm.vb             # About dialog
│   ├── CopyMoveForm.vb          # Copy/Move utility
│   ├── LanguageManager.vb       # Multi-language support
│   └── RiaLauncher.vbproj       # Project file
├── assets/
│   ├── documentation/           # Help documentation
│   │   ├── RiaLauncherHelp/     # HTML help files
│   │   │   ├── RiaLauncherHelp-tr.html
│   │   │   └── RiaLauncherHelp-en.html
│   │   ├── images/              # Documentation images
│   ├── lang/                    # Language files
│   ├── icon/                    # Application icons
│   └── logo/                    # Logo images
├── data/
│   └── RiaLauncher.xml          # Application data storage
└── README.md                    # This file
```

## 📖 Usage

### Adding Applications
- **Drag & Drop**: The easiest method - drag executables from Windows Explorer directly into the launcher
- **Manual Entry**: Use the Add Application feature to manually enter application paths

### Organizing Applications
- **Create Tabs**: Right-click on the tab bar to create new categories
- **Rename Tabs**: Right-click on a tab to rename it
- **Manual Sorting**: Use Tools > Sort > Manual Sort to arrange applications in custom order

### Context Menu Options
Right-click on any application to access:
- **Launch**: Start the application
- **Copy/Move**: Copy or move to another tab
- **Rename**: Change the application name
- **Change Icon**: Assign a custom icon
- **Update Path**: Change the application executable path
- **Open Folder**: Open the application's directory in Windows Explorer
- **Delete**: Remove the application
- **Properties**: View application details

### Settings
Access preferences through **File > Settings** or the settings button:
- **Launch Mode**: Single or double-click to launch
- **View Mode**: Icon only, text only, or icon with text
- **Always on Top**: Keep the launcher window on top
- **Default Tab**: Set which tab opens on startup
- **Language**: Switch between Turkish and English

### Built-in Tools
Access system utilities through **Tools** menu:
- Command Prompt
- PowerShell
- Task Manager
- Services Manager
- Control Panel
- Network Center
- Device Manager
- System Information (Computer Name, IP Address)

## 🔧 Technical Details

### Language
- **VB.NET** (.NET Framework 4.7.2)
- **Windows Forms** for UI

### Key Technologies
- XML for data persistence
- Windows Registry for configuration
- COM for shortcut resolution (.lnk files)
- System.Diagnostics for process launching

### Data Storage
- **settings.ini**: Application preferences
- **RiaLauncher.xml**: Launcher items and organization
- **Logs**: Application logs in the `log` directory

## 🌐 Localization

RiaLauncher supports multiple languages through the `LanguageManager` class. Language files are located in `assets/lang/`.

### Currently Supported Languages
- 🇹🇷 Turkish (TR) - `tr.lng`
- 🇺🇸 English (EN) - `en.lng`
- 🇩🇪 German (DE) - `de.lng`
- 🇪🇸 Spanish (ES) - `es.lng`
- 🇫🇷 French (FR) - `fr.lng`
- 🇮🇹 Italian (IT) - `it.lng`
- 🇯🇵 Japanese (JA) - `ja.lng`
- 🇰🇷 Korean (KO) - `ko.lng`
- 🇨🇳 Chinese (ZH) - `zh.lng`
- 🇻🇦 Latin (LA) - `la.lng`
- 🏴 Arabic (AR) - `ar.lng`

### Adding New Languages
1. Create a new language file in `assets/lang/[CODE].lng`
2. Copy the structure from an existing language file (e.g., `en.lng`)
3. Add translations for all keys in the new language
4. Update the language selector combo box in the application
5. Rebuild and test the application

## 🆘 Help & Documentation

- **In-Application Help**: Access through **Help > Documentation**
  - Available in Turkish and English
  - HTML-based help files

- **Keyboard Shortcuts**: The launcher supports standard Windows shortcuts
- **Tooltips**: Hover over buttons for helpful information

## 🐛 Troubleshooting

### Applications Not Opening
- Verify the application path is correct
- Try updating the path through the context menu
- Check if the application requires administrator privileges

### Launcher Won't Start
- Ensure .NET Framework 4.7.2 or higher is installed
- Try running with administrator privileges
- Delete corrupted data files and let the launcher recreate them

### Drag & Drop Not Working
- Disable antivirus sandbox/containment features
- Run the launcher as administrator
- Verify your antivirus isn't blocking the operation

### XML Data Corrupted
- Delete `RiaLauncher.xml` from the `data` directory
- Restart the launcher to recreate the file

## 📝 License

This project is provided as-is. See the LICENSE file for more information.

## 🤝 Contributing

Contributions are welcome! Feel free to:
- Report issues
- Suggest new features
- Submit pull requests

## 📫 Support

For support, questions, or feedback:
- Open an [Issue](https://github.com/Riasoftapp/RiaLauncher/issues) on GitHub
- Check the [Discussions](https://github.com/Riasoftapp/RiaLauncher/discussions) section

## 👨‍💻 Author

Created and maintained by Riasoft

## 🎉 Acknowledgments

- Windows Forms Community
- .NET Framework Contributors
- All users and contributors

---

**RiaLauncher** - Simplify Your Application Launching Experience
