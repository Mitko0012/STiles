using Seed;
using System.Drawing;

namespace STiles;

public class Tiles : GameLogic
{
    public static List<Tile> CurrTiles = new List<Tile>();
    public static FullRectangle Canvas = new FullRectangle(-5, -12.5, 18, 25, Color.White);
    public override void OnStart()
    {
        Canvas.IsSticky = true;
    }

    public override void OnFrame()
    {
        Canvas.Draw();
        foreach(Tile tile in CurrTiles)
        {
            if(Collider.IsColliding(tile.TileElement, Canvas))
                tile.Draw();
        }   
    }
}