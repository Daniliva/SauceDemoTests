# SauceDemo Automation Test Project

This project implements automated UI testing for the [SauceDemo](https://www.saucedemo.com/) web application in accordance with the specified business requirements (UC-1, UC-2, UC-3).

## Technology Stack
- **Language:** C# (.NET 9.0)
- **Test Framework:** NUnit 3
- **Automation Tool:** Selenium WebDriver
- **Driver Manager:** WebDriverManager
- **Assertions:** FluentAssertions
- **Locators:** CSS Selectors only
- **Browsers:** Google Chrome, Mozilla Firefox

## Architecture and Patterns
The project is designed using test automation best practices, aiming for the maximum score based on the evaluation criteria:

1. **Page Object Model (POM):**
   - UI interaction logic is strictly separated from test logic.
   - Implemented classes: `LoginPage`, `InventoryPage`, `ProductDetailsPage`.
2. **Singleton (Thread-Safe):**
   - The `DriverSingleton` class manages the WebDriver lifecycle using `ThreadLocal<IWebDriver>`. This ensures thread safety during parallel test execution.
3. **Factory Method:**
   - The `BrowserFactory` class encapsulates the logic for initializing the required browser based on the launch parameters.
4. **Data-Driven Testing:**
   - NUnit `[TestCase]` attributes are used to parameterize the tests.
5. **Parallel Execution:**
   - Tests are configured for parallel execution (at the `Fixtures` and `All` levels), significantly speeding up the suite execution across different browsers.

---

## Test Cases (Use Cases)

**UC-1: Test Login form with only Username provided**
- Enter any username.
- Enter password.
- Clear the "Password" field.
- Click the "Login" button.
- **Expected Result:** An error message appears: `Epic sadface: Password is required`.

**UC-2: Test Login form with valid credentials**
- Enter a valid username (`standard_user`) and password (`secret_sauce`).
- Click the "Login" button.
- **Expected Result:** Successful authorization, the main page displays the Burger Menu, "Swag Labs" logo, shopping cart icon, sorting filter dropdown, and the list of inventory items.

**UC-3: Test adding products to shopping cart**
- Login with a valid user.
- Open the details of any product by clicking on its name.
- Click the "Add to cart" button.
- **Expected Result:** The shopping cart icon displays a badge with the number `1`.

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