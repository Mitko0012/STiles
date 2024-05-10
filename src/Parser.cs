using Seed;
using System.Windows.Forms;

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
        catch (Exception)
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
        dialog.Filter = "JSON files (*.json) | *.json | All files (*.) | *.";
        if(dialog.ShowDialog() == DialogResult.OK)
        {
            ObjectSerialization.SerializeJsonToFile(map, dialog.FileName);
        }
    }
}