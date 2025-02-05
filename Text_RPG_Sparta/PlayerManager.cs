#nullable enable
using System;

public class PlayerManager
{
    private Player player;
    private const int AtkSlot = 0;
    private const int DefSlot = 1;

    //생성자
    public PlayerManager(Player player)
    {
        this.player = player;
    }

    //상태 보여주기
    public void ShowState()
    {
        Console.Clear();
        Console.WriteLine("[상태 보기]");
        Console.WriteLine("캐릭터의 정보가 표시됩니다.");
        Console.WriteLine();
        Console.WriteLine($"Lv. {player.Level}");
        Console.WriteLine($"({Math.Round((double)player.Exp / (double)player.MaxExp) * 100, 2}%)");
        Console.WriteLine();
        Console.WriteLine($"{player.Name} ( {player.Job} )");

        Console.Write($"공격력: {player.Atk}  ");
        if (player.ItemEffect_atk > 0)
        {
            Console.Write($"(+{player.ItemEffect_atk})");
        }
        Console.WriteLine();
        Console.Write($"방어력: {player.Def}  ");
        if (player.ItemEffect_def > 0)
        {
            Console.Write($"(+{player.ItemEffect_def})");
        }
        Console.WriteLine();
        Console.WriteLine($"체 력 : {player.Hp}/{player.MaxHp}");
        Console.WriteLine($"Gold  : {player.Gold}");
        Console.WriteLine();
        Console.WriteLine("1. 이름 다시 짓기");
        Console.WriteLine("0. 나가기");
    }

    //레벨업
    public void LevelUp()
    {
        if (player.Exp >= player.MaxExp)
        {
            //레벨업후 초기화
            player.Level++;
            player.Exp = 0;
            player.MaxExp++;

            //레벨업의 효과
            player.Atk += 0.5f;
            player.Def += 1f;
        }
        Console.WriteLine("!!! Level UP !!!");
        Console.WriteLine();
        Console.WriteLine($"{player.Job} {player.Name}의 레벨이 올랐습니다!");
        Console.WriteLine($"{player.Level - 1} -> {player.Level}");
    }

    //장착관리
    public void EquipManager(int index)
    {
        Item item = player.Inventory[index];

        //소모형 아이템인경우
        if (item.Effect == "HP")
        {
            UsePotion(index);
            Console.WriteLine($"{item.Name} 을 사용했습니다.");
            Thread.Sleep(400);
        }

        //아이템 장착하기 
        else if (item.isEquip == false)
        {
            if (item.Effect == "Def")
            {
                EquipDefItem(item);
            }
            else
            {
                EquipAtkItem(item);
            }
            Console.WriteLine($"{item.Name}을 장착했습니다.");
            Thread.Sleep(400);
        }

        //아이템 장착해제
        else
        {
            if (item.Effect == "Def")
            {
                UnequipDefItem(item);
            }
            else
            {
                UnequipAtkItem(item);
            }
            Console.WriteLine($"{item.Name} 의 장착을 해제하였습니다.");
            Thread.Sleep(400);
        }
    }

    //소모형 아이템(HP 포션)을 사용
    public void UsePotion(int index)
    {
        Item item = player.Inventory[index];
        player.Inventory[index] = null;
        player.Hp += 20f;

        //체력이 최대 체력을 오버했다면 최대체력으로 바꿔줌.
        if (player.Hp > player.MaxHp)
        {
            player.Hp = player.MaxHp;
        }
    }

    //무기 아이템을 장착
    public void EquipAtkItem(Item item)
    {
        //이미 다른 무기를 장착하였다면
        if (player.EquipItem[AtkSlot] != null)
        {
            //해당 무기 장착을 해제함
            UnequipAtkItem(player.EquipItem[AtkSlot]);
        }
        player.EquipItem[AtkSlot] = item;
        player.Atk += item.EffectValue;
        player.ItemEffect_atk += item.EffectValue;
        item.isEquip = true;
    }

    //무기 아이템의 장착을 해제
    public void UnequipAtkItem(Item oldItem)
    {
        player.Atk -= oldItem.EffectValue;
        player.ItemEffect_atk -= oldItem.EffectValue;
        oldItem.isEquip = false;
        player.EquipItem[AtkSlot] = null;
    }

