﻿#nullable enable
using System;
using System.Data.SqlTypes;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Serialization;

/// <summary>
/// Summary description for Class1
/// </summary>
public enum DungeonType
{
    Easy,
    Normal,
    Hard
}

public class GameManager
{
    //관리용 객체
    private Player player;
    private PlayerManager playerManager;
    private Item[] storeItem;
    private DungeonManager DManager;

    //{이름, 효과, 설명, 가격, 증가시키는 }
    private string[,] item = {
        { "수련자 갑옷", "방어력 +5", "수련에 도움을 주는 갑옷입니다.", "1000", "Def", "5"},
        { "무쇠갑옷", "방어력 +9", "무쇠로 만들어져 튼튼한 갑옷입니다.", "1500", "Def", "9"},
        { "스파르타의 갑옷", "방어력 +15", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", "3500", "Def", "15"},
        { "낡은 검", "공격력 +2", "쉽게 볼 수 있는 낡은 검 입니다.", "600", "Atk", "2"},
        { "청동 도끼", "공격력 +5", "어디선가 사용되던 것 같은 도끼입니다.", "1500", "Atk", "5"},
        { "스파르타의 창", "공격력 +7", "스파르타의 전사들이 사용했다는 전설의 창입니다.", "3500", "Atk", "7"},
        { "HP 포션", "체력 +20", "스파트라 전사들이 환장한다는 스파트라 국민 음료입니다...", "300", "HP", "20"}
    };

    public GameManager(Player player, PlayerManager playerManager)
    {
        this.DManager = new DungeonManager(player);

        this.player = player;
        this.playerManager = playerManager;

        storeItem = new Item[item.GetLength(0)];
        //아이템 목록 상점아이템으로 이동
        for (int i = 0; i < storeItem.Length; i++)
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
        string[] doing = { "상태 보기", "인벤토리", "상점", "던전입장", "휴식하기" };
        int command;

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

        for (int i = 0; i < doing.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {doing[i]}");
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
            Dungeon();
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
            Thread.Sleep(900);
        }
    }

    //1. 상태창 페이지
    public void State()
    {
        Console.Clear();
        playerManager.ShowState();

        while (true)
        {
            int command = InputCommand();
            if (command == 0)
            {
                Console.Clear();
                break;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadLine();
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
        while(true)
        {
            Console.Clear();
            //상점 아이템을 보여줌
            ShowItem(false);
            
            //행동 입력을 받음
            int command = InputCommand();

            //나가기
            if (command == 0)
            {
                Console.Clear();
                break;
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
                    else if (commend > 0 && commend <= storeItem.Length)
                    {
                        //선택한 아이템정보
                        Item seleteItem = storeItem[commend - 1];
                        BuyItem(seleteItem);
                    }
                    //테스트용 아이템 전체구매
                    else if (commend == 924)
                    {
                        for (int i = 0; i< storeItem.Length; i++)
                        {
                            Item seleteItem = storeItem[i];
                            BuyItem(seleteItem);
                        }
                        BuyItem(storeItem[6]);
                        BuyItem(storeItem[6]);
                        BuyItem(storeItem[6]);
                    }
                    //잘못된 번호를 입력
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다");
                        Thread.Sleep(900);
                    }
                }
            }
            //아이템판매
            else if (command == 2)
            {
                while (true)
                {
                    ShowInventory(true);

                    int commend = InputCommand();
                    int idx = commend - 1;

                    //나가기
                    if (commend == 0)
                    {
                        break;
                    }

                    //팔려하는 아이템의 번호가 제대로 입력되었고 비어있는 인벤토리 칸이 아니라면
                    else if ((-1 < idx)&&(idx< player.Inventory.Length)&&(player.Inventory[idx] != null))
                    {
                        //아이템을 판매함.
                        Item selItem = player.Inventory[idx];
                        SellItem(selItem, idx);
                    }
                    
                    //잘못된 입력 처리
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Thread.Sleep(500);
                    }
                }
            }
        }

    }
    
