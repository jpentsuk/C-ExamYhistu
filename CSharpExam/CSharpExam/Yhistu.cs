using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpExam
{
    public class Yhistu
    {
        public string _yhistunimi { get; set; }
        public string _yhistuaadress { get; set; }
        public long _registrikood { get; set; }
        public DateTime _yhistuloomiseaeg { get; set; }

        public List<Korter> _korterid { get; set; }

        public override string ToString()
        {
            return $"{_yhistunimi + "   | "} { _yhistuaadress + "   | "} {_registrikood + "   | "} {_yhistuloomiseaeg}";
        }

        //meetod korteri lisamiseks ühistusse

        public void LisaKorter(Korter korter)

        {

            _korterid.Add(korter);
        }

        public Yhistu()
        {
            //serialiseerimiseks
        }
    }
}
