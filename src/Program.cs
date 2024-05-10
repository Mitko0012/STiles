using Seed;

namespace SMap
{
    public class Program
    {
        public static void Main(string[] args)
        {
            UI uI = new UI();
            Tiles tiles = new Tiles();
            Cursor cursor = new Cursor();

            GameLogic.StartGameLoop();
        }
    }
}