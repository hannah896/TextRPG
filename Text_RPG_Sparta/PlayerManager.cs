using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class PlayerManager 
{
    private Player player;

    public PlayerManager(Player player)
    {
        this.player = player;
    }
    public void ShowState()
    {
        Console.Clear();
        Console.WriteLine($"Lv. {player.Level}");
        Console.WriteLine($"공격력: {player.Atk}");
        Console.WriteLine($"방어력: {player.Def}");
        Console.WriteLine($"체 력 : {player.Hp}");
        Console.WriteLine($"Gold  : {player.Gold}");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");

    }

    public void ShowInventory()
    {
        Console.Clear();
        foreach (string item in player.Inventory)
        {
            Console.Write(item);

        }
    }

    //이름을 다시 짓기
    public void ReName()
    {
        Console.Write("플레이어 이름을 입력해주세요: ");
        string name = Console.ReadLine();
        player.Name = name;
    }
}
