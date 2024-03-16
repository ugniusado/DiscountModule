Project: DiscountModule
=======================

Overview
--------

The DiscountModule application processes a series of shipping transactions, applying discount rules based on predefined business logic, and outputs the processed results. This solution adopts a Clean Architecture model, promoting separation of concerns and enhancing maintainability.

Project Structure
-----------------

*   **DiscountModule.Core (Core Domain):** Contains core business entities and enums that define the essential business concepts and rules.
    
    *   **Entities:** Business models like `Package` and `Shipment`.
    *   **Enums:** Enumerations that provide well-defined options for entities, such as `PackageSize` and `Carrier`.
*   **DiscountModule.Application (Application Layer):** Implements application-specific business logic and orchestrates the flow between the domain and infrastructure layers.
    
    *   **Services:** Such as `DiscountCalculatorService`, which contains rules for discount calculation.
    *   **Exceptions:** Custom exception types that handle domain-specific errors.
*   **DiscountModule.Domain (Presentation Layer):** The entry point of the application, containing the `Program.cs` file that triggers the execution flow.
    
*   **DiscountModule.Infrastructure (Infrastructure Layer):** Deals with data access and external concerns. In this context, it includes adapters for reading input data and outputting processed results.
    
    *   **Adapters:** Such as `FileInputAdapter` for reading input data and `ConsoleOutputAdapter` for outputting results to the console.

Key Decisions and Logic
-----------------------

*   **Discount Calculation:** The `DiscountCalculatorService` in the Application layer encapsulates the logic for discount application based on the transaction details, adhering to specified business rules.
    
*   **Exception Handling:** The application defines and uses custom exceptions within the Application layer to address domain-specific errors robustly.
    
*   **Input and Output Processing:** `FileInputAdapter` reads transaction data from a text file, and `ConsoleOutputAdapter` outputs the processed results, demonstrating clear separation between the application logic and input/output handling.
    

Technologies and Libraries
--------------------------

*   **C# and .NET 6/7:** Leveraging modern C# features and .NET capabilities for building robust console applications.
*   **xUnit:** For unit testing, ensuring the application logic behaves as expected.
*   **Moq and Fluent Assertions:** Enhancing test expressiveness and allowing for effective mocking and assertion in tests.
*   **AutoFixture:** Automating the creation of test objects to simplify test preparation.

Running the Program
-------------------

1.  **Build the Application:** Open the solution in Visual Studio and build the DiscountModule.Domain project, which is the entry point.
    
2.  **Configure the Input File:**
    
    *   Place `input.txt` inside the `InputFiles` folder at the Domain project root.
    *   Ensure the file is set to be copied to the output directory.
3.  **Execute the Program:**
    
    *   Run the DiscountModule.Domain project through Visual Studio or execute the built application directly from the output directory.
    *   The application reads the input file, processes the data, and outputs the results to the console.
4.  **Unit Tests:**
    
    *   Navigate to the test project in your solution.
    *   Use Visual Studio's Test Explorer to run the unit tests and verify the application components' behavior.
