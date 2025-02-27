using Microsoft.Win32;
using PhotoFinder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFinder.Services
{
    public static class PhotoFolderHandler
    {
        public static void LoadData(ObservableCollection<PhotoFolder> target)
        {
            var folderDialog = new OpenFolderDialog();
            folderDialog.Multiselect = true;

            if (folderDialog.ShowDialog() == true)
            {                
                foreach (string folder in folderDialog.FolderNames)
                    target.Add(ReadFolder(folder));
            }
        }
        public static PhotoFolder ReadFolder(string directory)
        {
            string photosFolderDirectory = Path.Combine(directory, "PHOTOS");

            if (!Directory.Exists(photosFolderDirectory))
                throw new ArgumentException($"Directory {directory} not containing photos folder");

            var innerDirectories = Directory.GetDirectories(photosFolderDirectory);
            
            var files = Directory.GetFiles(photosFolderDirectory).ToList();
            

            PhotoFolder folder = new PhotoFolder();
            folder.Directory = directory;
            folder.PhotoFolders.AddRange(innerDirectories);


            foreach (var file in files)
            {
                if (file.EndsWith("alignment.csv"))
                    folder.PhotosInfos.Add((file,PositionsInfoType.Alignment));
            }

            return folder;
        }
    }
}
