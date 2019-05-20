using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Bus myBus = new Bus("asdasd", 12))
            using (Bus myOtherBus = new Bus("asdasd", 12))
            {
                myBus.print();
                if (myBus.Equals(myOtherBus))
                {
                    Console.WriteLine("equal");
                }
                else
                {
                    Console.WriteLine("not equal");
                }
            }


            string regexTest = "123456abcd789qwer";
            Regex re = new Regex(@"[0-9]{1,3}[a-z]{1,3}");
            MatchCollection matches = re.Matches(regexTest);
            Console.WriteLine("{0} match(es) found in {1}", matches.Count, regexTest);

            foreach(Match m in matches)
            {
                Console.WriteLine(m.ToString());
            }

            Console.ReadKey();
        }
    }

    class Vehicle
    {
        protected string name;
        public Vehicle(string n)
        {
            this.name = n;
        }
        public void print()
        {
            Console.WriteLine("name: " + this.name);
        }
    }

    class Bus : Vehicle, IDisposable, BusEquatable<Bus>
    {
        private int capacity;
        public Bus(string n, int c) : base(n)
        {
            this.capacity = c;
        }

        public void Dispose() { }

        public new void print()
        {
            Console.WriteLine("name: " + this.name + "     capacity: " + this.capacity);
        }

        public bool Equals(Bus other)
        {
            if(this.capacity == other.capacity && this.name == other.name)
            {
                return true;
            }
            return false;
        }
    }

    interface BusEquatable<T>
    {
        bool Equals(T obj);
    }

}
