A brief explanation about my code...

On the first version, I started using TDD to achieve the expected output using the specified input with no care on a well-defined code structure. Once I achieved this, I began to work on a cleaner structure for the code, I did some interface segregation, applied some key points of the CQRS pattern, and also enable dependency injection on the console app. I ran the tests on almost every major change to check for workflow correctness.

======================================================================

Sale Tax project V0.1

In the beginning, I wanted my code to display the correct output given the specific input as described by the requirements. So, I started using TDD and I didn't care about the project structure, that's why this version 0.1 only has a solo Core project in the Solution.

I started to code the tests for the decimal round-up functionality since I considered this a low-level functionality and defined some test scenarios with known values (again from the specification on the document).

After having all Decimal round-up tests green, I moved to define how the taxes were going to be applied and calculated. I created an enum with the 2 types of taxes defined and a TaxingService class that handled these 2 functionalities based mainly on the product name and a kind of white-list for taxes assignment.

Then, I worked on how to display the receipt with the defined format, so I added a ReceiptService class which was responsible of generate the receipt using a list of ShoppingCartItem items, this class is a container of a product and the times it is received in the input data.

Once the tests for the receipt output passed, I write a class for translating the string input data into a list of products and called that class InputHandlerService. The translation is supported by a regex that extracts the product name and the product price from the input data.

At the end of this 0.1 version, I created a Console App project and added all the code needed to complete the challenge workflow:
> 1- Ask the user for input via Console and stored it on a string list 
> 2- Translate that input string list into a product list using the InputHandlerService class
> 2.1- If the product does not meet the regex criteria is discarded from the product list
> 3- Add every product within the product list to the ShoppingCart container through ReceiptService class and there the taxes are assigned to every product
> 4- Retrieve the receipt calling the GetReceipt method on ReceiptService and display it to the console

All previous steps ended with the console app showing the expected output given the specific input defined on the documentation. I finished tagging this version as 0.1.

======================================================================================================

Sale Tax project V0.2

At this point the project was working as expected, so I started to give a cleaner structure to the code.
First I created an Entities folder and moved the Product class and Shopping related classes to this folder, I also created an Enum folder and moved the only enum to this folder.

I wanted to follow a Clean Architecture structure so I did rename the Core project to Domain, then I created a new project called Application where I moved there the Services classes within a Service folder, but this time I also defined an interface for every Service class, so the code could relay on abstractions, not on concrete implementations>
-If decided, the IReceiptPrintingService could be implemented by another class to generate a receipt on another format like JSON, XML, HTML, etc, besides the string implementation.
-With the ITaxingService, we could switch the way the taxes are rounded up, assigned, and calculated by making a custom implementation.
-Through IInputHandlerService any kind of input can be expected, when creating a custom implementation

After defining the services class and its implementations, I created a new project called Infrastructure, and I defined a repository class for ShoppingCart, so it is in charge of the shopping cart handling instead of doing that within the receipt implementation.

Then I defined a command and a query following the CQRS pattern on the Application project. There I also added a reference to the MediaTR library to handle the command and the query.

Almost at the end of this version, I made some modifications to the Console App project to enable Dependency injection since I already have some interfaces with its concrete implementations, as well as the use of MediaTR to send the commands and query the receipt.

Almost on every reasonable major change, I run the unit tests to validate that the changes made don’t affect the correct flow of the app.


