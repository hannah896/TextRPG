using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Player
{
	private string name = "";
    private string job = "";

    private int level = 0;
    private	int atk = 0;
	private int hp = 0;
	private int def = 0;
	private int gold = 0;
	private Item[] inventory;
	private Item[] equipItem;

	//생성자
	public Player()
    {
        this.name = "James";
        this.level = 1;
        this.job = "전사";
        this.hp = 100;
        this.def = 5;
        this.atk = 10;
		this.gold = 1500;
		int itemCount = 5;
        inventory = new Item[6];
		equipItem = new Item[6];
    }
    public Player(string name)
	{
		this.name = name;
		this.level = 1;
		this.job = "전사";
		this.hp = 100;
		this.def = 5;
		this.atk = 10;
		this.gold = 1500;
	}

	//프로퍼티
	public string Name
	{
		get { return name; }
		set { name = value; }
	}
	
	public string Job
	{
		get { return job; }
		private set { job = value; } 
	}
	
	public int Level
	{
		get { return level; }
		private set { level = value; }
	}
	
	public int Atk
	{
		get { return atk; }
		private set { atk = value; }
	}

	public int Gold
	{
		get { return gold; }
		set { gold = value; }
	}
	
	public int Def
	{
		get { return def; }
		private set { def = value; }
	}
	
	public int Hp
	{
		get { return hp; }
		private set { hp = value; }
	}

    public Item [] Inventory
	{
		get { return  inventory; }
		private set { inventory = value; }
	}

	public Item[] EquipItem
	{
		get { return equipItem; }
		private set { equipItem = value; }
	}
}