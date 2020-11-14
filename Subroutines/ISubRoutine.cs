using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CustomPNGToolKit.Subroutines
{
    public interface ISubRoutine
    {

        public List<Bitmap> Execute(List<Bitmap> Input, List<String> param);

        public String GetDisplayedName { get; }
        public String GetInformations { get; }
        public String GetVersion { get; }
        public int GetVersionID { get; }
        public String GetInfoComplete { get; }
    }
}
