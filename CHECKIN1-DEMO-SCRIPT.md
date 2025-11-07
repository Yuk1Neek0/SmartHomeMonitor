# Check-In 1 - Demonstration Script
**Project**: Smart Home Environmental Monitor
**Duration**: 5-7 minutes
**Status**: ✅ Ready for Demo

---

## Pre-Demo Checklist

- [ ] Project builds successfully (`dotnet build`)
- [ ] Visual Studio open with SmartHomeMonitor.csproj loaded
- [ ] GitHub repository open in browser
- [ ] Practice run completed
- [ ] All team members present

---

## Demonstration Flow

### Opening (30 seconds)

"Hi, I'm Sikai Han. For my capstone project, I'm building a Smart Home Environmental Monitor that tracks sensor readings from IoT devices and integrates with the OpenWeatherMap API for comprehensive environmental monitoring."

---

### Part 1: UI Walkthrough (1-2 minutes)

**Action**: Launch the application
```bash
cd SmartHomeMonitor
dotnet run -f net9.0-windows10.0.19041.0
```

**Script**:
1. "Here's our main Dashboard showing statistics for devices and sensor readings"
2. *Click "Devices" in navigation*
3. "This is our Devices page where users can register and manage IoT sensors"
4. *Click "Sensor Readings" in navigation*
5. "And this is the Sensor Readings page where we display all environmental data"
6. "These screens align with the mockups we created in Assessment 2"

---

### Part 2: Data Models (1 minute)

**Action**: Switch to Visual Studio

**Script**:
1. "Let me show you our data models"
2. *Navigate to Models folder in Solution Explorer*
3. *Open SensorReading.cs*
4. "This is our SensorReading model with properties for Temperature, Humidity, Air Quality, and Timestamp"
5. *Scroll to show SQLite attributes*
6. "It uses SQLite attributes like [PrimaryKey] and [AutoIncrement]"
7. "This model, along with User, Device, and Alert models, matches our ERD from Assignment 4"

---

### Part 3: **DATABASE PERSISTENCE PROOF** ⭐ (3 minutes)

**THIS IS THE MOST CRITICAL PART**

**Action**: Switch back to running application

**Script**:

#### Step 1: Show Initial State
"First, let me show you the current state of our database"
- Navigate to "Sensor Readings" page
- Note the current count (e.g., "Currently we have 0 readings")

#### Step 2: Create Data
"Now I'll add a test sensor reading"
- Click **"Add Test Reading"** button
- *Point to the new row that appears*
- "You can see a new reading just appeared with a temperature, humidity, and air quality value"
- "Note the Reading ID - let's say it's ID number 1"
- *Click "Add Test Reading" 2-3 more times*
- "I'll add a few more readings so we have some data"
- "We now have 4 readings stored"

#### Step 3: Close Application
"Now, this is the critical part - I'm going to completely close the application"
- Close the application window
- "The application is now completely closed"
- *Optional: Show Task Manager to prove it's not running*

#### Step 4: Relaunch Application
"Let's relaunch the application"
- Start the application again
- Wait for it to load

#### Step 5: Verify Persistence
"And now, let's navigate back to Sensor Readings"
- Click "Sensor Readings" in navigation
- *Point to the data*
- "**The data is still here!**"
- "Same Reading IDs, same temperatures, same timestamps"
- "This proves our data is being persisted to the SQLite database"
- "When we restarted, the Repository's GetAllAsync method loaded all readings from the database"

---

### Part 4: Code Walkthrough (1-2 minutes)

**Action**: Switch to Visual Studio

**Script**:

#### Repository Pattern
1. *Open SensorReadingRepository.cs*
2. "This is our Repository class that handles all database operations"
3. *Scroll to constructor*
4. "The database file is stored in the LocalApplicationData folder"
5. *Show InsertAsync method*
6. "This method saves new sensor readings to SQLite"
7. *Show GetAllAsync method*
8. "And this retrieves all readings, ordered by timestamp"

#### Dependency Injection
1. *Open MauiProgram.cs*
2. *Scroll to lines 27-28*
3. "Here's where we register our repositories with Dependency Injection as Singletons"
4. "This allows our Razor pages to inject these services and use them"

---

### Closing (30 seconds)

