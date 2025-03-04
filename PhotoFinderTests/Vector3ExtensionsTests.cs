using FluentAssertions;
using PhotoFinder.Extensions;
using PhotoFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFinderTests
{
    public class Vector3ExtensionsTests
    {
        [Theory]
        [MemberData(nameof(GetIsPointAtPhotoTestCases))]
        public void IsPointAtPhoto_ShouldCheckIsPointAndPhoto_AndAssignPitchYaw(Vector3 target, PhotoData searching, double expectedYaw, double expectedPitch, bool expected)
        {
            //act
            double yaw, pitch;
            bool result = target.IsPointAtPhoto(searching, out yaw, out pitch);

            //assert
            result.Should().Be(expected);
            yaw.Should().BeApproximately(expectedYaw, 0.05f);
            pitch.Should().BeApproximately(expectedPitch, 0.05f);
        }

        public static IEnumerable<object[]> GetIsPointAtPhotoTestCases()
        {
            yield return new object[]
            {
                new Vector3(0, 0, 5),
                new PhotoData
                {
                    CameraPosition = new Vector3(0, 0, 0),
                    Front = new Vector3(0, 0, 1),
                    Up = new Vector3(0, 1, 0),
                    HFOV = 90,
                    VFOV = 90
                },
                0.0,   //yaw
                0.0,   //pitch
                true   //result
            };

            yield return new object[]
            {
                new Vector3(5, 0, 5),
                new PhotoData
                {
                    CameraPosition = new Vector3(0, 0, 0),
                    Front = new Vector3(0, 0, 1),
                    Up = new Vector3(0, 1, 0),
                    HFOV = 90,
                    VFOV = 90
                },
                45.0,  //yaw
                0.0,   //pitch
                true   //result
            };

            yield return new object[]
            {
                new Vector3(0, 5, 5),  // point on end of VFOV
                new PhotoData
                {
                    CameraPosition = new Vector3(0, 0, 0),
                    Front = new Vector3(0, 0, 1),
                    Up = new Vector3(0, 1, 0),
                    HFOV = 90,
                    VFOV = 90
                },
                0.0,   //yaw
                45.0,  //pitch
                true   //result
            };

            yield return new object[]
            {
                new Vector3(10, 0, 5),  // out of HFOV
                new PhotoData
                {
                    CameraPosition = new Vector3(0, 0, 0),
                    Front = new Vector3(0, 0, 1),
                    Up = new Vector3(0, 1, 0),
                    HFOV = 90,
                    VFOV = 90
                },
                63.4,  //yaw
                0.0,   //pitch
                false  //result
            };

            yield return new object[]
            {
                new Vector3(0, 10, 5),  // out of VFOV
                new PhotoData
                {
                    CameraPosition = new Vector3(   0, 0, 0),
                    Front = new Vector3(0, 0, 1),
                    Up = new Vector3(0, 1, 0),
                    HFOV = 90,
                    VFOV = 90
                },
                0.0,   //yaw
                63.4,  //pitch
                false  //result
            };
        }
    }
}
