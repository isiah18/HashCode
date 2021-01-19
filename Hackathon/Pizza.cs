using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hackathon
{
    class Pizza
    {
        public Pizza()
        { }

        public Pizza(IEnumerable<string> toppings) : this()
        {
            Toppings = new HashSet<string>(toppings.ToList().OrderBy(s => s));
        }

        public ISet<string> Toppings { get; set; }

        public int TotalToppings => (int)Toppings?.Count();
    }
}
