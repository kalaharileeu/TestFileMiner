using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace TestFileMiner
{
    public class FileMiner
    {
        public FileMiner()
        {
            dserialNo_Path = new Dictionary<string, List<string>>();
            testdictionary = new Dictionary<string, List<TestDirectoryInfo>>();
        }

        /// <summary>
        /// returns a array of filenames in the folder
        /// </summary>
        /// <param name="filename">string, filename to be investigated</param>
        /// <returns>string[]</returns>
        public bool FindAllFilesInFolder(string filename)
        {
            string[] dirs = null;
            try
            {
                // Only get files that begin with the letter "c."
                dirs = Directory.GetDirectories(@filename, "*");
            }
            catch (Exception)
            {
                return false;
            }

            if ((dirs != null) && (dirs.Length != 0))
            {
                //Call recusiverly
                for (int i = 0; i < dirs.Length; i++)
                {
                    //recursive call here, if the text file was not found in dir, then continue
                    if(!save_paths(dirs[i]))FindAllFilesInFolder(dirs[i]);
                }
                return true;
            }
            else
                return false;
        }
        //Returns the dictionary with paths
        public Dictionary<string, List<string>> DserialNo_Path
        {
            get { return dserialNo_Path; }
        }
        //Use Helper.cs. Reads .txt files to gather information
        public void ReadTextFilesForInfo()
        {
            //Search through the Serial vs .txt_path dict
            foreach(var v in dserialNo_Path)
            {
                //vvalue is the List<paths>
                foreach(var vvalue in v.Value)
                {
                    TestDirectoryInfo tdi = new TestDirectoryInfo();
                    //Read file with Helpers and populate tdi
                    tdi.Load(new Helpers(vvalue, v.Key));
                    //If key not in dictionary
                    if (!testdictionary.ContainsKey(v.Key))
                    {
                        //Add key
                        testdictionary.Add(v.Key, new List<TestDirectoryInfo>());
                        //Add the new TestDirectoryInfo
                        testdictionary.Last().Value.Add(tdi);
                    }
                    else
                    {
                        testdictionary[v.Key].Add(tdi);
                    }
                }
            }
        }
        
        private bool save_paths(string path)
        {
            //find the serial number in the path
            string twelvedigit = Regex.Match(path, @"\d{12}").Value;
            //If there is a twelve digit in the path
            if (twelvedigit.Length == 12)
            {
                //see if there is a txt file with a twelve digit number
                FileInfo[] wantedfile = getTheTextFilesinDir(path, twelvedigit);
                //load the serial number and full file name into dictionary
                if (wantedfile != null)
                {
                    //Check is the key is already in the dictionary
                    //If yes then a the file name to the list of file names
                    if (dserialNo_Path.ContainsKey(twelvedigit))
                        dserialNo_Path[twelvedigit].Add(wantedfile[0].FullName);
                    else
                    {
                        //Creat the new entry in the dictionar
                        dserialNo_Path.Add(twelvedigit, new List<string>());
                        //add a file name to the list of the new entry
                        dserialNo_Path[twelvedigit].Add(wantedfile[0].FullName);
                    }
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Find files with wanted signature. Return the file if correct.
        /// </summary>
        /// <param name="path">full folder path</param>
        /// <param name="serialnumber">Wanted folder names have serialnumbers</param>
        /// <returns></returns>
        private FileInfo[] getTheTextFilesinDir(string path, string serialnumber)
        {
            //convert to dir info to use getfiles
            DirectoryInfo dirWithTextFile = new DirectoryInfo(path);
            //Get a file with specific text in name
            FileInfo[] filesindir = dirWithTextFile.GetFiles("*" + serialnumber + "*" + ".txt");
            if (filesindir.Length != 0)
                return filesindir;
            else
                return null;
        }

        //Dictionare with serial number vs list of .tx file paths
        Dictionary<string, List<string>> dserialNo_Path;
        //Dictionary of serial number vs List of TestInformation
        Dictionary<string, List<TestDirectoryInfo>> testdictionary;
    }
}
