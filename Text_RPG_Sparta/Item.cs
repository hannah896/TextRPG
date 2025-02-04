using System;
using System.Runtime.CompilerServices;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Item
{
    private string name;
    private string effectDescription;
    private string description;
    private string effect;

    private float effectValue;
    private int price;

    public bool isSold = false;
    public bool isEquip = false;

    //생성자
    public Item(string name, string effectDescription, string description, int price, string effect, int effectValue)
    {
        this.name = name;
        this.effectDescription = effectDescription;
        this.description = description;
        this.price = price;
        this.effect = effect;
        this.effectValue = effectValue;
    }

    //프로퍼티
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public string EffectDescription
    {
        get { return effectDescription; }
        private set { effectDescription = value; }
    }
    public string Description
    {
        get { return description; }
        private set { description = value; }
    }
    public string Effect
    {
        get { return effect; }
        private set { effect = value; }
    }
    public int Price
    {
        get { return price; }
        set { price = value; }
    }
    public float EffectValue
    {
        get { return effectValue; }
        private set { effectValue = value; }
    }

    //구매했을때 판매완료를 띄우기위한 트리거
    public void Buy()
    {
        if (this.Effect == "HP") this.isSold = false;
        else this.isSold = true;
    }
}
