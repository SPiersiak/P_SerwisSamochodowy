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
                    Console.Clear(); HistoriaNapraw(a.id); break;
                case 2:
                    Console.Clear(); opcjaWBudowie(); break;
                case 3:
                    Console.Clear(); opcjaWBudowie(); break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("Wpisz 1 jezeli chcesz ręcznie zaktualizowac dane " +
                        "\n2 jeżeli chces zimportowac dane z pliku txt.");
                    int wybor = Convert.ToInt32(Console.ReadLine());
                    if (wybor == 1)
                        a.RecznaAktualizacjaDanych(a.id);
                    else
                    {
                        Console.WriteLine("Dane maja byc wpisane linie po lini w takiej samej kolejnoscie jak ponizej:" +
                            "\nNip" +
                            "\nTelefon" +
                            "\nAdres" +
                            "\nKod Pocztowy" +
                            "\n Wprowadzenie danych inaczej bedzie błędem!.");
                        Console.WriteLine("Podaj scieżke do pliku:");
                        string sciezka = Console.ReadLine();
                        a.ImportAktualizacjaDanych(sciezka);
                    }
                    break;
                case 5:
                    Environment.Exit(0); break;
            }
        }
        static void opcjaWBudowie()
        {
            Console.SetCursorPosition(12, 4);
            Console.Write("Opcja w budowie");
            Console.ReadKey();
        }

        static void HistoriaNapraw(int id)
        {
            //przeszukuje baze i zwraca naprawy
            try
            {
                SerwisDBEntities3 db = new SerwisDBEntities3();
                var lista = db.Naprawy.Where(a => a.Samochody.KlientID == id);
                foreach(var x in lista)
                {
                    Console.WriteLine($"Id Naprawy: {x.NaprawaID}, Id Samochodu: {x.SamochodID},Id Pracownika: {x.PracownikID}\n," +
                        $" Robocizna: {x.Robocizna}, Gwarancja Do: {x.GwarancjaDo},\n Opis: {x.OpisUwagi}");
                }
                
            }
            catch (SystemException e)
            {
                Console.WriteLine("Dla tego użytkownika nie było wykonanych żadnych napraw");
                
            }

            //Żeby się odrazu nie wylączało
            Console.ReadKey();
        }
    }
}
