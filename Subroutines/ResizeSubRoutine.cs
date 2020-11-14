using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Threading;

namespace CustomPNGToolKit.Subroutines
{
    public class ResizeSubRoutine : ISubRoutine
    {
        public string GetInformations => "Resize Sub Routine resizes input by a factor using a defined algorithm. usage: ResizeSubRoutine.Execute(input files, parametters);";

        public string GetVersion => "0.0.1";

        public int GetVersionID => 0x000001;

        public String GetInfoComplete
        {
            get
            {
                return GetDisplayedName+": \n" +
                    "\t" + this.GetInformations + "\n" +
                    "\tVersion: " + GetVersion + " :: " + GetVersionID;
            }
        }

        public string GetDisplayedName => "Resize Sub Routine";

        public List<Bitmap> Execute(List<Bitmap> Input, List<string> param)
        {
            List<Bitmap> Output = new List<Bitmap>();
            foreach(Bitmap bitmap in Input)
            {
                Output.Add(ResizeFile(bitmap, float.Parse(param[0], System.Globalization.NumberStyles.AllowDecimalPoint)));
            }
            return Output;
        }

        public static Bitmap ResizeFile(
            Bitmap inputFile,
            float resizeFactor,
            CompositingMode compositingMode = CompositingMode.SourceCopy,
            CompositingQuality compositingQuality = CompositingQuality.HighQuality,
            InterpolationMode interpolationMode = InterpolationMode.HighQualityBicubic,
            SmoothingMode smoothingMode = SmoothingMode.HighQuality,
            PixelOffsetMode pixelOffsetMode = PixelOffsetMode.HighQuality)
        {

            var destRect = new Rectangle(0, 0, (int)(inputFile.Width * resizeFactor), (int)(inputFile.Height * resizeFactor));
            Bitmap destImage = new Bitmap((int)(inputFile.Width * resizeFactor), (int)(inputFile.Height * resizeFactor));
            destImage.SetResolution(inputFile.HorizontalResolution, inputFile.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = compositingMode;
                graphics.CompositingQuality = compositingQuality;
                graphics.InterpolationMode = interpolationMode;
                graphics.SmoothingMode = smoothingMode;
                graphics.PixelOffsetMode = pixelOffsetMode;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(inputFile, destRect, 0, 0, inputFile.Width, inputFile.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            Console.WriteLine(Thread.CurrentThread.Name + ": Resized an image by {0} factor", resizeFactor);
            return destImage;

        }
    }
}
