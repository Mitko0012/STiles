using System.Drawing;
using System.Windows.Forms;
using Seed;

namespace STiles;

public class UI : GameLogic
{
    public static FullRectangle Canvas = new FullRectangle(-5, -12.5, 25, 25, Color.White);
    public static FullRectangle SideUI = new FullRectangle(-12.5, -12.5, 7.5, 25, Color.FromArgb(130, 43, 0));
    FullRectangle deleteButton = new FullRectangle(-11.5, -9, 2, 2, Color.Red);
    public static FullRectangle addButt = new FullRectangle(-11.7, 6.5, 5.5, 2, Color.DarkCyan);
    FullRectangle saveButt = new FullRectangle(-11.7, 9.5, 5.5, 2, Color.Cyan);
    FullRectangle loadImg = new FullRectangle(7, -11, 5.5, 2, Color.Red);

    FullRectangle goUp = new FullRectangle(-11.2, -11.7, 1.5, 1.5, Color.LightGray);
    FullRectangle goDown = new FullRectangle(-8.1, -11.7, 1.5, 1.5, Color.LightGray);

    public static List<TileBrush> Buttons = new List<TileBrush>();
    public static int ButtonOffset = -3;
    bool mouseDown;

    public static bool AddButtonVisible = false;

    public override void OnStart()
    {
        TileBrush dellButton = new TileBrush(deleteButton);
        GameLogic.SetColor(Color.Black);
        GameLogic.SetSize(600, 639);
        GameLogic.UnitsOnCanvas = 25;

        deleteButton.IsSticky = true;
        Canvas.IsSticky = true;
    }

    public override void OnFrame()
    {
        if(Cursor.SelectedType == 0)
        {
            AddButtonVisible = false;
        }
        else
        {
            AddButtonVisible = true;
        }
        
        if(Collider.IsPointInside(addButt, Mouse.PosX, Mouse.PosY) && Mouse.LeftDown && !mouseDown)
        {
            TileBrush butt = new TileBrush();
        }
        
        if(Collider.IsPointInside(goUp, Mouse.PosX, Mouse.PosY) && Mouse.LeftDown && !mouseDown)
        {
            ButtonOffset -= 2;
        }

        if(Collider.IsPointInside(goDown, Mouse.PosX, Mouse.PosY) && Mouse.LeftDown && !mouseDown)
        {
            ButtonOffset += 2;
        }

        if(Collider.IsPointInside(saveButt, Mouse.PosX, Mouse.PosY) && Mouse.LeftDown && !mouseDown)
        {
            Parser.Parse();
        }

        if(Collider.IsPointInside(loadImg, Mouse.PosX, Mouse.PosY) && Mouse.LeftDown && !mouseDown)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.png, *jpg) | *.png";
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    STexture texture = new STexture(dialog.FileName, STextureOrigin.FilePath);
                    Sprite sprite = new Sprite(1, 1, 1, 1, texture);
                    
                    TileBrush tileBrush = Buttons[Cursor.SelectedType];        
                    tileBrush.Rect = new Sprite(tileBrush.Rect.PosX , tileBrush.Rect.PosY, tileBrush.Rect.Width, tileBrush.Rect.Height, texture);
                    tileBrush.IsTextured = true;
            
                    foreach(Tile tile in Tiles.CurrTiles)
                    {
                        if(tile.Type == Cursor.SelectedType)
                        {
                            tile.TileElement = new Sprite(tile.PosX, tile.PosY, 1, 1, texture);
                        }
                    }
                }
                catch {}  
            }
        }

        if(Mouse.LeftDown == true)
        {
            mouseDown = true;
        }
        else
        {
            mouseDown = false;
        }
        
        Canvas.Draw();
        SideUI.Draw();
        
        if(AddButtonVisible)
            loadImg.Draw();
        
        goUp.Draw();
        goDown.Draw();
        saveButt.Draw();
        loadImg.Draw();
        addButt.Draw();

        foreach(TileBrush button in Buttons)
        {
            if(Collider.IsPointInside((CollidableElement)button.Rect, Mouse.PosX, Mouse.PosY))
            {
                Cursor.SelectedType = button.SelectedType;
            }
            button.Draw();
        }
    }
}