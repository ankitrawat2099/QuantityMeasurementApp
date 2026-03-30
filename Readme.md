### 📏 Quantity Measurement App (.NET)

A scalable and extensible .NET-based Quantity Measurement Application that supports multiple unit categories (Length, Weight, Volume, Temperature), arithmetic operations, conversions, and follows clean architecture principles.

#### 🚀 Use Cases (UC-wise)
##### 🔹 QM1 – UC1: Implemented feet measurement equality using value object semantics, floating-point comparison, and unit testing.
##### 🔹 QM2 – UC2: Added feet and inches equality with encapsulation and improved object comparison.
##### 🔹 QM3 – UC3: Introduced generic Quantity<T> class applying DRY, polymorphism, abstraction, and enum-based design.
##### 🔹 QM4 – UC4: Extended unit support ensuring scalability, validation, and backward compatibility.
##### 🔹 QM5 – UC5: Implemented unit-to-unit conversion using enums with conversion factors and immutable value objects.
##### 🔹 QM6 – UC6: Added arithmetic (addition) on length units with normalization to base unit and precision handling.
##### 🔹 QM7 – UC7: Enabled addition with target unit specification using method overloading and reusable conversion logic.
##### 🔹 QM8 – UC8: Refactored unit enum into standalone components following SRP and separation of concerns.
##### 🔹 QM9 – UC9: Introduced weight measurement with multi-category support and consistent conversion architecture.
##### 🔹 QM10 – UC10: Implemented generic quantity with unit interfaces supporting multi-category extensibility using DI principles.
##### 🔹 QM11 – UC11: Added volume measurement (Litre, Millilitre, Gallon) with equality, conversion, and arithmetic support.
##### 🔹 QM12 – UC12: Implemented subtraction and division with validation, immutability, and precision control.
##### 🔹 QM13 – UC13: Centralized arithmetic logic using functional patterns and enums to enforce DRY principle.
##### 🔹 QM14 – UC14: Added temperature measurement with non-linear conversions and selective arithmetic support.
##### 🔹 QM15 – UC15: Refactored application into N-Tier architecture with DTOs, services, and dependency injection.
##### 🔹 QM16 – UC16: Integrated database using ADO.NET with proper repository pattern and transaction handling.
##### 🔹 QM17 – UC17: Built REST APIs using ASP.NET Core with controllers, services, and logging.
##### 🔹 QM18 – UC18: Implemented authentication and user management using JWT-based security.

### 🛠️ Tech Stack (.NET)
#### 💻 Language: C#
#### ⚙️ Framework: ASP.NET Core Web API
#### 🏗️ Architecture: N-Tier (Controller → Service → Repository → DB)
#### 🗄️ ORM/DB Access: ADO.NET / (Optional: Entity Framework Core)
#### 🔐 Authentication: JWT
#### 🧪 Testing: MS Testing
