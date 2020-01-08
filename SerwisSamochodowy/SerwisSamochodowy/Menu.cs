using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy
{
    static class Menu
    {
        static string[] pozycjaMenu = {"Wyświetlenie danych klienta",
                                        "Historia napraw",
                                        "Status bierzacych napraw",
                                        "Faktury/Paragony",
                                        "Zaktualizuj swoje dane",
                                        "Zmien hasło",
                                        "Wyloguj"};
        static int aktywnaPozycjaMenu = 0;
        public static void StartMenu(Logged a)
        {
            Console.Title = "Witaj " + a.Imie + " " + a.Nazwisko;
            Console.CursorVisible = false;
            while (true)
            {
                PokazMenu();
                WybieranieOpcji();
                UruchomOpcje(a);
            }
        }
        static void PokazMenu()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Po menu poruszaj sie strałkami, jeżeli chcesz wybrac" +
                "zaznaczoną opcje kliknij enter, jeżeli chcesz opuscic wybrana kategorie klknij Esc");
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
        static void WybieranieOpcji()
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
                    aktywnaPozycjaMenu = pozycjaMenu.Length - 1;
                    break;
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
                    Console.Clear(); a.WypiszDane(); break;
                case 1:
                    Console.Clear(); a.HistoriaNapraw(); Console.ReadKey(); break;
                case 2:
                    Console.Clear(); a.StatusNapraw(); break;
                case 3:
                    Console.Clear(); PodMenu2.StartMenu(a); break;
                case 4:
                    Console.Clear(); PodMenu.StartMenu(a); break;
                case 5:
                    Console.Clear(); a.ZmienHaslo(); break;
                case 6:
                    Environment.Exit(0); break;
            }
        }
    }
}
