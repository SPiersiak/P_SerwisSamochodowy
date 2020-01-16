using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy
{
    interface ILogged
    {
        void WypiszDane();
        void ZmienHaslo();
        string Password();
        void RecznaAktualizacjaDanych();
        void ImportAktualizacjaDanych(string link);
        void HistoriaNapraw();
        void WystawFakture();
        void WystawParagon();
        void StatusNapraw();
    }
}
