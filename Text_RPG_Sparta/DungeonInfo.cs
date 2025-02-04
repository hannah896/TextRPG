using System;
using System.Globalization;

public struct DungeonInfo
{
    public string name;
    public int recommandDef;
    public int recommandAtk;
    public int reward;

    //생성자
    public DungeonInfo(string name, int Def, int Atk, int reward)
    {
        this.name = name;
        this.recommandDef = Def;
        this.recommandAtk = Atk;
        this.reward = reward;
    }
}
