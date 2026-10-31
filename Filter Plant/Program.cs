using System;
using System.Collections.Generic;

namespace Filter_Plant
{
    class Program
    {
        static List<Customer> customers = new List<Customer>();
        static List<DailyReport> dailyReports = new List<DailyReport>();
        static List<MonthlyReport> monthlyReports = new List<MonthlyReport>();

        static string ownerUsername = "admin";
        static string ownerPassword = "123";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=================================================");
                Console.WriteLine("             WATER FILTRATION PLANT              ");
                Console.WriteLine("=================================================\n");
                Console.WriteLine("[1] OWNER");
                Console.WriteLine("[2] CUSTOMER");
                Console.WriteLine("[0] Exit\n");
                Console.WriteLine("=================================================");
                Console.Write("Enter Your Choice: ");
                string choice = Console.ReadLine();

                if (choice == "0")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }
                else if (choice == "1")
                {
                    OwnerLogin();
                }
                else if (choice == "2")
                {
                    CustomerMenu();
                }
            }
        }

        static void CustomerMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("================================================");
                Console.WriteLine("            *** Customer Panel ***              ");
                Console.WriteLine("================================================\n");
                Console.WriteLine("[1] New Customer Registration");
                Console.WriteLine("[2] Existing Customer Login");
                Console.WriteLine("[0] Back\n");
                Console.WriteLine("=================================================");
                Console.Write("Enter Your Choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    RegisterCustomer();
                }
                else if (choice == "2")
                {
                    CustomerLogin();
                }
                else if (choice == "0")
                {
                    break;
                }
            }
        }

        static void RegisterCustomer()
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("         *** New Customer Registration ***       ");
            Console.WriteLine("=================================================\n");

            Console.Write("Enter Name                    : ");
            string name = Console.ReadLine();
            Console.Write("Enter Phone Number            : ");
            string phone = Console.ReadLine();
            Console.Write("Enter Complete Home Address   : ");
            string address = Console.ReadLine();
            Console.Write("Enter Password                : ");
            string pass = Console.ReadLine();

            int id = 1001 + customers.Count;
            Customer newCust = new Customer(name, phone, address, pass, id);
            customers.Add(newCust);

            Console.WriteLine($"\nAccount Created! Your ID: | {id} |\n");
            Console.WriteLine("[0] Back");
            Console.ReadLine();
        }

        static void CustomerLogin()
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("              *** Customer Login ***             ");
            Console.WriteLine("=================================================\n");

            int attempts = 0;
            while (attempts < 5)
            {
                Console.Write("Enter Your ID (Press 0 for Return): ");
                int id = int.Parse(Console.ReadLine());
                if (id == 0) return;

                Console.Write("Enter Password                     : ");
                string pass = Console.ReadLine();

                Console.WriteLine("\n[0] Forgot Password");
                Console.WriteLine("[1] Login");
                Console.Write("Enter Choice: ");
                string loginChoice = Console.ReadLine();

                if (loginChoice == "0")
                {
                    ForgotPassword();
                    return;
                }

                Customer loggedCustomer = null;
                foreach (Customer c in customers)
                {
                    if (c.id == id && c.password == pass)
                    {
                        loggedCustomer = c;
                        break;
                    }
                }

                if (loggedCustomer == null)
                {
                    attempts++;
                    Console.WriteLine($"\nInvalid ID / Password! Attempts Left: {5 - attempts}\n");
                    if (attempts == 5)
                    {
                        Console.WriteLine("Too Many Attempts! Closing...");
                        Environment.Exit(0);
                    }
                }
                else
                {
                    CustomerDashboard(loggedCustomer);
                    break;
                }
            }
        }

        static void ForgotPassword()
        {
            Console.Write("\nEnter Your Phone Number: ");
            string phone = Console.ReadLine();
            Customer found = null;

            foreach (Customer c in customers)
            {
                if (c.phone == phone)
                {
                    found = c;
                    break;
                }
            }

            if (found == null)
            {
                Console.WriteLine("Phone Number Not Found!\n");
            }
            else
            {
                Console.WriteLine($"Your ID       : {found.id}");
                Console.WriteLine($"Your Password : {found.password}\n");
            }
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        static void CustomerDashboard(Customer cust)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=================================================");
                Console.WriteLine($"   Welcome, {cust.name}!");
                Console.WriteLine("=================================================\n");
                Console.WriteLine("[1] Place Order");
                Console.WriteLine("[2] Update Order");
                Console.WriteLine("[3] Bill Status");
                Console.WriteLine("[4] Update Profile");
                Console.WriteLine("[5] View Cane Price");
                Console.WriteLine("[0] Logout\n");
                Console.WriteLine("=================================================");
                Console.Write("Enter Your Choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    PlaceOrderUI(cust);
                }
                else if (choice == "2")
                {
                    UpdateOrderUI(cust);
                }
                else if (choice == "3")
                {
                    BillStatusUI(cust);
                }
                else if (choice == "4")
                {
                    UpdateProfileUI(cust);
                }
                else if (choice == "5")
                {
                    ViewCanePriceUI();
                }
                else if (choice == "0")
                {
                    break;
                }
            }
        }

        static void PlaceOrderUI(Customer cust)
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("                *** Place Order ***              ");
            Console.WriteLine("=================================================\n");

            Console.Write("How Many Canes                               : ");
            int qty = int.Parse(Console.ReadLine());
            Console.Write("Delivery Gap (e.g., daily, 2 days, 5 days)   : ");
            string gap = Console.ReadLine();
            Console.Write("Start Date (e.g., 19-04-2026)                : ");
            string date = Console.ReadLine();

            cust.PlaceOrder(qty, gap, date);

            Console.WriteLine("\n=================================================");
            Console.WriteLine("               *** Order Summary ***             ");
            Console.WriteLine("=================================================");
            Console.WriteLine($"Name        : {cust.name}");
            Console.WriteLine($"Phone       : {cust.phone}");
            Console.WriteLine($"Address     : {cust.address}");
            Console.WriteLine($"Canes       : {cust.quantity}");
            Console.WriteLine($"Gap         : {cust.gap}");
            Console.WriteLine($"Start Date  : {cust.deliveryDate}");
            Console.WriteLine($"Total/Order : Rs. {cust.totalBill}");
            Console.WriteLine("=================================================\n");
            Console.WriteLine("[0] Back");
            Console.ReadLine();
        }

        static void UpdateOrderUI(Customer cust)
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("               *** Update Order ***              ");
            Console.WriteLine("=================================================\n");
            Console.WriteLine("--- Current Order ---");
            Console.WriteLine($"Canes      : {cust.quantity}");
            Console.WriteLine($"Gap        : {cust.gap}");
            Console.WriteLine($"Start Date : {cust.deliveryDate}\n");

            Console.WriteLine("[1] Update Order");
            Console.WriteLine("[2] Cancel Order");
            Console.WriteLine("[0] Back");
            Console.Write("Enter Choice: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("\nHow Many Canes                               : ");
                int qty = int.Parse(Console.ReadLine());
                Console.Write("Delivery Gap (e.g., daily, 2 days, 5 days)   : ");
                string gap = Console.ReadLine();
                Console.Write("Start Date                                   : ");
                string date = Console.ReadLine();

                cust.PlaceOrder(qty, gap, date);
                Console.WriteLine("\nOrder Updated Successfully!\n");
            }
            else if (choice == "2")
            {
                cust.CancelOrder();
                Console.WriteLine("\nOrder Cancelled Successfully!\n");
            }
            Console.WriteLine("[0] Back");
            Console.ReadLine();
        }

        static void BillStatusUI(Customer cust)
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("                *** Bill Status ***              ");
            Console.WriteLine("=================================================\n");
            Console.WriteLine($"Start Date      : {cust.deliveryDate}");
            Console.WriteLine($"Canes Per Order : {cust.quantity}");
            Console.WriteLine($"Delivery Gap    : {cust.gap}");
            Console.WriteLine($"Total Amount    : Rs. {cust.totalBill}");
            Console.WriteLine($"Amount Paid     : Rs. {cust.paidAmount}");
            Console.WriteLine($"Remaining       : Rs. {cust.totalBill - cust.paidAmount}");
            Console.WriteLine($"Payment Status  : {cust.billStatus}");
            Console.WriteLine("=================================================\n");
            Console.WriteLine("[0] Back");
            Console.ReadLine();
        }

        static void UpdateProfileUI(Customer cust)
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("               *** Update Profile ***            ");
            Console.WriteLine("=================================================\n");
            Console.WriteLine("[1] Update Name");
            Console.WriteLine("[2] Update Phone");
            Console.WriteLine("[3] Update Address");
            Console.WriteLine("[0] Back");
            Console.Write("Enter Choice: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Enter New Name: ");
                cust.name = Console.ReadLine();
                Console.WriteLine("Name Updated Successfully!");
            }
            else if (choice == "2")
            {
                Console.Write("Enter New Phone: ");
                cust.phone = Console.ReadLine();
                Console.WriteLine("Phone Updated Successfully!");
            }
            else if (choice == "3")
            {
                Console.Write("Enter New Address: ");
                cust.address = Console.ReadLine();
                Console.WriteLine("Address Updated Successfully!");
            }
            Console.WriteLine("\n[0] Back");
            Console.ReadLine();
        }

        static void ViewCanePriceUI()
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("                *** Cane Price ***               ");
            Console.WriteLine("=================================================\n");
            Console.WriteLine("Price     : Rs. 200 Per Cane");
            Console.WriteLine("Note      : Only Available Canes");
            Console.WriteLine("Delivery  : Free Home Delivery Available");
            Console.WriteLine("=================================================\n");
            Console.WriteLine("[0] Back");
            Console.ReadLine();
        }

        static void OwnerLogin()
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("                *** Owner Login ***              ");
            Console.WriteLine("=================================================\n");

            int attempts = 0;
            while (attempts < 5)
            {
                Console.Write("Enter Username (Press 0 for Return): ");
                string user = Console.ReadLine();
                if (user == "0") return;

                Console.Write("Enter Password                     : ");
                string pass = Console.ReadLine();

                if (user != ownerUsername || pass != ownerPassword)
                {
                    attempts++;
                    Console.WriteLine($"\nInvalid Username / Password! Attempts Left: {5 - attempts}\n");
                    if (attempts == 5)
                    {
                        Console.WriteLine("Too Many Attempts! Closing...");
                        Environment.Exit(0);
                    }
                }
                else
                {
                    OwnerDashboard();
                    break;
                }
            }
        }

        static void OwnerDashboard()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=================================================");
                Console.WriteLine("              *** Owner Dashboard ***            ");
                Console.WriteLine("=================================================\n");
                Console.WriteLine("[1] Daily Report");
                Console.WriteLine("[2] Update Customer Reports");
                Console.WriteLine("[3] Monthly Report");
                Console.WriteLine("[4] Update Username / Password");
                Console.WriteLine("[0] Logout\n");
                Console.WriteLine("=================================================");
                Console.Write("Enter Your Choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    DailyReportUI();
                }
                else if (choice == "2")
                {
                    UpdateCustomerReportsUI();
                }
                else if (choice == "3")
                {
                    MonthlyReportUI();
                }
                else if (choice == "4")
                {
                    UpdateOwnerPasswordUI();
                }
                else if (choice == "0")
                {
                    break;
                }
            }
        }

        static void DailyReportUI()
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("                *** Daily Report ***             ");
            Console.WriteLine("=================================================\n");

            Console.Write("Enter Date (e.g., 19-04-2026)  : ");
            string date = Console.ReadLine();
            Console.Write("Total Canes Delivered Today    : ");
            int total = int.Parse(Console.ReadLine());
            Console.Write("Paid Canes (Price Received)    : ");
            int paid = int.Parse(Console.ReadLine());
            Console.Write("Labour 1 Cost (Rs.)            : ");
            int l1 = int.Parse(Console.ReadLine());
            Console.Write("Labour 2 Cost (Rs.)            : ");
            int l2 = int.Parse(Console.ReadLine());
            Console.Write("Petrol Cost (Rs.)              : ");
            int petrol = int.Parse(Console.ReadLine());

            DailyReport rep = new DailyReport(date, total, paid, l1, l2, petrol);
            dailyReports.Add(rep);

            Console.WriteLine("\n=================================================");
            Console.WriteLine("               *** Report Summary ***            ");
            Console.WriteLine("=================================================");
            Console.WriteLine($"Date              : {rep.date}");
            Console.WriteLine($"Total Canes       : {rep.totalCanes}");
            Console.WriteLine($"Paid Canes        : {rep.paidCanes}");
            Console.WriteLine($"Total Earning     : Rs. {rep.earning}");
            Console.WriteLine($"Labour 1 Cost     : Rs. {rep.labour1}");
            Console.WriteLine($"Labour 2 Cost     : Rs. {rep.labour2}");
            Console.WriteLine($"Petrol Cost       : Rs. {rep.petrol}");
            Console.WriteLine($"Daily Remaining   : Rs. {rep.remaining}");
            Console.WriteLine("=================================================\n");
            Console.WriteLine("[0] Back");
            Console.ReadLine();
        }

        static void UpdateCustomerReportsUI()
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("            *** Update Customer Reports ***      ");
            Console.WriteLine("=================================================\n");

            if (customers.Count == 0)
            {
                Console.WriteLine("No Customers Registered Yet!");
            }
            else
            {
                foreach (Customer c in customers)
                {
                    Console.WriteLine($"ID: {c.id} | Name: {c.name} | Phone: {c.phone} | Canes: {c.quantity} | Status: {c.billStatus}");
                }

                Console.Write("\nEnter Customer ID to Update (0 to Back): ");
                int custId = int.Parse(Console.ReadLine());

                if (custId != 0)
                {
                    Customer target = null;
                    foreach (Customer c in customers)
                    {
                        if (c.id == custId)
                        {
                            target = c;
                            break;
                        }
                    }

                    if (target == null)
                    {
                        Console.WriteLine("Customer Not Found!");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("=================================================");
                        Console.WriteLine($"Customer : {target.name}");
                        Console.WriteLine("=================================================\n");
                        Console.WriteLine("--- Previous Data ---");
                        Console.WriteLine($"Canes Ordered  : {target.quantity}");
                        Console.WriteLine($"Total Bill     : Rs. {target.totalBill}");
                        Console.WriteLine($"Amount Paid    : Rs. {target.paidAmount}");
                        Console.WriteLine($"Status         : {target.billStatus}\n");

                        Console.WriteLine("--- Add New Delivery Data ---");
                        Console.Write("Date                          : ");
                        string date = Console.ReadLine();
                        Console.Write("Canes Delivered               : ");
                        int canes = int.Parse(Console.ReadLine());
                        Console.Write("Payment Status (Paid/Pending) : ");
                        string status = Console.ReadLine();

                        target.AddDeliveryPayment(canes, status);

                        Console.WriteLine("\nCustomer Report Updated!");
                        Console.WriteLine($"New Total Bill : Rs. {target.totalBill}");
                        Console.WriteLine($"Amount Paid    : Rs. {target.paidAmount}");
                        Console.WriteLine($"Remaining      : Rs. {target.totalBill - target.paidAmount}");
                        Console.WriteLine($"Status         : {target.billStatus}");
                    }
                }
            }

            Console.WriteLine("\n[0] Back");
            Console.ReadLine();
        }

        static void MonthlyReportUI()
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("               *** Monthly Report ***            ");
            Console.WriteLine("=================================================\n");

            Console.Write("Month Name (e.g., April-2026)  : ");
            string month = Console.ReadLine();
            Console.Write("Total Earning This Month (Rs.) : ");
            int earn = int.Parse(Console.ReadLine());
            Console.Write("Electric Cost (Rs.)            : ");
            int electric = int.Parse(Console.ReadLine());
            Console.Write("Labour Cost (Rs.)              : ");
            int labour = int.Parse(Console.ReadLine());
            Console.Write("Petrol Cost (Rs.)              : ");
            int petrol = int.Parse(Console.ReadLine());
            Console.Write("Other Costs (Rs.)              : ");
            int others = int.Parse(Console.ReadLine());

            MonthlyReport rep = new MonthlyReport(month, earn, electric, labour, petrol, others);
            monthlyReports.Add(rep);

            Console.WriteLine("\n=================================================");
            Console.WriteLine("             *** Monthly Summary ***             ");
            Console.WriteLine("=================================================");
            Console.WriteLine($"Month          : {rep.monthName}");
            Console.WriteLine($"Total Earning  : Rs. {rep.totalEarning}");
            Console.WriteLine($"Electric Cost  : Rs. {rep.electricCost}");
            Console.WriteLine($"Labour Cost    : Rs. {rep.labourCost}");
            Console.WriteLine($"Petrol Cost    : Rs. {rep.petrolCost}");
            Console.WriteLine($"Other Costs    : Rs. {rep.otherCost}");
            Console.WriteLine("------------------------------------------------");

            if (rep.profit > 0)
                Console.WriteLine($"Profit         : Rs. {rep.profit}");
            else if (rep.profit < 0)
                Console.WriteLine($"Loss           : Rs. {rep.profit * -1}");
            else
                Console.WriteLine("No Profit / No Loss This Month");

            Console.WriteLine("=================================================\n");
            Console.WriteLine("[0] Back");
            Console.ReadLine();
        }

        static void UpdateOwnerPasswordUI()
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("          *** Update Username / Password ***     ");
            Console.WriteLine("=================================================\n");

            int attempts = 0;
            while (attempts < 5)
            {
                Console.Write("Enter Current Username (0 to Back): ");
                string curUser = Console.ReadLine();
                if (curUser == "0") return;

                Console.Write("Enter Current Password            : ");
                string curPass = Console.ReadLine();

                if (curUser != ownerUsername || curPass != ownerPassword)
                {
                    attempts++;
                    Console.WriteLine($"\nInvalid Current Username / Password! Attempts Left: {5 - attempts}\n");
                    if (attempts == 5)
                    {
                        Console.WriteLine("Too Many Attempts! Closing...");
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Console.Write("Enter New Username : ");
                    ownerUsername = Console.ReadLine();
                    Console.Write("Enter New Password : ");
                    ownerPassword = Console.ReadLine();
                    Console.WriteLine("\nUsername / Password Updated Successfully!");
                    break;
                }
            }
            Console.WriteLine("\n[0] Back");
            Console.ReadLine();
        }
    }
}