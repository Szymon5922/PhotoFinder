using PhotoFinder.Helpers;
using PhotoFinder.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PhotoFinder.Services
{
    public class ImageGeneratorService
    {
        private string _outputPath;
        private int _totalResults;
        public readonly List<Result> Results;
        public ImageGeneratorService(List<Result> results, string outputPath)
        {
            Results = results;
            _outputPath = outputPath;
            _totalResults = results.Count();
        }

        public void GenerateImages(IProgress<double> progressBar)
        {
            int counter = 0;

            if (!Directory.Exists(_outputPath))
                Directory.CreateDirectory(_outputPath);

            foreach (var result in Results)
            {


                if (result.FoundPhotos.Any())
                    MergeImages(result.Target.ImageDirectory
                              , result.FoundPhotos.First(p => p.IsSelected)
                              , Path.Combine(_outputPath, result.Target.ImageName));

                else
                    MessageBox.Show("Result not found for " + result.Target);

                progressBar.Report(((counter++) / (double)_totalResults) * 100);

            }
        }
        private static void MergeImages(string path1, FoundPhoto foundPhoto, string outputPath)
        {
            string path2 = foundPhoto.FilePath;

            using (Image img1 = Image.FromFile(path1))
            using (Image img2 = Image.FromFile(path2))
            {
                int newWidth = img1.Width;
                int newHeight = img1.Height + img2.Height * newWidth / img2.Width;
                //int scaledHeight = img2.Height * newWidth / img2.Width;

                using (Bitmap mergedImage = new Bitmap(newWidth, newHeight))
                using (Graphics g = Graphics.FromImage(mergedImage))
                {
                    g.DrawImage(img1, 0, 0, img1.Width, img1.Height);

                    using (Bitmap img2Modified = new Bitmap(img2))
                    {
                        if (ExportParams.GeneratePhotoname)
                        {
                            string img2Name = Path.GetFileName(path2);
                            ImagesHelper.DrawStringInRightUpperCorner(img2Modified, img2Name);
                        }
                        if (ExportParams.GenerateCross)
                            ImagesHelper.DrawCross(foundPhoto, img2Modified);

                        g.DrawImage(img2Modified, 0, img1.Height, newWidth, newHeight);
                    }

                    mergedImage.Save(outputPath);
                }
            }

            Console.WriteLine("Image saved in: " + outputPath);
        }
    }
}
