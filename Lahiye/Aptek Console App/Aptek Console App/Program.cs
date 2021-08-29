using Aptek_Console_App.Models;
using Aptek_Console_App.Utils;
using System;

namespace Aptek_Console_App
{
    class Program
    {
        static void Main(string[] args)
        {
            Pharmacy pharmacy = new Pharmacy("Aptek");
            while (true)
            {
                Helper.Print(ConsoleColor.Green, $"{pharmacy.Name} Programi Achldi.");
                menu:
                Helper.Print(ConsoleColor.DarkYellow, "Ashagidaki emeliyyatlardan etmek istediyinizin qarshisindaki reqemini yazin.\n" +
                    "(1)Derman elave etmek\n" +
                    "(2)Derman haqqinda melumat almaq\n" +
                    "(3)Butun dermanlarin melumatin gostermek\n" +
                    "(4)Satish etmek\n" +
                    "(5)Programdan chixish etmek");
                int menu = Helper.ConsoleReadLineInt();
                
                switch (menu)
                {
                    case 1:
                        pharmacy.AddDrugToPharmacy();
                        goto menu;
                    case 2:
                        pharmacy.InfoDrug();
                        goto menu;
                    case 3:
                        pharmacy.ShowDrugItems();
                        goto menu;
                    case 4:
                        pharmacy.SaleDrug();
                        goto menu;
                    case 5:
                        Helper.Print(ConsoleColor.DarkGreen, "Programdan Chixish edildi.");
                        break;
                    default:
                        Helper.Print(ConsoleColor.Red, "Menu'da olmayan bir eded yazdiniz!");
                        goto menu;
                }
                break;
            }
        }
    }
}
