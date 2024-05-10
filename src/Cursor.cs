using System.Drawing;
using Seed;

namespace SMap;

public class Cursor : GameLogic
{
    FullRectangle cursor = new FullRectangle(0, 0, 1, 1, Color.LightGray);
    public static int SelectedTexture = 0;
    public override void OnStart()
    {
        
    }
    
    public override void OnFrame()
    {
        cursor.PosX = Math.Floor(Mouse.PosX);
        cursor.PosY = Math.Floor(Mouse.PosY);           
        if(Collider.IsPointInside(UI.Canvas, Mouse.PosX, Mouse.PosY))
        {
            if(Mouse.LeftDown)
            {
                AddTile();
            }
            
            cursor.Draw();
        }
    }

    void AddTile()
    {
        FullRectangle rect = new FullRectangle(cursor.PosX, cursor.PosY, cursor.Width, cursor.Height, Color.Gray);
        Text text = new Text(cursor.PosX + cursor.Width / 2, cursor.PosY + cursor.Height / 2,
        0.6, "Arial", "");
        text.Color = Color.White;
        text.HorisontalAlignment = HTextAlignment.Center;
        text.VerticalAlignment = VTextAlignment.Center;
        Tile? tile = CheckForCollision();
        if(tile != null)
            tile.Type = SelectedTexture;
        else
            Tiles.CurrTiles.Add(new Tile(rect, text, SelectedTexture));
    }

    Tile? CheckForCollision()
    {
        foreach (Tile tile in Tiles.CurrTiles)
        {
            if(Collider.IsColliding(cursor, tile.TileElement))
            {
                return tile;
            }
        }
        return null;
    }
}