# Solar Business Management System — Complete Code Manual

## 1. What this project does

This is a console-based Solar Business Management System written in C# and targeting .NET 8.
It lets an administrator manage customers, products, orders and reports. A customer can create
an account, sign in, estimate a solar system and view orders associated with their phone number.

The application stores information in text files. It does not use SQL or another database.

## 2. How the project is organised

The program follows this flow:

`UI -> Services -> Repositories -> Text files`

- **Program.cs** starts the application.
- **UI** contains menus, prompts and printed results.
- **Services** contain business rules and calculations.
- **Repositories** read and write text files.
- **Models** define the objects stored by the application.
- **Common** contains reusable input and validation helpers.

This separation means that a menu does not need to know how a file is written, and a repository
does not need to know how a menu is displayed.

## 3. Running the program

1. Install the .NET 8 SDK.
2. Extract the project folder.
3. Open a terminal in the folder containing `SolarManagement.csproj`.
4. Run `dotnet build` to compile it.
5. Run `dotnet run` to start it.

The application creates `users.txt`, `customers.txt`, `products.txt` and `orders.txt` in its
working directory when the relevant repositories are first created. Customer export creates
`Customer_List.txt`.

**Default administrator account (first run only):**
- Username: `admin`
- Password: `123`

Change this demonstration password before using the program for real business data. Passwords
are currently stored as plain text; this is suitable only for a small learning exercise, not
for a production authentication system.

## 4. C# concepts used throughout

- `using System;` makes common .NET classes such as `Console` available.
- `using System.Collections.Generic;` makes `List<T>` available.
- `using System.IO;` makes text-file classes such as `File`, `StreamReader` and `StreamWriter`
  available.
- `namespace SolarManagement` groups the project's classes under one name.
- `public class` declares a class that other classes can use.
- `private` restricts a field or method to its own class.
- `public` allows other classes to access a member.
- `static` means a member belongs to the class itself rather than to an object.
- A constructor has the same name as its class and runs when an object is created with `new`.
- `this` refers to the current object.
- `: base(...)` calls the parent-class constructor.
- `: BaseRepository(...)` calls the base repository constructor.
- `List<Customer>` is a resizable collection containing Customer objects.
- `new` creates an object.
- `return` sends a result back to the calling method.
- `if` and `else` select which code runs.
- `for` repeats code a known number of times.
- `while` repeats code while a condition remains true.
- `&&` means AND; `||` means OR; `!` means NOT.
- `==` compares values; `=` assigns a value.
- `++` increases a counter by one.
- `out` lets `TryParse` return a parsed value through a variable.
- `using (...) { ... }` ensures a file reader/writer is closed when the block finishes.

## 5. Program.cs — application entry point

- The opening comments describe the project and its layers; comments do not execute.
- `namespace SolarManagement` places `Program` in the project's namespace.
- `class Program` declares the startup class.
- `static void Main(string[] args)` is the entry point called by .NET.
- `string[] args` can contain command-line arguments; this project does not use them.
- `MainMenuUI mainMenu = new MainMenuUI();` constructs the main menu object.
- `mainMenu.Show();` starts the main menu loop.
- The closing braces end Main, Program and the namespace.

## 6. SolarManagement.csproj — project settings

- `<Project Sdk="Microsoft.NET.Sdk">` tells the .NET SDK how to build the project.
- `<OutputType>Exe</OutputType>` creates a runnable console application.
- `<TargetFramework>net8.0</TargetFramework>` targets .NET 8.
- `<ImplicitUsings>disable</ImplicitUsings>` means source files explicitly import namespaces.
- `<Nullable>disable</Nullable>` disables nullable-reference compiler annotations.
- `<RootNamespace>SolarManagement</RootNamespace>` sets the default project namespace.
- The closing XML tags finish the project configuration.

## 7. Models — the data classes

### Models/Person.cs

- `Person` is the parent/base class for data shared by people.
- `Name`, `Phone` and `Email` are public fields holding a person's details.
- `Person()` is an empty constructor, allowing an empty object to be created.
- `Person(string name, string phone, string email)` accepts the three details.
- The assignments copy the constructor parameters into the object's fields.
- The final braces close the constructor, class and namespace.

### Models/User.cs

- `User : Person` means User inherits the fields and members of Person.
- `Username`, `Password` and `Role` hold login details and account type.
- `User()` is the empty constructor.
- The parameterised constructor receives personal and login information.
- `: base(name, phone, email)` sends personal details to the Person constructor.
- The assignments store the username, password and role in this User object.
- `ToString()` converts the object to one text-file record.
- `|` separates the six values so the repository can split them later.
- `override` replaces the inherited `ToString` implementation.

