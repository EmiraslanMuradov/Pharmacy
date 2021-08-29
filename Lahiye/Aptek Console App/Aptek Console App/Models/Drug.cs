using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptek_Console_App.Models
{
    class Drug
    {
        public string Name { get; set; }

        public DrugType Type { get; set; }

        public int Price { get; set; }

        public int Count { get; set; }

        public int Id { get; }

        private static int _id = 0;

        public Drug()
        {
            _id++;
            Id = _id;
        }

        public Drug(string name, DrugType type, int price, int count) : this()
        {
            Name = name;
            Type = type;
            Price = price;
            Count = count;
        }

        public override string ToString()
        {
            return $"({Id}) Dermanin adi: {Name} | Qiymeti: {Price} AZN | Sayi: {Count}";
        }

    }
}
