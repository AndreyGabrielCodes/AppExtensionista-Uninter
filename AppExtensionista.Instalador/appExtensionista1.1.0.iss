#define MyAppName "Controle De Alimentos"
#define MyAppVersion "1.1.0"
#define MyAppPublisher "Andrey Gabriel de Andrade Moraes - RU 4731589"
#define MyAppExeName "AppExtensionista.exe"
#define MyIconPath "C:\PROJETOS\AppExtensionista-Uninter\AppExtensionista\appextensionista.ico"

; Pasta publish do projeto
#define SourcePath "C:\PROJETOS\AppExtensionista-Uninter\AppExtensionista\bin\Release\net8.0\win-x64\publish"

[Setup]
AppId={{9B23C456-7890-5EFG-0123-456789ABCDEF}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\ControleDeAlimentos
OutputBaseFilename=AppExtensionista_Setup
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "brazilianportuguese"; MessagesFile: "compiler:Languages\BrazilianPortuguese.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
; Inclui todos os arquivos e pastas da pasta publish
Source: "{#SourcePath}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#MyIconPath}"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
; Define os atalhos usando o ícone instalado
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\appextensionista.ico"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\appextensionista.ico"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent