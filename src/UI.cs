using System.Drawing;
using System.Windows.Forms;
using NAudio.Wave;
using Seed;

namespace STiles;

public class UI : GameLogic
{
    const double camSpeed = 30;
    public static FullRectangle SideUI = new FullRectangle(-12.5, -12.5, 7.5, 25, Color.FromArgb(130, 43, 0));
    Sprite deleteButton = new Sprite(-11.5, -9, 2, 2, new STexture("STiles.Textures.Close.png", STextureOrigin.EmbeddedImage));
    public static Sprite addButt = new Sprite(-11.8, 9.6, 2.5, 2.5, new STexture("STiles.Textures.Add.png", STextureOrigin.EmbeddedImage));    
    Sprite saveButt = new Sprite(-8.3, 3.3, 2.5, 2.5, new STexture("STiles.Textures.Save.png", STextureOrigin.EmbeddedImage));
    public static Sprite LoadImg = new Sprite(-8.3, 9.6, 2.5, 2.5, new STexture("STiles.Textures.Load.png", STextureOrigin.EmbeddedImage));
    Sprite saveImageTile = new Sprite(-8.3, 6.5, 2.5, 2.5, new STexture("STiles.Textures.SaveImageTile.png", STextureOrigin.EmbeddedImage));
    Sprite loadImageTile = new Sprite(-11.8, 6.5, 2.5, 2.5, new STexture("STiles.Textures.LoadImageTile.png", STextureOrigin.EmbeddedImage));

    Sprite goUp = new Sprite(-11.2, -11.7, 1.5, 1.5, new STexture("STiles.Textures.DownArrow.png", STextureOrigin.EmbeddedImage));
    Sprite goDown = new Sprite(-8.1, -11.7, 1.5, 1.5, new STexture("STiles.Textures.UpArrow.png", STextureOrigin.EmbeddedImage));
    public const int MaxBrushes = 5;
    public static double MaxOffset = 0;

    public static FullRectangle ControlRect = new FullRectangle(-5, 7.5, 18, 5.1, Color.Gray);
    Sprite scrollLeft = new Sprite(4.5, 10.3, 1.8, 1.8, new STexture("STiles.Textures.LeftArrow.png", STextureOrigin.EmbeddedImage));
    Sprite scrollDown = new Sprite(7.2, 10.3, 1.8, 1.8, new STexture("STiles.Textures.DownArrow.png", STextureOrigin.EmbeddedImage));
    Sprite scrollUp = new Sprite(7.2, 7.8, 1.8, 1.8, new STexture("STiles.Textures.UpArrow.png", STextureOrigin.EmbeddedImage));
    Sprite scrollRight = new Sprite(9.9, 10.3, 1.8, 1.8, new STexture("STiles.Textures.RightArrow.png", STextureOrigin.EmbeddedImage));
    Sprite loadTile = new Sprite(-11.8, 3.3, 2.5, 2.5, new STexture("STiles.Textures.LoadTile.png", STextureOrigin.EmbeddedImage));
    Sprite localCheckMark = new Sprite(-11.8, 0.5, 2.5, 2.5, new STexture("STiles.Textures.uncheckedbox.png", STextureOrigin.EmbeddedImage))
    {
        IsSticky = true
    };
    STexture _uncheckedTexture = new STexture("STiles.Textures.uncheckedbox.png", STextureOrigin.EmbeddedImage);
    STexture _checkedTexture = new STexture("STiles.Textures.checkedbox.png", STextureOrigin.EmbeddedImage);

    Text coordText = new Text(-4, 8.6, 1.5, "Arial", "222");
    const double scrollSpeed = 0.2;
    
    public static List<TileBrush> Buttons = new List<TileBrush>();
    public static double ButtonOffset = 0;
    bool mouseDown;
    public static bool AddButtonVisible = false;
    public static bool LocalPathActive = false;

    public override void OnStart()
    {
        TileBrush dellButton = new TileBrush(deleteButton);
        dellButton.IsTextured = false;
        GameLogic.SetColor(Color.Black);
        GameLogic.SetSize(600, 639);
        GameLogic.UnitsOnCanvas = 25;

        deleteButton.IsSticky = true;
        SideUI.IsSticky = true;
        addButt.IsSticky = true;
        LoadImg.IsSticky = true;
        saveButt.IsSticky= true;
        ControlRect.IsSticky = true;
        scrollDown.IsSticky = true;
        scrollLeft.IsSticky = true;
        scrollRight.IsSticky = true;
        scrollUp.IsSticky = true;
        goUp.IsSticky= true;
        goDown.IsSticky= true;
        loadTile.IsSticky = true;
        loadImageTile.IsSticky = true;
        saveImageTile.IsSticky = true;
        coordText.IsSticky = true;
        coordText.Color = Color.White;

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("STiles version 1.0.2");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Created by Mitko0012");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Selected tile: " + Cursor.SelectedType + (Cursor.SelectedType == 0? " (Delete)": ""));

        GameLogic.SetTitle("STiles version 1.0.2");
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
        
