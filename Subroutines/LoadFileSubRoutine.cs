using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPNGToolKit.Subroutines
{
    public class LoadFileSubRoutine : ISubRoutine
    {
        public string GetDisplayedName => "LoadFile SubRoutine";

        public string GetInformations => "Loads a single file into the providen file list";

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

        /// <summary>
        /// Loads a single file into the providen file list
        /// </summary>
        /// <param name="Input">input variable</param>
        /// <param name="param">Eventual Parametters</param>
        /// <returns></returns>
        public List<Bitmap> Execute(List<Bitmap> Input, List<string> param)
        {
            try
            {
                Input.Add((Bitmap)Bitmap.FromFile(param[0]));
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(OutOfMemoryException))
                {
                    Console.WriteLine("Could not load file into memory, WrongFormat");
                    if(Constants.AbortOnFail)
                        Program.Exit(-1);
                    return Input;
                }
                if (e.GetType() == typeof(System.IO.FileNotFoundException))
                {
                    Console.WriteLine("Could not found the file "+param[0]);
                    if (Constants.AbortOnFail)
                        Program.Exit(-1);
                    return Input;
                }
                if (e.GetType() == typeof(ArgumentException))
                {
                    Console.WriteLine("The file name has been providen in the wrong format.");
                    if (Constants.AbortOnFail)
                        Program.Exit(-1);
                    return Input;
                }
            }
            Console.WriteLine("Added file :: " + param[0] + " to the input list");

            return Input;
        }
    }
}
