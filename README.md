# ConferenceRepo
# Festival Conference Management System

This project is a **.NET MVC application** written in **C#** with a **SQL Server database**.  
It is designed to manage a conference or festival event efficiently.  
The system provides functionality to register attendees, track their participation, manage gifts and lunches, and print certificates and ID cards.

---

## 📌 Features by Tabs

### ✅ **Tab 1 – Register Attendee**
Used to register a conference attendee.  
Information collected includes:
- First Name
- Last Name
- ID (national ID or unique identifier)
- Speciality / Field
- Additional details as required by the event

---

### ✅ **Tab 2 – Attendance Tracking**
Stores the times each attendee is present in conference halls.  
This allows the system to:
- Record entry and exit times
- Track total attendance duration
- Associate each record with specific halls or sessions

---

### ✅ **Tab 3 – Gift Management**
Checks whether each attendee has received their gift (e.g., conference bag or welcome package).  
- Ensures that each attendee only receives **one gift**.

---

### ✅ **Tab 4 – Lunch Verification**
Verifies if each attendee has claimed their lunch.  
- Prevents duplicate lunch claims
- Tracks lunch distribution per day/session

---

### ✅ **Tab 5 – Print Certificates & Cards**
Generates and prints:
- **Certificate of Attendance**
- **Certificate of Speaker** (if the attendee presented a talk)
- **Certificate of Poster Presenter** (if applicable)
- **Event ID Card** for each attendee

All documents are generated dynamically based on the attendee’s information and participation data.

---

## 🛠️ **Technology Stack**
- **Frontend:** ASP.NET MVC (Views, Razor)
- **Backend:** C# (Controllers, Models)
- **Database:** SQL Server
- **Architecture:** Model–View–Controller (MVC)

---

## 🚀 **How to Run**
1. Clone this repository.
2. Open the solution in **Visual Studio**.
3. Configure the **SQL Server connection string** in `web.config` or `appsettings.json`.
4. Run the database migrations or execute the provided SQL script.
5. Press **F5** to run the project locally.

---

## ✨ **Usage**
- Navigate through the tabs in the application to access the different functionalities described above.
- Admins can register new attendees, track their participation, and print necessary documents directly from the system interface.

---

## 📄 **License**
This project is for internal use in managing conference and festival events.  
Feel free to modify and expand based on your specific requirements.

---
