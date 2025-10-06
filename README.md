# MD-Editor

A lightweight markdown editing experience focused on speed and simplicity.

## Quick start

```bash
dotnet restore MyApp/MyApp.sln
dotnet build MyApp/MyApp.sln
```

After the solution builds, Visual Studio will list four projects in **Solution Explorer**:

* **MyApp** – the shared class library.
* **MyApp.Windows** – WinUI 3 desktop head (default debugging target).
* **MyApp.Android** – native Android head.
* **MyApp.Skia.Gtk** – cross-platform GTK desktop head.

Set one of the head projects as the startup project before debugging. Then launch it using the table below:

| Platform | Launch command | Expected result |
| --- | --- | --- |
| Windows | Start **MyApp.Windows (Unpackaged)** from Visual Studio or run `dotnet run --project MyApp.Windows/MyApp.Windows.csproj` from an elevated Windows shell. | A 1024×768 window titled **MyApp** opens with a command bar containing **New**, **Open**, and **Save** buttons. Clicking any button writes `"<Command> document requested"` to the debug/output console. |
| Linux/macOS | `dotnet run --project MyApp.Skia.Gtk/MyApp.Skia.Gtk.csproj` | A GTK window titled **MyApp** appears with a **File** menu that contains **New**, **Open**, **Save**, and **Exit**. Choosing a menu item prints `"<Command> document requested"` to the terminal running the app. |
| Android | Deploy **MyApp.Android** from Visual Studio or run `dotnet build MyApp.Android/MyApp.Android.csproj -t:Run -f net8.0-android`. | The emulator/device launches a simple screen ready for further UI work. |

The shared project (`MyApp/MyApp.csproj`) is a class library. Starting it directly results in the standard Visual Studio message **"A project with an Output Type of Class Library cannot be started directly"**. Always run one of the platform heads instead.

## Configuration

No configuration options have been provided by the user at this time.

## Advanced Usage

Additional modes or advanced workflows have not been supplied by the user.

## Troubleshooting & FAQ

* [Testing on a Linux VM](docs/linux-vm-testing.md) – step-by-step guidance for provisioning a Linux virtual machine, installing dependencies, and running the GTK head.

### Hot Reload Workflow

MD-Editor targets multiple heads (Windows, Android, Skia.Gtk). We use the Windows head as the primary Hot Reload target. See [docs/hot-reload.md](docs/hot-reload.md) for setup guidance, validated launch commands, and platform-specific notes.
## Overview
MD-Editor is a markdown-focused editing project. This repository will grow to include the core editor implementation, supporting assets, and documentation so contributors can help shape the experience.

## Contributing
Contributions are welcome! To propose improvements or report bugs, please open an issue describing the change or problem in detail. When you're ready to contribute code, fork the repository, create a feature branch, and open a pull request that references the relevant issue and includes clear testing notes. Be sure to follow any repository-specific guidelines noted in the issue templates or pull request checklist.

## Support
For questions or assistance, please open a GitHub issue so maintainers and the community can respond and track follow-up actions.

## License
This project is distributed under the terms of the [MIT License](LICENSE).

MD-Editor is a lightweight Markdown editor designed to make drafting and previewing Markdown documents fast, simple, and distraction-free

