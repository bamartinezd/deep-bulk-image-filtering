# Deep Bulk Image Filtering

A powerful .NET 8 console application for bulk filtering and organizing images based on metadata criteria including resolution, aspect ratio, and orientation.

## 🚀 Features

### Resolution Filtering
The application supports filtering by standard resolutions:

- **3840x2160 (4K UHD)** - Professional grade resolution with four times the pixels of 1080p, ideal for graphic design and video editing
- **1920x1080 (Full HD)** - Standard resolution used by most displays worldwide *(Coming Soon)*
- **2560x1440/1600 (2K)** - High-end resolution for premium displays *(Coming Soon)*
- **7680x4320 (8K)** - Ultra-high resolution for future-proof content *(Coming Soon)*

### Aspect Ratio Detection
Automatically identifies and filters images by aspect ratio:

- **16:9 (1.78:1)** - Widescreen format, most common for HDTV and computer monitors
- **9:16** - Mobile-optimized format, popular for Instagram Stories and TikTok
- **4:3 (1.33:1)** - Traditional fullscreen format *(Coming Soon)*
- **1.85:1 & 2.40:1** - Cinematic aspect ratios *(Coming Soon)*
- **3:2** - Standard photography format *(Coming Soon)*
- **5:4 (1.25:1)** - Large format photography standard *(Coming Soon)*
- **1:1** - Square format for social media and prints *(Coming Soon)*

### Image Orientation Support
Handles all image orientations:
- **Landscape** - Normal horizontal orientation
- **Portrait** - Vertical orientation (90° rotation)
- **Landscape Flipped** - Inverted horizontal orientation
- **Portrait Flipped** - Inverted vertical orientation

## 📋 Prerequisites

- .NET 8.0 SDK or Runtime
- Windows, macOS, or Linux

## 🛠️ Installation

1. Clone the repository:
```bash
git clone https://github.com/yourusername/deep-bulk-image-filtering.git
cd deep-bulk-image-filtering
```

2. Navigate to the project directory:
```bash
cd Images-by-aspect-ratio
```

3. Restore dependencies:
```bash
dotnet restore
```

## 🚀 Usage

1. Run the application:
```bash
dotnet run
```

2. Follow the interactive prompts:
   - Enter the **source directory** containing your images
   - Enter the **destination directory** where filtered images will be saved

3. The application will automatically:
   - Scan all JPG files in the source directory (including subdirectories)
   - Filter images based on resolution (minimum 4000px width)
   - Check aspect ratio (currently 16:9)
   - Organize images by orientation in the destination folder
   - Generate unique filenames using GUIDs

## 📁 Output Structure

```
destination/
├── 1/          # Normal orientation
│   ├── img-{guid}.jpg
│   └── ...
├── 3/          # 180° rotation
│   ├── img-{guid}.jpg
│   └── ...
├── 6/          # 90° clockwise rotation
│   ├── img-{guid}.jpg
│   └── ...
└── 8/          # 270° clockwise rotation
    ├── img-{guid}.jpg
    └── ...
```

## 🔧 Configuration

The application currently filters for:
- **Minimum resolution**: 4000px width
- **Aspect ratio**: 16:9 (1.78:1)
- **File format**: JPG only

## 📦 Dependencies

- **SixLabors.ImageSharp** (3.1.10) - Image processing and metadata extraction
- **MetadataExtractor** (2.8.1) - EXIF metadata handling

## 🐛 Known Issues

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- Built with .NET 8
- Powered by SixLabors.ImageSharp for image processing
- Uses MetadataExtractor for EXIF data handling

---

**Note**: This application is designed for bulk processing of large image collections. Ensure you have sufficient disk space and backup your original images before processing.
