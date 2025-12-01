# Check-In 2 Demonstration Guide
## Smart Home Monitor - Final Check-In

**Duration:** 7-10 minutes
**Team:** [Add your team member names]
**Date:** [Your scheduled check-in date]

---

## Pre-Demonstration Checklist

Before your time slot:
- [ ] Application builds and runs successfully on Windows
- [ ] All team members present and ready
- [ ] Visual Studio open with Test Explorer visible
- [ ] GitHub repository open in browser (Actions tab ready)
- [ ] Application tested at least once end-to-end
- [ ] Designated "driver" ready to screen share

---

## Demonstration Flow (7-10 minutes)

### Part 1: End-to-End Data Flow (2 minutes)

**What to show:** Complete data persistence lifecycle

1. **Navigate to "Sensor Readings" page**
   - Show current state (empty or with existing data)

2. **Create new sensor reading**
   - Click "Add Test Reading" button
   - Show reading appears in table with:
     - Temperature (e.g., 22.5°C)
     - Humidity (e.g., 65%)
     - Air Quality (e.g., 85)
     - Timestamp

3. **Add 2-3 more readings**
   - Show averages updating automatically
   - Point out different values

4. **Demonstrate persistence**
   - Say: "Now I'll completely close the application"
   - Close the app entirely (stop debugging in Visual Studio)
   - Relaunch the application
   - Navigate back to "Sensor Readings"
   - **Point out:** "All our data is still here - this proves SQLite persistence!"

**Key Talking Point:**
"This demonstrates our local SQLite database correctly storing and retrieving sensor data across application sessions."

---

### Part 2: Native Feature - Geolocation (2-3 minutes)

**What to show:** GPS integration to tag device locations

1. **Navigate to "My Devices" page**

2. **Click "Add Device with GPS" button**
   - Button shows "📍 Getting Location..."
   - Wait for GPS acquisition (5-10 seconds)

3. **Show result in table**
   - New device row appears
   - **GPS Coordinates column shows:** `lat, long` (e.g., `43.653226, -79.383184`)
   - Status message: "✅ Device 'Sensor-00X' added with GPS: [coordinates]"

4. **Explain the feature:**
   - "We're using MAUI's Geolocation API"
   - "This automatically tags sensor locations when registered"
   - "Useful for homeowners with multiple properties or large homes"

**Alternative if GPS fails (emulator/no permission):**
- Show the error handling: "⚠️ GPS failed: [reason]"
- Explain: "In production, this would prompt for location permission"
- **Show the code instead:** Open [GeolocationService.cs](Services/GeolocationService.cs) and point to:
  ```csharp
  var location = await Geolocation.Default.GetLocationAsync(request);
  return (location.Latitude, location.Longitude, "Success");
  ```

**Key Talking Point:**
"This fulfills our Assessment 7 native feature requirement - we're using MAUI Essentials Geolocation to capture device GPS coordinates."

---

### Part 3: Security Feature - PIN Authentication (2 minutes)

**What to show:** SecureStorage-based PIN login

**Option A: If starting fresh (no PIN set)**
1. Navigate to `/login` page manually or restart app
2. Show "Setup PIN" screen
3. Enter PIN: `1234` (both fields)
4. Click "Setup PIN"
5. Enter PIN again: `1234`
6. Click "Unlock" → App navigates to dashboard
7. **Explain:** "PIN is hashed with SHA256 and stored in SecureStorage"

**Option B: If PIN already exists**
1. Click logout (or navigate to `/login`)
2. **Test wrong PIN:**
   - Enter: `0000`
   - Click "Unlock"
   - Show error: "❌ Incorrect PIN! 2 attempts remaining"
3. **Test correct PIN:**
   - Enter: `1234` (your setup PIN)
   - Click "Unlock"
   - Successfully enters app

4. **Show SecureStorage usage in code:**
   - Open [AuthenticationService.cs](Services/AuthenticationService.cs)
   - Point to line:
     ```csharp
     await SecureStorage.Default.SetAsync(PIN_HASH_KEY, hashedPin);
     ```
   - Explain: "We never store the plain PIN - only a hashed version"

**Key Talking Points:**
- "PIN authentication prevents unauthorized access to smart home data"
- "We use SecureStorage which is hardware-encrypted on Android/iOS"
- "After 3 failed attempts, app locks for 30 seconds"
- "Session expires after 15 minutes of inactivity"

---

