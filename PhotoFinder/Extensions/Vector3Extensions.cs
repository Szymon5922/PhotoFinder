using PhotoFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFinder.Extensions
{
    public static class Vector3Extensions
    {
        public static bool IsPointAtPhoto(this Vector3 point, PhotoData photo, out double distance)
        {
            Vector3 vTarget = Vector3.Normalize(point - photo.CameraPosition);

            double yaw = MathF.Acos(Vector3.Dot(vTarget, photo.Front)) * (180 / Math.PI);
            double pitch = MathF.Asin(Vector3.Dot(vTarget, photo.Up)) * (180 / Math.PI);

            distance = yaw+pitch;

            return (yaw <= photo.HFOV / 2) && (pitch <= photo.VFOV / 2);
        }
    }
}