    //방어구 아이템을 장착
    public void EquipDefItem(Item item)
    {
        //이미 다른 방어구를 장착하였다면
        if (player.EquipItem[DefSlot] != null)
        {
            //해당 방어구 장착을 해제함
            UnequipDefItem(player.EquipItem[DefSlot]);
        }
        player.EquipItem[DefSlot] = item;
        player.Def += item.EffectValue;
        player.ItemEffect_def += item.EffectValue;
        item.isEquip = true;
        Thread.Sleep(400);
    }

    //방어구 아이템을 해제
    public void UnequipDefItem(Item oldItem)
    {
        player.Def -= oldItem.EffectValue;
        player.ItemEffect_def -= oldItem.EffectValue;
        oldItem.isEquip = false;
        player.EquipItem[DefSlot] = null;
    }

    //인벤토리 아이템 보여주기
    public void ShowInventory(bool itemEquip)
    {
        Console.Clear();
        //인벤토리 정렬과정
        SortInven();

        Console.Write("[인벤토리]");
        
        //장착관리에 들어갈 때 사용
        if (itemEquip == true)
        {
            Console.Write(" - 장착 관리");
        }
        Console.WriteLine();
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        
        //인벤토리 시각화
        ShowInven();
        Console.WriteLine();
        
        for (int i = 0; i < player.Inventory.Length; i++)
        {
            //비어있는 아이템칸은 스킵
            if (player.Inventory[i] == null)
            {
                continue;
            }
            //아이템 목록 출력
            else
            {
                Console.Write("- ");
                //장착관리에 들어갈때만 뜨게함
                if (itemEquip == true)
                {
                    Console.Write($"{i + 1}. ");
                }
                //장착한 장비만 [E]가 뜨게 함
                if (player.Inventory[i].isEquip == true)
                {
                    Console.Write($"[E]");
                }

                Console.SetCursorPosition(10, (6 + i));
                Console.Write($"{player.Inventory[i].Name}");
                Console.SetCursorPosition(25, (6 + i));
                Console.Write($"|{player.Inventory[i].EffectDescription}");
                Console.SetCursorPosition(37, (6 + i));
                Console.Write($"|{player.Inventory[i].Description}");
                Console.WriteLine();
            }
        }
        Console.WriteLine();

        if (itemEquip == false)
        {
            Console.WriteLine("1. 장착관리");
        }

        Console.WriteLine("0. 나가기");
    }

    //사용골드 빠지기
    public void SpendMoney(int money)
    {
        player.Gold = player.Gold - money;
    }

    //아이템을 인벤토리에 지급
    public bool GetItem(Item item)
    {
        for (int i = 0; i<player.Inventory.Length; i++)
        {
            if (player.Inventory[i] == null)
            {
                player.Inventory[i] = item;
                return true;
            }
        }
        return false;
    }

    //인벤토리의 아이템의 갯수 배열리턴
    public int[] GetInventoryArray()
    {
        int[] inven = new int[player.Inventory.Length];

        for (int i = 0; i < inven.Length; i++)
        {
            if (player.Inventory[i] != null)
            {
                inven[i] = 1;
            }
            else
            {
                inven[i] = 0;
            }
        }
        return inven;
    }

    //원래는 정렬오류때문에 확인하려고 만든 시각적 배열
    public void ShowInven()
    {
        int[] inven = GetInventoryArray();
        for (int i=0; i< inven.Length; i++) 
        {
            if (inven[i] == 0)
            {
                Console.Write("□");
            }
            else
            {
                Console.Write("■");
            }
        }
        Console.WriteLine() ;
    }

    //인벤토리 정렬
    public void SortInven()
    {
        int lastValidIndex = -1;
        for (int i=0; i< player.Inventory.Length -1; i++)
        {
            //앞뒤로 null일떄
            if ((player.Inventory[i] == null)&&(player.Inventory[i+1] != null))
            {
                player.Inventory[lastValidIndex + 1] = player.Inventory[i + 1];
                player.Inventory[i + 1] = null;
                lastValidIndex++;
            }
            //인벤토리에 아이템이 있을때
            else
            {
                lastValidIndex = i;
            }
        }
    }

    //이름을 다시 짓기
    public void ReName()
    {
        Console.Write("플레이어 이름을 입력해주세요: ");
        string name = Console.ReadLine();
        player.Name = name;
    }
}