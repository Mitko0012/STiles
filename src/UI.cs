using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;
using Seed;

namespace SMap;

public class UI : GameLogic
{
    public static FullRectangle Canvas = new FullRectangle(-5, -12.5, 25, 25, Color.White);
    public static FullRectangle SideUI = new FullRectangle(-12.5, -12.5, 7.5, 25, Color.FromArgb(130, 43, 0));
    FullRectangle deleteButton = new FullRectangle(-11.5, -9, 2, 2, Color.Red);
    FullRectangle addButt = new FullRectangle(-11.7, 6.5, 5.5, 2, Color.DarkCyan);

    FullRectangle goUp = new FullRectangle(-11.2, -11.7, 1.5, 1.5, Color.LightGray);
    FullRectangle goDown = new FullRectangle(-8.1, -11.7, 1.5, 1.5, Color.LightGray);

    public static List<TileBrush> Buttons = new List<TileBrush>();
    public static int ButtonOffset = -3;
    bool mouseDown;
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
        Canvas.Draw();
        SideUI.Draw();
        addButt.Draw();
        goUp.Draw();
        goDown.Draw();

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

        foreach(TileBrush button in Buttons)
        {
            if(Collider.IsPointInside((CollidableElement)button.Rect, Mouse.PosX, Mouse.PosY))
            {
                Cursor.SelectedTexture = button.Index;
            }
            button.Draw();
        }

        if(Mouse.LeftDown == true)
        {
            mouseDown = true;
        }
        else
        {
            mouseDown = false;
        }
    }
}