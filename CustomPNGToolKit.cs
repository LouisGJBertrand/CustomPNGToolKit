
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
    public static class CustomPNGToolKit
    {

        public static void DisplayHelpToScreen()
        {
            Console.WriteLine("Help Page");
            Console.WriteLine("");
            Console.WriteLine("Usage List:");
            Console.WriteLine("Resize <type> <input Path> <output Path> <resize ratio> [<thread count = 1> <display watermark = false>]\n" +
                "   <type>: either file or folder\n" +
                "   <input path>: path to file or folder\n" +
                "   <output path>: path to output file or folder\n" +
                "   <resize ration>: a floating numbre to resize the file to, ex: 0.5 to divide by two the file resolution\n" +
                "   [<thread count>]: how many threads Custom Png Toolkit will use (def = 1)\n" + 
                "   [<display watermark>]: used to display custom watermark defined in the Custom PNG ToolKit config file (def = false)\n");
        }

        public static void DisplayInvalidRequest(string message)
        {
            Console.WriteLine("Invalid Request");
            Console.WriteLine(message);
            Console.WriteLine("");
            Console.WriteLine("");
            DisplayHelpToScreen();
        }

        public static async void ParseResize(List<string> args)
        {

            if(args.Count < 5)
            {
                DisplayInvalidRequest("Missing Arguments, " + args.Count + " given but 5 needed and 2 optional");
                Program.Exit(1);
            }

            if(args.Count == 5)
            {
                args.Add((1).ToString());
            }

            if (args.Count == 6)
            {
                args.Add("false");
            }

            string[] acceptedType = new string[] { "file", "folder" };

            if (!acceptedType.Contains(args[1]))
            {
                DisplayInvalidRequest("argument 1 is not well defined, the type must be either 'file' or 'folder'");
                Program.Exit(1);
            }


            if (args[1] == "folder")
            {
                int directoryCount = 0;
                string[] fileNames = null;
                try {

                    fileNames = Directory.GetFiles(args[2]);
                    directoryCount = fileNames.Length;
                
                }
                catch (Exception e)
                {
                    DisplayInvalidRequest(e.Message);
                    Program.Exit(1);
                }

                bool Working = true;
                int FileToTreatLeft = directoryCount;

                int ActualFileTreatingIndex = 0;

                CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                ci.NumberFormat.CurrencyDecimalSeparator = ".";
                float resizeFactor = float.Parse(args[4], NumberStyles.Any, ci);

                while (Working)
                {

                    Thread[] treatmentTasks = new Thread[int.Parse(args[5])];

                    for(int t = 0; t < int.Parse(args[5]); t++)
                    {

                        Bitmap actualImage = null;
                        if (fileNames.Length <= ActualFileTreatingIndex)
                        {
                            break;
                        }

                        while (actualImage == null)
                        {
                            if (fileNames.Length <= ActualFileTreatingIndex)
                            {
                                actualImage = null;
                                break;
                            }
                            try
                            {
                                actualImage = (Bitmap)Bitmap.FromFile(fileNames[ActualFileTreatingIndex]);
                            }
                            catch (Exception e)
                            {/*
                                if(!(e.GetType() == typeof(OutOfMemoryException)))
                                {
                                    DisplayInvalidRequest(e.Message);
                                    Environment.Exit(2);
                                }*/
                                actualImage = null;
                                ActualFileTreatingIndex++;
                                FileToTreatLeft--;
                            }
                        }
                        if(actualImage != null)
                        {
                            ThreadStart rf = delegate () {
                                ResizeFile(actualImage, args[3] + Path.GetFileName(fileNames[ActualFileTreatingIndex]), resizeFactor);
                            };
                            treatmentTasks[t] = new Thread(rf); // 
                            treatmentTasks[t].Name = "Thread" + (t+1);
                            treatmentTasks[t].Start();
                            Thread.Sleep(10); // Sleep to avoid the ActualFileTreatingIndex going out of sync
                            ActualFileTreatingIndex++;
                        }

                    }

                    foreach(Thread thread in treatmentTasks)
                    {
                        if(thread == null)
                        {
                            continue;
                        }
                        thread.Join();
                        FileToTreatLeft--;
                    }

                    Thread.Sleep(200); // Sleep to avoid GDI to overheat


                    if (FileToTreatLeft == 0)
                    {
                        Working = false;
                        Console.WriteLine("Work Finished");
                        Console.WriteLine("Resized a total of " + ActualFileTreatingIndex + " files");
                        Console.WriteLine(" ");
                        Console.WriteLine("Thanks for using "+Constants.SoftwareDisplayedName+" "+Constants.SoftwareCopyright);
                    }
                    
                }


            } else
            {
                /*try
                {*/
                CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                ci.NumberFormat.CurrencyDecimalSeparator = ".";
                float resizeFactor = float.Parse(args[4],NumberStyles.Any, ci);
                ResizeFile((Bitmap)Bitmap.FromFile(args[2]), args[3], resizeFactor);
                
                /*}
                catch (Exception e)
                {
                    DisplayInvalidRequest(e.Message);


                    Program.Exit(1);
                }*/
            }


        }

        public static void ResizeFile(Bitmap inputFile, string outputFilePath, float resizeFactor)
        {

            var destRect = new Rectangle(0, 0, (int)(inputFile.Width * resizeFactor), (int)(inputFile.Height * resizeFactor));
            Bitmap destImage = new Bitmap((int)(inputFile.Width * resizeFactor), (int)(inputFile.Height * resizeFactor));
            destImage.SetResolution(inputFile.HorizontalResolution, inputFile.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(inputFile, destRect, 0, 0, inputFile.Width, inputFile.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            destImage.Save(outputFilePath);
            Console.WriteLine(Thread.CurrentThread.Name+": Resized an image by {0} factor and saved it at {1}", resizeFactor, outputFilePath);
        }
    }
}
