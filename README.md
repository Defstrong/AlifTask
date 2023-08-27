# Installment Payments Service

This project is an example of implementing the "Installment Payments" service for a financial institution that has signed a contract with an online electronics store. Depending on the product category, the duration of the installment plan may vary. The range of installment plans is: 3-6-9-12-18-24 months. The online store has only three categories with the following installment plan durations:

- Smartphone: installment plan [3–9] months
- Computer: installment plan [3–12] months
- TV: installment plan [3–18] months

For each additional unit of the range, 3% will be added for smartphones, 4% for computers and 5% for TVs from the total amount of the product. For example, if a customer wants to buy a smartphone with an installment plan for 18 months for 1000 somoni, the total amount will be 1060 somoni (1000 somoni + 6%).

The project is written in C# and uses the following technologies:

- ASP.NET CORE web API
- XUnit for unit testing

# Installation and running

To install and run the project, you need to perform the following steps:

1. Clone the project repository using the command `git clone https://github.com/yourusername/installment-payment-service.git`
2. Open the project solution `JobAlifTask.sln`
3. Build the project solution by typing `dotnet build` in the terminal
4. Run the application by navigating to `\src\Presentation'` and typing `dotnet run` in a terminal.
5. The application will run on the local server. Go to `http://localhost:{PORT}/swagger/index.html`. There you will be prompted to enter data on the purchase of goods in installments: the name of the goods, the cost of the goods, the customer's phone number and the installment period.
6. After entering the data, the application will display the total amount of payment for the product with an installment plan and send an SMS message with purchase details to the customer's phone number
