namespace Text_RPG_Sparta
{
    internal class Start
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            PlayerManager playerManager = new PlayerManager(player);
            GameManager gameManager = new GameManager(player, playerManager);

            


            while (true)
            {
                //마을에 시작할때
                gameManager.Villiage();

            }
        }
    }
}
