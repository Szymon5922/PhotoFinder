using DotSpatial.Data;
using Microsoft.Win32;
using PhotoFinder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFinder.Handlers
{
    public static class SHPFileHandler
    {
        public static void LoadData(ObservableCollection<SearchingTarget> target)
        {
            var folderDialog = new OpenFolderDialog();
            folderDialog.Multiselect = true;

            if (folderDialog.ShowDialog() == true)
            {
                foreach (string folder in folderDialog.FolderNames)
                {
                    foreach (var searchingTarget in ReadFolder(folder))
                        target.Add(searchingTarget);
                }
            }
        }
        public static List<SearchingTarget> ReadFolder(string shpFilepath)
        {
            List<SearchingTarget> targets = new();

            string directory = Path.GetDirectoryName(shpFilepath);

            using (Shapefile shapefile = Shapefile.OpenFile(shpFilepath))
            {
                var data = shapefile.Attributes.Table;
                bool containsPhotos = data.Columns.Contains("JPG");

                if (IsDataTableValid(data))
                {
                    foreach (DataRow row in data.Rows)
                    {
                        SearchingTarget target = new SearchingTarget(GetVector(row));

                        if(containsPhotos)
                            target.ImageDirectory = Path.Combine(directory,row["JPG"].ToString());

                        targets.Add(target);
                    }
                }
            }
            return targets;
        }
        private static Vector3 GetVector(DataRow row)
        {
            Vector3 vector = new Vector3();

            vector.X = float.Parse(row["Y"].ToString());
            vector.Y = float.Parse(row["X"].ToString());
            vector.Z = float.Parse(row["Z"].ToString());

            return vector;
        }
        public static bool IsDataTableValid(DataTable table)
        {
            return (table != null &&
                    table.Columns.Contains("X") &&
                    table.Columns.Contains("Y") &&
                    table.Columns.Contains("Z"));
        }
    }
}
