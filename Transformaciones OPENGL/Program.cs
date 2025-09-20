namespace Transformaciones_OPENGL
{
    class Program
    {
        static void Main(string[] argvs)
        {
            using (Game game = new Game(1044, 680,"Transformaciones"))
            {
                game.Run();
            }
        }
    }
}