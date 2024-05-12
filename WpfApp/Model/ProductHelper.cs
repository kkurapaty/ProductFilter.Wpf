using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfApp.Model
{
    public static class ProductHelper
    {
        public static List<string> BusinessLines = new List<string>
        {
            "Global Banking",
            "Investment Banking",
            "Retail Banking",
            "Resource Management",
            "Equity Derivatives",
            "All Business Lines"
        };

        public static List<Product> GetProducts(int count)
        {
            List<Product> products = new List<Product>();
            Random random = new Random();
            for (int i = 1; i <= count; i++)
            {
                var parentProduct = CreateProduct(i, $"Product {i}");
                parentProduct.BusinessLine = BusinessLines[random.Next(0, BusinessLines.Count-1)];
                products.Add(parentProduct);
                int childCount = random.Next(0, 10);
                for (int j = 1; j <= childCount; j++)
                {
                    var childProduct = CreateProduct(j, $"{parentProduct.Name} -> Child {j:##}");
                    childProduct.ParentId = parentProduct.ProductId;
                    childProduct.BusinessLine = parentProduct.BusinessLine;
                    products.Add(childProduct);
                }
            }

            return products;
        }

        public static Product CreateProduct(int id, string name)
        {
            var product = new Product()
            {
                ProductId = Guid.NewGuid().ToString(),
                Name = name,
                Enabled = true, // !(id % 3==0 || id % 5==0),
                IsSelected = false //id.IsPrime()
            };
            return product;
        }

        public static bool IsPrime(this int number)
        {
            if ((number <= 1) || (number % 2 == 0)) return false;
            if (number == 2) return true;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0) return false;

            return true;
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            var result = new ObservableCollection<T>();
            foreach (T item in source)
            {
                result.Add(item);
            }
            return result;
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
                action(item);
        }
    }
}