using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Net.Mime;

public class CustomColor
{
    public int R = 0;
    public int G = 0;
    public int B = 0;

    public CustomColor(int r, int g, int b)
    {
        this.R = r;
        this.G = g;
        this.B = b;
    }
}

namespace ImageChangeColorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nПривет, введите цвет в формате RGB который нужно заменить:");
            Console.WriteLine("\nВведите R:"); 
            var r = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nВведите G:"); 
            var g = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nВведите B:");  
            var b = Convert.ToInt32(Console.ReadLine()); 
            Console.WriteLine($"\nВы выбрали {r},{g},{b}");
            Console.WriteLine("Ввыдите цвет на который нужно заменить в формате RGB: ");
            Console.WriteLine("\nВведите R:"); 
            var nr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nВведите G:"); 
            var ng = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nВведите B:");  
            var nb = Convert.ToInt32(Console.ReadLine()); 
            Console.WriteLine($"\nВы заменяете {r},{g},{b} => {nr},{ng},{nb}"); 
            
            CustomColor changeColor = new CustomColor(r, g, b);
            
            string[] files = Directory.GetFiles(Environment.CurrentDirectory);
            List<string> images = new List<string>();

            string[] imagesExtension = {".png", ".jpg", ".jpeg"};
            
            foreach (var file in files)
            {
                if (imagesExtension.Contains(Path.GetExtension(file)))
                {
                    images.Add(file); 
                }
            }

            if (images.Count == 0)
            {
                Console.WriteLine("В директории нет фото");
                Environment.Exit(0);   
            }

            string newPath = Environment.CurrentDirectory;
            
            foreach (var image in images)
            {
                Bitmap newImage = new Bitmap(image, true);

                newImage = ChangeColor(newImage, changeColor, Color.FromArgb(nr, ng, nb));
            
                newImage.Save(newPath+Path.GetFileNameWithoutExtension(image)+"_change"+Path.GetExtension(image));   
            }
            Console.Write("\nPress any key to exit...");
            Console.ReadKey(true);
        }

        static Bitmap ChangeColor(Bitmap image, CustomColor color, Color nColor)
        {
            for (var x = 0; x < image.Width; x++)
            for (var y = 0; y < image.Height; y++)
            {
                var pixel = image.GetPixel(x, y);
                if (pixel.R >= color.R - 10 && pixel.R <= color.R + 10 && pixel.G >= color.G - 10 && pixel.G <= color.G + 10 &&
                    pixel.B >= color.B - 10 && pixel.B <= color.B + 10)
                    image.SetPixel(x, y, nColor);
            }
            return image;
        }
    }
}