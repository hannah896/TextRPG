using System;

public class Dungeon
{
    private string name;
    private float recommandDef;
    private float recommandAtk;
    public int reward;

    //생성자
    public Dungeon(string name, int Def, int Atk, int reward)
    {
        this.name = name;
        this.recommandDef = Def;
        this.recommandAtk = Atk;
        this.reward = reward;
    }

    //프로퍼티
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public float RecommandDef
    {
        get { return recommandDef; }
        private set { recommandDef = value; }
    }
    public float RecommandAtk
    {
        get { return recommandAtk; }
        private set { recommandAtk = value; }
    }
    public int Reward
    {
        get { return reward; }
        private set { reward = value; }
    }
}