"So to summarize:"
- "✅ We have a working .NET MAUI application"
- "✅ With UI screens for Dashboard, Devices, and Sensor Readings"
- "✅ Data models that match our ERD"
- "✅ Repository Pattern for database operations"
- "✅ And we've proven SQLite persistence by adding data, closing the app, and showing it persists after relaunch"

"Next steps are integrating the OpenWeatherMap API for real-time weather data and building out the alert system."

"Thank you! Do you have any questions?"

---

## Common Questions & Prepared Answers

### Q: "Where exactly is the database file stored?"
**A**: "It's stored at `C:\Users\[Username]\AppData\Local\[AppName]\smarthome.db3`. This path is set in the SensorReadingRepository constructor using `Environment.SpecialFolder.LocalApplicationData`."

### Q: "How does the Repository Pattern benefit your application?"
**A**: "It provides a clean separation between the UI and data access logic. This makes the code more maintainable and testable. If we wanted to switch from SQLite to another database, we'd only need to change the Repository implementation."

### Q: "Show me how Dependency Injection works"
**A**:
1. *Show MauiProgram.cs registration*: "Here we register the services"
2. *Show Readings.razor line 4*: "@inject IRepository<SensorReading> ReadingRepository"
3. "The framework automatically injects the SensorReadingRepository instance when the page loads"

### Q: "What if the database doesn't exist on first launch?"
**A**: "The `InitializeAsync()` method in the Repository checks if the database exists, and if not, creates the table using `CreateTableAsync<SensorReading>()`."

### Q: "How do you prevent duplicate data?"
**A**: "Each record has an auto-incrementing primary key (ReadingId), so each entry is unique. We're not checking for duplicates in this version since each sensor reading is timestamped and represents a distinct measurement."

### Q: "What's next for Check-In 2?"
**A**:
- "Integrating the OpenWeatherMap API to fetch real-time weather data"
- "Implementing the Alert system to notify users when thresholds are exceeded"
- "Adding user authentication"
- "Creating data visualization with charts"

---

## Emergency Backup

If something breaks during the demo:

### Backup Plan A: Use Screenshots
- Have screenshots ready of:
  - Dashboard
  - Devices page
  - Sensor Readings with data
  - Code (Repository and Models)

### Backup Plan B: Explain What Should Happen
"The application should show [describe expected behavior]. Let me walk you through the code to explain how it works..."

### Backup Plan C: Use GitHub
"I can show you the complete source code on GitHub and walk through how the persistence works in the code"

---

## File Reference Guide

Quick reference for navigation during demo:

**Models** (Show structure):
- `Models/SensorReading.cs` - Main demo model
- `Models/Device.cs`
- `Models/User.cs`
- `Models/Alert.cs`

**Services** (Show Repository Pattern):
- `Services/IRepository.cs` - Generic interface
- `Services/SensorReadingRepository.cs` - Implementation

**UI Pages** (Show functionality):
- `Components/Pages/Home.razor` - Dashboard
- `Components/Pages/Devices.razor` - Device management
- `Components/Pages/Readings.razor` - **Main demo page**

**Configuration**:
- `MauiProgram.cs` lines 27-28 - DI registration

---

## Success Criteria

By the end of your demo, the instructor should clearly see:

1. ✅ Working .NET MAUI application
2. ✅ 2-3 main UI screens with navigation
3. ✅ Data models aligned with ERD
4. ✅ Repository Pattern implemented
5. ✅ Services registered with Dependency Injection
6. ✅ **PROOF of database persistence** (add → close → relaunch → data persists)
7. ✅ Clean, organized code on GitHub

---

## Time Management

- **0:00-0:30** - Introduction
- **0:30-2:00** - UI Walkthrough
- **2:00-3:00** - Data Models
- **3:00-6:00** - **Database Persistence Proof** ⭐
- **6:00-7:00** - Code Walkthrough
- **7:00** - Closing & Questions

**Total**: 7 minutes (can compress to 5 if needed)

---

## Final Pre-Demo Check

Run through this the night before:

1. [ ] `dotnet build` succeeds
2. [ ] Application launches successfully
3. [ ] Can add test readings
4. [ ] Can close and relaunch app
5. [ ] Data persists across restarts
6. [ ] All team members know their parts
7. [ ] GitHub repository is up-to-date
8. [ ] README.md is complete

---

Good luck! 🚀 You've got this!
