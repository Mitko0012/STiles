using Seed;

namespace SMap;

public class Tiles : GameLogic
{
    public static List<Tile> CurrTiles = new List<Tile>();
    
    public override void OnStart()
    {

    }

    public override void OnFrame()
    {
        foreach(Tile tile in CurrTiles)
        {
            tile.Draw();
        }
    }
}