### Models/Customer.cs

- `Customer : Person` inherits Name, Phone and Email.
- `Address` stores the customer's location.
- `SystemType` stores Residential, Commercial or Industrial.
- `InverterCapacity` stores the inverter description, such as 5KW.
- `SolarPanels` stores the panel model.
- `SystemCapacity` stores system capacity in kilowatts.
- `ProjectPrice` stores the estimated project price in PKR.
- The empty constructor creates a customer without setting fields.
- The parameterised constructor accepts all customer-specific details.
- `base(name, phone, email)` initialises the inherited Person fields.
- The assignments initialise the remaining Customer fields.
- `ToString()` writes all nine fields in a fixed order separated by `|`.
- The repository must read the fields in exactly the same order.

### Models/Product.cs

- `Product` represents one inventory product.
- `Name` is the product name; `Description` explains it.
- `Quantity` is the number of units available.
- `Price` is the unit price in PKR.
- The empty constructor creates a blank Product object.
- The parameterised constructor receives and stores all four values.
- `ToString()` serialises the four fields separated by `|`.

### Models/Order.cs

- `Order` represents one order.
- `CustomerName` and `CustomerPhone` identify the customer who placed it.
- `ProductName` identifies the ordered inventory item.
- `Quantity` is the number of units ordered.
- `TotalPrice` is quantity multiplied by the product's unit price.
- `Status` stores the order state, initially Pending.
- The empty constructor creates an empty Order object.
- The parameterised constructor stores the six supplied values.
- `ToString()` serialises the six values separated by `|`.

## 8. Common — input and validation

### Common/Validator.cs

- `Validator` is static, so helper methods can be called without creating an object.
- `IsValidPhone` checks that a phone string is not null and has exactly 11 characters.
- Its `for` loop checks every character.
- A character outside `'0'` through `'9'` makes the phone invalid.
- It returns true only when all 11 characters are digits.
- `IsValidCapacity` accepts values from 1 through 100 inclusive.
- `IsNumber` rejects null, empty strings and strings longer than nine characters.
- Its loop rejects every character that is not a digit.
- This prevents signs, decimal points and letters from being accepted.
- `HasSeparator` searches for `|`, which is reserved for separating file fields.
- It returns true if the separator is found and false otherwise.

### Common/InputHelper.cs

- `InputHelper` is a static class containing reusable console-input methods.
- `ReadText` displays a prompt and reads a line from the keyboard.
- A null input is converted to an empty string.
- Its `while` loop repeats while the input is blank/whitespace or contains `|`.
- The returned text is trimmed, removing leading and trailing spaces.
- `ReadNumber` prompts for a whole number.
- It calls `Validator.IsNumber` and repeats until the input is valid.
- `int.Parse` converts the validated digit string into an integer.
- `ReadPositiveNumber` calls ReadNumber and repeats while the value is below 1.
- `ReadPhone` calls ReadText and repeats until the phone passes IsValidPhone.
- `ReadCapacity` calls ReadNumber and repeats until capacity is between 1 and 100.
- `ClearScreen` attempts to clear the console before a new screen is displayed.
- Its `try/catch` prevents a console-clearing failure from terminating the program.
- `Pause` prints an instruction and waits for Enter.

## 9. Repositories — text-file storage

### Repositories/BaseRepository.cs

- `BaseRepository` is the parent class for the repositories.
- `FilePath` stores the name/path of the text file used by that repository.
- Its constructor receives a path and assigns it to FilePath.
- `ReadLines` creates an empty `List<string>`.
- `File.Exists` checks whether the file exists.
- If it does not exist, the empty list is returned.
- `StreamReader` opens the file for reading.
- `ReadLine` reads one line at a time until it returns null at end-of-file.
- Empty lines are ignored; non-empty lines are added to the list.
- `AppendLine` opens a StreamWriter in append mode (`true`).
- It adds one record without deleting earlier records.
- `WriteLines` opens a StreamWriter in overwrite mode (`false`).
- Its loop writes every supplied line, replacing the previous file contents.

### Repositories/UserRepository.cs

- `UserRepository : BaseRepository` inherits the shared file methods.
- Its constructor selects `users.txt`.
- If the file does not exist, it saves a demonstration Administrator account.
- `Save` converts a User to text using `ToString` and appends it.
- `GetAll` creates an empty list of User objects.
- It reads all lines through `ReadLines`.
- `Split('|')` divides each record into fields.
- Only records with six fields are considered.
- A User object is created using the fields in the saved order.
- The method returns the list of users.

