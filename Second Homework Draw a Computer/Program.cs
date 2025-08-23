namespace Second_Homework_Draw_a_Computer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(1044, 680))
            {
                game.Run();
            }
        }
    }
}
