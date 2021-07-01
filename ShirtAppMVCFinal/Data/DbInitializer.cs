using ShirtAppMVCFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShirtAppMVCFinal.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Shirts.Any())
            {
                return;   // DB has been seeded
            }

            var shirts = new Shirt[]
            {
            new Shirt{ShirtName="Test1",Size = Size.S, Price = 3},
            new Shirt{ShirtName="Test2",Size = Size.M, Price = 3}
            };
            foreach (Shirt s in shirts)
            {
                context.Shirts.Add(s);
            }
            context.SaveChanges();

            var transactions = new Transaction[]
            {
            new Transaction{Email="Test@gmail.com",ShirtID = 1},
            new Transaction{Email="admin@gmail.com",ShirtID = 2},
            };
            foreach (Transaction c in transactions)
            {
                context.Transactions.Add(c);
            }
            context.SaveChanges();
        }
    }
}
