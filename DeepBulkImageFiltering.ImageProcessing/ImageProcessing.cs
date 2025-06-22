using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace DeepBulkImageFiltering.ImageProcessing;

public class ImageProcessor
{
    private readonly string _sourcePath;
    private readonly string _destinationPath;

    public ImageProcessor(string sourcePath, string destinationPath)
    {
        _sourcePath = sourcePath;
        _destinationPath = destinationPath;
    }

    public void ProcessImages()
    {
        IEnumerable<string> filePaths = Directory.EnumerateFiles(_sourcePath, "*.jpg", SearchOption.AllDirectories);

        foreach (var filePath in filePaths)
        {
            ProcessImage(filePath);
        }
    }

    private void ProcessImage(string filePath)
    {
        using (var image = Image.Load(filePath))
        {
            int imageWidth = image.Width;
            int imageHeight = image.Height;

            if (imageWidth < 3840 || imageHeight < 2160)
            {
                return;
            }

            var exifProfile = image.Metadata.ExifProfile;
            if (exifProfile is not null)
            {
                exifProfile.TryGetValue(ExifTag.Orientation, out var orientation);
                
                string aspectRatio = GetAspectRatio(imageWidth, imageHeight);

                if (!string.IsNullOrEmpty(aspectRatio))
                {
                    Console.WriteLine($"{filePath}: {aspectRatio}");

                    string destinationFile = $"{_destinationPath}/{orientation}/{aspectRatio}/img-{Guid.NewGuid()}.jpg";

                    string? destinationDirectory = Path.GetDirectoryName(destinationFile);
                    Directory.CreateDirectory(destinationDirectory);
                    File.Copy(filePath, destinationFile, true);
                }
            }
        }
    }

    private string GetAspectRatio(double imageWidth, double imageHeight)
    {
        if (IsAspectRatioSixteenNinths(imageWidth, imageHeight)) return "16_9";
        //if (IsAspectRatioFourThirds(imageWidth, imageHeight)) return "4_3";
        //if (IsAspectRatioThreeTwo(imageWidth, imageHeight)) return "3_2";
        //if (IsAspectRatioOneToOne(imageWidth, imageHeight)) return "1_1";
        //if (IsAspectRatioTwentyOneNine(imageWidth, imageHeight)) return "21_9";
        return null;
    }

    private bool IsAspectRatioSixteenNinths(double imageWidth, double imageHeight) => IsAspectRatio(imageWidth, imageHeight, 16.0 / 9.0);
    private bool IsAspectRatioFourThirds(double imageWidth, double imageHeight) => IsAspectRatio(imageWidth, imageHeight, 4.0 / 3.0);
    private bool IsAspectRatioThreeTwo(double imageWidth, double imageHeight) => IsAspectRatio(imageWidth, imageHeight, 3.0 / 2.0);
    private bool IsAspectRatioOneToOne(double imageWidth, double imageHeight) => IsAspectRatio(imageWidth, imageHeight, 1.0);
    private bool IsAspectRatioTwentyOneNine(double imageWidth, double imageHeight) => IsAspectRatio(imageWidth, imageHeight, 21.0 / 9.0);

    private bool IsAspectRatio(double imageWidth, double imageHeight, double targetRatio)
    {
        var imgAspectRatio = imageWidth / imageHeight;
        return Math.Abs(imgAspectRatio - targetRatio) < 0.01;
    }
}