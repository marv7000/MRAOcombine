﻿using System;
using System.Drawing;
using System.IO;

namespace MRAOcombine
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[INFO] This tool combines Metallic, Roughness and AO maps into one MRAO map.");
            Bitmap M = null;
            Bitmap R = null;
            Bitmap AO = null;
            
            string path = "";

            foreach (string arg in args)
            {
                if (arg.Contains("Metal"))
                {
                    M = new Bitmap(arg);
                    Console.WriteLine("Metallic:\t" + arg);
                    path = arg.Replace(".png", "_MRAO.png");
                }
                else if (arg.Contains("Rough"))
                {
                    R = new Bitmap(arg);
                    Console.WriteLine("Roughness:\t" + arg);
                }
                else if (arg.Contains("AO"))
                {
                    AO = new Bitmap(arg);
                    Console.WriteLine("AO:\t\t" + arg);
                }
                else
                    throw new ArgumentException();
            }

            Bitmap Out = new Bitmap(M.Width, M.Height);
            for (int x = 0; x < M.Width; x++)
            {
                for(int y = 0; y < M.Height; y++)
                {
                    // Use R channels to set RGB channels on the output texture
                    Out.SetPixel(x, y, Color.FromArgb(M.GetPixel(x,y).R, R.GetPixel(x, y).R, AO.GetPixel(x, y).R, 255));
                }
            }


            // Save to file
            Out.Save(path);
            Console.WriteLine("\nSaved file to " + path);

            Console.ReadLine();
        }
    }
}