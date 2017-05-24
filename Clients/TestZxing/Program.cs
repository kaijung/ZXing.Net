﻿using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using ZXing;
using ZXing.Client.Result;
using System.Text;

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
                byte[] argb = new byte[W * H * 3];
                int i = 0;
				for (int y = 0; y < H; y++)
				{
                    for (int x = 0; x < W; x++)
                    {
						//argb[i] = image.GetPixel(x, y).A;
						//i++;
						argb[i] = image.GetPixel(x, y).R;
						i++;
						argb[i] = image.GetPixel(x, y).G;
						i++;
						argb[i] = image.GetPixel(x, y).B;
                        i++;
                    }
                }
				//Console.WriteLine(BitConverter.ToString(argb));
                //var result = barcodeReader.Decode(c, W, H);
                // decode the current frame
                //LuminanceSource source;

                //source = new BitmapLuminanceSource(image);


                var reader = new BarcodeReader { AutoRotate = true };

                RGBLuminanceSource rgbSource = new RGBLuminanceSource(argb, W, H, RGBLuminanceSource.BitmapFormat.RGB24);

				//var result = reader.Decode(luminanceSource);
				//var result = reader.Decode(argb, W, H, RGBLuminanceSource.BitmapFormat.ARGB32);
				//Console.WriteLine("RGB:"+BitConverter.ToString(rgbSource.Matrix));

				//Console.WriteLine("Bitmap:"+BitConverter.ToString(source.Matrix));

                var result = reader.Decode(rgbSource);
                //result = reader.Decode(source);
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
