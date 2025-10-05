# Codex Tasks: Uno Platform Typical Workflow

## Task 1: Install Uno Platform tooling
- **Goal:** Ensure development environment has the Uno Platform templates and dependencies installed.
- **Steps:**
  1. Install Visual Studio 2022 with the Uno Platform extension or set up the `uno.check` CLI and `dotnet new unoapp` templates.
  2. Verify workload installation (Windows, Android) and confirm the Uno templates appear in `dotnet new --list`.
- **Acceptance Criteria:** Uno templates are available in the tooling of choice and workloads restore without errors.

## Task 2: Scaffold the multi-platform solution
- **Goal:** Generate the Uno Platform solution targeting Windows, Skia.Gtk (Linux), and Android.
- **Steps:**
  1. Run the Uno project generator (Visual Studio wizard or `dotnet new unoapp`) selecting the required heads.
  2. Restore NuGet packages and build each project to ensure the scaffold compiles.
- **Acceptance Criteria:** Solution builds successfully with Windows, Skia.Gtk, and Android heads referencing the shared project.

## Task 3: Implement shared UI and services
- **Goal:** Define the core application UI and shared services using WinUI XAML and MVVM patterns.
- **Steps:**
  1. Create shared XAML pages, view models, and services in the common project.
  2. Wire up dependency injection and navigation so all heads can reuse the shared logic.
- **Acceptance Criteria:** Shared project compiles independently and renders the initial UI when launched from any head.

## Task 4: Add platform-specific integrations
- **Goal:** Provide per-platform capabilities required for Windows, Linux (Skia.Gtk), and Android.
- **Steps:**
  1. Implement platform-specific services or partial classes in each head (e.g., Android permissions, Linux window chrome customization).
  2. Update project manifests or configuration files as needed for each platform.
- **Acceptance Criteria:** Platform-specific features compile and behave correctly on their respective targets without breaking shared functionality.

## Task 5: Enable and validate Hot Reload workflows
- **Goal:** Accelerate iteration by configuring XAML and C# Hot Reload for the primary development target and confirming parity across others.
- **Steps:**
  1. Configure Hot Reload settings in the IDE/CLI for the primary target (e.g., Windows or Android).
  2. Test Hot Reload updates on each head to confirm UI changes apply without a full rebuild.
- **Acceptance Criteria:** Hot Reload works reliably on the chosen primary target and basic validations succeed on the remaining heads.
