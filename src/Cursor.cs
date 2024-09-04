using System.Drawing;
using Seed;

namespace STiles;

public class Cursor : GameLogic
{
    FullRectangle cursor = new FullRectangle(0, 0, 1, 1, Color.FromArgb(100, 100, 100, 30));
    public static int SelectedType = 0;
    public override void OnStart()
    {

    }
    
    public override void OnFrame()
    {
        cursor.PosX = Math.Floor(Mouse.PosX);
        cursor.PosY = Math.Floor(Mouse.PosY);           
        if(Collider.IsColliding(cursor, Tiles.Canvas) && (!Collider.IsColliding(UI.LoadImg, cursor) || !UI.AddButtonVisible) && !Collider.IsColliding(UI.ControlRect, cursor))
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
        {
            if(SelectedType == 0)
            {
                Tiles.CurrTiles.Remove(tile);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Removed tile at {cursor.PosX}; {cursor.PosY};");   
            }    
            else
            {
                if(tile.Type != SelectedType)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"Set tile at {cursor.PosX}; {cursor.PosY}; to {SelectedType}");  
                }
                tile.Type = SelectedType;
                if(UI.Buttons[SelectedType].IsTextured)
                {
                    Sprite sprite = new Sprite(cursor.PosX, cursor.PosY, 1, 1, UI.Buttons[SelectedType].Texture);
                    tile.TileElement = sprite;
                }
                else
                {
                    tile.TileElement = rect;
                }
            }
        }
        else
        {
            if(SelectedType != 0)
            {    
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Set tile at {cursor.PosX}; {cursor.PosY}; to {SelectedType}");  
                if(UI.Buttons[SelectedType].IsTextured)
                {
                    Tiles.CurrTiles.Add(new Tile(new Sprite(cursor.PosX, cursor.PosY, 1, 1, UI.Buttons[SelectedType].Texture), text, SelectedType));
                }
                else
                {
                    Tiles.CurrTiles.Add(new Tile(rect, text, SelectedType));
                }
            }    
        }
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