using System;

namespace GameProject3
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameProject3())
                game.Run();
        }
    }
}