    //4. 던전입장
    public void Dungeon()
    {
        bool isSuccess;
        while (true)
        {
            Console.Clear();
            DManager.ShowDungeon();

            int command = InputCommand();
            int idx = command - 1;

            //나가기
            if (command == 0)
            {
                break;
            }

            //던전에 입장
            else if ((command > 0) && (command < 4))
            {
                isSuccess = DManager.EnterDungeon(idx);
                //던전 클리어 실패시
                if (isSuccess == false)
                {
                    while (true)
                    {
                        DManager.Fail();
                        int commend = InputCommand();
                        //나가기
                        if (commend == 0) break;
                    }

                }
                //던전 클리어 성공시
                else
                {
                    while (true)
                    {
                        DManager.Success();
                        int commend = InputCommand();
                        //나가기
                        if (commend == 0) break;
                    }
                }
            }
            //잘못된 입력 처리
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(300);
            }
        }
    }

    //5. 휴식
    public void Rest()
    {
        Console.Clear();
        int cost = 500;
        Console.WriteLine("휴식");
        Console.WriteLine($"{cost} G 를 내면 체력을 회복할 수 있습니다. (현재 보유 골드: {player.Gold} G)");
        Console.WriteLine();
        Console.WriteLine("1. 휴식하기");
        Console.WriteLine("0. 나가기");

        int command = InputCommand();
        //1. 휴식하기
        if (command == 1)
        {
            //보유 금액이 모자랄 시
            if (player.Gold < cost)
            {
                Console.WriteLine("Gold 가 부족합니다.");
                Thread.Sleep(900);
            }
            //보유 금액이 충분할 시
            else
            {
                player.Hp = 100;
                playerManager.SpendMoney(cost);
                Console.WriteLine("휴식을 완료했습니다");
                Thread.Sleep(900);
            }
        }
        //0. 나가기
        else if (command == 0)
        {
            Console.WriteLine("마을로 돌아갑니다.");
            Thread.Sleep(900);
        }
    }

    //상점에 내 인벤토리를 보여주는 메서드
    private void ShowInventory(bool isSell)
    {
        Console.Clear();
        Console.Write("[상점]");
        Console.WriteLine(" - 아이템 판매");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine();
        
        Console.WriteLine($"[보유 골드]\n {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < player.Inventory.Length; i++)
        {
            Item item = player.Inventory[i];
            //비어있는 아이템칸은 스킵
            if (item == null)
            {
                continue;
            }
            //아이템 목록 출력
            else
            {
                Console.Write("- ");
                //아이템 판매를 눌렀을때만 활성화
                if (isSell == true)
                {
                    Console.Write($"{i + 1}. ");
                }
                //장착된 장비 표시
                if (item.isEquip)
                {
                    Console.Write("[E] ");
                }

                Console.SetCursorPosition(10, (7 + i));
                Console.Write($"{player.Inventory[i].Name}");
                Console.SetCursorPosition(25, (7 + i));
                Console.Write($"|{player.Inventory[i].EffectDescription}");
                Console.SetCursorPosition(37, (7 + i));
                Console.Write($"|{player.Inventory[i].Description}");
                Console.SetCursorPosition(95, (7 + i));
                Console.Write($"|{player.Inventory[i].Price*0.85f} G");
                Console.WriteLine();
            }
        }
        Console.WriteLine("0. 나가기");
    }
    
    //상점의 아이템을 보여주는 메서드
    private void ShowItem(bool isbuy)
    {
        Console.Clear();
        Console.Write("[상점]");
        if (isbuy == true)
        {
            Console.Write(" - 아이템구매");
        }
        Console.WriteLine();
        Console.WriteLine();
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
            Console.SetCursorPosition(7, (8 + i));
            Console.Write($"{storeItem[i].Name}");
            Console.SetCursorPosition(24, (8 + i));
            Console.Write($"|{storeItem[i].EffectDescription}");
            Console.SetCursorPosition(37, (8 + i));
            Console.Write($"|{storeItem[i].Description}");
            Console.SetCursorPosition(95, (8 + i));
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
            Console.WriteLine("2. 아이템 판매");
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
            if (flag == false)
            {
                player.Gold += seleteItem.Price;
                seleteItem.isSold = false;
                Console.WriteLine("인벤토리가 가득찼습니다! 인벤토리의 공간이 충분할 때 다시 시도해주세요.");
            }
            else
            {
                Console.WriteLine("구매를 완료했습니다.");
            }
        }
        Thread.Sleep(900);
    }

    //아이템 판매 메서드
    public void SellItem(Item seleteItem, int idx)
    {
        //장착중인 아이템이라면 판매불가
        if (seleteItem.isEquip == true)
        {
            Console.WriteLine("장착중인 아이템은 판매할 수 없습니다.");
            Thread.Sleep(400);
        }
        else
        {
            //인벤토리의 해당아이템 null
            player.Inventory[idx] = null;
            //아이템의 판매여부
            seleteItem.isSold = false;
            //아이템의 판매가격 -> 플레이어의 골드로 보내기
            int money = (int)(seleteItem.Price * 0.85)*(-1);
            playerManager.SpendMoney(money);
            //아이템정렬
            playerManager.SortInven();
            Console.WriteLine("아이템을 판매했습니다.");
            Thread.Sleep(500);
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
            try
            {
                return int.Parse(command);
            }
            catch (FormatException)
            {
                return -1;
            }
        }
    }
}
