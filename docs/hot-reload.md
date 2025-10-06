# Hot Reload Workflow

This guide explains how to run each MD-Editor head with XAML and C# Hot Reload so day-to-day UI work can happen without full rebuilds. The **primary development target** for Hot Reload is `MD-Editor.Windows` (WinUI). Android and Skia.Gtk heads are also covered below, along with their current limitations.

> **Prerequisites**
>
> * Windows development machine with Visual Studio 2022 17.9+ and the Uno Platform extension, or a shell with the .NET 7+ SDK and the `uno.check` tool.
> * Restored NuGet dependencies (`dotnet restore`).
> * Device/emulator provisioning for Android and the GTK runtime available on Linux/macOS when working with Skia.Gtk.

## 1. Enable Hot Reload in Visual Studio

1. Open the solution and set **MD-Editor.Windows** as the startup project.
2. Navigate to **Tools ▸ Options ▸ Debugging ▸ Hot Reload**.
3. Enable both **XAML Hot Reload** and **.NET/C# Hot Reload**.
4. In the debug dropdown select **MD-Editor.Windows (Unpackaged)** or **MD-Editor.Windows (Packaged)** and ensure **Hot Reload on file save** is checked in the toolbar.
5. Start debugging (`F5`) or use **Hot Reload in Debugger** to launch once. Subsequent XAML edits in the shared project or the Windows head will apply immediately.

### Recommended Visual Studio workflow

* Keep the **Live Visual Tree** window visible to confirm that XAML reloads succeed.
* When editing C#, prefer using **Apply Code Changes** from the Hot Reload toolbar after saving.
* If a XAML change fails to apply, Visual Studio surfaces an in-editor warning; resolve the error or use **Restart Required** to rebuild once.

## 2. CLI Hot Reload with `dotnet watch`

For contributors working outside Visual Studio (for example on macOS or Linux) Hot Reload is available through `dotnet watch`.

```bash
dotnet watch --project src/MD-Editor.Windows/MD-Editor.Windows.csproj --hot-reload --no-launch-profile
```

* The command builds the Windows head once, attaches the Hot Reload agent, and watches for XAML/C# changes.
* Passing `--no-launch-profile` avoids Visual Studio-specific launch profile requirements. Supply `/p:UnoEnableHotReload=true` if the solution introduces a custom target that disables the Uno hooks.

> **Tip:** The Windows head must execute on Windows. When running the command from a non-Windows shell use PowerShell remoting or Visual Studio’s Dev Tunnels to execute remotely.

## 3. Validating Hot Reload on Windows (Primary Head)

1. Start either Visual Studio debugging or the `dotnet watch` command above.
2. After the application window opens, edit a XAML file in the shared project (for example `MainPage.xaml`).
3. Save the file; the UI should update in-place without rebuilding.
4. Repeat with a simple C# change inside the page’s code-behind (e.g., tweak a label string) and use the **Apply Code Changes** button.
5. Confirm that no full rebuild occurred by reviewing the **Hot Reload** output window or the `dotnet watch` console logs (look for `Hot reload applied successfully`).

Record any issues in an engineering log or GitHub issue so we can track regressions.

## 4. Android Head (`MD-Editor.Mobile`) Hot Reload

* Launch Visual Studio with the **Android** startup project or execute:

  ```bash
  dotnet watch --project src/MD-Editor.Mobile/MD-Editor.Mobile.csproj --hot-reload --framework net8.0-android
  ```

* The Android tooling requires an emulator or device connected before running `dotnet watch`.
* XAML Hot Reload supports most view changes; C# Hot Reload is limited while the app is running under the Mono runtime. Visual Studio will flag unsupported edits (for example method signature changes) and request a rebuild.
* Expect the initial deployment to take longer because the application package must be deployed to the device. Once running, Hot Reload applies most UI tweaks without reinstalling.

## 5. Skia.Gtk Head (`MD-Editor.Skia.Gtk`) Hot Reload

* Start the head via:

  ```bash
  dotnet watch --project src/MD-Editor.Skia.Gtk/MD-Editor.Skia.Gtk.csproj --hot-reload --framework net8.0
  ```

* On Linux hosts ensure GTK 3 and WebKitGTK packages are installed. On macOS install them via Homebrew prior to running the command.
* Hot Reload applies XAML updates instantly; however, changes to platform-specific C# files (e.g., GTK window chrome customizations) may require a manual restart.
* Some GPU drivers on Linux block dynamic shader recompilation. If rendering artifacts appear after Hot Reload, restart the watch session.

## 6. Known Limitations & Troubleshooting

* **Large refactors** (adding/removing `x:Name`, modifying resource dictionaries) may require a rebuild on all heads.
* **Android**: When using `dotnet watch`, keep `adb logcat` open. If the app disconnects, stop and re-run the watch command so the Mono runtime reattaches Hot Reload.
* **Skia.Gtk**: GTK windows launched via `dotnet watch` may stay resident after closing. Kill the process (`pkill -f MD-Editor.Skia.Gtk`) before starting a new session.
* **Primary target unavailable**: If you cannot run the Windows head locally (e.g., working from macOS), configure a Windows VM or Dev Box and forward your repo via source control syncing.

## 7. Capturing configuration for PRs

When validating changes, record:

* The head (`MD-Editor.Windows`, `MD-Editor.Mobile`, or `MD-Editor.Skia.Gtk`).
* Command or launch profile used.
* Whether Hot Reload succeeded for XAML and/or C# edits.
* Any failures or required rebuilds.

Include these notes in pull request descriptions so reviewers can reproduce the workflow.

## Appendix: IDE Checklist

* ✔ Enable XAML + C# Hot Reload in Visual Studio options.
* ✔ Confirm **MD-Editor.Windows** is set as the primary startup project.
* ✔ Verify `dotnet watch --hot-reload` is available (run `dotnet watch --version`).
* ✔ Keep diagnostics windows open (Hot Reload output, Live Visual Tree, Android Device Log).

Following the practices above keeps iteration fast while ensuring every platform head remains testable.
