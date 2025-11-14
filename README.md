```markdown
# SauceDemo Login Automation Tests

---

## Task Description (Original Assignment)

> **Launch URL:** `https://www.saucedemo.com/`  
>
> **UC-1** Test Login form with empty credentials:  
> - Type any credentials into "Username" and "Password" fields.  
> - Clear the inputs.  
> - Hit the "Login" button.  
> - Check the error messages: `Username is required`.
>
> **UC-2** Test Login form with credentials by passing Username:  
> - Type any credentials in username.  
> - Enter password.  
> - Clear the "Password" input.  
> - Hit the "Login" button.  
> - Check the error messages: `Password is required`.
>
> **UC-3** Test Login form with credentials by passing Username & Password:  
> - Type credentials in username which are under *Accepted username are* sections.  
> - Enter password as `secret_sauce`.  
> - Click on Login and validate the title **“Swag Labs”** in the dashboard.
>
> **Requirements:**  
> - Provide **parallel execution**  
> - Add **logging** for tests  
> - Use **Data Provider** to parametrize tests  
> - Make sure all tasks are supported by these 3 conditions: UC-1; UC-2; UC-3  
> - Add task description as `README.md` into your solution!
>
> **Tools & Options:**  
> - **Test Automation tool:** Selenium WebDriver  
> - **Browsers:** 1) Firefox; 2) Edge  
> - **Locators:** CSS  
> - **Test Runner:** MSTest  
> - **[Optional] Patterns:** 1) Singleton; 2) Adapter; 3) Strategy  
> - **[Optional] Test automation approach:** BDD  
> - **Assertions:** FluentAssertions  
> - **[Optional] Loggers:** Serilog

---

## Project Structure

```
SauceDemoTests/
│
├── Pages/
│   └── LoginPage.cs                  # Page Object with CSS locators
│
├── Utilities/
│   ├── BrowserFactory.cs             # Strategy Pattern: creates driver
│   ├── FirefoxStrategy.cs
│   ├── EdgeStrategy.cs
│   ├── ElementAdapter.cs             # Adapter Pattern: wraps IWebElement + logging + Clear() fix
│   └── LoggerManager.cs              # Singleton Pattern: Serilog logger
│
├── Interfaces/
│   └── IBrowserStrategy.cs           # Strategy interface
│
├── TestResults/                      # Auto-generated: .trx + .html reports
│
├── MSTestSettings.cs                 # [assembly: Parallelize] attribute
├── test.runsettings                  # Parallel execution config
├── trx-to-html.xslt                  # Converts .trx → HTML (namespace-aware)
├── README.md                         # This file
└── SauceDemoTests.csproj
```

---

## How to Run

### Prerequisites
- **.NET 9.0 SDK**
- **Firefox** and **Microsoft Edge** installed
- **PowerShell** (built-in on Windows)

### Step 1: Restore Packages

```powershell
dotnet restore
```

### Step 2: Run Tests + Generate `.trx`

```powershell
dotnet test --settings test.runsettings --logger "trx" --results-directory .\TestResults
```

> Output: `.\TestResults\*.trx`

### Step 3: Generate **HTML Report**

```powershell
powershell -Command "$xslt = [System.Xml.Xsl.XslCompiledTransform]::new($true); $xslt.Load('trx-to-html.xslt'); Get-ChildItem '.\TestResults\*.trx' | ForEach-Object { $html = '.\TestResults\' + $_.BaseName + '.html'; $xslt.Transform($_.FullName, $html); Write-Host 'HTML report created: ' $html }"
```

> Output: `.\TestResults\*.html`  
> Open in browser → full report with logs, durations, colors, and Swag Labs logo
