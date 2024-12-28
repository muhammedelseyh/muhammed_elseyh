using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace TravelBookingLibrary
{
    // Namespace: TravelBookingSystem

    // Base Class: Person
    public class Person
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }

        public Person(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
        }

        public virtual void DisplayDetails()
        {
            Console.WriteLine($"Name: {Name}, Email: {Email}, Phone: {Phone}");
        }
    }

    // Derived Class: Passenger
    public class Passenger : Person
    {
        public string PassportNumber { get; private set; }

        public Passenger(string name, string email, string phone, string passportNumber)
            : base(name, email, phone)
        {
            PassportNumber = passportNumber;
        }

        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"Passport Number: {PassportNumber}");
        }
    }

    // Interface: ITicket
    public interface ITicket
    {
        int TicketNumber { get; }
        string Destination { get; }
        DateTime Date { get; }
        decimal CalculatePrice();
        void DisplayTicketDetails();
    }

    // Abstract Class: Ticket (abstracts common behavior)
    public abstract class Ticket : ITicket
    {
        public int TicketNumber { get; private set; }
        public Passenger Passenger { get; private set; }
        public string Destination { get; private set; }
        public DateTime Date { get; private set; }
        public decimal BasePrice { get; private set; }

        protected Ticket(int ticketNumber, Passenger passenger, string destination, DateTime date, decimal basePrice)
        {
            TicketNumber = ticketNumber;
            Passenger = passenger;
            Destination = destination;
            Date = date;
            BasePrice = basePrice;
        }

        public abstract decimal CalculatePrice();

        public void DisplayTicketDetails()
        {
            Console.WriteLine($"Ticket Number: {TicketNumber}, Destination: {Destination}, Date: {Date.ToShortDateString()}, Price: {CalculatePrice():C}");
            Passenger.DisplayDetails();
        }
    }

    // Derived Class: EconomyTicket
    public class EconomyTicket : Ticket
    {
        public EconomyTicket(int ticketNumber, Passenger passenger, string destination, DateTime date, decimal basePrice)
            : base(ticketNumber, passenger, destination, date, basePrice)
        {
        }

        public override decimal CalculatePrice()
        {
            return BasePrice;
        }
    }

    // Derived Class: BusinessTicket
    public class BusinessTicket : Ticket
    {
        private const decimal BusinessClassMultiplier = 1.5m;

        public BusinessTicket(int ticketNumber, Passenger passenger, string destination, DateTime date, decimal basePrice)
            : base(ticketNumber, passenger, destination, date, basePrice)
        {
        }

        public override decimal CalculatePrice()
        {
            return BasePrice * BusinessClassMultiplier;
        }
    }

    // Abstract Class: Transportation
    public abstract class Transportation
    {
        public string VehicleNumber { get; private set; }
        public string Type { get; private set; }
        public int Capacity { get; private set; }

        protected Transportation(string vehicleNumber, string type, int capacity)
        {
            VehicleNumber = vehicleNumber;
            Type = type;
            Capacity = capacity;
        }

        public abstract void DisplayInfo();
    }

    // Derived Class: Flight
    public class Flight : Transportation
    {
        public string Airline { get; private set; }

        public Flight(string vehicleNumber, string airline, int capacity)
            : base(vehicleNumber, "Flight", capacity)
        {
            Airline = airline;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Flight Number: {VehicleNumber}, Airline: {Airline}, Capacity: {Capacity}");
        }
    }

    // Derived Class: Bus
    public class Bus : Transportation
    {
        public string Route { get; private set; }

        public Bus(string vehicleNumber, string route, int capacity)
            : base(vehicleNumber, "Bus", capacity)
        {
            Route = route;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Bus Number: {VehicleNumber}, Route: {Route}, Capacity: {Capacity}");
        }
    }

    // Manager Class: BookingManager
    public class BookingManager
    {
        private List<ITicket> bookings = new List<ITicket>();

        public void AddBooking(ITicket ticket)
        {
            bookings.Add(ticket);
            Console.WriteLine("Booking added successfully!");
        }

        public void CancelBooking(int ticketNumber)
        {
            var ticket = bookings.FirstOrDefault(t => t.TicketNumber == ticketNumber);
            if (ticket != null)
            {
                bookings.Remove(ticket);
                Console.WriteLine("Booking cancelled successfully!");
            }
            else
            {
                Console.WriteLine("Ticket not found.");
            }
        }

        public void ViewBookings()
        {
            if (!bookings.Any())
            {
                Console.WriteLine("No bookings available.");
                return;
            }

            foreach (var ticket in bookings)
            {
                ticket.DisplayTicketDetails();
            }
        }
    }

    // Ticket Factory: Generates tickets based on user input
    public class TicketFactory
    {
        public ITicket CreateTicket(int ticketType, Passenger passenger, string destination, DateTime date, decimal basePrice, int ticketNumber)
        {
            if (ticketType == 1)
            {
                return new EconomyTicket(ticketNumber, passenger, destination, date, basePrice);
            }
            else if (ticketType == 2)
            {
                return new BusinessTicket(ticketNumber, passenger, destination, date, basePrice);
            }
            else
            {
                throw new ArgumentException("Invalid ticket type.");
            }
        }
    }
}