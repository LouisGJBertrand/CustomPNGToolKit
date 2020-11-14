using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPNGToolKit.Subroutines
{
    public class LoadFolderSubRoutine : ISubRoutine
    {
        public string GetInformations => "loads a folder inside's bitmap files into a list";

        public string GetVersion => "0.0.1";

        public int GetVersionID => 0x000001;

        public String GetInfoComplete
        {
            get
            {
                return GetDisplayedName + ": \n" +
                    "\t" + this.GetInformations + "\n" +
                    "\tVersion: " + GetVersion + " :: " + GetVersionID;
            }
        }

        public string GetDisplayedName => "LoadFolder SubRoutine";

        /// <summary>
        /// Loads a folder inside's bitmap files into the providen file list
        /// </summary>
        /// <param name="Input">input variable</param>
        /// <param name="param">Eventual Parametters</param>
        /// <returns></returns>
        public List<Bitmap> Execute(List<Bitmap> Input, List<string> param)
        {

            DirectoryInfo d = new DirectoryInfo(param[0]);

            int loaded = 0;
            foreach (var file in d.GetFiles("*.*"))
            {
                try
                {

                    Input.Add((Bitmap)Bitmap.FromFile(file.FullName));
                    loaded++;

                } catch
                {
                    // do nothing just pass
                }
                continue;
            }

            if(loaded <=0 )
            {
                Console.WriteLine("no bitmap has been found in directory "+d.FullName);
            }

            return Input;
        }
    }
}
