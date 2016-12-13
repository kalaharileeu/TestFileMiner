using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFileMiner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TestFileMiner.Tests
{
    [TestClass()]
    public class DirectoryInfoTests
    {
        [TestMethod()]
        public void convertTest()
        {
            Helpers H = new Helpers(@"C:\testvsc\121121121121\text_test_121121121121_456.txt", "121121121121");
            TestDirectoryInfo DI  = new TestDirectoryInfo();
            DI.Load(H);
            Debug.WriteLine(DI.Serialnumber + " " + DI.Parametertable + " " + DI.Procload);

            if ((DI.Serialnumber == "") || (DI.Procload == 0) || (DI.Procload == 0))
            {
                Assert.Fail();
            }
        }
    }
}