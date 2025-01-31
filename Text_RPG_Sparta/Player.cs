using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Player
{
	private string Name = "";
    private string Chad = "";
    private int LV = 0;
    private	int STR = 0;
	private int HP = 0;
	private int DEF = 0;
	private int Gold = 0;

	//생성자
	public Player()
	{
		this.LV = 1;
		this.Chad = "전사";
		this.HP = 100;
		this.DEF = 5;
		this.STR = 10;
		this.Gold = 1500;
	}

	public void Show_State()
	{
		while (true)
		{
			Console.WriteLine($"Lv. {this.LV}");
			Console.WriteLine($"공격력: {this.STR}");
			Console.WriteLine($"방어력: {this.DEF}");
			Console.WriteLine($"체 력 : {this.LV}");
			Console.WriteLine($"Gold  : {this.LV}");
			Console.WriteLine();
			Console.WriteLine("0. 나가기");
        }
	}

	public void Show_Inventory()
	{
		
	}
}
