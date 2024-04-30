
using MetadataExtractor;

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
            if (IsAspectRatioSixteenNinths(filePath)){
                
                Console.WriteLine($"{filePath}: {IsAspectRatioSixteenNinths(filePath)}");
                File.Copy(filePath, $"{destinationPath}/img-{Guid.NewGuid()}.jpg", false);
            }
        }
    }

    public static bool IsAspectRatioSixteenNinths(string pathToImage){
        var directories = ImageMetadataReader.ReadMetadata(pathToImage);
        
        var imageHeightTag = directories
            .SelectMany(directory => directory.Tags)
            .FirstOrDefault(tag => tag.Name == "Image Height");

        var imageWidthTag = directories
            .SelectMany(directory => directory.Tags)
            .FirstOrDefault(tag => tag.Name == "Image Width");

        double imageHeight = int.Parse(imageHeightTag.Description.Split(" ")[0]);
        double imageWidth = int.Parse(imageWidthTag.Description.Split(" ")[0]);

        var sixteenNinths = 16.0/9.0;
        //Console.WriteLine($"16.0/9.0: { Math.Round(sixteenNinths,2) }");
        
        var imgAspectRatio = imageWidth / imageHeight;
        // Console.WriteLine($"imageWidth: {imageWidth}");
        // Console.WriteLine($"imageHeight: {imageHeight}");
        // Console.WriteLine($"imageWidth / imageHeight: {Math.Round(imgAspectRatio, 2)}");

        bool result = (Math.Round(imgAspectRatio, 2) == Math.Round(sixteenNinths,2)); 

        // Taking only 4k Images
        if (imageWidth < 4000)
        {
            result = false;
        }

        return result;
    }
}