# SauceDemo Automation Test Project

This project implements automated UI testing for the [SauceDemo](https://www.saucedemo.com/) web application in accordance with the specified business requirements (UC-1, UC-2, UC-3).

## Technology Stack
- **Language:** C# (.NET 9.0)
- **Test Framework:** NUnit
- **Automation Tool:** Selenium WebDriver
- **Driver Manager:** WebDriverManager
- **Assertions:** FluentAssertions
- **Locators:** CSS Selectors only
- **Browsers:** Multi-browser support (Chrome, Firefox) via JSON config in Headless mode
- **Logging:** Serilog

## Architecture and Patterns
The project is designed using test automation best practices, aiming for the maximum score based on the evaluation criteria:

1. **Page Object Model (POM):**
   - UI interaction logic is strictly separated from test logic.
   - Implemented classes: `LoginPage`, `InventoryPage`, `ProductDetailsPage`.
2. **Singleton (Thread-Safe):**
   - The `DriverSingleton` class manages the WebDriver lifecycle using `ThreadLocal<IWebDriver>`. 
   - The `LoggerManager` implements a global, thread-safe Serilog instance for console logging.
3. **Factory Method / Abstract Factory:**
   - The `BrowserFactory` acts as a dispatcher that utilizes specific factories (`ChromeDriverFactory`, `FirefoxDriverFactory`) to initialize the required browser based on the launch parameters.
4. **Data-Driven Testing (JSON):**
   - NUnit `[TestCaseSource]` attributes are used to parameterize tests. Test data (multiple users, expected results) is loaded dynamically from `users.json`.
5. **Dependency Injection & Service Locator:**
   - The `TestApp` class acts as a Thread-Safe static DI container (`ThreadLocal<ServiceProvider>`) to automatically resolve dependencies for all Page Objects.
6. **Parallel Execution:**
   - Tests are configured for parallel execution (at the `Fixtures` and `All` levels), significantly speeding up the suite execution across different browsers.

---

## Test Cases (Use Cases)

**UC-1: Test Login form with only Username provided**
- Iterates over users from the JSON dataset.
- Enter any username.
- Enter password.
- Clear the "Password" field.
- Click the "Login" button.
- **Expected Result:** An error message appears: `Epic sadface: Password is required`.

**UC-2: Test Login form with valid credentials**
- Iterates over users from the JSON dataset.
- Enter username and password.
- Click the "Login" button.
- **Expected Result:** Successful authorization, the main page displays the Burger Menu, "Swag Labs" logo, shopping cart icon, sorting filter dropdown, and the list of inventory items. *(Gracefully handles and validates `locked_out_user`)*.

**UC-3: Test adding products to shopping cart**
- Login with a valid user from the JSON dataset.
- Open the details of any product by clicking on its name.
- Click the "Add to cart" button.
- **Expected Result:** The shopping cart icon displays a badge matching the dynamically expected value (e.g., `1` or `0` for problem users).

---

## How to Run Tests and Generate HTML Report

### Prerequisites
- **.NET 9.0 SDK** installed
- **Google Chrome** and **Mozilla Firefox** browsers installed
- **PowerShell** (built-in on Windows)

### Step 1: Clean and Restore Dependencies
Open your terminal in the project root directory and run:
```powershell
dotnet clean
dotnet restore
```
### Step 2: Run Tests & Generate .trx File
To execute the tests in parallel and generate the .trx results file
required for the HTML report, run the following command:
```powershell
dotnet test --logger "trx" --results-directory .\TestResults
```
### Step 3: Generate a Beautiful HTML Report
Run this command in PowerShell. It processes the generated .trx file 
through the trx-to-html.xslt template and creates a readable HTML page with your test results:
```powershell
$xslt = [System.Xml.Xsl.XslCompiledTransform]::new($true); $xslt.Load('trx-to-html.xslt'); Get-ChildItem '.\TestResults\*.trx' | ForEach-Object { $html = '.\TestResults\' + $_.BaseName + '.html'; $xslt.Transform($_.FullName, $html); Write-Host 'HTML report created: ' $html }
```
After running the script, go to the TestResults folder and open the generated .html file in any browser to see the full report.