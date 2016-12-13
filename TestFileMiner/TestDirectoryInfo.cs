using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFileMiner
{
    public class TestDirectoryInfo
    {
        //converts the parameter to wanted values
        public void Load(Helpers filehelper)
        {
            // TryParse returns true if the conversion succeeded
            int j;
            if (int.TryParse((filehelper.Parameterversion).Replace(".", ""), out j))
                Parametertable = j;
            else
                return;
            if (int.TryParse((filehelper.Procload).Replace(".", ""), out j))
                Procload = j;
            else
                return;
            Serialnumber = filehelper.Serialnumber;
            Filename = filehelper.Filename;
            InverterType = filehelper.InverterType;
            Testname = filehelper.Testname;
        }

        public string Serialnumber { get; set; } = "";
        public int Parametertable { get; set; } = 0;
        public int Procload { get; set; } = 0;
        public string Filename { get; set; } = "";
        public string Testname { get; set; } = "";
        public string InverterType { get; set; } = "";
    }
}
