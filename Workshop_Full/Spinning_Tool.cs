using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Workshop
{
    class Spinning_Tool
    {
        public double Dia { get; set; }
        public double Vc { get; set; }
        public double Freq { get; set; }
        public XmlWriterSettings settings;

        public Spinning_Tool()
        {
        }

        public Spinning_Tool(double d, double v, double f)
        {
            Dia = d;
            Vc = v;
            Freq = f;
        }
    }
}
