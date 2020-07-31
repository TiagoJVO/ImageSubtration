using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screenshot
{
    public partial class ImageProcessor : Form
    {
        Point initialPoint;
        Size size;
        CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
        Bitmap previousBitMap;
        public ImageProcessor()
        {
            var ratio = 0.2915f;
            var processName = "[Release]";
            var sc = new CaptureUtils();
            sc.SetDPIAware();
            var process = GetProcess(processName);
            var windowRC = sc.GetWindowRect(process.MainWindowHandle);
            size = new Size(
                (int)(windowRC.Width * ratio), 
                (int)(windowRC.Height * ratio));

            initialPoint = new Point(
                (windowRC.Width - size.Width) / 2,
                (windowRC.Height - size.Height) / 2);

            InitializeComponent();
            this.Opacity = .7D; //Make trasparent
            //C#: how to take a screenshot of a portion of screen https://stackoverflow.com/a/3306633/5260872
            
            Task processing = new Task(() =>
            {
                while(!CancellationTokenSource.Token.IsCancellationRequested)
                {
                    if (process != null)
                    {
                        var bmp = sc.GetScreenshot(process.MainWindowHandle, initialPoint, size);

                        pbCapture.Invoke((MethodInvoker) delegate {
                            if (previousBitMap != null)
                            {
                                var differ = DifferImmages(previousBitMap, bmp);
                                pbCapture.Image?.Dispose();
                                pbCapture.Image = differ;
                                previousBitMap = bmp;
                                pbCapture.Refresh();
                            } else
                            {
                                previousBitMap = bmp;
                            }
                        });
                    }
                    Task.Delay(500).Wait();
                }
            }, CancellationTokenSource.Token);
            processing.Start();
        }

        public static Process GetProcess(string windowPartialName)
        {
            return Process
                .GetProcesses()
                .Where(p => p.MainWindowTitle.StartsWith(windowPartialName, StringComparison.CurrentCultureIgnoreCase))
                .SingleOrDefault();
        }

        private Bitmap DifferImmages(Bitmap bitmap1, Bitmap bitmap2)
        {
            // Get the differences.
            var maxDiff = 50;
            var unchangedPixels = new List<(int x, int y)>();
            for (int x = 0; x < bitmap1.Width; x++)
            {
                for (int y = 0; y < bitmap1.Height; y++)
                {
                    // Calculate the pixels' difference.
                    Color color1 = bitmap1.GetPixel(x, y);
                    Color color2 = bitmap2.GetPixel(x, y);
                    var d = (int)(
                        Math.Abs(color1.R - color2.R) +
                        Math.Abs(color1.G - color2.G) +
                        Math.Abs(color1.B - color2.B));
                    if (d < maxDiff)
                        unchangedPixels.Add((x, y));
                }
            }

            // Create the difference image.
            Bitmap bm3 = new Bitmap(bitmap1.Width, bitmap1.Height);
            Graphics g = Graphics.FromImage(bm3);
            g.Clear(Color.GreenYellow);
            foreach (var (x, y) in unchangedPixels)
            {
                bm3.SetPixel(x, y, Color.BlueViolet);
            }
            return bm3;
        }

        private void btnStopProcessing_Click(object sender, EventArgs e)
        {
            CancellationTokenSource.Cancel();
            Application.Exit();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            frmHome home = new frmHome();
            this.Hide();
            home.Show();
        }
    }
}
