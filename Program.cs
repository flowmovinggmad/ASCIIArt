using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;

var asciiChars = " .,:ilwW@@";
Bitmap img = new("image.jpg");

var divideBy = img.Width / img.Width;
img = new(img, new Size(img.Width / divideBy, img.Height / divideBy));

// Font settings
Font font = new Font("Consolas", 10); // Monospaced font works best
int fontWidth = 9;  // Approximate width of monospaced character
int fontHeight = 9; // Approximate height of character

// Create a new bitmap to draw ASCII art on
Bitmap outputImage = new Bitmap(img.Width * fontWidth, img.Height * fontHeight);
using (Graphics g = Graphics.FromImage(outputImage))
{
    // Set up the graphics object for better text quality
    g.Clear(Color.Black); // Black background instead of white
    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

    // Draw ASCII characters to both console and image
    for (int i = 0; i < img.Height; i++)
    {
        for (int j = 0; j < img.Width; j++)
        {
            var pixel = img.GetPixel(j, i);
            var avg = (pixel.R + pixel.G + pixel.B) / 3;

            char asciiChar = asciiChars[avg * asciiChars.Length / 255 % asciiChars.Length];

            // Output to console
            Console.Write(asciiChar);

            // Skip drawing the space character to keep background black
            if (asciiChar != ' ')
            {
                // Draw to image - use white text instead of black
                g.DrawString(asciiChar.ToString(), font, Brushes.White, j * fontWidth, i * fontHeight);
            }
        }
        // Add a new line in console
        Console.WriteLine();
    }
}

// Save the image
outputImage.Save("ascii_art.png", ImageFormat.Png);
Console.WriteLine("ASCII art has been saved to ascii_art.png");

//RESIZING

string inputImagePath = "ascii_art.png";
        
// Path to save the resized image
string outputImagePath = "ResizedAsciiArt.jpg";

// Load the original image
using (Image originalImage = Image.FromFile(inputImagePath))
{
    // Calculate the new dimensions (20% smaller)
    int newWidth = (int)(originalImage.Width * 0.8);
    int newHeight = (int)(originalImage.Height * 0.8);

    // Create a new bitmap with the new dimensions
    using (Bitmap resizedImage = new Bitmap(newWidth, newHeight))
    {
                // Set the resolution of the new image to match the original
    resizedImage.SetResolution(originalImage.HorizontalResolution, originalImage.VerticalResolution);

                // Draw the original image onto the new bitmap with the new dimensions
    using (Graphics graphics = Graphics.FromImage(resizedImage))
        {
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
        }

        // Save the resized image to the output file
            resizedImage.Save(outputImagePath, ImageFormat.Jpeg);
        }
}

Console.WriteLine("Image resized and saved successfullyto ResizedAsciiArt.jpg");
