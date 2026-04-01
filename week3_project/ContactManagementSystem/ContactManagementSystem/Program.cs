using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactManagementSystem
{
    class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public override string ToString() =>
            $"ID: {Id}, Name: {Name}, Phone: {Phone}, Email: {Email}, Address: {Address}";
    }

    class Program
    {
        static List<Contact> contacts = new List<Contact>
        {
            new Contact { Id = 1, Name = "Alice Smith", Phone = "123-456-7890", Email = "alice@email.com", Address = "123 Main St" },
            new Contact { Id = 2, Name = "Bob Johnson", Phone = "987-654-3210", Email = "bob@email.com", Address = "456 Elm St" }
        };

        static int nextId = 3;

        static void Main(string[] args)
        {
            Console.WriteLine("=== Contact Management System ===");

            var actions = new Dictionary<string, Action>
            {
                { "1", AddContact },
                { "2", ViewAllContacts },
                { "3", SearchContact },
                { "4", UpdateContact },
                { "5", DeleteContact }
            };

            while (true)
            {
                Console.WriteLine("\nSelect an action:");
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. View All Contacts");
                Console.WriteLine("3. Search Contact");
                Console.WriteLine("4. Update Contact");
                Console.WriteLine("5. Delete Contact");
                Console.WriteLine("0. Exit");

                var choice = Console.ReadLine();

                if (choice == "0")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }

                if (actions.ContainsKey(choice))
                    actions[choice]();
                else
                    Console.WriteLine("Invalid choice, try again.");
            }
        }

        private static void AddContact()
        {
            string GetInput(string prompt)
            {
                Console.Write(prompt);
                return Console.ReadLine();
            }

            contacts.Add(new Contact
            {
                Id = nextId++,
                Name = GetInput("Name: "),
                Phone = GetInput("Phone: "),
                Email = GetInput("Email: "),
                Address = GetInput("Address: ")
            });

            Console.WriteLine("Contact added successfully.");
        }

        private static void ViewAllContacts()
        {
            if (!contacts.Any())
            {
                Console.WriteLine("No contacts found.");
                return;
            }

            Console.WriteLine("\n--- All Contacts ---");
            contacts.OrderBy(c => c.Name).ToList().ForEach(Console.WriteLine);
        }

        private static void SearchContact()
        {
            Console.Write("Enter search term (name/phone): ");
            string term = Console.ReadLine().ToLower();

            var results = contacts
                .Where(c => c.Name.ToLower().Contains(term) || c.Phone.Contains(term))
                .ToList();

            Console.WriteLine(results.Count == 0 ? "No contacts found." : "\n--- Search Results ---");
            results.ForEach(Console.WriteLine);
        }

        private static void UpdateContact()
        {
            Console.Write("Enter contact ID to update: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID!");
                return;
            }

            var contact = contacts.FirstOrDefault(c => c.Id == id);

            if (contact == null)
            {
                Console.WriteLine("Contact not found!");
                return;
            }

            Console.WriteLine($"Updating: {contact}\nPress Enter to keep current value");

            string UpdateField(string fieldName, string currentValue)
            {
                Console.Write($"{fieldName} ({currentValue}): ");
                var input = Console.ReadLine();
                return string.IsNullOrWhiteSpace(input) ? currentValue : input;
            }

            contact.Name = UpdateField("Name", contact.Name);
            contact.Phone = UpdateField("Phone", contact.Phone);
            contact.Email = UpdateField("Email", contact.Email);
            contact.Address = UpdateField("Address", contact.Address);

            Console.WriteLine("Contact updated successfully.");
        }

        private static void DeleteContact()
        {
            Console.Write("Enter contact ID to delete: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID!");
                return;
            }

            var contact = contacts.FirstOrDefault(c => c.Id == id);

            if (contact == null)
            {
                Console.WriteLine("Contact not found!");
                return;
            }

            Console.WriteLine($"Are you sure you want to delete: {contact}? (y/n)");

            if (Console.ReadLine().ToLower() == "y")
            {
                contacts.Remove(contact);
                Console.WriteLine("Contact deleted successfully.");
            }
            else
            {
                Console.WriteLine("Deletion cancelled.");
            }
        }
    }
}
