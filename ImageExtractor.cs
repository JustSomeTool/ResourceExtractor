using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text.Json;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ResourceExtractor;

[SupportedOSPlatform("windows")]
public static class ImageExtractor {
    public static void Extract(string outDir) {

        for (var i = 0; i < 10; i++) {
            var romDir = "ROM";
            if (i != 0)
                romDir += $"{i}";
            for (var j = 0; j < 385; j++) {
                var subdir = j.ToString();
                for (var k = 0; k < 128; k++) {
                    try {
                        using var dat = File.Open(Path.Combine(Program.FfxiDir, romDir, subdir, $"{k}.dat"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                        var image = ImageParser.Parse(dat, true);
                        if (image != null && image != default(Bitmap)) {
                            using var file = File.Open($"{outDir}/resources/images2/{i}-{j}-{k}.png", FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                            image.Save(file, ImageFormat.Png);
                            Console.WriteLine($"{i}:{j}:{k} >> Image saved!");
                        } else {
                            Console.WriteLine($"{i}:{j}:{k} >> No image found!");
                        }
                    } catch (Exception ex) {
                        Console.WriteLine($"{i}:{j}:{k} >> {ex.Message}");
                    }
                }
            }
        }
        /*
        for (var i = 110000; i < 150000; i++) 
        {
            try {
                Console.WriteLine(i.ToString());
                using var dat = File.Open(Program.GetPath(i), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            
                var image = ImageParser.Parse(dat, true);
                if (image != null && image != default(Bitmap)) {
                    using var file = File.Open($"{outDir}/resources/images2/{i}.png", FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                    image.Save(file, ImageFormat.Png);
                    Console.WriteLine($"{i}: Image saved!");
                }
                else {
                    Console.WriteLine($"{i}: No image found!");
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"{i}: {ex.Message}");
            }
        }
        */
        
    }

    #region JSON Comments/Posterity
    //There are a number of duplicate or unused map DAT's.
    //These are values that have been removed from MapDats.json.
    //"2": {"0": 5689},                     //0 is not used in game.
    //"29": {"2": 5729},                    //Duplicate of map 1.
    //"30": {"2": 5731},                    //Duplicate of map 1.
    //"44": {"2": 5748},                    //Duplicate of map 1.
    //"140": {"15": 5341},                  //15 is not used in game.
    //"142": {"0": 5343},                   //Duplicate of map 1.
    //"169": {"3": 5633},                   //Duplicate of map 2.
    //"171": {"0": 5408,                    //0 is a dummy map.
    //"173": {"0": 5410},                   //0 is not used in game.
    //"174": {"0": 5411},                   //0 is not used in game.
    //"190": {"1001": 5430},                //Duplicate of map 1.
    //"191": {"0": 5431},                   //Duplicate of map 1.
    //"205": {"1015": 5456, "1016": 5457    //These are maps that never made it to game, different zone name.
    //"205": {"18": 5685                    //18 is not used in game.
    //"226": {"0": 5475},                   //0 is a dummy map.
    //"242": {"0": 5490},                   //0 is a dummy map.
    #endregion
}
