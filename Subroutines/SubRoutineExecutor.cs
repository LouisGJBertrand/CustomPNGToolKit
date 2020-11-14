using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CustomPNGToolKit.Subroutines
{
    public class SubRoutineExecutor : ISubRoutine
    {

        public ISubRoutine subRoutine;
        public List<String> Param;

        public string GetDisplayedName => "SubRoutineExecutor Subroutine";

        public string GetInformations => "Executes a defined subroutine with given parametters";

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

        public List<Bitmap> Execute(List<Bitmap> Input)
        {
            return subRoutine.Execute(Input, Param);
        }

        public List<Bitmap> Execute(List<Bitmap> Input, List<string> param)
        {
            return subRoutine.Execute(Input, param);
        }
    }
}
