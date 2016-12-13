using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace TestFileMiner.Tests
{
    [TestClass()]
    public class HelpersTests
    {
        [TestMethod()]
        public void HelpersTest()
        {
            Helpers H = new Helpers(@"C:\testvsc\121121121121\text_test_121121121121_456.txt", "121121121121");

            if (H.InverterType != null && H.Parameterversion != null && H.Procload != null && H.Testname != null)
                Debug.WriteLine(H.InverterType + " " + H.Parameterversion + " " + H.Procload + " " + H.Testname);
            else
                Assert.Fail();
        }
    }
}