using PhotoFinder.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PhotoFinder.Helpers
{
    public static class ImagesHelper
    {
        public static void DrawStringInRightUpperCorner(Bitmap source, string text)
        {
            using (Graphics g = Graphics.FromImage(source))
            {
                Font font = new Font("Arial", 150, FontStyle.Bold);
                Brush brush = new SolidBrush(Color.DarkBlue);
                SizeF textSize = g.MeasureString(text, font);

                float x = source.Width - textSize.Width - 10;
                float y = 10;

                Brush backgroundBrush = new SolidBrush(Color.White);
                g.FillRectangle(backgroundBrush, x - 5, y - 5, textSize.Width + 10, textSize.Height + 10);

                g.DrawString(text, font, brush, x, y);
            }
        }
        public static BitmapImage GetImageWithCross(FoundPhoto photo)
        {
            using (Bitmap originalBitmap = new Bitmap(photo.FilePath))
            {
                using Bitmap bitmap = new Bitmap(originalBitmap);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    int width = bitmap.Width;
                    int height = bitmap.Height;

                    int x = (int)((photo.Yaw / (photo.HFOV / 2)) * (width / 2));
                    int y = (int)((photo.Pitch / (photo.VFOV) / 2) * (height / 2));

                    int tmpx = x;
                    x = width / 2 - y;
                    y = height / 2 + tmpx;

                    int crossSize = 500;
                    Pen pen = new Pen(Color.Red, 12);
                    g.DrawLine(pen, x - crossSize, y, x + crossSize, y);
                    g.DrawLine(pen, x, y - crossSize, x, y + crossSize);
                    g.DrawImage(bitmap, 0, 0, width, height);

                    using (MemoryStream memory = new MemoryStream())
                    {
                        bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                        memory.Position = 0;
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = memory;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();
                        bitmapImage.Freeze();
                        return bitmapImage;
                    }
                }
            }
        }
        public static void DrawCross(FoundPhoto photo, Bitmap bitmapSource)
        {
            using Bitmap bitmap = new Bitmap(bitmapSource);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                int width = bitmap.Width;
                int height = bitmap.Height;

                int x = (int)((photo.Yaw / (photo.HFOV / 2)) * (width / 2));
                int y = (int)((photo.Pitch / (photo.VFOV) / 2) * (height / 2));

                int crossSize = 500;
                Pen pen = new Pen(Color.Red, 15);
                g.DrawLine(pen, x - crossSize, y, x + crossSize, y);
                g.DrawLine(pen, x, y - crossSize, x, y + crossSize);
            }
        }
    }
}
