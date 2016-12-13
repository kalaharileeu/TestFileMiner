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
    public class FileMinerTests
    {
        [TestMethod()]
        public void FindAllFilesInFolderTest()
        {
            //Arrange
            FileMiner Fm = new FileMiner();
            //string filename = "N:\\automation\\HW_test_automation_data";
            string filename = "C:\\testvsc";
            //Act
            if (Fm.FindAllFilesInFolder(filename))
            {
                Debug.WriteLine(Fm.DserialNo_Path.Count());
                foreach (var v in Fm.DserialNo_Path)
                    Debug.Write(v.Key + " ");
                Debug.WriteLine(" ");
                return;
            }
            else
                Assert.Fail();
            //Assert
            Assert.Fail();
        }
    }
}