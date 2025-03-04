using FluentAssertions;
using PhotoFinder.Models;
using PhotoFinder.Services.Reader;
using System.Numerics;

namespace PhotoFinderTests
{
    public class AlignmentReadingStrategyTests
    {
        private AlignmentReadingStrategy _readingStrategy = new FakeAlignmentReadingStrategy();

        [Theory]
        [MemberData(nameof(GetIsHdaderCompleteTestData))]
        public void IsHeaderComplete_ShouldVerifyHeader(List<string> header, bool expected)
        {
            //act
            bool result = _readingStrategy.IsHeaderComplete(header);

            //assert
            result.Should().Be(expected);
        }
        public static IEnumerable<object[]> GetIsHdaderCompleteTestData => new List<object[]>
        {
            new object[] { new List<string> { "X", "Y", "Z", "FrontX", "FrontY", "FrontZ", "UpX", "UpY", "UpZ", "VFOV", "HFOV", "FileName" } , true },
            new object[] { new List<string> { "X", "Y", "Z", "FrontX", "FrontY", "FrontZ", "UpX", "UpY", "UpZ", "VFOV", "HFOV" } , true },
            new object[] { new List<string> { "X", "Y", "Z", "FrontX", "FrontY", "FrontZ", "UpX", "UpY", "UpZ", "VFOV", "HFOV" , "abc", "qwer1234"} , true },
            new object[] { new List<string> { "X", "Y", "Z", "FrontX", "FrontY", "UpX", "UpY", "UpZ", "VFOV", "HFOV", "FileName" } , false },
            new object[] { new List<string> { "X", "Y", "Z", "FrontX", "FrontY", "FrontZ", "UpX", "UpY", "UpZ", "HFOV", "abc", "qwe" } , false }
        };

        [Theory]
        [MemberData(nameof(GetColumnsIndexesTestData))]
        public void GetColumnsIndexes_ShouldReturnMapping(Dictionary<string, int> expectedMapping)
        {
            //arrange
            var header = expectedMapping.Keys.ToList();

            //act
            var mapping = _readingStrategy.GetColumnsIndexes(header);

            //assert
            mapping.Should().Equal(expectedMapping);
        }
        public static IEnumerable<object[]> GetColumnsIndexesTestData => new List<object[]>
        {
            new object[] { new Dictionary<string, int> { {"X", 0},{"Y", 1}, {"Z",2 }, { "FrontX",3 }, {"FrontY",4 },{ "FrontZ",5 },{"UpX",6 },{"UpY",7 },{"UpZ",8 },{"VFOV",9 }, {"HFOV",10}, { "FileName",11} } },
            new object[] { new Dictionary<string, int> { {"Y", 0},{"HFOV", 1}, {"X",2 }, { "FrontX",3 }, {"FrontZ",4 },{ "FrontY",5 },{"VFOV",6 },{"UpY",7 },{"FileName",8 },{"UpX",9 }, {"Z",10}, { "UpZ",11} } },
        };
        [Theory]
        [MemberData(nameof(GetGetPhotoDataFromLineTestData))]
        public void GetPhotoDataFromLine_ShouldCreatePhotoDataWithData(string[] values, Dictionary<string, int> mapping, PhotoData expected)
        {
            //act
            var result = _readingStrategy.GetPhotoDataFromLine(values, mapping);

            //assert
            result.Should().BeEquivalentTo(expected);
        }
        public static IEnumerable<object[]> GetGetPhotoDataFromLineTestData => new List<object[]>
        {
            new object[]
            {
                new string[]{"55215.12","2123.4","55.4","0.6544","0.5211","-0.12356","0.00102","0.124121421","-0.42174","20.5","30.71","testcase1" },
                new Dictionary<string, int> { {"X", 0},{"Y", 1}, {"Z",2 }, { "FrontX",3 }, {"FrontY",4 },{ "FrontZ",5 },{"UpX",6 },{"UpY",7 },{"UpZ",8 },{"VFOV",9 }, {"HFOV",10}, { "FileName",11} },
                new PhotoData()
                {
                    CameraPosition = new Vector3( 55215.12f, 2123.4f,55.4f),
                    Front = new Vector3(0.6544f,0.5211f,-0.12356f),
                    Up = new Vector3(0.00102f,0.124121421f,-0.42174f),
                    VFOV = 20.5,
                    HFOV = 30.71,
                    PhotoDirectory = "path\\testcase1"
                }
            },
            new object[]
            {   //            Y          HFOV   X          FrontX      FrontZ      FrontY       VFOV   UpY       FileName    UpX        Z            UpZ
                new string[]{"22265.25","12.6","544445.4","-0.241242","0.1111111","-0.4788865","45.2","0.55221","testcase2","0.654654","35.71","-0.6666" },
                new Dictionary<string, int> { { "Y", 0 }, { "HFOV", 1 }, { "X", 2 }, { "FrontX", 3 }, { "FrontZ", 4 }, { "FrontY", 5 }, { "VFOV", 6 }, { "UpY", 7 }, { "FileName", 8 }, { "UpX", 9 }, { "Z", 10 }, { "UpZ", 11 } },
                new PhotoData()
                {
                    CameraPosition = new Vector3( 544445.4f, 22265.25f,35.71f),
                    Front = new Vector3(-0.241242f,-0.4788865f,0.1111111f),
                    Up = new Vector3(0.654654f,0.55221f,-0.6666f),
                    VFOV = 45.2,
                    HFOV = 12.6,
                    PhotoDirectory = "path\\testcase2",
                }
            }
        };
    }
    public class FakeAlignmentReadingStrategy : AlignmentReadingStrategy
    {
        protected override string GetPhotoPath(string filename)
        {
            return Path.Combine("path", filename);
        }
    }
}
