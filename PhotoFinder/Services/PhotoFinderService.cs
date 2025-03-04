using PhotoFinder.Extensions;
using PhotoFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFinder.Services
{
    public class PhotoFinderService : IPhotoFinderService
    {
        public List<Result> GetResults(List<SearchingTarget> targets, List<PhotoData> photos)
        {
            List<Result> results = new List<Result>();
            foreach (var target in targets)
            {
                var result = new Result
                {
                    Target = target,
                    FoundPhotos = GetPhotosForPoint(target.Position, photos)
                };

                results.Add(result);
            }
            return results;
        }
        public List<FoundPhoto> GetPhotosForPoint(Vector3 target, List<PhotoData> photos)
        {
            List<FoundPhoto> foundPhotos = new();

            foreach (var photo in photos)
            {
                double distanceFromCamera = double.MaxValue;
                double distanceFromCenter = double.MaxValue;
                double yaw = 0;
                double pitch = 0;

                if (target.IsPointAtPhoto(photo, out yaw, out pitch))
                {
                    foundPhotos.Add(new FoundPhoto
                    {
                        FilePath = photo.PhotoDirectory,
                        Yaw = yaw,
                        Pitch = pitch,
                        HFOV = photo.HFOV,
                        VFOV = photo.VFOV,
                        DistanceFromCenter = Math.Sqrt(yaw * yaw + pitch * pitch),
                        DistanceFromCamera = Vector3.Distance(target, photo.CameraPosition)
                    });
                }
            }

            foundPhotos = foundPhotos
                         .Where(p => p.DistanceFromCamera <= 200)
                         .OrderBy(p => p.DistanceFromCenter)
                         .ToList();

            if (foundPhotos.Any())
                foundPhotos.First().IsSelected = true;

            return foundPhotos;
        }
    }
}
