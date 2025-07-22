# Smart .NET Solution Organizer

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

**SmartDotNetSolutionOrganizer** is a developer productivity tool that automatically organizes .NET projects and enforces clean architecture conventions. It includes a reusable core engine and a Visual Studio extension to keep your solutions clean, modular, and maintainable.

---

## 🚀 Features

- 🧠 **Best Practices Validation**
  - Enforces PascalCase naming
  - Requires meaningful project suffixes (e.g., `Core`, `Web`, `Data`)
  - Ensures `.csproj` file name matches project name
  - Validates logical folder structure

- 🗂️ **Automatic Project Folder Organization**
  - Suggests and validates project location based on naming convention
  - e.g., `MyApp.Data` should reside in `/Data` folder

- 🛠️ **Visual Studio Integration**
  - Context menu integration in Solution Explorer
  - One-click `.NET Solution Organizer` button

- 🔧 **Configurable Rules**
  - Define your own naming suffixes and structural rules via `BestPractices.json`

- 🧱 **Modular Architecture**
  - Clean architecture approach with Core, Interface, and UI layers

---

## 📂 Project Structure

```plaintext
SmartDotNetSolutionOrganizer/
│
├── SmartSolutionOrganizer.Core/         # Core logic and models (Domain + Application layer)
│   ├── Models/                          # POCO classes like ProjectInfo, BestPracticesConfig
│   ├── Interface/                       # Interfaces for service abstractions
│   ├── Helper/                          # Utilities and configuration loaders
│   ├── Services/                        # Concrete services like SolutionScannerService
│   └── BestPractices.json              # Config file for rules (copied to output on build)
│
├── SmartSolutionOrganizer.VSExt/        # Visual Studio Extension (Presentation layer)
│   ├── Commands/                        # VS Command handler classes
│   ├── VSExtPackage.vsct               # Menu command definitions
│   ├── VSExtPackage.cs                 # VS Package main class
│   └── source.extension.vsixmanifest   # Extension manifest
│
├── SmartSolutionOrganizer.Tests/        # Unit tests for Core services and rules
│
├── .gitignore
├── SmartDotNetSolutionOrganizer.sln     # Solution file
└── README.md                            # This file
```

📦 Installation
🛠️ From Source
Clone this repository

Open SmartDotNetSolutionOrganizer.sln in Visual Studio

Set SmartSolutionOrganizer.VSExt as the startup project

Press F5 to launch an experimental VS instance

📥 (Coming Soon) From Marketplace
Will be published to the Visual Studio Marketplace for one-click install.

✅ Usage
Open any .NET solution

Right-click the solution node in Solution Explorer

Click "Organize .NET Solution"

View suggestions and issues detected in your output window

🔧 Roadmap
 Auto-fix mode (move and rename projects)

 UI panel for displaying violations

 Project/solution templates

 Integration with Roslyn analyzers

 CLI version

🤝 Contributing
Contributions and ideas are welcome! Please open an issue or a pull request.

📄 License
This project is licensed under the MIT License. See LICENSE for details.

🙌 Author
Developed by Axel Jumagdao — silently organizing code and solutions since 2016.

