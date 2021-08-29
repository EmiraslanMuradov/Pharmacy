using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptek_Console_App
{
    class DrugType
    {
        public string TypeName { get; set; }

        public int Id { get; }

        private static int _id = 0;

        public DrugType()
        {
            _id++;
            Id = _id;
        }        

        public DrugType(string typeName) : this()
        {
            TypeName = typeName;
        }

        public override string ToString()
        {
            return $"({Id}) Dermanin tipi - {TypeName}:";
        }
    }
}
