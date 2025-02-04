using System;
using System.Collections.Generic;

public class DungeonManager
{
    //key값= DungeonType, value = Dungeon을 갖는 딕셔너리 생성
    private Dictionary<DungeonType, Dungeon> dungeonInfo = new Dictionary<DungeonType, Dungeon>();
    
    private DungeonType easy = DungeonType.Easy;
    private DungeonType normal = DungeonType.Normal;
    private DungeonType hard = DungeonType.Hard;
    private DungeonType type;
    private Random rand = new Random();

    private Player player;

    private float diffDef;
    private float lostHP;
    
    private int rewards;



    //생성자
    public DungeonManager(Player player)
    {
        dungeonInfo.Add(DungeonType.Easy, new Dungeon("쉬운 던전", 5, 10, 1000));
        dungeonInfo.Add(DungeonType.Normal, new Dungeon("일반 던전", 11, 13, 1700));
        dungeonInfo.Add(DungeonType.Hard, new Dungeon("어려운 던전", 17, 15, 2500));

        this.player = player;
        easy = DungeonType.Easy;
        normal = DungeonType.Normal;
        hard = DungeonType.Hard;
    }

    //던전 정보 보여주기
    public void ShowDungeon()
    {
        Console.Clear();
        int i = 0;
        Console.WriteLine("[던전입장]");
        Console.WriteLine();
        Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
        Console.WriteLine();

        //던전 종류 출력
        foreach (var dict in dungeonInfo)
        {
            i++;
            Console.Write($"{i}. ");
            Console.SetCursorPosition(4, (i + 3));
            Console.Write($"{dict.Value.Name}");
            Console.SetCursorPosition(17, (i + 3));
            Console.Write($"| 방어력 {dict.Value.RecommandDef}");
            Console.SetCursorPosition(29, (i + 3));
            Console.Write($"이상 권장");
            Console.WriteLine();
        }
        Console.WriteLine("0. 나가기");
    }

    //던전 입장하기
    public bool EnterDungeon(int num)
    {
        if (num == 0)
        {
            type = easy;
        }
        else if (num == 1)
        {
            type = normal;
        }
        else
        {
            type = hard;
        }

        diffDef = player.Def- dungeonInfo[type].RecommandDef;

        //방어력 비교
        if (diffDef < 0)
        {
            //실패
            if (rand.Next(0, 100) < 40)
            {
                lostHP = player.Hp / 2;
                player.Hp /= 2;
                return false;
            }
        }

        //던전 클리어 시
        lostHP = rand.Next((int)(25.0f - diffDef), (int)(35.0f - diffDef));
        player.Hp -= lostHP;

        //공격력에 따른 보너스
        float bonus = 0.01f * (100 + rand.Next((int)player.Atk, (int)player.Atk * 2));
        
        //보상 총 계산
        float money = dungeonInfo[type].reward * bonus;
        rewards = (int)money;

        //입금
        player.Gold += rewards;

        return true;
    }

    public void Success()
    {
        Console.Clear();
        Console.WriteLine("던전 클리어");
        Console.WriteLine();
        Console.WriteLine($"{type}을 클리어하였습니다");
        Console.WriteLine();
        Console.WriteLine("탐험결과");
        Console.WriteLine($"체력: {player.Hp + lostHP} -> {player.Hp}");
        Console.WriteLine($"Gold: {player.Gold - rewards} -> {player.Gold}");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
    }

    public void Fail()
    {
        Console.Clear();
        Console.WriteLine("던전 실패");
        Console.WriteLine();
        Console.WriteLine($"이런... {type}을 클리어하는데 실패하였습니다");
        Console.WriteLine();
        Console.WriteLine("탐험결과");
        Console.WriteLine($"체력: {player.Hp + lostHP} -> {player.Hp}");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
    }
}
