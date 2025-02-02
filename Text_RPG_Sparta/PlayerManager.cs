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
        Console.Clear();
        Console.WriteLine($"Lv. {player.Level}");
        Console.WriteLine($"{player.Name} ( {player.Job} )");
        Console.WriteLine($"공격력: {player.Atk}");
        Console.WriteLine($"방어력: {player.Def}");
        Console.WriteLine($"체 력 : {player.Hp}");
        Console.WriteLine($"Gold  : {player.Gold}");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
    }

    //인벤토리 보여주기
    public void ShowInventory()
    {
        Console.Clear();
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