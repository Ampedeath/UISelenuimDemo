# UI Selenium Demo (C# + NUnit + POM)

This is a training UI automation project built using:
- Selenium WebDriver  
- C# / .NET  
- NUnit  
- Page Object Model (POM)  
- Explicit Waits  
- ChromeDriver  
- FluentAssertions  

The project automates the website "epicentrk.ua", including:
- Login (positive & negative tests)  
- Product search  
- Search results validation  
- Opening a product page  
- Adding items to favorites

---

## Project Structure

UISeleniumDemo/  
├── Pages/  
├── Tests/  
├── Utils/  
├── appsettings.json  
├── .gitignore  
└── README.md  

---

## Setup Instructions

### 1. Install required tools

- Visual Studio 2022+
- Chrome browser

### 2. Install NuGet packages

dotnet add package Selenium.WebDriver
dotnet add package Selenium.WebDriver.ChromeDriver
dotnet add package Selenium.Support
dotnet add package DotNetSeleniumExtras.WaitHelpers
dotnet add package NUnit
dotnet add package NUnit3TestAdapter
dotnet add package FluentAssertions


---

## Configuration (appsettings.json)

Create **appsettings.json** in the project root:

```json
{
  "BaseUrl": "https://epicentrk.ua/",
  "ValidPhone": "your-phone-here",
  "ValidPassword": "your-password-here"
}
```

## Git Ignore Sensitive Data

Add this to .gitignore:
```
appsettings.json
bin/
obj/
.vs/
```

## Running Tests

In Visual Studio:
Test → Run All Tests
