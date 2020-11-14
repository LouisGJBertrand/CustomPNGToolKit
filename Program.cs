
#define TESTPHASE

using System;
using System.Linq;

namespace CustomPNGToolKit
{
    class Program
    {

        public static void Exit(int code)
        {
            Environment.Exit(code);
        }

        static void Main(string[] args)
        {
            Console.WriteLine(Constants.SoftwareDisplayedName);
            Console.WriteLine(Constants.SoftwareDeveloppers + " " + Constants.SoftwareCopyright);
            Console.WriteLine(" ");

            Console.WriteLine("Abort On Fail: "+Constants.AbortOnFail.ToString());

            if (args.Length <= 0)
            {
                CustomPNGToolKitV1.DisplayHelpToScreen();
                Exit(1);
            }
            CustomPNGToolKitV1.Parse(args.ToList<string>());
            Exit(0);
        }
    }
}
