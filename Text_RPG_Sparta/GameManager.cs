using System;
using System.Data.SqlTypes;
using System.Globalization;
using System.Xml.Serialization;

/// <summary>
/// Summary description for Class1
/// </summary>
public class GameManager
{
    private Player player;
    private PlayerManager playerManager;
    private Item[] storeItem;

    //{이름, 효과, 설명, 가격, 증가시키는 }
    private string[,] item = {
        { "수련자 갑옷", "방어력 +5", "수련에 도움을 주는 갑옷입니다.", "1000", "Def", "5"},
        { "무쇠갑옷", "방어력 +9", "무쇠로 만들어져 튼튼한 갑옷입니다.", "1500", "Def", "9"},
        { "스파르타의 갑옷", "방어력 +15", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", "3500", "Def", "15"},
        { "낡은 검", "공격력 +2", "쉽게 볼 수 있는 낡은 검 입니다.", "600", "Atk", "2"},
        { "청동 도끼", "공격력 +5", "어디선가 사용되던 것 같은 도끼입니다.", "1500", "Atk", "5"},
        { "스파르타의 창", "공격력 +7", "스파르타의 전사들이 사용했다는 전설의 창입니다.", "3500", "Atk", "7"},
        { "HP포션", "체력 +20", "스파트라 전사들이 환장하는 국민 음료...", "300", "HP", "20"}
    };

    public GameManager(Player player, PlayerManager playerManager)
	{
        storeItem = new Item[item.GetLength(0)];

        this.player = player;
        this.playerManager = playerManager;

        for (int i = 0; i< storeItem.Length; i++)
        {
            storeItem[i] = new Item(
                item[i, 0],
                item[i, 1],
                item[i, 2],
                int.Parse(item[i, 3]),
                item[i, 4],
                int.Parse(item[i, 5])
            );
        }
    }

    //마을 페이지
    public void Villiage()
	{
        Console.Clear();
        string[] doing = { "상태 보기", "인벤토리", "상점", "던전입장", "휴식하기"};
        int command;

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
        
        for (int i = 0; i < doing.Length; i++)
        {
            Console.WriteLine($"{i+1}. {doing[i]}");
        }

        command = InputCommand();

        //1. 상태보기
        if (command == 1)
        {
            State();
        }

        //2. 인벤토리
        else if (command == 2)
        {
            Inventory();
        }

        //3. 상점
        else if (command == 3)
        {
            Store();
        }
        
        //4. 던전
        else if (command == 4)
        {

        }
        
        //5. 휴식하기
        else if (command == 5)
        {
            Rest();
        }
        
        //잘못된 입력을 하였을 때
        else
        {
            Console.WriteLine("잘못된 입력입니다.");

            Thread.Sleep(1300);
        }
    }

    //1. 상태창 페이지
    public void State()
    {
        playerManager.ShowState();

        while (true)
        {
            int command = InputCommand();
            if (command == 0)
            {
                Console.Clear();
                break;
            }
        }
    }

    //2. 인벤토리 페이지
    public void Inventory()
    {
        Console.Clear();

        playerManager.ShowInventory(false);

        
        int command = InputCommand();
        //나가기
        if (command == 0)
        {
            Console.Clear();
        }

        //인벤토리 장착관리
        else if (command == 1)
        {
            int[] invenItem = playerManager.InventoryCount();

            while (true)
            {
                Console.Clear();
                playerManager.ShowInventory(true);

                //
                int commend = InputCommand();
                int commendIdx = commend - 1;

                //존재하는 아이템인 경우
                if (commend == 0)
                {
                    Console.Clear();
                    break;

                }
                else if (invenItem[commendIdx] == 1)
                {
                    playerManager.EqualManager(commendIdx);
                }

                //잘못된 숫자를 입력한 경우
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(900);
                }
            }
        }

    }

    //3. 상점 페이지
    public void Store()
    {
        Console.Clear();
        //처음 상점 아이템을 보여줌
        ShowItem(false);

        int command = InputCommand();

        //나가기
        if (command == 0)
        {
            Console.Clear();
        }
        //아이템구매
        else if (command == 1)
        {
            while (true)
            {
                ShowItem(true);
                //구매할 아이템을 입력받음
                int commend = InputCommand();
                //나가기
                if (commend == 0)
                {
                    Console.Clear();
                    break;
                }
                //구매할 아이템 선택
                else if(commend >0 && commend <= storeItem.Length)
                {
                    //선택한 아이템정보
                    Item seleteItem = storeItem[commend - 1];
                    BuyItem(seleteItem);
                }
                //잘못된 번호를 입력
                else
                {
                    Console.WriteLine("잘못된 입력입니다");
                    Thread.Sleep(900);
                }
            }
        }
    }

    //상점의 아이템을 보여주는 메서드
    private void ShowItem(bool isbuy)
    {
        Console.Clear();
        Console.WriteLine("[상점]");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine();
        bool isBuy = isbuy;
        Console.WriteLine($"[보유 골드]\n {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < storeItem.Length; i++)
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
        Console.WriteLine();

        if (isBuy == false)
        {
            Console.WriteLine("1. 아이템 구매");
        }
        Console.WriteLine("0. 나가기");
    }

    //아이템 구매 메서드
    public void BuyItem(Item seleteItem)
    {
        //이미 구매한 아이템이라면
        if (seleteItem.isSold == true)
        {
            Console.WriteLine("이미 구매한 아이템입니다.");
        }
        //보유 금액이 부족하다면
        else if (seleteItem.Price > player.Gold)
        {
            Console.WriteLine("Gold 가 부족합니다.");
        }
        //구매가 가능하다면
        else
        {
            //골드 차감
            playerManager.SpendMoney(seleteItem.Price);
            //인벤토리에 추가&잘 처리되었는지 확인
            bool flag  = playerManager.GetItem(seleteItem);
            //아이템 구매완료 표시
            seleteItem.Buy();

            //인벤토리가 가득찼다면
            if (!flag)
            {
                player.Gold += seleteItem.Price;
                seleteItem.isSold = false;
            }
            else
            {
                Console.WriteLine("구매를 완료했습니다.");
            }
        }
        Thread.Sleep(900);
    }

    //5. 휴식
    public void Rest()
    {
        Console.Clear();
        int cost = 500;
        Console.WriteLine("휴식");
        Console.WriteLine($"{cost} G 를 내면 체력을 회복할 수 있습니다. (현재 보유 골드: {player.Gold})");
        Console.WriteLine();
        Console.WriteLine("1. 휴식하기");
        Console.WriteLine("0. 나가기");

        int command = InputCommand();
        if (command == 1)
        {
            //보유 금액이 모자랄 시
            if (player.Gold < cost)
            {
                Console.WriteLine("Gold 가 부족합니다.");
            }
            //보유 금액이 충분할 시
            else
            {
                player.Hp = 100;
                Console.WriteLine("휴식을 완료했습니다");
                Thread.Sleep(900);
            }
        }
        else if (command == 0)
        {
            Console.WriteLine("마을로 돌아갑니다.");
            Thread.Sleep(900);
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