### Repositories/CustomerRepository.cs

- `CustomerRepository` inherits BaseRepository and uses `customers.txt`.
- Its constructor creates three sample customers if the file is absent.
- `Save` appends one customer's serialised record.
- `SaveAll` converts every customer into a string and overwrites the file.
- `GetAll` reads the file and creates a list of Customer objects.
- Each line is split using `|`.
- A valid customer record must contain nine fields.
- `int.TryParse` checks that capacity and project price are valid integers.
- Invalid numeric records are skipped instead of crashing the application.
- A Customer object is created from the nine fields in their defined order.
- `ExportToTxt` opens `Customer_List.txt` in overwrite mode.
- It writes a heading and then each customer's details in readable form.
- `GetDefaultCustomers` creates the three sample Customer objects.
- These sample records are saved only when the customer file is first created. Their sample project estimates\n  are aligned with the calculator assumptions used by this version.

### Repositories/ProductRepository.cs

- `ProductRepository` inherits BaseRepository and uses `products.txt`.
- If the file does not exist, the constructor saves five sample products.
- `Save` appends one Product record.
- `SaveAll` converts the product list into strings and replaces the file.
- `GetAll` reads and splits every record.
- A valid product line must have four fields.
- `int.TryParse` validates Quantity and Price before creating the object.
- Invalid records are ignored rather than causing a parsing exception.
- `GetDefaultProducts` constructs the five demonstration inventory products.

### Repositories/OrderRepository.cs

- `OrderRepository` inherits BaseRepository and uses `orders.txt`.
- Its constructor selects the order file.
- `Save` appends one order record.
- `GetAll` reads the records and creates a list of Order objects.
- A valid record must contain six fields.
- `int.TryParse` validates Quantity and TotalPrice.
- Malformed numeric records are skipped.
- The method returns all successfully read orders.

## 10. Services — business logic

### Services/AuthService.cs

- `AuthService` applies the rules for account registration and sign-in.
- `_repository` is a UserRepository used to access saved accounts.
- The constructor creates that repository.
- `SignUp` receives a User object.
- It rejects an invalid 11-digit phone number.
- It rejects passwords shorter than three characters.
- It loads all existing users.
- A loop checks whether the username is already taken.
- If it is, an error message is returned.
- New public registrations are assigned the Customer role.
- The repository saves the new account.
- An empty string means registration succeeded.
- `SignIn` loads all users and checks username and password together.
- It returns the matching User, or null if no match exists.

### Services/CustomerService.cs

- `_repository` accesses customer records.
- `_calculator` calculates the estimated project price.
- The constructor creates both helper objects.
- `AddCustomer` first loads the existing customer list.
- It rejects additions when there are already 1000 records.
- It checks that the phone number is not already used.
- It calculates ProjectPrice using the system capacity.
- It saves the customer and returns true on success.
- `GetAllCustomers` returns the repository's customer list.
- `FindCustomerByPhone` loops through customers and returns a matching record.
- If no phone matches, it returns null.
- `DeleteCustomerByPhone` searches for the phone number.
- `RemoveAt(i)` removes the matching customer from the list.
- `SaveAll` writes the updated list back to the file.
- `ExportCustomers` passes all customers to the export method.

### Services/ProductService.cs

- `_repository` accesses the inventory file.
- The constructor creates ProductRepository.
- `AddProduct` loads existing products and enforces the 1000-product limit.
- It rejects a product name that already exists, ignoring letter case.
- It saves the product and returns true when accepted.
- `GetAllProducts` returns every product.
- `FindProductByName` searches for a name without case sensitivity.
- It returns the matching product or null.
- `GetProductsAbove` creates a result list for quantities greater than a limit.
- `GetLowStockProducts` creates a result list for quantities at or below a limit.
- `ReduceStock` rejects zero or negative quantities.
- It loads the product list and finds the requested product.
- It checks that sufficient stock exists before subtracting.
- It saves the updated list and returns true.
- It returns false if the quantity is invalid, stock is insufficient or product is absent.

### Services/OrderService.cs

- `_orderRepository` saves and reads orders.
- `_productService` finds products and updates inventory.
- The constructor creates both objects.
- `CreateOrder` first checks the 1000-order limit.
- It searches inventory for the requested product.
- It returns `NotFound` if the product does not exist.
- It returns `LowStock` if the quantity is invalid or exceeds available stock.
- It calculates the total using `long` so multiplication cannot overflow an int.
- It returns `InvalidTotal` if the total is larger than `int.MaxValue`.
- It calls ReduceStock and stops if the stock update fails.
- It stores the checked total as an int.
- It sets the status to Pending and saves the order.
- `Success` tells the UI that the order was created.
- `GetAllOrders` returns every order.
- `GetOrdersByPhone` filters orders to the supplied customer phone.
- `GetAvailableStock` returns the product's quantity, or zero if absent.

