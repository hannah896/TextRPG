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
	private int exp = 0;
    private int maxExp = 1;

    private float hp = 0;
	private float maxHp = 0;

	private int gold = 0;

    private float itemEffect_atk = 0f;
    private float itemEffect_def = 0f;
    private float atk = 0f;
    private float def = 0f;

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
        this.def = 5f;
        this.atk = 10f;
		this.gold = 50000;
        inventory = new Item[10];
		equipItem = new Item[2, 1];
		this.exp = 0;
		this.maxExp = 1;
    }
    public Player(string name, string job)
	{
		this.name = name;
		this.job = job;
        this.level = 1;
        this.gold = 1500;
        this.exp = 0;
		this.maxExp = 1;
        if (job == "전사")
		{
            this.hp = 100;
            this.def = 10f;
            this.atk = 10f;
        }
		else if (job == "도적")
		{
            this.hp = 80;
            this.def = 5f;
            this.atk = 20f;
        }
		else if (job == "마법사")
		{
            this.hp = 90;
            this.def = 10f;
            this.atk = 25f;
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
        set { level = value; }
	}
    public int Exp
    {
        get { return exp; }
        set { exp = value; }
    }

    public int MaxExp
    {
        get { return maxExp; }
        set { maxExp = value; }
    }

    public float Atk
	{
		get { return atk; }
        set { atk = value; }
	}

    public float Def
    {
        get { return def; }
        set { def = value; }
    }

    public int Gold
	{
		get { return gold; }
        set { gold = value; }
	}
	
	public float Hp
	{
		get { return hp; }
        set { hp = value; }
	}

    public float MaxHp
    {
        get { return maxHp; }
        private set { maxHp = value; }
    }

    public float ItemEffect_atk
	{
		get { return itemEffect_atk; }
		set { itemEffect_atk = value; }
	}

    public float ItemEffect_def
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