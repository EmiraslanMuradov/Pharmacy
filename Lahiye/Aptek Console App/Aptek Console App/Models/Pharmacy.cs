using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptek_Console_App.Models
{
    partial class Pharmacy
    {
        public string Name { get; set; }
        
        public int Id { get; set; }

        private static int _id = 0;

        private List<Drug> _drugs;

        private List<DrugType> _types;

        public Pharmacy()
        {
            _id++;
            Id = _id;
            _drugs = new List<Drug>();
            _types = new List<DrugType>();
        }

        public Pharmacy(string name) : this()
        {
            Name = name;
        }
    }
}
