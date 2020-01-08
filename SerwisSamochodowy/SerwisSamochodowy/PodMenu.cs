﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy
{
    static class PodMenu
    {
        static string[] pozycjaMenu = {"Ręczna aktualizacja danych osobowych",
                                        "Aktualizacja danych osobowych za pomoca importu z pliu txt"};
        static int aktywnaPozycjaMenu = 0;
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
        static void PokazMenu()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine();
            for (int i = 0; i < pozycjaMenu.Length; i++)
            {
                if (i == aktywnaPozycjaMenu)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("{0,35}", pozycjaMenu[i]);
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                else
                {
                    Console.WriteLine(pozycjaMenu[i]);
                }
            }
        }
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
                    string sciezka = Console.ReadLine();
                    a.ImportAktualizacjaDanych(sciezka); break;
            }
        }
    }
}
