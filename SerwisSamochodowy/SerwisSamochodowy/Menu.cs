using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy
{
    static class Menu
    {
        //tablica z nazwami które beda uzyte jako nazwy menu
        static string[] pozycjaMenu = {"Wyświetlenie danych",
                                        "Historia napraw",
                                        "Status bieżacych napraw",
                                        "Faktury/Paragony",
                                        "Zaktualizuj swoje dane",
                                        "Zmien hasło",
                                        "Wyloguj"};

        static int aktywnaPozycjaMenu = 0;

        //metoda rozpoczynaja wyswietlanie menu, wywoływana jest ona w funkcji main, wywoluje ona inne metody do wyswietlania menu
        public static void StartMenu(Logged a)
        {
            Console.Title = "Serwis Samochodowy";
            Console.CursorVisible = false;
            while (true)
            {
                PokazMenu();
                WybieranieOpcji();
                UruchomOpcje(a);
            }
        }

        //metoda wypisujaca na ekranie menu
        static void PokazMenu()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Po menu poruszaj sie strzałkami, jeżeli chcesz wybrac " +
                                "zaznaczoną opcje kliknij Enter, \n" +
                                "jeżeli chcesz opuscic wybrana kategorie klknij Esc");
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

        //metoda urchamiajaca metody które zostały wybrane w menu
        static void UruchomOpcje(Logged a)
        {
            switch (aktywnaPozycjaMenu)
            {
                case 0:
                    Console.Clear(); a.WypiszDane(); break;
                case 1:
                    Console.Clear(); a.HistoriaNapraw(); break;
                case 2:
                    Console.Clear(); a.StatusNapraw(); break;
                case 3:
                    Console.Clear(); PodMenu2.StartMenu(a); break;
                case 4:
                    Console.Clear(); PodMenu.StartMenu(a); break;
                case 5:
                    Console.Clear(); a.ZmienHaslo(); break;
                case 6:
                    System.Windows.Forms.Application.Restart();
                    Environment.Exit(0); 
                    break;
            }
        }
    }
}
