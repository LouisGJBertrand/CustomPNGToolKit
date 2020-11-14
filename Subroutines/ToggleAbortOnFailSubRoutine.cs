using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPNGToolKit.Subroutines
{
    public class ToggleAbortOnFailSubRoutine : ISubRoutine
    {
        public string GetDisplayedName => "ToggleAbortOnFail SubRoutine";

        public string GetInformations => "Toggle the abort on fail global variable or set to a defined value";

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

        public List<Bitmap> Execute(List<Bitmap> Input, List<string> param)
        {

            if(param.Count > 0)
            {
                try
                {
                    Constants.AbortOnFail = bool.Parse(param[0]);
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(ArgumentNullException))
                    {
                        Console.WriteLine("Could not set AbortOnFail global constant. value given is null.");
                        if (Constants.AbortOnFail)
                            Program.Exit(-1);
                        return Input;
                    }
                    if (e.GetType() == typeof(FormatException))
                    {
                        Console.WriteLine("Could not set AbortOnFail global constant. value given is not boolean.");
                        if (Constants.AbortOnFail)
                            Program.Exit(-1);
                        return Input;
                    }
                }
            } else
            {
                Constants.AbortOnFail = !Constants.AbortOnFail;
            }
            return Input;

        }
    }
}
