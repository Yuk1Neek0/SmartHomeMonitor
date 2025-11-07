# Smart Home Environmental Monitor
**Capstone Project - Check-In 1**
**Team Member**: Sikai Han
**Course**: Mobile Application Development

---

## Project Overview

Smart Home Environmental Monitor is a .NET MAUI Blazor application designed to track and monitor environmental conditions in a smart home environment. The application integrates local sensor data with external weather information from the OpenWeatherMap API to provide comprehensive environmental monitoring.

---

## Features Implemented (Check-In 1)

### ✅ Local Data Models
- `User` - User account information
- `Device` - IoT device registration
- `SensorReading` - Environmental sensor data (Temperature, Humidity, Air Quality)
- `Alert` - Environmental alerts and notifications

All models align with the Entity-Relationship Diagram from Assignment 4.

### ✅ Repository Pattern
- `IRepository<T>` - Generic repository interface
- `SensorReadingRepository` - SQLite operations for sensor data
- `DeviceRepository` - SQLite operations for device management

### ✅ Dependency Injection
Services are registered in [MauiProgram.cs:27-28](MauiProgram.cs#L27-L28):
- SensorReadingRepository as Singleton
- DeviceRepository as Singleton

### ✅ UI Components
1. **Dashboard** ([Home.razor](Components/Pages/Home.razor)) - Overview with statistics cards
2. **Devices** ([Devices.razor](Components/Pages/Devices.razor)) - Device management interface
3. **Sensor Readings** ([Readings.razor](Components/Pages/Readings.razor)) - Complete sensor data display

### ✅ Database Persistence Proof
The application demonstrates full SQLite persistence:
1. Click "Add Test Reading" to create data
2. Data appears in the UI
3. Close the application completely
4. Relaunch and navigate to Sensor Readings
5. **Data persists** - proving SQLite database storage

---

## External API Plan

### OpenWeatherMap API
**Documentation**: https://openweathermap.org/api

**Endpoint 1: Current Weather Data**
- **HTTP Method**: GET
- **Endpoint**: `/weather`
- **Parameters**:
  - `q` - City name
  - `appid` - API key
  - `units` - "metric" for Celsius
- **Example**: `https://api.openweathermap.org/data/2.5/weather?q=Toronto&appid=YOUR_API_KEY&units=metric`

**Endpoint 2: Air Quality Data**
- **HTTP Method**: GET
- **Endpoint**: `/air_pollution`
- **Parameters**:
  - `lat` - Latitude
  - `lon` - Longitude
  - `appid` - API key
- **Example**: `https://api.openweathermap.org/data/2.5/air_pollution?lat=43.65&lon=-79.38&appid=YOUR_API_KEY`

---

## Technical Stack

- **.NET 9.0 MAUI** - Cross-platform framework
- **Blazor** - Web-based UI components
- **SQLite** - Local database (sqlite-net-pcl)
- **Repository Pattern** - Data access abstraction
- **Dependency Injection** - Service management
- **OpenWeatherMap API** - External weather data (planned for Check-In 2)

---

## Database Schema

Based on ERD from Assignment 4:

**Users Table**
- UserId (PK), Username, Email, PasswordHash, CreatedAt

**Devices Table**
- DeviceId (PK), UserId (FK), DeviceName, Location, DeviceType, IsActive, RegisteredAt

**SensorReadings Table** (Primary focus for Check-In 1)
- ReadingId (PK), DeviceId (FK), Temperature, Humidity, AirQuality, Timestamp

**Alerts Table**
- AlertId (PK), UserId (FK), DeviceId (FK), AlertType, Message, Severity, IsRead, CreatedAt

---

## How to Run

### Option 1: Visual Studio 2022 (Recommended)

1. Open `SmartHomeMonitor.csproj` in Visual Studio 2022
2. Select **"Windows Machine"** from the target dropdown
3. Press **F5** or click the green play button
4. Application will launch as a Windows desktop app

### Option 2: Command Line

```bash
cd SmartHomeMonitor
dotnet build
dotnet run -f net9.0-windows10.0.19041.0
```

### Option 3: Visual Studio Code

1. Install .NET MAUI extension
2. Open folder in VS Code
3. Press F5 to run

---

## Check-In 1 Demonstration Guide

### Preparation
1. Ensure application builds successfully
2. Test the persistence flow beforehand
3. Have Visual Studio and GitHub open
4. Practice the 5-7 minute demonstration

### Demonstration Flow (5-7 minutes)

#### 1. UI Walkthrough (1 minute)
- Launch application
- Show Dashboard with statistics cards
- Navigate to Devices page
- Navigate to Sensor Readings page

#### 2. Data Models (1 minute)
- Open Visual Studio
- Show `Models` folder structure
- Open [SensorReading.cs](Models/SensorReading.cs)
- Explain alignment with ERD

#### 3. **DATABASE PERSISTENCE PROOF** (3 minutes) ⭐ CRITICAL
**Step-by-step demonstration:**

1. **Show empty/initial state**
   - Navigate to Sensor Readings page
   - Show current reading count

2. **Create new data**
   - Click "Add Test Reading" button
   - Observe new reading appears in table
   - Note the ReadingId, temperature, timestamp

3. **Close application completely**
   - Close window
   - Verify in Task Manager if needed

4. **Relaunch application**
   - Start app again
   - Navigate to Sensor Readings

5. **Verify persistence**
   - Data is still present!
   - Same ReadingId, same values
   - Proves SQLite database persistence

#### 4. Code Walkthrough (1-2 minutes)
- Show [SensorReadingRepository.cs:46-50](Services/SensorReadingRepository.cs#L46-L50) `InsertAsync()`
- Show [SensorReadingRepository.cs:31-36](Services/SensorReadingRepository.cs#L31-L36) `GetAllAsync()`
- Show [MauiProgram.cs:27-28](MauiProgram.cs#L27-L28) - Dependency Injection registration

---

## Project Structure

```
SmartHomeMonitor/
├── Models/
│   ├── User.cs
│   ├── Device.cs
│   ├── SensorReading.cs
│   └── Alert.cs
├── Services/
│   ├── IRepository.cs
│   ├── SensorReadingRepository.cs
│   └── DeviceRepository.cs
├── Components/
│   ├── Pages/
│   │   ├── Home.razor (Dashboard)
│   │   ├── Devices.razor
│   │   └── Readings.razor (Main demo page)
│   └── Layout/
│       └── NavMenu.razor
├── MauiProgram.cs (DI Registration)
└── README.md
```

---

## Database Location

SQLite database is stored at:
```
C:\Users\AppData\Local\SmartHomeMonitor\smarthome.db3
```

You can verify this path in [SensorReadingRepository.cs:16-18](Services/SensorReadingRepository.cs#L16-L18)

---

## Check-In 1 Requirements

| Requirement | Status | Evidence |
|------------|--------|----------|
| UI and Component Structure | ✅ Complete | 3 main screens built |
| Local Data Models | ✅ Complete | 4 models matching ERD |
| Repository Pattern | ✅ Complete | Generic IRepository<T> implemented |
| Dependency Injection | ✅ Complete | Services registered in MauiProgram.cs |
| **Database Persistence Proof** | ✅ Complete | Add → Close → Relaunch → Data persists |
| Code Walkthrough Ready | ✅ Complete | Repository methods documented |

---

## Next Steps (After Check-In 1)

1. **API Integration**
   - Implement OpenWeatherMap API service
   - Fetch real-time weather data
   - Parse and store API responses

2. **Alert System**
   - Create threshold configuration
   - Implement alert generation logic
   - Add notification display

3. **User Authentication**
   - Implement login/register functionality
   - Add password hashing
   - Create user sessions

4. **Data Visualization**
   - Add charts for temperature trends
   - Implement historical data views
   - Create comparison visualizations

5. **UI Polish**
   - Refine styling and layout
   - Add loading indicators
   - Improve error handling

---

## Common Questions & Answers

**Q: Where is the data stored?**
A: In a local SQLite database at `AppData\Local\SmartHomeMonitor\smarthome.db3`

**Q: How does the Repository pattern work?**
A: It provides an abstraction layer between the UI and data access, making code more maintainable and testable.

**Q: Why use Dependency Injection?**
A: DI allows loose coupling, making services easily replaceable and testable.

**Q: How do you prove persistence?**
A: By adding data, completely closing the app, relaunching, and showing the data is still present.

---

## Team

**Sikai Han** - Full Stack Development

---


