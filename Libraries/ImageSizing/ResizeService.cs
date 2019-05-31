﻿using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace ImageSizing
{
    public class ResizeService
    {
        private static byte[] GetBytes(Stream stream)
        {
            byte[] data;
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                data = ms.ToArray();
                ms.Flush();
                ms.Close();
            }
            return data;
        }

        private static byte[] ResizeImage(byte[] data, int maxSize)
        {
            byte[] result;
            Image img;

            using (MemoryStream ms = new MemoryStream(data))
            {
                img = Image.FromStream(ms);

                int height = img.Height;
                int width = img.Width;
                int newHeight, newWidth;

                if (height > width && height > maxSize)
                {
                    newHeight = maxSize;
                    newWidth = (width * maxSize) / height;
                }
                else if (width > height && width > maxSize)
                {
                    newWidth = maxSize;
                    newHeight = (height * maxSize) / width;
                }
                else
                {
                    newHeight = height;
                    newWidth = width;
                }

                if (height != newHeight && width != newWidth)
                {
                    Bitmap newImg = new Bitmap(img, newWidth, newHeight);
                    Graphics g = Graphics.FromImage(newImg);
                    g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                    g.DrawImage(img, 0, 0, newImg.Width, newImg.Height);
                    img = newImg;
                }

                ms.Flush();
                ms.Close();
            }

            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Png);
                result = ms.ToArray();
                ms.Flush();
                ms.Close();
            }

            return result;
        }

        public static byte[] Resize(Stream stream, int maxSize)
        {
            byte[] data = GetBytes(stream);
            data = ResizeImage(data, maxSize);
            return data;
        }
    }
}
