using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Player
{
	

	private string name = "";
    private string chad = "";

    private int level = 0;
    private	int atk = 0;
	private int hp = 0;
	private int def = 0;
	private int gold = 0;

    private string [] inventory = { };
	private string[] equipItem = { };


    //생성자
    public Player()
    {
        this.name = "James";
        this.level = 1;
        this.chad = "전사";
        this.hp = 100;
        this.def = 5;
        this.atk = 10;
        this.gold = 1500;
    }
    public Player(string name)
	{
		this.name = name;
		this.level = 1;
		this.chad = "전사";
		this.hp = 100;
		this.def = 5;
		this.atk = 10;
		this.gold = 1500;
	}

	//프로퍼티
	public string Name
	{
		get { return name; }
		private set { name = value; }
	}
	
	public string Chad
	{
		get { return chad; }
		private set { chad = value; } 
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
		get { return hp; }
		private set { hp = value; }
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

    public string [] Inventory
	{
		get { return  inventory; }
		private set { inventory = value; }
	}

	public string[] EquipItem
	{
		get { return equipItem; }
		private set { equipItem = value; }
	}
}