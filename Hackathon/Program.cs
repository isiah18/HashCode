using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hackathon
{
    class Program
    {
        static List<Pizza> pizzas = new List<Pizza>();
        static double globalPoints;
        static IDictionary<int, int> recievers;
        static int ordersDelivered = 0;
        static string fileResultPath;
        static StringBuilder orders = new StringBuilder();
        static void Main(string[] args)
        {
            var fileLocation = args[0];
            fileResultPath = Path.Combine(Directory.GetCurrentDirectory(), args[1]);
            Initialize(fileLocation);

            ProcessOrders();

            orders.Replace("#", ordersDelivered.ToString(), 0, ordersDelivered.ToString().Length);
            using (var file = File.AppendText(fileResultPath))
            {
                file.WriteLine(orders);
            }

            Console.WriteLine("Points : " + globalPoints);            
        }

        private static void Initialize(string fileLocation)
        {
            using (File.CreateText(fileResultPath))
            {

            }

            orders.AppendLine("#");

            using (var file = new StreamReader(fileLocation))
            {
                var pg2g3g4 = file.ReadLine().Split(' ');
                var p = Convert.ToInt32(pg2g3g4[0]);
                var g2 = Convert.ToInt32(pg2g3g4[1]);
                var g3 = Convert.ToInt32(pg2g3g4[2]);
                var g4 = Convert.ToInt32(pg2g3g4[3]);

                recievers = new Dictionary<int, int>() { { 2, g2 * 2 }, { 3, g3 * 3 }, { 4, g4 * 4 } };

                for (var i = 0; i < p; i++)
                {
                    var toppings = file.ReadLine().Split(' ').ToList().Skip(1);
                    pizzas.Add(new Pizza(toppings, i));
                }
            }

            pizzas = pizzas.OrderBy(s => s.TotalToppings).ToList();
        }

        private static void ProcessOrders()
        {
            while (pizzas.Count > 0)
            {

                if (recievers[2] > 0 && pizzas.Count() % 2 == 0)
                {
                    CreateOrder(2);

                }
                else if (recievers[3] > 0 && pizzas.Count() % 3 == 0)
                {
                    CreateOrder(3);
                }
                else if (recievers[4] > 0 && pizzas.Count() % 4 == 0)
                {
                    CreateOrder(4);
                }

                else if (recievers[2] > 0 && pizzas.Count >= 2)
                {
                    CreateOrder(2);
                }
                else if (recievers[3] > 0 && pizzas.Count >= 3)
                {
                    CreateOrder(3);
                }
                else if (recievers[4] > 0 && pizzas.Count >= 4)
                {
                    CreateOrder(4);
                }
                else
                {
                    break;
                }

                ordersDelivered++;
            }
        }

        public static void CreateOrder(int totalPizzas)
        {
            var order = new StringBuilder();
            switch (totalPizzas)
            {
                case 2:
                    order.Append("2 ");
                    var toppings2 = new HashSet<string>(pizzas[0].Toppings);
                    //delete pizza
                    order.Append($"{pizzas[0].Index} ");
                    pizzas.RemoveAt(0);
                    toppings2.UnionWith(pizzas[pizzas.Count - 1].Toppings);
                    //delete pizza
                    order.Append($"{pizzas[pizzas.Count - 1].Index}");
                    pizzas.RemoveAt(pizzas.Count - 1);

                    recievers[2] -= 2;
                    globalPoints += Math.Pow(toppings2.Count(), 2);
                    break;

                case 3:
                    order.Append("3 ");
                    var toppings3 = new HashSet<string>(pizzas[0].Toppings);

                    //delete pizza
                    order.Append($"{pizzas[0].Index} ");
                    pizzas.RemoveAt(0);
                    toppings3.UnionWith(pizzas[pizzas.Count - 1].Toppings);

                    //delete pizza
                    order.Append($"{pizzas[pizzas.Count - 1].Index} ");
                    pizzas.RemoveAt(pizzas.Count - 1);

                    var middlePointer = (int)Math.Floor(pizzas.Count / 2.0);
                    toppings3.UnionWith(pizzas[middlePointer].Toppings);
                    //delete pizza
                    order.Append($"{pizzas[middlePointer].Index}");
                    pizzas.RemoveAt(middlePointer);

                    recievers[3] -= 3;
                    globalPoints += Math.Pow(toppings3.Count(), 2);
                    break;
                case 4:
                    order.Append("4 ");
                    var toppings4 = new HashSet<string>(pizzas[0].Toppings);
                    //delete pizza
                    order.Append($"{pizzas[0].Index} ");
                    pizzas.RemoveAt(0);
                    toppings4.UnionWith(pizzas[0].Toppings);

                    var firstQuarter = (int)Math.Floor(pizzas.Count / 4.0);
                    order.Append($"{pizzas[firstQuarter].Index} ");
                    pizzas.RemoveAt(firstQuarter);
                    toppings4.UnionWith(pizzas[firstQuarter].Toppings);

                    var thirdQuarter = 3 * ((int)Math.Floor(pizzas.Count / 4.0));
                    order.Append($"{pizzas[thirdQuarter].Index} ");
                    pizzas.RemoveAt(thirdQuarter);
                    toppings4.UnionWith(pizzas[thirdQuarter].Toppings);

                    ////delete pizza
                    //order.Append($"{pizzas[0].Index} ");
                    //pizzas.RemoveAt(0);
                    //toppings4.UnionWith(pizzas[pizzas.Count - 1].Toppings);

                    ////delete pizza
                    //order.Append($"{pizzas[pizzas.Count - 1].Index} ");
                    //pizzas.RemoveAt(pizzas.Count - 1);
                    //toppings4.UnionWith(pizzas[pizzas.Count - 1].Toppings);

                    //delete pizza
                    order.Append($"{pizzas[pizzas.Count - 1].Index} ");
                    pizzas.RemoveAt(pizzas.Count - 1);

                    recievers[4] -= 4;
                    globalPoints += Math.Pow(toppings4.Count(), 2);
                    break;
            }
            orders.AppendLine(order.ToString());
        }
    }
}