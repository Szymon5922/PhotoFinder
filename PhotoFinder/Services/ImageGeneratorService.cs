using PhotoFinder.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFinder.Services
{
    public class ImageGeneratorService
    {
        public ImageGeneratorService(List<Result> results, string outputPath)
        {
            foreach (var result in results)
            {
                MergeImages(result.Target.ImageDirectory, result.FoundPhotos.First().FilePath, Path.Combine(outputPath,result.Target.ImageName));
            }
        }


        static void MergeImages(string path1, string path2, string outputPath)
        {
            using (Image img1 = Image.FromFile(path1))
            using (Image img2 = Image.FromFile(path2))
            {
                int newWidth = img1.Width;
                int newHeight = img1.Height + img2.Height * newWidth / img2.Width; 

                using (Bitmap mergedImage = new Bitmap(newWidth, newHeight))
                using (Graphics g = Graphics.FromImage(mergedImage))
                {
                    g.DrawImage(img1, 0, 0, img1.Width, img1.Height);

                    int scaledHeight = img2.Height * newWidth / img2.Width; 
                    g.DrawImage(img2, 0, img1.Height, newWidth, scaledHeight); 

                    mergedImage.Save(outputPath); 
                }
            }

            Console.WriteLine("Obraz zapisany w: " + outputPath);
        }
    }        
}
