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

