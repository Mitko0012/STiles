using Seed;
using System.Reflection;

namespace STiles
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            EmbdeddedResourceLoader.CurrAssembly = Assembly.GetExecutingAssembly();
            
            Tiles tiles = new Tiles();
            UI uI = new UI();
            Cursor cursor = new Cursor();

            GameLogic.StartGameLoop();
        }
    }
}