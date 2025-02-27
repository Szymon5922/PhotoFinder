using PhotoFinder.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFinder.Services.Reader
{
    public class AlignmentReadingStrategy : IReadingStrategy
    {
        private string _directory;
        private readonly IFormatProvider _provider = CultureInfo.InvariantCulture;
        public List<PhotoData> Read(string directory)
        {
            _directory = directory;

            List<PhotoData> data = new();

            using (var stream = File.OpenRead(directory))
            {
                using (var streamReader = new StreamReader(stream, Encoding.UTF8))
                {
                    string line;
                    var header = streamReader.ReadLine().Split(',').ToList();
                    var mapping = GetColumnsIndexes(header);

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var columns = line.Split(',');

                        data.Add(GetPhotoDataFromLine(columns, mapping));
                    }
                }
            }

            return data;
        }
        private PhotoData GetPhotoDataFromLine(string[] values, Dictionary<string, int> mapping)
        {
            float x = float.Parse(values[mapping["X"]], _provider);
            float y = float.Parse(values[mapping["Y"]], _provider);
            float z = float.Parse(values[mapping["Z"]], _provider);
            float frontX = float.Parse(values[mapping["FrontX"]], _provider);
            float frontY = float.Parse(values[mapping["FrontY"]], _provider);
            float frontZ = float.Parse(values[mapping["FrontZ"]], _provider);
            float upX = float.Parse(values[mapping["UpX"]], _provider);
            float upY = float.Parse(values[mapping["UpY"]], _provider);
            float upZ = float.Parse(values[mapping["UpZ"]], _provider);
            double vfov = double.Parse(values[mapping["VFOV"]], _provider);
            double hfov = double.Parse(values[mapping["HFOV"]], _provider);
            string filename = values[mapping["FileName"]];

            PhotoData photoData = new PhotoData()
            {
                CameraPosition = new Vector3(x, y, z),
                Front = new Vector3(frontX, frontY, frontZ),
                Up = new Vector3(upX, upY, upZ),
                VFOV = vfov,
                HFOV = hfov,
                PhotoDirectory = GetPhotoPath(filename)
            };

            return photoData;
        }
        private Dictionary<string, int> GetColumnsIndexes(List<string> header)
        {
            Dictionary<string, int> columns = new Dictionary<string, int>();
            string[] values = { "X", "Y", "Z", "FrontX", "FrontY", "FrontZ", "UpX", "UpY", "UpZ", "VFOV", "HFOV", "FileName" };

            foreach (string value in values)
                columns.Add(value, header.IndexOf(value));

            return columns;
        }
        private string GetPhotoPath(string filename)
        {
            var directoryParts = _directory.Split('\\').ToList();
            directoryParts.RemoveAt(directoryParts.Count - 1);

            if (filename.Contains("FRONT"))
                directoryParts.Add("FRONT");
            else if (filename.Contains("REAR"))
                directoryParts.Add("REAR");

            directoryParts.Add(filename);

            return String.Join("\\", directoryParts);
        }
    }
}
