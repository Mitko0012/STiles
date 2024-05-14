using System.Drawing;
using System.Runtime.CompilerServices;
using Seed;

namespace STiles;

public class TileBrush
{
    static int count = -1;
    public int BrushType;
    public CollidableElement Rect;
    static double currY = -11.5;
    public double PosX;
    public double PosY;
    public bool IsTextured = false;
    public STexture Texture = new STexture(1, 1);
    Text text = new Text(1, 1, 1, "Arial", "Nigger");
    

    public TileBrush(CollidableElement button)
    {
        count++;
        BrushType = count;
        double x = count % 2 != 0? -11.5 : -8;
        if(count % 2 == 0)
            currY += 3;
        PosX = x;
        PosY = currY;
        Rect = button;
        AddButt();
        Rect.IsSticky = true;
        if(count > UI.MaxBrushes)
        {
            if(count % 2 == 0)
            {
                UI.MaxOffset += 5;
            }
        }
        IsTextured = true;
    }

    public TileBrush()
    {
        count++;
        double x = count % 2 == 0? -11.5 : -8;
        BrushType = count;
        if(count % 2 == 0)
            currY += 3;
        Rect = new FullRectangle(x, currY, 2, 2, Color.Gray);
        PosX = x;
        PosY = currY;
        AddButt();
        Rect.IsSticky = true;
        if(count > UI.MaxBrushes)
        {
            if(count % 2 == 0)
            {
                UI.MaxOffset += 3;
            }
        }
        text.HorisontalAlignment = HTextAlignment.Center;
        text.VerticalAlignment = VTextAlignment.Center;
        text.Color = Color.White;
        text.IsSticky = true; 
    }

    public void AddButt()
    {
        UI.Buttons.Add(this);
    }

    public void Draw()
    {
        Rect.PosY = PosY - UI.ButtonOffset;
        Rect.Draw();
        if(!IsTextured)
        {
            text.PosX = Rect.PosX + 1;
            text.PosY = Rect.PosY + 1;
            text.DisplayText = Convert.ToString(BrushType);
            text.Draw();
        }
    }
}