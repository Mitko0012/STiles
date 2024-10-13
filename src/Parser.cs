using Seed;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace STiles;

public static class Parser
{
    public static void Parse()
    {
        Tilemap map = new Tilemap();
        
        int firstX = 0;
        int firstY = 0;
        int lastX = 0;
        int lastY = 0;

        try
        {
            firstX = Tiles.CurrTiles[0].PosX;
            firstY = Tiles.CurrTiles[0].PosY;
            lastX = Tiles.CurrTiles[0].PosX;
            lastY = Tiles.CurrTiles[0].PosY;
        }
        catch
        {
            firstX = 0;
            firstY = 0;
            lastX = 0;
            lastY = 0;
        }

        foreach (Tile tile in Tiles.CurrTiles)
        {
            if(tile.PosX < firstX)
                firstX = tile.PosX;
            if(tile.PosY < firstY)
                firstY = tile.PosY;
            if(tile.PosX > lastX)
                lastX = tile.PosX;
            if(tile.PosY > lastY)
                lastY = tile.PosY;
        }

        for(int i = 0; i <= lastY - firstY; i++)
        {
            List<int> row = new List<int>();
            for(int j = 0; j <= lastX - firstX; j++)
            {
                row.Add(0);
            }
            map.Map.Add(row);
        }


        foreach(Tile tile in Tiles.CurrTiles)
        {
            map.Map[tile.PosY - firstY][tile.PosX - firstX] = tile.Type;
        }
        
        SaveFileDialog dialog = new SaveFileDialog();
        dialog.Filter = "JSON files (*.json)|*.json|All files (*.)|*.";
        if(dialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                ObjectSerialization.SerializeJsonToFile(map, dialog.FileName);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Successfully saved tilemap!");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Failed to save tilemap at {dialog.FileName}!");
            }
        }
    }
    public static void DeParse()
    {
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = "JSON files (*.json)|*.json|All files (*.)|*.";
        if(dialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                StreamReader sr = new StreamReader(dialog.FileName);
                string json = sr.ReadToEnd();
                sr.Close();
                    Tilemap? testMap = ObjectSerialization.DeserializeJson<Tilemap>(json);
                Tilemap map = new Tilemap();
                if(testMap != null)
                {
                    map = testMap;
                }
                Tiles.CurrTiles = new List<Tile>();
                int x = 0;
                int y = 0;
                foreach(List<int> i in map.Map)
                {
                    foreach(int j in i)
                    {
                        Text text = new Text(x + 1 / 2, y + 1 / 2,
                        0.6, "Arial", "");
                        text.Color = Color.White;
                        text.HorisontalAlignment = HTextAlignment.Center;
                        text.VerticalAlignment = VTextAlignment.Center;
                        if(j != 0)
                        { 
                            if(UI.Buttons.Count < j + 1)
                            {
                                while(UI.Buttons.Count < j + 1)
                                {
                                    TileBrush butt = new TileBrush();
                                }
                            }
                            if(UI.Buttons[j].IsTextured)
                            {
                                Tiles.CurrTiles.Add(new Tile(new Sprite(x, y, 1, 1, UI.Buttons[j].Texture), text, j));
                            }
                            else
                            {
                                FullRectangle rect = new FullRectangle(x, y, 1, 1, Color.Gray);
                                Tiles.CurrTiles.Add(new Tile(rect, text, j));
                            }
                        }
                        x++;
                    }
                    x = 0;
                    y++;
                }
                Camera.PosX = 5.25;
                Camera.PosY = 10;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Successfully loaded tilemap!");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to load tilemap! Couldn't load or parse " + dialog.FileName);
            }
        }
    }

    public static void SaveImageTile()
    {
        FileLocation[] locations = new FileLocation[UI.Buttons.Count];
        int repeatCount = 0;
        foreach(TileBrush brush in UI.Buttons)
        {
            locations[repeatCount] = new FileLocation(brush.IsTextured, brush.BrushType);
            if(brush.IsTextured)
            {
                if(UI.LocalPathActive && brush.FileLocation.StartsWith(Directory.GetCurrentDirectory()))
                {
                    locations[repeatCount].Location = brush.FileLocation.Split(Directory.GetCurrentDirectory())[1];
                    locations[repeatCount].Location = locations[repeatCount].Location.Split("\\")[1];
                }
                else
                {
                    locations[repeatCount].Location = brush.FileLocation;
                }
            }
            else
            {
                locations[repeatCount].Location = "";
            }
            repeatCount++;
        }
        SaveFileDialog dialog = new SaveFileDialog();
        dialog.Filter = "JSON files (*.json)|*.json|All files (*.)|*.";
        if(dialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                ObjectSerialization.SerializeJsonToFile(locations, dialog.FileName);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Successfully saved tileset!");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Failed to save tileset at {dialog.FileName}!");
            }
        }
    }

    public static void LoadImageTile()
    {
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = "JSON files (*.json)|*.json|All files (*.)|*.";
        if(dialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                FileLocation[] fileLocations;
                var testLocations = ObjectSerialization.DeserializeJsonFromFile<FileLocation[]>(dialog.FileName);
                if(testLocations != null)
                {
                    fileLocations = testLocations;
                }
                else
                {
                    fileLocations = [new FileLocation(false, 0)];
                }
                foreach(FileLocation location in fileLocations)
                {
                    try
                    {
                        if(location.IsTextured)
                        {
                            var brush = UI.Buttons.FirstOrDefault(x => x.BrushType == location.Index);
                            if(brush != null)
                            {
                                UI.LoadImage(location.Location, location.Index);
                            }
                            else
                            {
                                while(UI.Buttons.Count <= location.Index)
                                {
                                    new TileBrush();
                                }
                                UI.LoadImage(location.Location, location.Index);
                            }
                        }
                    }
                    catch(Exception ex) {Console.WriteLine(ex.Message);}
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Successfully loaded tileset!");
            }
            catch 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to load tileset! Couldn't load or parse " + dialog.FileName);
            }
        }    
    }
}