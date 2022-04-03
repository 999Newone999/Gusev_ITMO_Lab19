using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gusev_ITMO_Lab19
{
    class Program
    {

            // Задание 19 LINQ
            /* 
             1.    Модель  компьютера  характеризуется  кодом  и  названием  марки компьютера,  типом  процессора,  частотой  работы  процессора,
             объемом оперативной памяти, объемом жесткого диска, объемом памяти видеокарты, стоимостью компьютера в условных единицах и количеством экземпляров,
             имеющихся в наличии. Создать список, содержащий 6-10 записей с различным набором значений характеристик.

Определить:

- все компьютеры с указанным процессором. Название процессора запросить у пользователя;

- все компьютеры с объемом ОЗУ не ниже, чем указано. Объем ОЗУ запросить у пользователя;

- вывести весь список, отсортированный по увеличению стоимости;

- вывести весь список, сгруппированный по типу процессора;

- найти самый дорогой и самый бюджетный компьютер;

- есть ли хотя бы один компьютер в количестве не менее 30 штук?
             * 
             */
       static void Main(string[] args)
       {
            List<Computer> listOfComputers = new List<Computer>
            {
                new Computer() {Code="90Q7000QRS", Model="Lenovo IdeaCentre Mini 5 01IMH05 (9",ProcessorType="Intel Core i5",ProcessorFrequency=2000, AmauntOfRAM=8, HardDiskCapacity=256, VideoMemory=0, Price=92881, NumberOnStock=30 },
                new Computer() {Code="10T70099RU", Model="Lenovo ThinkCentre M720 Tiny",ProcessorType="Intel Core i3",ProcessorFrequency=3700, AmauntOfRAM=8, HardDiskCapacity=256, VideoMemory=0, Price=73990, NumberOnStock=21 },
                new Computer() {Code="3681-2673", Model="DELL Vostro 3681 SFF",ProcessorType="Intel Core i5",ProcessorFrequency=2900, AmauntOfRAM=8, HardDiskCapacity=256, VideoMemory=0, Price=88286, NumberOnStock=4 },
                new Computer() {Code="11EF0002RU", Model="Lenovo V50s 07IMB",ProcessorType="Intel Core i3",ProcessorFrequency=3600, AmauntOfRAM=8, HardDiskCapacity=1000, VideoMemory=0, Price=83221, NumberOnStock=56 },
                new Computer() {Code="1D2S9EA", Model="HP EliteDesk 800 G6 SFF",ProcessorType="Intel Core i7",ProcessorFrequency=2900, AmauntOfRAM=16, HardDiskCapacity=512, VideoMemory=0, Price=158700, NumberOnStock=6 },
                new Computer() {Code="DT.VS2ER.0AD", Model="Acer Veriton ES2730G MT",ProcessorType="Intel Core i5",ProcessorFrequency=2900, AmauntOfRAM=8, HardDiskCapacity=256, VideoMemory=0, Price=60295, NumberOnStock=507 }
            };
            List<Computer> selectedComputersByProc = new List<Computer>();
            List<Computer> selectedComputersByOZU = new List<Computer>();
            List<Computer> selectedComputersByPrice = new List<Computer>();
            List<Computer> selectedComputersByNumberOnStock = new List<Computer>();
            List<string> processorTypes = new List<string>();
            bool strEq = false;
            string custStr;
            int ozuNeeded=0;
           
           //находим число различных процессоров
            foreach (Computer c in listOfComputers)
            {
                foreach (string str in processorTypes)
                {
                    if (System.String.CompareOrdinal(c.ProcessorType, str) == 0)
                    {
                        strEq = true;
                        break;
                    }
                    else strEq = false;
                }
                if (!strEq)
                    processorTypes.Add(c.ProcessorType);
            }
            Console.Write("Доступные модели процесоров: ");
            if (processorTypes.Count > 1)
            {
                for (int i = 0; i < processorTypes.Count - 1; i++)
                {
                    Console.Write(processorTypes.ElementAt(i));
                    Console.Write(", ");
                }
            }
            Console.Write(processorTypes.ElementAt(processorTypes.Count - 1));
            Console.WriteLine(".");
            do
            {
                Console.WriteLine("Введите модель процессора искомого компьютера: ");
                custStr = Console.ReadLine();
                foreach (string str in processorTypes)
                {
                    if (System.String.CompareOrdinal(custStr, str) == 0)
                    {
                        strEq = true;
                        break;
                    }
                    else strEq = false;
                }
                if (!strEq)
                {
                    Console.WriteLine("Вы ввели тип отсутствующий в списке процессора.");
                }
            } while (!strEq);

            foreach (Computer c in listOfComputers)
            {
                if (System.String.CompareOrdinal(c.ProcessorType, custStr) == 0)
                    selectedComputersByProc.Add(c);
            }
            foreach (Computer comp in selectedComputersByProc)
            {
                Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}",comp.Code, comp.Model, comp.ProcessorType, comp.ProcessorFrequency, comp.AmauntOfRAM, comp.HardDiskCapacity, comp.VideoMemory, comp.Price, comp.NumberOnStock);
            }

            do
            {
                Console.WriteLine("Введите минимальное количество ОЗУ: ");
                strEq = true;
                try
                {
                    ozuNeeded = Convert.ToInt32(Console.ReadLine());
                    if (ozuNeeded < 0)
                    {
                        Console.WriteLine("Значение ОЗУ должно быть больше 0.");
                        strEq = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    strEq = false;
                }
            } while (!strEq);
           
            /*foreach (Computer c in listOfComputers)
            {
                if (c.AmauntOfRAM >= ozuNeeded)
                    selectedComputersByOZU.Add(c);
            }*/
            /*selectedComputersByOZU = (from comp in listOfComputers
                                     where comp.AmauntOfRAM >= ozuNeeded
                                     select comp).ToList();*/
            selectedComputersByOZU = (listOfComputers
                                     .Where(comp => comp.AmauntOfRAM >= ozuNeeded))
                                     .ToList();
            foreach (Computer comp in selectedComputersByOZU)
            {
                Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}", comp.Code, comp.Model, comp.ProcessorType, comp.ProcessorFrequency, comp.AmauntOfRAM, comp.HardDiskCapacity, comp.VideoMemory, comp.Price, comp.NumberOnStock);
            }
            Console.WriteLine("");

            Console.WriteLine("Список компьютеров отсортированный по цене: ");
            selectedComputersByPrice = listOfComputers;
            selectedComputersByPrice.Sort(Computer.CompareCompByPrise);
            foreach (Computer comp in selectedComputersByPrice)
            {
                Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}", comp.Code, comp.Model, comp.ProcessorType, comp.ProcessorFrequency, comp.AmauntOfRAM, comp.HardDiskCapacity, comp.VideoMemory, comp.Price, comp.NumberOnStock);
            }
            Console.WriteLine("");

            Console.WriteLine("Список компьютеров отсортированный по типу процессора: ");
            selectedComputersByProc = listOfComputers;
            selectedComputersByProc.Sort(Computer.CompareCompByProcessorType);
            foreach (Computer comp in selectedComputersByProc)
            {
                Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}", comp.Code, comp.Model, comp.ProcessorType, comp.ProcessorFrequency, comp.AmauntOfRAM, comp.HardDiskCapacity, comp.VideoMemory, comp.Price, comp.NumberOnStock);
            }
            Console.WriteLine("");

            Console.WriteLine("Самый дорогой процессор");
            Computer comp1 = selectedComputersByPrice[selectedComputersByPrice.Count() - 1];
            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}", comp1.Code, comp1.Model, comp1.ProcessorType, comp1.ProcessorFrequency, comp1.AmauntOfRAM, comp1.HardDiskCapacity, comp1.VideoMemory, comp1.Price, comp1.NumberOnStock);
            Console.WriteLine("");

            Console.WriteLine("Самый дешевый процессор");
            comp1 = selectedComputersByPrice[0];
            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}", comp1.Code, comp1.Model, comp1.ProcessorType, comp1.ProcessorFrequency, comp1.AmauntOfRAM, comp1.HardDiskCapacity, comp1.VideoMemory, comp1.Price, comp1.NumberOnStock);
            Console.WriteLine("");

            selectedComputersByNumberOnStock = (listOfComputers
                          .Where(comp => comp.NumberOnStock >= 30))
                          .ToList();
            if (selectedComputersByNumberOnStock.Count == 1)
            {
                Console.WriteLine("Компьютер в количестве не менее 30 штук есть:");
                comp1 = selectedComputersByNumberOnStock[0];
                Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}", comp1.Code, comp1.Model, comp1.ProcessorType, comp1.ProcessorFrequency, comp1.AmauntOfRAM, comp1.HardDiskCapacity, comp1.VideoMemory, comp1.Price, comp1.NumberOnStock);
            }
            else if (selectedComputersByNumberOnStock.Count > 1)
            {
                Console.WriteLine("Компьютеры в количестве не менее 30 штук есть:");
                foreach (Computer comp in selectedComputersByNumberOnStock)
                {
                    Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}", comp.Code, comp.Model, comp.ProcessorType, comp.ProcessorFrequency, comp.AmauntOfRAM, comp.HardDiskCapacity, comp.VideoMemory, comp.Price, comp.NumberOnStock);
                }
            }
            else
            {
                Console.WriteLine("Компьютеров в количестве не менее 30 штук нет.");    
            }
           
                Console.WriteLine("");

            Console.ReadKey();
        }


    }

    class Computer
    {
        public string Code { get; set; }
        public string Model { get; set; }
        public string ProcessorType { get; set; }
        public int ProcessorFrequency { get; set; }
        public int AmauntOfRAM { get; set; }
        public int HardDiskCapacity { get; set; }
        public int VideoMemory { get; set; }
        public decimal Price { get; set; }
        public int NumberOnStock { get; set; }

        internal static int CompareCompByPrise(Computer x, Computer y)
        {
            decimal answer;
            answer = x.Price - y.Price;
            return (int)answer;
        }

        internal static int CompareCompByProcessorType(Computer x, Computer y)
        {
            return System.String.CompareOrdinal(x.ProcessorType, y.ProcessorType); ;
        }
    }
}