### Services/SolarCalculatorService.cs

- `CalculatePanels` assumes one panel supplies 550 watts.
- It converts kW to units of 100 watts and uses integer arithmetic to round up.
- Formula: `(capacityKW * 100 + 54) / 55`.
- `CalculateInverters` assumes one inverter supports 5 kW.
- `(capacityKW + 4) / 5` rounds the number of inverters upward.
- `CalculateBatteries` assumes one battery per kW.
- `CalculatePrice` adds panel, inverter and battery costs.
- These are demonstration assumptions and should be replaced with actual supplier pricing
  and engineering design rules before commercial quotations are issued.

### Services/ReportService.cs

- The class combines information from customer, product and order services.
- Its constructor creates those service objects.
- `GetTotalCustomers` returns the number of customer records.
- `GetTotalProducts` returns the number of inventory products.
- `GetTotalOrders` returns the number of saved orders.
- `GetTotalRevenue` adds the ProjectPrice of every customer.
- This is the sum of saved customer project estimates, not cash received or order sales.
- `GetLowStockProducts` returns products whose quantity is five or less.

## 11. UI — menus and screens

### UI/MainMenuUI.cs

- The class coordinates the application's first menu.
- Its fields hold AuthUI, AdminUI and CustomerPortalUI objects.
- The constructor creates these three UI objects.
- `Show` starts a `running` loop.
- `ClearScreen` clears the previous screen at the start of each menu iteration.
- The menu displays Sign Up, Sign In and Exit.
- `Console.ReadLine` reads the user's selection.
- Choice 1 opens the sign-up screen.
- Choice 2 opens sign-in and receives a User or null.
- A non-null user with role Admin sees the admin panel.
- Other signed-in users see the customer portal.
- Choice 3 prints a goodbye message and ends the loop.
- Any other choice displays an error and pauses.

### UI/AuthUI.cs

- `_authService` connects the authentication screen to account rules.
- The constructor creates AuthService.
- `ShowSignUp` clears the screen and asks for name, phone, email, username and password.
- The helper methods validate text and phone input.
- It creates a User with an empty role; AuthService assigns Customer.
- It calls SignUp and displays either success or the returned error.
- `Pause` waits before returning to the previous menu.
- `ShowSignIn` clears the screen and allows up to three attempts.
- Each attempt asks for username and password.
- It calls AuthService.SignIn.
- A matching user is welcomed and returned to MainMenuUI.
- A failed attempt decreases the number of attempts remaining.
- After three failures, it pauses and returns null.

### UI/AdminUI.cs

- The fields hold the five admin feature screens.
- The constructor creates CustomerUI, InventoryUI, OrderUI, CalculatorUI and ReportUI.
- `ShowAdminPanel` receives the currently signed-in admin.
- Its loop displays the admin name and six menu options.
- Each numbered choice opens the corresponding feature.
- Choice 6 sets `running` to false and returns to the main menu.
- Invalid input displays an error and pauses.

### UI/CustomerUI.cs

- `_customerService` handles customer records.
- The constructor creates CustomerService.
- `ShowCustomerMenu` loops through six customer-management choices.
- Choice 1 calls AddCustomerUI.
- Choice 2 calls ViewCustomersUI.
- Choice 3 calls SearchCustomerUI.
- Choice 4 calls DeleteCustomerUI.
- Choice 5 exports the customer list.
- Choice 6 returns to the admin panel.
- `AddCustomerUI` asks for all customer and solar-system details.
- It creates a Customer with temporary price zero.
- CustomerService calculates the actual estimate before saving.
- It displays the calculated price or a duplicate/full-storage warning.
- `ViewCustomersUI` gets all customers and prints each field.
- It prints a message if the list is empty.
- `SearchCustomerUI` asks for a validated phone and displays the matching record.
- `DeleteCustomerUI` asks for a validated phone and reports whether deletion worked.
- `ExportCustomersUI` writes the formatted text report and confirms the action.

### UI/InventoryUI.cs

- `_productService` provides inventory operations.
- The constructor creates ProductService.
- `ShowInventoryMenu` displays Add, View and Back options in a loop.
- `AddProductUI` asks for name, description, positive quantity and positive price.
- It constructs a Product object and asks the service to save it.
- It reports success or a duplicate/full-inventory warning.
- `ViewProductsUI` obtains all products and prints their details.
- It also calls `GetProductsAbove(20)` to display products with more than 20 units.
- The user presses Enter before returning.

