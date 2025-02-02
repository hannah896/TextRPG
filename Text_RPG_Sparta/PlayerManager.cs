using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class PlayerManager
{
    private Player player;
    
    //생성자
    public PlayerManager(Player player)
    {
        this.player = player;
    }
    
    //상태 보여주기
    public void ShowState()
    {
        Console.WriteLine($"Lv. {player.Level}");
        Console.WriteLine($"{player.Name} ( {player.Job} )");
        Console.WriteLine($"공격력: {player.Atk}");
        Console.WriteLine($"방어력: {player.Def}");
        Console.WriteLine($"체 력 : {player.Hp}");
        Console.WriteLine($"Gold  : {player.Gold}");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
    }

    //장착관리
    public void EqualManager()
    {
        ShowInventory();
        Console.WriteLine();
    }

    //인벤토리 아이템 보여주기
    public void ShowInventory()
    {
        Console.Clear();
        Console.WriteLine("[인벤토리]");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < player.Inventory.Length; i++)
        {
            if (player.Inventory[i] == null)
            {
                continue;
            }
            else
            {
                //아이템 목록 출력하는거
                Console.Write($"- {i + 1}. ");
                if (player.Inventory[i].isEquip == true)
                {
                    Console.Write($"[E]");
                }

                Console.SetCursorPosition(10, (4 + i));
                Console.Write($"{player.Inventory[i].Name}");
                Console.SetCursorPosition(24, (4 + i));
                Console.Write($"|{player.Inventory[i].EffectDescription}");
                Console.SetCursorPosition(37, (4 + i));
                Console.Write($"|{player.Inventory[i].Description}");
                Console.WriteLine();
            }

        }
        Console.WriteLine();
    }


    //이름을 다시 짓기
    public void ReName()
    {
        Console.Write("플레이어 이름을 입력해주세요: ");
        string name = Console.ReadLine();
        player.Name = name;
    }

    //사용골드 빠지기
    public void SpendMoney(int money)
    {
        player.Gold = player.Gold - money;
    }

    //아이템을 인벤토리에 지급
    public bool GetItem(Item item)
    {
        for (int i = 0; i<player.Inventory.Length; i++)
        {
            if (player.Inventory[i] == null)
            {
                player.Inventory[i] = item;
                return true;
            }
        }
        Console.WriteLine("인벤토리가 가득찼습니다.");
        return false;
    }
}