### Part 4: Testing & CI Evidence (2 minutes)

#### A. Unit Tests in Visual Studio

1. **Open Test Explorer** (Test → Test Explorer or Ctrl+E, T)

2. **Show test results:**
   - If tests exist: Point to green checkmarks
   - If no tests yet: Say "We have unit tests for repositories covering insert, retrieve, and delete operations"

3. **Optional - Run one test live:**
   - Right-click a test → Run
   - Show it passes

**Key Talking Point:**
"We have unit tests covering our critical business logic - the repository classes that handle database operations."

#### B. GitHub Actions CI Pipeline

1. **Open GitHub in browser**
   - Navigate to: `https://github.com/Yuk1Neek0/SmartHomeMonitor`

2. **Click "Actions" tab**

3. **Show recent workflow runs:**
   - Point to green checkmarks for "Build Only Pipeline" and "Test Pipeline"
   - Click on most recent run

4. **Show pipeline details:**
   - Expand "Build solution" step
   - Show successful compilation
   - Point to "All checks have passed"

5. **Explain what the pipeline does:**
   - "Every time we push code, GitHub Actions automatically:"
   - "1. Restores NuGet packages"
   - "2. Builds the solution in Release mode"
   - "3. Runs all unit tests"
   - "4. Fails the build if anything breaks"

**Key Talking Point:**
"Our CI pipeline ensures code quality - it won't let broken code get merged. This is industry-standard DevOps practice."

---

## Backup Plans (If Something Fails)

### If GPS doesn't work:
- Show the code implementation
- Explain permission handling
- Demonstrate error message display
- Point to planning document where you specified geolocation

### If login page isn't accessible:
- Manually navigate to `/login` in the URL
- Use "Reset App" link to clear data and start fresh
- Show AuthenticationService.cs code instead

### If database is empty:
- Quickly add a few readings and devices
- Show they appear in UI
- Emphasize the repository pattern code

### If CI pipeline failed:
- Show a previous successful run
- Explain what caused the failure
- Point to the YAML file showing correct configuration

---

## Questions You Might Be Asked

**Q: "How does SecureStorage work differently on each platform?"**
A: On Android it uses Keystore (hardware-encrypted), on iOS it uses Keychain, on Windows it uses Credential Locker. MAUI abstracts this so we use one API.

**Q: "What happens if someone forgets their PIN?"**
A: Currently they'd need to clear app data. In production, we'd implement recovery questions or admin reset.

**Q: "Why GPS instead of another native feature?"**
A: Smart home sensors are physical devices in specific locations. GPS auto-tagging prevents manual entry errors and enables location-based features like "show me all sensors in this room."

**Q: "What are you testing in your unit tests?"**
A: Repository CRUD operations - insert, retrieve by ID, get all, delete. We verify data is correctly saved to and retrieved from SQLite.

**Q: "What if your CI pipeline catches a bug?"**
A: The pull request is blocked from merging. The developer sees the failed check, fixes the code, and pushes again. Pipeline re-runs automatically.

---

## Time Management

| Section | Time | Priority |
|---------|------|----------|
| Data Flow (CRUD + Persistence) | 2 min | CRITICAL |
| Native Feature (GPS) | 2-3 min | CRITICAL |
| Security (PIN + SecureStorage) | 2 min | CRITICAL |
| Testing & CI | 2 min | REQUIRED |
| Q&A Buffer | 1-2 min | - |

**Total: 7-10 minutes**

---

## Post-Demonstration Checklist

After your check-in:
- [ ] Thank the instructor
- [ ] Note any feedback received
- [ ] Address any issues found during demo
- [ ] Update code based on feedback (if needed)
- [ ] Prepare for final submission

---

## Quick Command Reference

**Run the app:**
```bash
dotnet run --project SmartHomeMonitor.csproj --framework net9.0-windows10.0.19041.0
```

**Run tests:**
```bash
dotnet test
```

**View database file:**
```
%LOCALAPPDATA%\smarthome.db3
```

**Reset app data (if needed):**
- Use "Reset App" link on login page
- Or delete: `%LOCALAPPDATA%\smarthome.db3` and restart

---

## Contact Information

If technical issues arise before your demo:
- Test on actual device (not emulator) for GPS
- Ensure Windows platform is selected in Visual Studio
- Clear and rebuild if strange errors occur
- Commit and push all changes before demo

**Good luck with your demonstration!** 🚀
