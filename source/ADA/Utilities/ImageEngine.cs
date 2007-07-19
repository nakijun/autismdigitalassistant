using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace Utilities
{
    public class ImageEngine
    {
        public static Image Resize(Image image, Size newSize)
        {
            //Create a new Bitmap object
            Image canvas = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format16bppRgb555);

            //create an object that will do the drawing operations
            Graphics artist = Graphics.FromImage(canvas);

            // Create rectangle for displaying image.
            Rectangle destRect = new Rectangle(0, 0, newSize.Width, newSize.Height);
            artist.FillRectangle(Brushes.White, destRect);

            // Create rectangle for source image.
            Rectangle srcRect = new Rectangle(0, 0, image.Width, image.Height);

            // Draw image to Graphics.
            artist.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel);

            //now the drawing is done, we can discard the artist object
            artist.Dispose();

            //return the picture
            return canvas;
        }

        public static byte[] ToArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Png);

                return ms.ToArray();
            }
        }

        public static Image FromArray(byte[] image)
        {
            using (MemoryStream ms = new MemoryStream(image))
            {
                return new Bitmap(ms);
            }
        }
    }
}
