using Aptek_Console_App.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptek_Console_App.Models
{
    partial class Pharmacy
    {
        public void AddDrugToPharmacy()
        {
            addDrug:
            Drug drug = new Drug();

            //Dermanin adi
            drugName:
            Helper.Print(ConsoleColor.Blue, "Dermanin adin yazin:");
            string drugName = Console.ReadLine();
            if (drugName.TrimStart().Count() == 0)
            {
                Helper.Print(ConsoleColor.Red, "Dermanin adi bosluq ola bilmez!");
                goto drugName;
            }
            drug.Name = drugName;

            //Dermanin tipi
            drugType:
            Helper.Print(ConsoleColor.Blue, "Dermanin tipini yazin:");
            string typeName = Console.ReadLine();
            if (typeName.TrimStart().Count() == 0)
            {
                Helper.Print(ConsoleColor.Red, "Dermanin tipi bosluq ola bilmez!");
                goto drugType;
            }
            bool checkType = false;
            foreach (DrugType type in _types)
            {
                if (type.TypeName == typeName)
                {
                    checkType = true;
                    drug.Type = type;
                }
            }
            if (!checkType)
            {
                DrugType drugType = new DrugType(typeName);
                drug.Type = drugType;
                _types.Add(drugType);
            }

            //Dermanin qiymeti
            if (_drugs.Count() == 0)
            {
                Helper.Print(ConsoleColor.Blue, "Dermanin qiymetini yazin:");
                int intDrugPrice = Helper.ConsoleReadLineInt();
                drug.Price = intDrugPrice;
            }
            foreach (Drug item in _drugs)
            {
                if (item.Name == drugName && item.Type.TypeName == typeName)
                {
                    drug.Price = item.Price;
                    break;
                }
                else
                {
                    Helper.Print(ConsoleColor.Blue, "Dermanin qiymetini yazin:");
                    int intDrugPrice = Helper.ConsoleReadLineInt();
                    drug.Price = intDrugPrice;
                    break;
                }
            }
            
            //Dermanin sayi
            Helper.Print(ConsoleColor.Blue, "Dermanin sayini yazin:");
            int intDrugCount = Helper.ConsoleReadLineInt();
            drug.Count = intDrugCount;

            //Yaradilan drug'i _drugs listine elave etmek
            bool checkAdd = true;
            foreach (Drug item in _drugs)
            {
                if (item.Name == drug.Name && item.Type == drug.Type /*&& item.Price == drug.Price*/)
                {
                    item.Count += drug.Count;
                    checkAdd = false;
                }
            }
            if (checkAdd)            
                _drugs.Add(drug);            
            Helper.Print(ConsoleColor.Green, "Derman elave edildi.");

            //Yeni derman elave etmek teklifi
            Helper.Print(ConsoleColor.DarkCyan, "Elave edilecek bashqa bir derman var?");
            bool chechAddDrug = Helper.YesNoCheck();
            if (chechAddDrug)
                goto addDrug;
        }

        public void InfoDrug()
        {
            infoDrug:
            Helper.Print(ConsoleColor.Blue, "Melumat almaq istediyiniz dermanin adini yazin:");
            string drugName = Console.ReadLine();
            if (drugName.TrimStart().Count() == 0)
            {
                Helper.Print(ConsoleColor.Red, "Derman adi daxil etmediniz!");
                goto infoDrug;
            }

            //Eger varsa bu adda olan dermanlari gostermek
            foreach (Drug drug in _drugs.FindAll(drug => drug.Name.ToUpper().Contains(drugName.ToUpper())))
            {
                Helper.Print(ConsoleColor.Yellow, drug.ToString());
            }

            //Eger derman yoxdursa yeniden cehd teklif etmek
            if (_drugs.FindAll(drug => drug.Name.ToUpper().Contains(drugName.ToUpper())).Count() == 0)
            {
                Helper.Print(ConsoleColor.Red, "Bele adda derman yoxdur!");
                Helper.Print(ConsoleColor.DarkCyan, "Bashqa derman haqqinda melumat almaq isteyirsiniz?");
                bool checkInfoDrug = Helper.YesNoCheck();
                if (checkInfoDrug)
                    goto infoDrug;             
            }
        }

        public void ShowDrugItems()
        {
            foreach (DrugType type in _types)
            {
                Helper.Print(ConsoleColor.Magenta, type.ToString());
                foreach (Drug drug in _drugs)
                {
                    if (drug.Type == type)
                        Helper.Print(ConsoleColor.Yellow, "    " + drug.ToString());
                }
            }
            if(_drugs.Count()==0)
                Helper.Print(ConsoleColor.Red, "Aptekde derman yoxdur");
            Helper.Print(ConsoleColor.Green, "Butun dermanlar haqqinda melumat gosterildi.");
        }

        public void SaleDrug()
        {
            List<Drug> sellingDrugList = new List<Drug>();
            List<int> drugCountList = new List<int>();

            //Satilacaq dermanin adinin alinmasi
            sellDrug:
            Helper.Print(ConsoleColor.Blue, "Satilacaq dermanin adini yazin:");
            string sellingDrug = Console.ReadLine();
            if (sellingDrug.TrimStart().Count() == 0)
            {
                Helper.Print(ConsoleColor.Red, "Derman adi daxil etmediniz!");
                goto sellDrug;
            }

            chooseDrug:
            if (_drugs.FindAll(drug => drug.Name.ToUpper().Contains(sellingDrug.ToUpper())).Count() == 0)
                Helper.Print(ConsoleColor.Red, "Aptekde bu ve ya buna benzer bir derman yoxdur!");

            if (_drugs.FindAll(drug => drug.Name.ToUpper().Contains(sellingDrug.ToUpper())).Count() != 0)
            {
                Helper.Print(ConsoleColor.DarkCyan, "Istediyiniz derman ashagidakilardan hansidir?");
                foreach (Drug drug in _drugs.FindAll(drug => drug.Name.ToUpper().Contains(sellingDrug.ToUpper())))
                {
                    Helper.Print(ConsoleColor.Yellow, $"({drug.Id}) Dermanin adi: {drug.Name} | Dermanin tipi: {drug.Type.TypeName}");
                }
            }

            Helper.Print(ConsoleColor.DarkGray, "Yuxarida (istediyiniz) derman yoxdursa S(siradaki) herfini daxil edin.");

            Helper.Print(ConsoleColor.DarkGray, "Satishi legv etmek uchun L(legv) herfini daxil edin.");
            string drugIdStr = Console.ReadLine();
            if (drugIdStr.ToUpper() == "S")
                goto sellDrug;
            if (drugIdStr.ToUpper() == "L")
            {
                Helper.Print(ConsoleColor.DarkRed, "SATISH LEGV EDILDI!!!");
                return;
            }
            if (int.TryParse(drugIdStr, out int drugIdInt))
            {
                foreach (Drug drug in _drugs.FindAll(drug => drug.Name.ToUpper().Contains(sellingDrug.ToUpper())))
                {
                    if (drug.Id == drugIdInt)
                        sellingDrug = drug.Name;
                }

                List<int> idList = new List<int>();
                foreach (Drug drug in _drugs.FindAll(drug => drug.Name.ToUpper().Contains(sellingDrug.ToUpper())))
                {
                    idList.Add(drug.Id);
                }
                if (!idList.Contains(drugIdInt))
                {
                    Helper.Print(ConsoleColor.Red, "Yanlish simvol daxil etdiniz!");
                    goto chooseDrug;
                }
            }
            else
            {
                Helper.Print(ConsoleColor.Red, "Yanlish simvol daxil etdiniz!");
                goto chooseDrug;
            }

            //Derman yoxdursa gelen, yeni derman axtarma teklifi
            if (_drugs.FindAll(drug => drug.Name.ToUpper().Contains(sellingDrug.ToUpper())).Count() == 0)
            {
                Helper.Print(ConsoleColor.Red, "Bu adda derman yoxdur!");
                Helper.Print(ConsoleColor.DarkCyan, "Bashqa satilacaq derman var?");
                bool checkSellDrug = Helper.YesNoCheck();
                if (checkSellDrug)
                    goto sellDrug;
                else
                    return;
            }

            //Satilacaq dermanin sayinin alinmasi
            Helper.Print(ConsoleColor.Blue, "Satilacaq dermanin sayini yazin:");
            int drugCount = Helper.ConsoleReadLineInt();

            //Satilacaq dermanlari siyahiya elave etmek
            bool checkSameDrug = false;
            foreach (Drug _drug in sellingDrugList)
            {
                if (_drug.Name == sellingDrug)
                {
                    checkSameDrug = true;
                }
            }
            foreach (Drug drug in _drugs)
            {   
                if (drug.Name.ToUpper() == sellingDrug.ToUpper() && !checkSameDrug)
                    sellingDrugList.Add(drug);
            }

            //Satilacaq dermanlarin saylarini siyahiya elave etmek
            if (checkSameDrug)
            {
                foreach (Drug drug in sellingDrugList)
                {
                    if (drug.Name == sellingDrug)
                    {
                        drugCountList[sellingDrugList.IndexOf(drug)] += drugCount;
                    }
                }
            }
            if(!checkSameDrug)
                drugCountList.Add(drugCount);

            //Bashqa satilacaq derman olub olmadiqin sorushmaq
            Helper.Print(ConsoleColor.DarkCyan, "Bashqa satilacaq derman var?");
            bool checkAnotherDrug = Helper.YesNoCheck();
            if (checkAnotherDrug)
                goto sellDrug;

            //Dermanlarin aptekde olub olmadiginin yoxlanilmasi
            checkDrugInPharmacy:
            foreach (Drug drug in sellingDrugList)
            {
                if (drug.Count == 0)
                {
                    Helper.Print(ConsoleColor.Red, $"{drug.Name} adli derman bitib!");
                    drugCountList.Remove(drugCountList[sellingDrugList.IndexOf(drug)]);
                    sellingDrugList.Remove(drug);                                           
                }

                if (sellingDrugList.Count() == 0)
                {
                    Helper.Print(ConsoleColor.DarkRed, "SATISH LEGV EDILDI!!!");
                    return;
                }
            }
            foreach (Drug drug in sellingDrugList)
            {
                if (0 < drug.Count && drug.Count < drugCountList[sellingDrugList.IndexOf(drug)])
                {
                    Helper.Print(ConsoleColor.DarkRed, $"{drug.Name} adli dermandan sadece {drug.Count} eded qalib. Mushteri almaq isteyirmi?");
                    bool checkDrugCount = Helper.YesNoCheck();
                    if (checkDrugCount)
                        drugCountList[sellingDrugList.IndexOf(drug)] = drug.Count;
                    else
                    {
                        drugCountList.Remove(drugCountList[sellingDrugList.IndexOf(drug)]);
                        sellingDrugList.Remove(drug);
                        goto checkDrugInPharmacy;
                    }
                }
            }

            //Qiymetlerin hesaplanmasi
            int priceOfDrugs = 0;
            foreach (Drug drug in sellingDrugList)
            {
                int drugPrice = drug.Price * drugCountList[sellingDrugList.IndexOf(drug)];
                priceOfDrugs += drugPrice;
                Helper.Print(ConsoleColor.Yellow, $"({drug.Id}) Dermanin adi: {drug.Name} " +
                    $"| Satilacaq dermanin sayi: {drugCountList[sellingDrugList.IndexOf(drug)]} " +
                    $"| Bu dermanin toplam qiymeti: {drugPrice} AZN");
            }
            Helper.Print(ConsoleColor.Yellow, $"Toplam qiymet: {priceOfDrugs} AZN");

            //Satishin davam etmesi/legv edilmesi
            inputMenu:
            Helper.Print(ConsoleColor.DarkYellow, "Ashagidaki emeliyyatlardan etmek istediyinizin qarshisindaki reqemini yazin!\n" +
                "(1)Satisha davam etmek uchun\n" +
                "(2)Satishi legv etmek uchun\n");            
            int menu = Helper.ConsoleReadLineInt();
            switch (menu)
            {
                case 1:
                    case1:
                    foreach (Drug drug in _drugs)
                    {
                        foreach (Drug item in sellingDrugList)
                        {
                            if (drug.Name == item.Name && drug.Type.TypeName == item.Type.TypeName)
                                drug.Count -= drugCountList[sellingDrugList.IndexOf(item)];
                        }
                    }
                    Helper.Print(ConsoleColor.Blue, "Mushterinin verdiyi pulu yazin:");
                    int cash = Helper.ConsoleReadLineInt();
                    if (priceOfDrugs > cash || cash < 0)
                    {
                        Helper.Print(ConsoleColor.Red, "Yazilan pul toplam qiymete beraber ve ya boyuk olmalidir!");
                        goto case1;
                    }
                    Helper.Print(ConsoleColor.DarkGreen, $"Musteriye qaytarilacaq mebleg: {cash-priceOfDrugs} AZN");
                    break;
                case 2:
                    Helper.Print(ConsoleColor.DarkRed, "SATISH LEGV EDILDI!!!");
                    return;
                default:
                    Helper.Print(ConsoleColor.Red, "Menu'da olan bir eded yazin!");
                    goto inputMenu;
            }
        }
    }
}
