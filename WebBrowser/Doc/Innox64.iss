; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{FA08E3BC-3813-4C2C-91BF-07869609D9F0}
AppName=平安力合浏览器
AppVersion=1.0
;AppVerName=平安力合浏览器 1.0
AppPublisher=北京平安力合科技发展股份有限公司
AppPublisherURL=http://p-an.com/
AppSupportURL=http://p-an.com/
AppUpdatesURL=http://p-an.com/
DefaultDirName={pf}\平安力合浏览器
DefaultGroupName=平安力合浏览器
AllowNoIcons=yes
OutputBaseFilename=平安力合浏览器x64
Compression=lzma
SolidCompression=yes

[Languages]
Name: "chinesesimplified"; MessagesFile: "compiler:Languages\ChineseSimplified.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "C:\WorkSpacesC\PanSvn\tvm-cash\trunk\Test\WebBrowser\WebBrowser\bin\x64\Release\WebBrowserWPF.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\WorkSpacesC\PanSvn\tvm-cash\trunk\Test\WebBrowser\WebBrowser\bin\x64\Release\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\平安力合浏览器"; Filename: "{app}\WebBrowserWPF.exe"
Name: "{group}\{cm:ProgramOnTheWeb,平安力合浏览器}"; Filename: "http://p-an.com/"
Name: "{group}\{cm:UninstallProgram,平安力合浏览器}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\平安力合浏览器"; Filename: "{app}\WebBrowserWPF.exe"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\平安力合浏览器"; Filename: "{app}\WebBrowserWPF.exe"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\WebBrowserWPF.exe"; Description: "{cm:LaunchProgram,平安力合浏览器}"; Flags: nowait postinstall skipifsilent

