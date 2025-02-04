#nullable enable
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
	private int maxHp = 0;
	private int def = 0;
	private int gold = 0;
    private int itemEffect_atk = 0;
    private int itemEffect_def = 0;

	private Item[]? inventory;
	private Item[,]? equipItem;

	//생성자
	
	//개발자용 테스터 생성자
	public Player()
    {
        this.name = "James";
        this.level = 1;
        this.job = "전사";
        this.hp = 100;
        this.maxHp = 100;
        this.def = 5;
        this.atk = 10;
		this.gold = 50000;
        inventory = new Item[10];
		equipItem = new Item[2, 1];
    }
    public Player(string name, string job)
	{
		this.name = name;
		this.job = job;
        this.level = 1;
        this.gold = 1500;
        if (job == "전사")
		{
            this.hp = 100;
            this.def = 10;
            this.atk = 10;
        }
		else if (job == "도적")
		{
            this.hp = 80;
            this.def = 5;
            this.atk = 20;
        }
		else if (job == "마법사")
		{
            this.hp = 90;
            this.def = 10;
            this.atk = 25;
        }
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
        protected set { job = value; } 
	}
	
	public int Level
	{
		get { return level; }
        protected set { level = value; }
	}
	
	public int Atk
	{
		get { return atk; }
        set { atk = value; }
	}

	public int Gold
	{
		get { return gold; }
        set { gold = value; }
	}
	
	public int Def
	{
		get { return def; }
        set { def = value; }
	}
	
	public int Hp
	{
		get { return hp; }
        set { hp = value; }
	}

    public int MaxHp
    {
        get { return maxHp; }
        private set { maxHp = value; }
    }

    public int ItemEffect_atk
	{
		get { return itemEffect_atk; }
		set { itemEffect_atk = value; }
	}

    public int ItemEffect_def
    {
        get { return itemEffect_def; }
        set { itemEffect_def = value; }
    }

    public Item [] Inventory
	{
		get { return  inventory; }
        protected set { inventory = value; }
	}

	public Item[,] EquipItem
	{
		get { return equipItem; }
        set { equipItem = value; }
	}
}