### UI/OrderUI.cs

- `_orderService` handles order operations.
- The constructor creates OrderService.
- `ShowOrderMenu` offers Create Order, View Orders and Back.
- `CreateOrderUI` asks for customer name, validated phone, product name and quantity.
- It constructs an Order with temporary total zero and blank status.
- `CreateOrder` performs the actual business checks and inventory deduction.
- The UI displays success, product-not-found, low-stock, invalid-total or full-storage messages.
- `ViewOrdersUI` gets all orders and prints customer, product, quantity, price and status.
- It displays a message if there are no orders.

### UI/CalculatorUI.cs

- `_calculator` holds the solar calculation service.
- The constructor creates SolarCalculatorService.
- `ShowCalculator` clears the screen and asks for capacity from 1 to 100 kW.
- It prints panel, inverter and battery estimates.
- It prints the estimated project price and pauses.

### UI/CustomerPortalUI.cs

- `_calculator` provides solar estimates to customers.
- `_orderService` retrieves orders.
- The constructor creates both service objects.
- `ShowPortal` receives the signed-in User.
- It displays the customer's name and three options.
- Choice 1 opens the estimator.
- Choice 2 shows orders associated with the user's phone.
- Choice 3 logs out by ending the loop.
- `ShowEstimator` validates the requested capacity and prints calculated quantities and price.
- `ShowMyOrders` calls GetOrdersByPhone with the signed-in user's phone.
- It prints only the matching orders, not every customer's orders.

### UI/ReportUI.cs

- `_reportService` provides the report numbers.
- The constructor creates ReportService.
- `ShowReports` clears the screen and prints customer, product and order counts.
- It prints the report's current revenue calculation.
- It requests products with stock quantity five or less.
- It prints a warning for each low-stock product, or a message if none exist.
- `Pause` waits before returning to the admin menu.

## 12. Important logic and fixes made

1. **Duplicate customer records:** CustomerService now rejects a second customer with the same phone.
2. **Duplicate products:** ProductService now rejects duplicate product names, case-insensitively.
3. **Order arithmetic overflow:** OrderService calculates the multiplication using `long` and checks
   the result before converting it to the model's `int` field.
4. **Stock protection:** ReduceStock now validates the quantity, verifies available stock, saves
   the updated inventory and reports success/failure. An order is not saved if stock reduction fails.
5. **Damaged numeric records:** Customer, Product and Order repositories use `int.TryParse` so a
   malformed numeric field is skipped rather than causing `int.Parse` to crash.
6. **Search/delete phone input:** Customer search and delete now use the 11-digit phone validator.
7. **Clear screen:** Menu loops and standalone screens call ClearScreen to avoid stacking old
   menus and results. The helper catches console-clearing errors, for example when output is redirected.
8. **Whitespace input:** ReadText rejects whitespace-only input and trims surrounding spaces.\n9. **Sample estimate consistency:** the three sample customer prices now match the calculator assumptions.

## 13. Known limitations — important before real use

- Passwords are stored as plain text and the default admin password is weak.
- There is no role-management screen; public sign-up always creates a Customer.
- The order screen is admin-only. The customer portal can view orders but cannot create them.
- Customer orders are linked by phone number, not by a permanent customer ID.
- Deleting a customer does not delete their orders.
- The report's “revenue” is the sum of customer project estimates, not payments collected.
- Solar panel, inverter, battery and price assumptions are illustrative, not a technical design.
- File records use `|` as a delimiter. InputHelper blocks this character in interactive text fields.
- The file format is simple and does not provide database transactions or multi-user concurrency.
- The project was reviewed and edited, but a .NET build could not be executed in this environment
  because the .NET SDK is not installed here. Run `dotnet build` on a machine with .NET 8 to verify
  compilation before submission.

## 14. Suggested manual test plan

1. Start the application and sign in with the default admin account.
2. Add a customer using an 11-digit phone number.
3. Try adding the same phone number again; it should be rejected.
4. Search for the customer and then delete the customer.
5. Add a product and try adding the same name with different letter casing.
6. Create an order with a valid product and quantity; confirm stock decreases.
7. Try ordering more than the available quantity; no order should be saved.
8. Try an unknown product name; the program should report Product not found.
9. Try an extremely large order total; the program should reject it rather than overflow.
10. View reports and check the counts and low-stock list.
11. Export customers and open `Customer_List.txt`.
12. Sign up a customer account, sign in, run the estimator and view My Orders.
13. Test invalid menu choices, blank input and malformed numeric data in a copy of a data file.
