
using CustomPNGToolKit.Subroutines;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CustomPNGToolKit
{
    public static class CustomPNGToolKitV1
    {

        public static void DisplayHelpToScreen()
        {
            Console.WriteLine("Help Page");
            Console.WriteLine("");
            Console.WriteLine("Usage List:");
            Console.WriteLine("");
        }

        public static void DisplayInvalidRequest(string message)
        {
            Console.WriteLine("Invalid Request");
            Console.WriteLine(message);
            Console.WriteLine("\n");
            DisplayHelpToScreen();
        }

        public static void Parse(List<string> args)
        {

            List<SubRoutineExecutor> subRoutineExecutors = new List<SubRoutineExecutor>();
            List<Bitmap> Input = new List<Bitmap>();

            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            Boolean Acknoledgement = false;

            for (int parsingIndex = 0; parsingIndex < args.Count; parsingIndex++)
            {

                if (args[parsingIndex] == "-a" || textInfo.ToLower(args[parsingIndex]) == "--acknoledge")
                {

                    if (args.Count > parsingIndex+1)
                    {

                        if (args[parsingIndex + 1] == "true" || args[parsingIndex + 1] == "false")
                        {
                            Acknoledgement = Boolean.Parse(args[parsingIndex + 1]);
                            parsingIndex++;
                            continue;
                        }
                        //Console.WriteLine("missing argument");
                    }
                    Acknoledgement = true;
                    continue;
                }

                if (args[parsingIndex] == "-i" || textInfo.ToLower(args[parsingIndex]) == "--input")
                {


                    if (args.Count <= parsingIndex + 1)
                    {

                        Console.WriteLine("Missing argument after -i. please provide a file/folder path");
                        return;

                    }
                    string filePath = args[parsingIndex + 1];

                    FileAttributes attr = File.GetAttributes(filePath);

                    //detect whether its a directory or file
                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        // directory it is
                        var _ = new SubRoutineExecutor();
                        _.subRoutine = new LoadFolderSubRoutine();
                        _.Param = new List<string>();

                        _.Param.Add(filePath);

                    }
                    else
                    {
                        // else it's a file
                        // directory it is
                        var _ = new SubRoutineExecutor();
                        _.subRoutine = new LoadFileSubRoutine();
                        _.Param = new List<string>();

                        _.Param.Add(filePath);
                    }
                    parsingIndex++;
                    continue;
                }

                if(args[parsingIndex] == "-r" || textInfo.ToLower(args[parsingIndex]) == "--resize")
                {

                    var _ = new SubRoutineExecutor();
                    _.subRoutine = new ResizeSubRoutine();
                    _.Param = new List<string>();

                    try { 
                        int.Parse(args[parsingIndex + 1]);
                    }
                    catch (Exception e)
                    {
                        if (e.GetType() == typeof(ArgumentNullException) || e.GetType() == typeof(IndexOutOfRangeException))
                        {
                            Console.WriteLine("Missing Arguments at position " + (parsingIndex + 1) + ".");
                        }
                        if (e.GetType() == typeof(FormatException))
                        {
                            Console.WriteLine("Invalid Argument format at position " + (parsingIndex + 1) + ". integer awaited.");
                        }
                        if (e.GetType() == typeof(OverflowException))
                        {
                            Console.WriteLine("Invalid Argument at position " + (parsingIndex + 1) + ". the input contains a number that procudes an overflow.");
                        }

                        throw (e);
                    }

                    _.Param.Add(args[parsingIndex] + 1);
                    _.Param.Add(args[parsingIndex] + 2);

                    subRoutineExecutors.Add(_);

                    parsingIndex+=2;
                    continue;
                }

            }

            if(!Acknoledgement)
            {
                if (!UserAcknoledgment())
                {
                    Console.WriteLine("Operations Aborted!");
                    return;
                }
            }

            foreach(SubRoutineExecutor subRoutineExecutor in subRoutineExecutors)
            {
                Input = subRoutineExecutor.Execute(Input);
            }

        }

        public static bool UserAcknoledgment()
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            string rl = "";

            while(rl != "y" && rl != "n" && rl != "yes" && rl != "no")
            {
                Console.Write("Do you wish to continue performing operations? (Y/N) : ");
                rl = textInfo.ToLower(Console.ReadLine());
            }

            if (rl == "y" || rl == "yes")
            {
                return true;
            } return false;
        }

        public static bool UserAcknoledgment(string question)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            string rl = "";

            while (rl != "y" && rl != "n" && rl != "yes" && rl != "no")
            {
                Console.Write(question);
                rl = textInfo.ToLower(Console.ReadLine());
            }

            if (rl == "y" || rl == "yes")
            {
                return true;
            }
            return false;
        }

        public static bool UserAcknoledgment(string question, string continue_string, string break_string)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            string rl = "";

            while (rl != continue_string && rl != break_string)
            {
                Console.Write(question);
                rl = textInfo.ToLower(Console.ReadLine());
            }

            if (rl == continue_string)
            {
                return true;
            }
            return false;
        }
    }
}
