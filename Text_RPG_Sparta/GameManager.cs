using System;
using System.Data.SqlTypes;
using System.Globalization;

/// <summary>
/// Summary description for Class1
/// </summary>
public class GameManager
{
    private Player player;
    private PlayerManager playerManager;
    private Item [] storeItem = new Item [6];

    //{이름, 효과, 설명, 가격, 증가시키는 }
    private string[,] item = {
        { "수련자 갑옷", "방어력 +5", "수련에 도움을 주는 갑옷입니다.", "1000", "Def", "5"},
        { "무쇠갑옷", "방어력 +9", "무쇠로 만들어져 튼튼한 갑옷입니다.", "1500", "Def", "9"},
        { "스파르타의 갑옷", "방어력 +15", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", "3500", "Def", "15"},
        { "낡은 검", "공격력 +2", "쉽게 볼 수 있는 낡은 검 입니다.", "600", "Atk", "2"},
        { "청동 도끼", "공격력 +5", "어디선가 사용되던 것 같은 도끼입니다.", "1500", "Atk", "5"},
        { "스파르타의 창", "공격력 +7", "스파르타의 전사들이 사용했다는 전설의 창입니다.", "3500", "Atk", "7"}
    };

    public GameManager(Player player, PlayerManager playerManager)
	{
        this.player = player;
        this.playerManager = playerManager;
        for (int i = 0; i< 6; i++)
        {
            storeItem[i] = new Item(item[i, 0],item[i, 1], item[i, 2], int.Parse(item[i, 3]), item[i, 4], int.Parse(item[i, 5]));
        }
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
            Store();
        }

        //잘못된 입력을 하였을 때
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

    private void ShowItem(bool isbuy)
    {
        Console.Clear();
        Console.WriteLine("상점\n필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine();
        bool isBuy = isbuy;
        Console.WriteLine($"[보유 골드]\n {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < storeItem.GetLength(0); i++)
        {
            //아이템 목록 출력하는거
            Console.Write("- ");
            if (isBuy == true)
            {
                Console.Write($" {i + 1}. ");
            }
            Console.SetCursorPosition(7, (7 + i));
            Console.Write($"{storeItem[i].Name}");
            Console.SetCursorPosition(24, (7 + i));
            Console.Write($"|{storeItem[i].EffectDescription}");
            Console.SetCursorPosition(37, (7 + i));
            Console.Write($"|{storeItem[i].Description}");
            Console.SetCursorPosition(88, (7 + i));
            if (storeItem[i].isSold == false)
            {
                Console.Write($"|{storeItem[i].Price} G");
            }
            else
            {
                Console.Write($"| 구매완료");
            }
            Console.WriteLine();
        }
    }
    public void Store()
    {
        Console.Clear();
        ShowItem(false);

        Console.WriteLine("1. 아이템 구매");
        Console.WriteLine("0. 나가기");

        int command = InputCommand();

        //나가기
        if (command == 0)
        {
            Console.Clear();
        }
        //아이템구매
        else if (command == 1)
        {
            ShowItem(true);
            int commend = InputCommand();
            if (commend == 0)
            {
                Console.Clear();
            }
            else
            {
                //골드 차감
                playerManager.SpendMoney(commend);
                Console.WriteLine();
                //인벤토리에 아이템
                
            }
        }
    }
}
