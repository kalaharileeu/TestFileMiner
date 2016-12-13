using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestFileMiner
{
    /// <summary>
    /// Helper needs to get all the infomation from text file
    /// </summary>
    public class Helpers
    {
        public Helpers(string fullTextFilePath, string serialnumber)
        {
            FileInfo FI = new FileInfo(fullTextFilePath);
            string theDirPath = Path.GetDirectoryName(fullTextFilePath);
            //if the directory not there return a message
            if (Directory.Exists(theDirPath) && Path.IsPathRooted(fullTextFilePath) && !HelperStatic.IsFileLocked(FI))
            {
                ReadFirmware_version(fullTextFilePath);
                ReadParameter_version(fullTextFilePath);
                ReadTestName(fullTextFilePath);
                ReadInverterType(fullTextFilePath);
                Filename = fullTextFilePath;
                Serialnumber = serialnumber;
            }
        }

        /// <summary>
        /// Read the firmware version from file
        /// Need a full pathname of a file in the .csv file folder
        /// </summary>
        /// <returns></returns>
        private void ReadFirmware_version(string fullTextFilePath)
        {
            //Find/read the line with the firmware version
            foreach (var lin in File.ReadLines(fullTextFilePath)
                .SkipWhile(line => !line.Contains("Debugger")).TakeWhile(line => line.Contains("Debugger")))
            {
                //Get the serial number in the file path to identify the text file
                //string serialnumber = Regex.Match(fullDataFilePath, @"\d{12}").Value;
                Procload = Regex.Match(lin, @"\d{2}\.\d{2}\.\d{2}").Value;
            }
        }
        /// <summary>
        /// Read the parameter table version from file
        /// Need a full pathname of a file in the .csv file folder
        /// </summary>
        /// <returns></returns>
        private void ReadParameter_version(string fullTextFilePath)
        {
            //Find/read the line with the parameter version
            foreach (var lin in File.ReadLines(fullTextFilePath)
                .SkipWhile(line => !line.Contains("Parameter")).TakeWhile(line => line.Contains("Parameter")))
            {
                //Get the parameter number in the file path to identify the text file
                Parameterversion = Regex.Match(lin, @"\d{2}\.\d{2}\.\d{2}").Value;
            }
        }

        private void ReadTestName(string fullTextFilePath)
        {
            //Find/read the line with the firmware version
            foreach (var lin in File.ReadLines(fullTextFilePath)
                .SkipWhile(line => !line.Contains("Test Name:")).TakeWhile(line => line.Contains("Test Name:")))
            {
                //Get the serial number in the file path to identify the text file
                //string serialnumber = Regex.Match(fullDataFilePath, @"\d{12}").Value;
                if (lin.Contains("Test Name"))
                    Testname = lin.Remove(0, 11);
            }
        }

        private void ReadInverterType(string fullTextFilePath)
        {
            //Find/read the line with the firmware version
            foreach (var lin in File.ReadLines(fullTextFilePath)
                .SkipWhile(line => !line.Contains("PCU Type:")).TakeWhile(line => line.Contains("PCU Type:")))
            {
                //Get the serial number in the file path to identify the text file
                //string serialnumber = Regex.Match(fullDataFilePath, @"\d{12}").Value;
                if (lin.Contains("PCU Type:"))
                    InverterType = lin.Remove(0, 10);
            }
        }

        public string Procload { get; set; } = null;
        public string Parameterversion { get; set; } = null;
        public string Testname { get; set; } = null;
        public string InverterType { get; set; } = null;
        public string Serialnumber { get; set; } = null;
        public string Filename { get; set; } = null;
    }
}
