using System.Collections.Generic;
using System.Linq;

namespace Hackathon
{
    class Pizza
    {
        public Pizza()
        { }

        public Pizza(IEnumerable<string> toppings, int index) : this()
        {
            Toppings = new HashSet<string>(toppings.ToList().OrderBy(s => s));
            Index = index;
        }

        public ISet<string> Toppings { get; }
        public int Index { get; }

        public int TotalToppings => (int)Toppings?.Count();
    }
}