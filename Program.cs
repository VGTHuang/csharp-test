using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Bus myBus = new Bus("asdasd", 12))
            {
                myBus.print();
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

    class Bus : Vehicle, IDisposable
    {
        private int capacity;
        public Bus(string n, int c) : base(n)
        {
            this.capacity = c;
        }

        public void Dispose()
        {
            
        }

        public new void print()
        {
            Console.WriteLine("name: " + this.name + "     capacity: " + this.capacity);
        }
    }

}
