using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class GameManager 
{

    public void ShowState()
    {
        Console.WriteLine($"Lv. {Lv}");
        Console.WriteLine($"공격력: {Atk}");
        Console.WriteLine($"방어력: {Def}");
        Console.WriteLine($"체 력 : {Hp}");
        Console.WriteLine($"Gold  : {Gold}");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
    }

    public void Show_Inventory()
    {
        foreach(item in inventory)
        {
            Console.Write()
        }
    }
}
