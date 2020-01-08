using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy
{
    static class PodMenu
    {
        //tablica z nazwami które beda uzyte jako nazwy menu
        static string[] pozycjaMenu = {"Ręczna aktualizacja danych osobowych",
                                        "Aktualizacja danych osobowych za pomoca importu z pliu txt"};

        static int aktywnaPozycjaMenu = 0;

        //metoda rozpoczynaja wyswietlanie menu, wywoływana jest ona w metodzie uruchom opcje w klasie menu jako podmenu, wywoluje ona inne metody do wyswietlania menu
        public static void StartMenu(Logged a)
        {
            Console.CursorVisible = false;
            while (true)
            {
                PokazMenu();
                WybieranieOpcji(a);
                UruchomOpcje(a);
            }
        }

        //metoda wypisujaca na ekranie menu
        static void PokazMenu()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine();
            for (int i = 0; i < pozycjaMenu.Length; i++)
            {
                if (i == aktywnaPozycjaMenu)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("{0,35}", pozycjaMenu[i]);
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                }
                else
                {
                    Console.WriteLine(pozycjaMenu[i]);
                }
            }
        }

        //metoda służaca do nawigacji po menu
        static void WybieranieOpcji(Logged obj)
        {
            do
            {
                ConsoleKeyInfo klawisz = Console.ReadKey();
                if (klawisz.Key == ConsoleKey.UpArrow)
                {
                    aktywnaPozycjaMenu = (aktywnaPozycjaMenu > 0) ? aktywnaPozycjaMenu - 1 : pozycjaMenu.Length - 1;
                    PokazMenu();
                }
                else if (klawisz.Key == ConsoleKey.DownArrow)
                {
                    aktywnaPozycjaMenu = (aktywnaPozycjaMenu + 1) % pozycjaMenu.Length;
                    PokazMenu();
                }
                else if (klawisz.Key == ConsoleKey.Escape)
                {
                    Menu.StartMenu(obj);
                }
                else if (klawisz.Key == ConsoleKey.Enter)
                    break;
            } while (true);
        }

        //metoda urchamiajaca metody które zostały wybrane w menu
        static void UruchomOpcje(Logged a)
        {
            switch (aktywnaPozycjaMenu)
            {
                case 0:
                    Console.Clear(); a.RecznaAktualizacjaDanych(); break;
                case 1:
                    Console.Clear(); Console.WriteLine("Dane maja byc wpisane w jednej lini rozdzielone przecinkami,\n" +
                        "podajac sciezke pamietaj ze musisz wpisac podwójne \\!");
                    Console.WriteLine("Podaj scieżke do pliku:");
                    //pokazuje kursor aby ułatwić wpisywanie
                    Console.CursorVisible = true;
                    string sciezka = Console.ReadLine();
                    //ukrywa kursor
                    Console.CursorVisible = false;
                    a.ImportAktualizacjaDanych(sciezka); break;
            }
        }
    }
}
