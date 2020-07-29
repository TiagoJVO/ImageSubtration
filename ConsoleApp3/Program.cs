using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            var wid = 1920;
            var hgt = 1024;
            Bitmap prevMap = null;
            while (true)
            {
                var captureBmp = new Bitmap(wid, hgt, PixelFormat.Format32bppArgb);
                using (var captureGraphic = Graphics.FromImage(captureBmp))
                {
                    captureGraphic.CopyFromScreen(0, 0, 0, 0, captureBmp.Size);
                    Thread.Sleep(100);
                    if (prevMap == null)
                    {
                        prevMap = captureBmp;
                        continue;
                    }
                }

                // Get the differences.
                var maxDiff = 100;
                var differs = new List<(int w, int h)>();
                for (int x = 0; x < wid; x++)
                {
                    for (int y = 0; y < hgt; y++)
                    {
                        // Calculate the pixels' difference.
                        Color color1 = prevMap.GetPixel(x, y);
                        Color color2 = captureBmp.GetPixel(x, y);
                        var d = (int)(
                            Math.Abs(color1.R - color2.R) +
                            Math.Abs(color1.G - color2.G) +
                            Math.Abs(color1.B - color2.B));
                        if (d < maxDiff)
                            differs.Add((x, y));
                    }
                }

                // Create the difference image.
                Bitmap bm3 = new Bitmap(wid, hgt);
                foreach (var dif in differs)
                {
                    bm3.SetPixel(dif.w, dif.h, Color.Yellow);
                }

                prevMap.Save("prevMap.jpg", ImageFormat.Jpeg);
                captureBmp.Save("capture.jpg", ImageFormat.Jpeg);
                bm3.Save("bm3.jpg", ImageFormat.Jpeg);
                prevMap = captureBmp;
                Console.ReadLine();
            }
        }
    }
}
