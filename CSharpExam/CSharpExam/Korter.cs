using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpExam
{
    public class Korter
    {
        public string _vastutavisik { get; set; }
        public string _korteritunnus { get; set; }
        public int _tubadearv { get; set; }
        public int _ruutmeetrid { get; set; }
        public DateTime _korteriloomiseaeg { get; set; }

        public Korter(string vastutavisik, string korteritunnus, int tubadearv, int ruutmeetrid, DateTime korteriloomiseaeg)
        {
            _vastutavisik = vastutavisik;
            _korteritunnus = korteritunnus;
            _tubadearv = tubadearv;
            _ruutmeetrid = ruutmeetrid;
            _korteriloomiseaeg = korteriloomiseaeg;
        }

        public override string ToString()
        {
            return $"{_vastutavisik + "   | "} {_korteritunnus + "   | "} {_tubadearv + "   | "} {_ruutmeetrid + "   | "} {_korteriloomiseaeg}";
        }

        public Korter()

        {
            //serialiseerimiseks
        }


    }
}
