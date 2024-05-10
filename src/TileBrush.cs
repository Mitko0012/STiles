using System.Drawing;
using Seed;

namespace STiles;

public class TileBrush
{
    static int count = -1;
    public int Index;
    public Element Rect;
    static double currY = -9;
    public double PosX;
    public double PosY;
    

    public TileBrush(CollidableElement button)
    {
        count++;
        Index = count;
        double x = count % 2 != 0? -11.5 : -8;
        if(count % 2 == 0)
            currY += 3;
        PosX = x;
        PosY = currY;
        Rect = button;
        AddButt();
    }

    public TileBrush()
    {
        count++;
        double x = count % 2 == 0? -11.5 : -8;
        Index = count;
        if(count % 2 == 0)
            currY += 3;
        Rect = new FullRectangle(x, currY, 2, 2, Color.LightGray);
        PosX = x;
        PosY = currY;
        AddButt();
    }

    public void AddButt()
    {
        UI.Buttons.Add(this);
    }

    public void Draw()
    {
        Rect.PosY = PosY + UI.ButtonOffset;
        if(Rect.PosY < 6 && Rect.PosY > -9.5)
        {
            Rect.Draw();
        }
    }
}