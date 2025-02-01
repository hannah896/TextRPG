using System;
using System.Globalization;

/// <summary>
/// Summary description for Class1
/// </summary>
public class GameManager
{
    private Player player;
    private PlayerManager playerManager;

    public GameManager(Player player, PlayerManager playerManager)
	{
        this.player = player;
        this.playerManager = playerManager;
    }
    //마을 페이지
    public void Villiage()
	{
        Console.Clear();
        string[] doing = { "상태 보기", "인벤토리", "상점" };
        int command;

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
        
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"{i+1}. {doing[i]}");
        }

        command = InputCommand();

        //상태보기
        if (command == 1)
        {
            playerManager.ShowState();

            while(true)
            {
                int commend = InputCommand();
                if (commend == 0)
                {
                    Console.Clear();
                    break;
                }
            }
        }

        //인벤토리
        else if (command == 2)
        {
            playerManager.ShowInventory();
        }

        //상점
        else if (command == 3)
        {

        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");

            Thread.Sleep(1300);
        }
    }
    
    // 행동 입력기
	public int InputCommand()
	{
        Console.WriteLine();
        string command;
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        while (true)
        {
            Console.Write(">> ");
            command = Console.ReadLine();
            if (command != "" && int.TryParse(command, out int result))
            {
                return result;
            }

            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}
