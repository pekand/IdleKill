# IdleKill

**IdleKill** is a lightweight background utility for Windows that automatically puts your PC to **sleep** or **hibernate** after a specified idle interval.

It includes a system tray icon, popup notifications, audio alerts, and customizable settings — perfect for power-saving, break reminders, or keeping control over your idle time.

---

## 🧠 Features

- ✅ Run silently in the **background**
- ⏱️ Automatically **sleep or hibernate** PC after N seconds of inactivity
- 🔔 **Popup notification + beep** 10 seconds before action
- 🛠️ Option to **set idle interval** via GUI
- 🧾 **Log file** with detailed event history
- 🚀 Option to **autorun at startup**
- 🧊 **System tray icon** with full control (Windows 11)
- 🖱️ **Double-click tray icon** to open quick settings
- 📋 **Right-click context menu** for advanced options

---

## 📦 How to Use

### 💡 First Run

1. Start `IdleKill.exe`
2. The app will minimize to the **tray area** (bottom-right)
3. Double-click the tray icon to:
   - Enter **idle interval** in seconds
   - Press `Set` to apply

### ⚙️ Tray Context Menu

Right-click the tray icon to open the menu:

#### Main Options
- 🔄 **Set Interval** – same as double-click
- 🛠️ **Options** (submenu):
  - 🌙 `Hibernate` – switch to hibernation mode
  - 💤 `Sleep` – switch to sleep mode
  - 🚀 `Autorun` – enable/disable app at Windows startup
  - 📂 `View Log` – open log file in the system's default text viewer
  - 🧹 `Clear Log` – empties the log file

#### Exit
- ❌ `Exit` – closes IdleKill completely

---

## 📁 Files

- `IdleKill.exe` – main executable
- `log.txt` – log file (created in the same folder)

---

## 🔊 Notifications

- 10 seconds before the PC sleeps/hibernates, a **popup notification** will appear and a **beep sound** will play to alert the user.

---

## 🚀 Autorun

- When enabled, IdleKill adds itself to the Windows **Startup folder**
- It will launch silently with the system

---

## 🔐 Permissions

- IdleKill does **not** require admin rights.
- It stores config and logs in the same folder as the `.exe`

---

## 🛠️ System Requirements

- Windows 11 (works on Windows 10 too)
- .NET 6.0+ (or bundled runtime)

---

## 🧑‍💻 License

MIT License – free to use, modify, and distribute.

---

## ❤️ Credits

Developed with C# by someone who’s tired of leaving the PC on by accident 😄

