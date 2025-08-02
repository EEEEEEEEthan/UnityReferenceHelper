# UnityReferenceHelper

A Unity editor extension that automates GameObject and Component referencing through custom attributes, eliminating the need for manual drag-and-drop operations in the Inspector.

## ✨ Features

- **Automatic Reference Resolution**: Use `[ObjectReference("path")]` to automatically link GameObjects and Components by path
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

```csharp
using ReferenceHelper;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Reference a child GameObject
    [ObjectReference("UI/HealthBar")] 
    public GameObject healthBarUI;
    
    // Reference a specific component
    [ObjectReference("Player/WeaponSocket")] 
    public Transform weaponSocket;
    
    // Reference nested objects
    [ObjectReference("UI/MainPanel/Settings/VolumeSlider")] 
    public Slider volumeSlider;
}
```

### Hierarchy Example
```
PlayerController (this script)
├── UI/
│   ├── HealthBar (GameObject)
│   └── MainPanel/
│       └── Settings/
│           └── VolumeSlider (Slider Component)
└── Player/
    └── WeaponSocket (Transform Component)
```

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

**Examples:**
```csharp
[ObjectReference("")]              // Current GameObject
[ObjectReference("Child")]         // Direct child named "Child"
[ObjectReference("UI/Button")]     // Child "Button" under "UI"
[ObjectReference("A/B/C")]         // Nested path A → B → C
```

## 🔍 Troubleshooting

### Common Issues

**Q: The reference isn't being set automatically**
- A: Ensure the path exactly matches your GameObject hierarchy (case-sensitive)
- Check that the target GameObject exists in the scene

**Q: "Create" button doesn't work**
- A: Make sure the field type matches what you want to create
- For Components, ensure the Component type exists and is accessible

**Q: References break when I move objects**
- A: The tool uses string paths, so moving objects in hierarchy may break references
- Simply click "Create" again or update the path in code

### Best Practices

1. **Use Clear Naming**: Name your GameObjects descriptively for easier path writing
2. **Keep Paths Short**: Avoid overly deep hierarchies when possible
3. **Group Related Objects**: Organize UI elements and game objects logically
4. **Test in Play Mode**: Ensure references work correctly during runtime

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**Ethan**
- Email: tyx1993@live.cn

## 🙏 Acknowledgments

- Unity Technologies for the excellent Editor API
- Community feedback and contributions

---

**Unity Version Compatibility:** Unity 2019.4.0f1 and later
