using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using ZXing;
using ZXing.Client.Result;


namespace TestZxing
{
    class MainClass
    {




        public static void Main(string[] args)
        {

            int W, H;
            if (args.Length == 0)
                return;
            Bitmap image = null;
            try
            {

                image = (Bitmap)Bitmap.FromFile(args[0]);
            }
            catch (Exception)
            {
                System.Console.WriteLine("Resource not found: " + args[0]);
            }

            if (image == null)
                return;

            using (image)
            {
                W = image.Width;
                H = image.Height;
                byte[] rgb = new byte[W * H * 3];
                int i = 0;
                for (int x = 0; x < W; x++)
                {
                    for (int y = 0; y < H; y++)
                    {
                        rgb[i] = image.GetPixel(x, y).R;
                        i++;
                        rgb[i] = image.GetPixel(x, y).G;
                        i++;
                        rgb[i] = image.GetPixel(x, y).B;
                        i++;
                    }
                }

                //var result = barcodeReader.Decode(c, W, H);
                // decode the current frame

                var reader = new BarcodeReader { AutoRotate = true };

                var result = reader.Decode(rgb, W, H, RGBLuminanceSource.BitmapFormat.RGB24);
                if (result != null)
                {

                    Console.Out.WriteLine(args[0] + ": Success" + result.Text);
                }
                else
                {
                    var resultString = args[0] + ": No barcode found";
                    Console.Out.WriteLine(resultString);
                }
                return;
            }
        }
    }
}
