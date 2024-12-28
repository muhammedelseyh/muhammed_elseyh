using System;
using TravelBookingLibrary;
using System.Globalization;

public class Program
{
    public static void Main(string[] args)
    {
        TravelBookingLibrary.BookingManager bookingManager = new TravelBookingLibrary.BookingManager();
        TravelBookingLibrary.TicketFactory ticketFactory = new TravelBookingLibrary.TicketFactory();

        while (true)
        {
            Console.WriteLine("\n--- Travel Booking System ---");
            Console.WriteLine("1. Add Booking");
            Console.WriteLine("2. View Bookings");
            Console.WriteLine("3. Cancel Booking");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Enter Passenger Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Email: ");
                    string email = Console.ReadLine();
                    Console.Write("Enter Phone: ");
                    string phone = Console.ReadLine();
                    Console.Write("Enter Passport Number: ");
                    string passport = Console.ReadLine();

                    TravelBookingLibrary.Passenger passenger = new TravelBookingLibrary.Passenger(name, email, phone, passport);

                    Console.Write("Enter Destination: ");
                    string destination = Console.ReadLine();
                    Console.Write("Enter Travel Date (yyyy-MM-dd): ");
                    DateTime date;
                    if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    {
                        Console.WriteLine("Invalid date format. Please use yyyy-MM-dd.");
                        continue;
                    }

                    Console.Write("Enter Base Price: ");
                    decimal basePrice;
                    if (!decimal.TryParse(Console.ReadLine(), out basePrice))
                    {
                        Console.WriteLine("Invalid price format.");
                        continue;
                    }

                    Console.Write("Choose Ticket Type (1: Economy, 2: Business): ");
                    int ticketType = int.Parse(Console.ReadLine());

                    int ticketNumber = bookingManager.GetHashCode(); // Simulate unique ticket number
                    ITicket ticket = ticketFactory.CreateTicket(ticketType, passenger, destination, date, basePrice, ticketNumber);

                    bookingManager.AddBooking(ticket);
                    break;

                case 2:
                    bookingManager.ViewBookings();
                    break;

                case 3:
                    Console.Write("Enter Ticket Number to Cancel: ");
                    int ticketNumberToCancel;
                    if (!int.TryParse(Console.ReadLine(), out ticketNumberToCancel))
                    {
                        Console.WriteLine("Invalid input.");
                        continue;
                    }

                    bookingManager.CancelBooking(ticketNumberToCancel);
                    break;

                case 4:
                    Console.WriteLine("Exiting the application. Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
