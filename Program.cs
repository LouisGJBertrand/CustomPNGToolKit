
//#define TESTPHASE

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

            #if TESTPHASE
                args = new string[]{
                    "Resize",
                    "folder",
                    @"E:\Data\Persos\3d Animations\Blender\Factorio\Conveyor Belt png Sequences\",
                    @"E:\Data\Persos\3d Animations\Blender\Factorio\Conveyor Belt png Sequences.Resized\",
                    "0.10",
                    "74"};
            #endif

            if (args.Length <= 0)
            {
                CustomPNGToolKit.DisplayHelpToScreen();
                Exit(1);
            }

            if (args[0] == "Resize")
            {
                CustomPNGToolKit.ParseResize(args.ToList<string>());
            } else
            {
                CustomPNGToolKit.DisplayInvalidRequest("Argument 0 is not a valid action");
                Exit(1);
            }
            Exit(0);
        }
    }
}
