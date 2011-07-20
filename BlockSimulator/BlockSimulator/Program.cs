namespace Indiv0.BlockSimulator
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (BlockSimulatorGame game = new BlockSimulatorGame())
            {
                game.Run();
            }
        }
    }
#endif
}