        if(LocalPathActive)
        {
            localCheckMark.Texture = _checkedTexture;
        }
        else
        {
            localCheckMark.Texture = _uncheckedTexture;
        }
        if(Collider.IsPointInside(addButt, Mouse.PosX, Mouse.PosY) && Mouse.LeftDown && !mouseDown)
        {
            TileBrush butt = new TileBrush();
        }
        
        if(Collider.IsPointInside(goUp, Mouse.PosX, Mouse.PosY) && Mouse.LeftDown)
        {
            if(ButtonOffset + 3 <= MaxOffset)
                ButtonOffset += 3;
        }

        if(Collider.IsPointInside(goDown, Mouse.PosX, Mouse.PosY) && Mouse.LeftDown)
        {
            if(ButtonOffset - 3 >= 0)
                ButtonOffset -= 3;
        }

        if(Collider.IsPointInside(saveButt, Mouse.PosX, Mouse.PosY) && Mouse.LeftDown && !mouseDown)
        {
            Parser.Parse();
        }

        if(Collider.IsPointInside(LoadImg, Mouse.PosX, Mouse.PosY) && Mouse.LeftDown && !mouseDown && AddButtonVisible)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.png, *.jpg, *.jfif) | *.png; *.jpg; *.jfif";
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                LoadImage(dialog.FileName, Cursor.SelectedType);  
            }
        }
        if(Collider.IsPointInside(loadTile, Mouse.PosX, Mouse.PosY) && Mouse.LeftDown && !mouseDown)
        {
            Parser.DeParse();
        }
        if(Collider.IsPointInside(saveImageTile, Mouse.PosX, Mouse.PosY) && Mouse.LeftDown && !mouseDown)
        {
            Parser.SaveImageTile();
        }
        if(Collider.IsPointInside(loadImageTile, Mouse.PosX, Mouse.PosY) && Mouse.LeftDown && !mouseDown)
        {
            Parser.LoadImageTile();
        }
        if(Collider.IsPointInside(scrollLeft, Mouse.PosX, Mouse.PosY) && mouseDown)
        {   
            Camera.PosX -= camSpeed * DeltaTime;
        }
        if(Collider.IsPointInside(scrollRight, Mouse.PosX, Mouse.PosY) && mouseDown)
        {
            Camera.PosX += camSpeed * DeltaTime;
        }
        if(Collider.IsPointInside(scrollUp, Mouse.PosX, Mouse.PosY) && mouseDown)
        {
            Camera.PosY -= camSpeed * DeltaTime;
        }    
        if(Collider.IsPointInside(scrollDown, Mouse.PosX, Mouse.PosY) && mouseDown)
        {
            Camera.PosY += camSpeed * DeltaTime;
        }
        if(Collider.IsPointInside(localCheckMark, Mouse.PosX, Mouse.PosY) && Mouse.LeftDown && !mouseDown)
        {
            if(LocalPathActive)
            {
                LocalPathActive = false;
            }
            else
            {
                LocalPathActive = true;
            }
        }

        coordText.DisplayText = $"{Math.Floor(Mouse.PosX)}; {Math.Floor(Mouse.PosY)};";

        SideUI.Draw();
        goUp.Draw();
        goDown.Draw();
        addButt.Draw();
        saveButt.Draw();
        ControlRect.Draw();
        scrollLeft.Draw();
        scrollDown.Draw();
        scrollRight.Draw();
        scrollUp.Draw();
        loadTile.Draw();
        saveImageTile.Draw();
        loadImageTile.Draw();
        coordText.Draw();
        localCheckMark.Draw();

        foreach(TileBrush button in Buttons)
        {
            if(button.PosY - ButtonOffset < 0 && button.PosY - ButtonOffset > -9.5)
            {
                if(Collider.IsPointInside((CollidableElement)button.Rect, Mouse.PosX, Mouse.PosY) && Mouse.LeftDown && !mouseDown)
                {
                    Cursor.SelectedType = button.BrushType;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Selected tile: " + Cursor.SelectedType + (Cursor.SelectedType == 0? " (Delete)": ""));
                }
                button.Draw();
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

        if(AddButtonVisible)
            LoadImg.Draw();
    }
    public static void LoadImage(string fileName, int type)
    {
        try
        {
            STexture texture = new STexture(fileName, STextureOrigin.FilePath);
            Sprite sprite = new Sprite(1, 1, 1, 1, texture);
                
            TileBrush tileBrush = Buttons[type];        
            tileBrush.Rect = new Sprite(tileBrush.Rect.PosX , tileBrush.Rect.PosY, tileBrush.Rect.Width, tileBrush.Rect.Height, texture);
            tileBrush.Rect.IsSticky = true;
            tileBrush.IsTextured = true;    
            tileBrush.Texture = texture;
            tileBrush.FileLocation = fileName;
        
            foreach(Tile tile in Tiles.CurrTiles)
            {
                if(tile.Type == type)
                {
                    tile.TileElement = new Sprite(tile.PosX, tile.PosY, 1, 1, texture);
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Successfully loaded image!");
        }
        catch 
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Failed to load " + fileName + "!");
        }  
    }
}