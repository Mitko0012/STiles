using System.Drawing;
using Seed;

namespace SMap;

public class Tile
{
    public CollidableElement TileElement;
    public Text Text;

    public int Type;
    
    public Tile(CollidableElement element, Text text, int type)
    {
        TileElement = element;
        Text = text;
        Type = type;
    }

    public void Draw()
    {
        if(Type != 0)
        {
            EmptyRectangle rect = new EmptyRectangle(TileElement.PosX + 0.05, TileElement.PosY + 0.05, TileElement.Width - 0.1, TileElement.Height - 0.1, 0.15, Color.Black);
            Text.DisplayText = Convert.ToString(Type);
            TileElement.Draw();
            Text.Draw();
            rect.Draw();
        }
    }
}