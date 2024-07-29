using MetadataExtractor;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using System.Diagnostics;
using Directory = System.IO.Directory;

public class Program
{
    public static void Main (string[] args){
        //string pathToImage = @"C:\Users\Public\Documents\Camera\20240218_081621.jpg";// Windows
        //string pathToImage = @"/media/bamartinezd/8256A54E56A5442F/Users/Public/Documents/Camera/20240218_081621.jpg";

        Console.WriteLine("Copy and paste here your source image directory:");
        string sourcePath = Console.ReadLine();
        while (!System.IO.Directory.Exists(sourcePath))
        {
            Console.WriteLine($"{sourcePath} is an invalid directory, try again.");
            sourcePath = Console.ReadLine();
        }

        
        Console.WriteLine("Copy and paste here your destination image directory:");
        string destinationPath = Console.ReadLine();
        while (!System.IO.Directory.Exists(destinationPath))
        {
            Console.WriteLine($"{sourcePath} is an invalid directory, try again.");
            destinationPath = Console.ReadLine();
        }

        IEnumerable<string> filePaths = System.IO.Directory.EnumerateFiles(sourcePath, "*.jpg", SearchOption.AllDirectories);

        foreach (var filePath in filePaths)
        {
            using (var image = Image.Load(filePath))
            {
                int imageWidth = image.Width;
                int imageHeight = image.Height;

                if (imageWidth < 4000)
                {
                    continue;
                }

                var exifProfile = image.Metadata.ExifProfile;
                if (exifProfile is not null)
                {
                    exifProfile.TryGetValue(ExifTag.Orientation, out var orientation);
                    var result = IsAspectRatioSixteenNinths(imageWidth, imageHeight);

                    if (result)
                    {
                        Console.WriteLine($"{filePath}: {result}");

                        string destinationFile = $"{destinationPath}/{orientation}/img-{Guid.NewGuid()}.jpg";

                        string destinationDirectory = Path.GetDirectoryName(destinationFile);
                        Directory.CreateDirectory(destinationDirectory);
                        File.Copy(filePath, destinationFile, true);
                    }
                }
                
            }
        }
    }

    public static bool IsAspectRatioSixteenNinths(double imageWidth, double imageHeight){
        var sixteenNinths = 16.0/9.0;
        var imgAspectRatio = imageWidth / imageHeight;
        return (Math.Round(imgAspectRatio, 2) == Math.Round(sixteenNinths,2));
    }
}