using System.Drawing;
using Seed;

namespace STiles;

public class Tile
{
    public CollidableElement TileElement;
    public Text Text;
    public int Type;

    public int PosX;
    public int PosY;
    
    public Tile(CollidableElement element, Text text, int type)
    {
        TileElement = element;
        PosX = (int)TileElement.PosX;
        PosY = (int)TileElement.PosY;
        Text = text;
        Type = type;
    }

    public void Draw()
    {
        if(Type != 0)
        {
            TileElement.PosX = PosX;
            TileElement.PosY = PosY;
            Text.PosX = PosX + TileElement.Width / 2;
            Text.PosY = PosY + TileElement.Height / 2;
            Text.DisplayText = Convert.ToString(Type);
            TileElement.Draw();
            if(!UI.Buttons[Type].IsTextured)
                Text.Draw();
        }
    }
}