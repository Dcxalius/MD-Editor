# Testing MD-Editor on a Linux VM

This guide walks through validating the repository on a Linux-based virtual machine. It focuses on the GTK head because it provides the native desktop experience on Linux and macOS.

## 1. Provision the VM

1. Install a supported Linux distribution (Ubuntu 22.04 LTS or later is recommended).
2. Ensure the VM has at least 4 vCPUs, 8 GB RAM, and hardware accelerated graphics if available. Hardware acceleration greatly improves the GTK head’s rendering performance.
3. Install Guest Additions (VirtualBox) or VMware Tools to unlock clipboard and folder sharing. This makes copying build logs and screenshots easier.
4. Update the package index and upgrade the base OS before installing prerequisites:

   ```bash
   sudo apt update && sudo apt upgrade -y
   ```

## 2. Install Prerequisites

Run the following commands after cloning the repository inside the VM:

```bash
# .NET SDK (installs the current LTS SDK and runtime)
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --channel STS

# GTK/WebKit dependencies for the Skia head
sudo apt install -y libgtk-3-dev libwebkit2gtk-4.0-dev libgtk-3-0 libwebkit2gtk-4.0-37 gstreamer1.0-plugins-base

# Optional: enable global dotnet command (adjust path as needed)
echo 'export PATH="$HOME/.dotnet:$PATH"' >> ~/.bashrc
source ~/.bashrc
```

If the VM will run Uno tooling or additional heads, install the Uno Platform prerequisites documented in [docs/hot-reload.md](hot-reload.md).

## 3. Restore the Solution

From the repository root:

```bash
dotnet restore
```

This downloads all NuGet dependencies required for the shared project and the GTK head.

## 4. Build the Solution

Execute the build once to confirm the VM can produce binaries:

```bash
dotnet build MyApp.sln
```

Investigate any warnings or errors before proceeding.

## 5. Run the GTK Head

After a successful build launch the GTK head:

```bash
dotnet run --project MyApp.Skia.Gtk/MyApp.Skia.Gtk.csproj
```

You should see a GTK window titled **MyApp** with a **File** menu that contains **New**, **Open**, **Save**, and **Exit**. Choosing a menu item prints a `"<Command> document requested"` message to the terminal. Capture a screenshot or the console output to include in pull request validation notes.

> **Note:** The shared project (`MyApp/MyApp.csproj`) is a class library. Attempting to launch it directly will produce the standard .NET message: "A project with an Output Type of Class Library cannot be started directly." Always run the GTK head when validating on Linux.

## 6. Optional: Hot Reload with `dotnet watch`

To iterate quickly, use Hot Reload:

```bash
dotnet watch --project MyApp.Skia.Gtk/MyApp.Skia.Gtk.csproj --hot-reload
```

Save changes in the shared project or the GTK head to see updates without rebuilding. If Hot Reload fails, fall back to `dotnet run` and restart the process after making changes.

## 7. Troubleshooting Tips

* **Missing GTK dependencies** – If the app fails to launch with `Gtk` or `WebKit` errors, re-run `sudo apt install` to ensure all packages are present.
* **Permission errors** – Confirm the repo directory is writable. When using shared folders, map them to a user-owned directory inside the VM.
* **Slow UI updates** – Increase the VM’s video memory allocation and enable 3D acceleration if supported by the hypervisor.
* **Logging** – Redirect `dotnet run` output to a file (`dotnet run ... | tee gtk-run.log`) to share with the team during review discussions.

Following these steps provides a repeatable Linux validation workflow that mirrors the expected behavior described in the project overview.
