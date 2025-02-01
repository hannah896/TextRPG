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
        Console.WriteLine($"Lv. {player.Level}");
        Console.WriteLine($"공격력: {player.Atk}");
        Console.WriteLine($"방어력: {player.Def}");
        Console.WriteLine($"체 력 : {player.Hp}");
        Console.WriteLine($"Gold  : {player.Gold}");
        Console.WriteLine();
    }

    public void ShowInventory()
    {
        foreach(string item in player.Inventory)
        {
            Console.Write(item);

        }
    }

}
