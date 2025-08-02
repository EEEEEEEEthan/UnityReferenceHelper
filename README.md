# UnityReferenceHelper

A Unity editor extension that automates GameObject and Component referencing through custom attributes, eliminating the need for manual drag-and-drop operations in the Inspector.

## ✨ Features

- **Path-Based Reference Linking**: Use `[ObjectReference("path")]` to link GameObjects and Components by hierarchy path
- **Smart Error Detection**: Visual indicators for missing or mismatched references
- **One-Click Fixes**: Built-in "Create" and "Clear" buttons to resolve reference issues
- **Type Safety**: Supports both GameObject and Component references with automatic type checking
- **Undo Support**: All operations are fully integrated with Unity's Undo system
- **Zero Dependencies**: Lightweight solution with no external dependencies

## 📦 Installation

### Method 1: Unity Package Manager (Recommended)
1. Open Unity Package Manager (Window → Package Manager)
2. Click the "+" button and select "Add package from git URL"
3. Enter: `https://github.com/EEEEEEEEthan/UnityReferenceHelper.git`

### Method 2: Manual Installation
1. Download the latest release from GitHub
2. Extract to your project's `Assets/` folder
3. Unity will automatically import the package

## 🚀 Quick Start

### Basic Usage

**Examples:**
```csharp
[ObjectReference("")] public GameObject test1; // Current GameObject
[ObjectReference("Child")] public GameObject test2; // Direct child named "Child"
[ObjectReference("UI/Button")] public Button test3; // Child "Button" under "UI"
[ObjectReference("A/B/C")] public MeshCollider test4; // Nested path A → B → C
```
![img_v3_02op_2ab1edc3-d2f1-4510-b54a-1e49be3723bg](https://github.com/user-attachments/assets/9e29709b-8405-4945-8d51-b8158fa138ea)

**One-Click Creation Feature:**
When referenced GameObjects or Components don't exist, the Inspector displays a red "Create" button. Click it to:
- Automatically create missing GameObject hierarchy structures
- Auto-add required Components for Component types
- Full support for Unity's Undo operations

## 🔧 How It Works

1. **Path Resolution**: The attribute uses Unity's Transform.Find() with nested path support
2. **Type Matching**: Automatically detects whether you need a GameObject or specific Component
3. **Missing Reference Detection**: Shows red "Create" button when objects don't exist
4. **Mismatch Detection**: Shows orange "Clear" button when references point to wrong objects
5. **Auto-Creation**: Creates missing GameObjects and adds required Components automatically

## 🎯 Advanced Features

### Error Indicators

| Button | Color | Description | Action |
|--------|-------|-------------|---------|
| Create | Red | Missing reference detected | Creates missing GameObject/Component |
| Clear | Orange | Reference mismatch detected | Clears incorrect reference |
| None | - | Reference is correct | No action needed |

### Supported Types
- `GameObject` - References the GameObject directly
- Any `Component` type - References the specific component, adds it if missing
- `Transform`, `Rigidbody`, `Collider`, etc. - All Unity and custom components

## 📝 API Reference

### ObjectReferenceAttribute

```csharp
[ObjectReference(string path = "")]
```

**Parameters:**
- `path` (string): Relative path from the current GameObject to the target object
  - Use "/" to separate hierarchy levels
  - Empty string references the current GameObject

## 👨‍💻 Author

**Ethan**
- Email: tyx1993@live.cn

---

**Unity Version Compatibility:** Unity 2019.4.0f1 and later
