#nullable enable
using System;
using System.Diagnostics.Metrics;

/// <summary>
/// Summary description for Class1
/// </summary>
public class PlayerManager
{
    private Player player;

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
        Console.WriteLine($"체 력 : {player.Hp}");
        Console.WriteLine($"Gold  : {player.Gold}");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
    }

    //장착관리
    public void EqualManager(int commend)
    {
        Item item = player.Inventory[commend];
        //아이템 장착하기 
        if (item.isEquip == false)
        {
            //아이템 장착 활성화
            item.isEquip = true;
            //무기일 때
            if (item.Effect == "Atk")
            {
                //이미 다른 무기를 장착하였다면
                if (player.EquipItem[0, 0] != null)
                {
                    //해당 무기 장착을 해제함
                    Item oldItem = player.EquipItem[0, 0];

                    player.Atk -= oldItem.EffectValue;
                    player.ItemEffect_atk -= oldItem.EffectValue;
                    oldItem.isEquip = false;
                }
                player.EquipItem[0, 0] = item;
                player.Atk += item.EffectValue;
                player.ItemEffect_atk += item.EffectValue;

                Console.WriteLine($"{item.Name} 을 장착하였습니다.");
            }

            //방어구일 때
            else if (item.Effect == "Def")
            {
                //이미 다른 방어구를 장착하였다면
                if (player.EquipItem[1, 0] != null)
                {
                    //해당 방어구 장착을 해제함
                    Item oldItem = player.EquipItem[1, 0];

                    player.Def -= oldItem.EffectValue;
                    player.ItemEffect_def -= oldItem.EffectValue;
                    oldItem.isEquip = false;
                }
                player.EquipItem[1, 0] = item;
                player.Atk += item.EffectValue;
                player.ItemEffect_atk += item.EffectValue;

                Console.WriteLine($"{item.Name} 을 장착하였습니다.");
            }

            //회복형 아이템(소모형)인 경우
            else if (item.Effect == "HP")
            {
                player.Inventory[commend] = null;
                player.Hp += 20;
                //체력이 최대 체력을 오버했다면 최대체력으로 바꿔줌.
                if (player.Hp > player.MaxHp)
                {
                    player.Hp = player.MaxHp;
                }
                
                Console.WriteLine($"{item.Name} 을 사용했습니다.");
            }
            
        }
        //아이템 장착 해제하기
        else
        {
            item.isEquip = false;
            //무기일때
            if (item.Effect == "Atk")
            {
                for (int i = 0; i < 3; i++)
                {
                    if (player.EquipItem[0, i] == item)
                    {
                        player.EquipItem[0, i] = null;
                        player.Atk -= item.EffectValue;
                        player.ItemEffect_atk -= item.EffectValue;
                    }
                }
            }

            //방어구일 때
            else if (item.Effect == "Def")
            {
                for (int i = 0; i < 3; i++)
                {
                    if (player.EquipItem[1, i] == item)
                    {
                        player.EquipItem[1, i] = null;
                        player.Def -= item.EffectValue;
                        player.ItemEffect_def -= item.EffectValue;
                    }
                }
            }

            Console.WriteLine($"{item.Name} 의 장착을 해제하였습니다.");
        }
    }

    //인벤토리의 아이템의 갯수 배열리턴
    public int [] InventoryCount()
    {
        int[] inven = new int [player.Inventory.Length];

        for (int i=0;i<inven.Length; i++)
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

    //인벤토리 아이템 보여주기
    public void ShowInventory(bool itemEquip)
    {
        Console.Clear();
        //인벤토리 정렬과정
        SortInven();

        Console.Write("[인벤토리]");
        if (itemEquip == true)
        {
            Console.Write(" - 장착 관리");
        }
        Console.WriteLine();
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
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

    //이름을 다시 짓기
    public void ReName()
    {
        Console.Write("플레이어 이름을 입력해주세요: ");
        string name = Console.ReadLine();
        player.Name = name;
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

    //원래는 정렬오류때문에 확인하려고 만든 시각적 배열
    public void ShowInven()
    {
        int[] inven = InventoryCount();
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
        int lastMemoryidx = -1;
        for (int i=0; i< player.Inventory.Length -1; i++)
        {
            //앞뒤로 null일떄
            if ((player.Inventory[i] == null)&&(player.Inventory[i+1] != null))
            {
                player.Inventory[lastMemoryidx + 1] = player.Inventory[i + 1];
                player.Inventory[i + 1] = null;
                lastMemoryidx++;
            }
            //인벤토리에 아이템이 있을때
            else
            {
                lastMemoryidx = i;
            }
        }
    }
}