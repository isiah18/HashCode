using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hackathon
{
    class Program
    {
        static List<Pizza> pizzas = new List<Pizza>();
        static double globalPoints;
        static int totalPeople;
        static void Main(string[] args)
        {
            string fileLocation = args[0];
            StreamReader file = new StreamReader(fileLocation);
            var pg2g3g4 = file.ReadLine().Split(' ');
            var p = Convert.ToInt32(pg2g3g4[0]);
            var g2 = Convert.ToInt32(pg2g3g4[1]);
            var g3 = Convert.ToInt32(pg2g3g4[2]);
            var g4 = Convert.ToInt32(pg2g3g4[3]);

            totalPeople = (g2 * 2) + (g3 * 3) + (g4 * 4);
            var whileValidation = totalPeople - p;

            var recievers = new Dictionary<int, int>() { { 2, g2 * 2 }, { 3, g3 * 3 }, { 4, g4 * 4 } };
            

            for (var i = 0; i < p; i++)
            {
                var toppings = file.ReadLine().Split(' ').ToList().Skip(1);
                pizzas.Add(new Pizza(toppings));
            }

            pizzas = pizzas.OrderBy(s => s.TotalToppings).ToList();

            //while (p == whileValidation)
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

                else if (recievers[2] > 0)
                {
                    CreateOrder(2);
                }
                else if (recievers[3] > 0)
                {
                    CreateOrder(3);
                }
                else if (recievers[4] > 0)
                {
                    CreateOrder(4);
                }

                //whileValidation = totalPeople - p;

            }

            Console.WriteLine(globalPoints);
            Console.ReadLine();
        }

        public static void CreateOrder(int totalPizzas)
        {
            switch (totalPizzas)
            {
                case 2:
                    var toppings2 = new HashSet<string>(pizzas[0].Toppings);
                    pizzas.RemoveAt(0);
                    //delete pizza
                    toppings2.UnionWith(pizzas[pizzas.Count - 1].Toppings);
                    pizzas.RemoveAt(pizzas.Count - 1);
                    //delete pizza
                    globalPoints += Math.Pow(toppings2.Count(), 2);
                    //totalPeople = totalPeople - 2;
                    break;

                case 3:
                    var toppings3 = new HashSet<string>(pizzas[0].Toppings);
                    pizzas.RemoveAt(0);
                    //delete pizza
                    toppings3.UnionWith(pizzas[pizzas.Count - 1].Toppings);
                    pizzas.RemoveAt(pizzas.Count - 1);
                    //delete pizza
                    var middlePointer = (int)Math.Floor(pizzas.Count / 2.0);
                    toppings3.UnionWith(pizzas[middlePointer].Toppings);
                    pizzas.RemoveAt(middlePointer);
                    //delete pizza
                    globalPoints += Math.Pow(toppings3.Count(), 2);
                    //totalPeople = totalPeople - 3;
                    break;
                case 4:
                    var toppings4 = new HashSet<string>(pizzas[0].Toppings);
                    pizzas.RemoveAt(0);
                    //delete pizza
                    toppings4.UnionWith(pizzas[0].Toppings);
                    pizzas.RemoveAt(0);
                    //delete pizza
                    toppings4.UnionWith(pizzas[pizzas.Count - 1].Toppings);
                    pizzas.RemoveAt(pizzas.Count - 1);
                    //delete pizza
                    toppings4.UnionWith(pizzas[pizzas.Count - 1].Toppings);
                    pizzas.RemoveAt(pizzas.Count - 1);
                    //delete pizza
                    globalPoints += Math.Pow(toppings4.Count(), 2);
                    //totalPeople = totalPeople - 4;
                    break;
            }
        }
    }
}
