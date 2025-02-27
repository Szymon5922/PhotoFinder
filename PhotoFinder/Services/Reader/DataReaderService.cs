using PhotoFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFinder.Services.Reader
{
    public static class DataReaderService
    {
        public static List<PhotoData> ReadInput(List<PhotoFolder> photoFolders)
        {
            List<PhotoData> photoDatas = new List<PhotoData>();

            photoFolders.ForEach(photoFolder => {photoDatas.AddRange(ReadFolder(photoFolder));});

            return photoDatas;
        }
        public static List<PhotoData> ReadFolder(PhotoFolder folder)
        {
            List<PhotoData> loadedDatas = new List<PhotoData>();

            foreach (var photoInfo in folder.PhotosInfos)
            {
                IReadingStrategy readingStrategy = GetReadingStrategy(photoInfo.type);
                
                loadedDatas.AddRange(readingStrategy.Read(photoInfo.directory));
            }

            return loadedDatas;
        }
        private static IReadingStrategy GetReadingStrategy(PositionsInfoType positionInfoType)
        {
            IReadingStrategy readingStrategy = null;

            switch (positionInfoType)
            {
                case PositionsInfoType.Alignment:
                    readingStrategy = new AlignmentReadingStrategy();
                    break;
                case PositionsInfoType.Centers:
                    readingStrategy = new CentersReadingStrategy();
                    break;
                default:
                    break;
            }

            return readingStrategy;
        }
    }
}
