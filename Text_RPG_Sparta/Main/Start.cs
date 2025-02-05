using System;

namespace Text_RPG_Sparta
{
    internal class Start
    {
        static void Main(string[] args)
        {
            //캐릭터 생성하는것부터 시작하는거!!!
            Player player;
            while (true)
            {
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                Console.WriteLine("1. 캐릭터를 생성하기");
                Console.WriteLine("0. 종료하기");
                Console.Write("\n>>>");
                int command = int.Parse(Console.ReadLine());
                if (command == 1)
                {
                    player = MakeCharacter();
                    break;
                }
                else if (command == 0)
                {
                    Environment.Exit(0);
                }
            }

            PlayerManager playerManager = new PlayerManager(player);
            GameManager gameManager = new GameManager(player, playerManager);

            while (true)
            {
                //마을에 시작할때
                gameManager.Villiage();
            }
        }

        //캐릭터 생성
        static Player MakeCharacter()
        {
            Console.Clear();
            String name = MakeName();
            String job = JobChoice();
            Player player = new Player(name, job);
            return player;
        }

        //이름 생성
        static string MakeName()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                Console.Write("원하시는 이름을 설정해주세요: ");
                string name = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine($"입력하신 이름은 {name} 입니다.");
                Console.WriteLine();
                Console.WriteLine("1. 저장");
                Console.WriteLine("2. 취소");
                Console.WriteLine();
                Console.Write("원하시는 행동을 입력해주세요.\n>>>");
                try
                {
                    int command = int.Parse(Console.ReadLine());
                    if (command == 1)
                    {
                        return name;
                    }
                    else if (command == 2)
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Thread.Sleep(500);
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500);
                }

            }
        }
        //직업 선택
        static string JobChoice()
        {
            while(true)
            {
                String[] job = { "전사", "도적", "마법사" };
                Console.Clear();
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                Console.WriteLine("직업을 선택하세요.");
                Console.WriteLine();
                for (int i = 0; i < job.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {job[i]}");
                }
                Console.WriteLine();
                Console.Write("원하시는 직업의 번호를 입력해주세요.\n>>>");

                //입력이 숫자로 들어왔을때
                try
                {
                    int command = int.Parse(Console.ReadLine()) - 1;

                    if ((command < 0) || (command >= job.Length))
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Thread.Sleep(500);
                    }
                    else
                    {
                        Console.WriteLine($"\n선택하신 직업은 {job[command]} 입니다.");
                        Console.WriteLine();
                        Console.WriteLine("1. 네");
                        Console.WriteLine("2. 아니요");
                        Console.WriteLine();
                        Console.Write("원하시는 행동을 입력해주세요.\n>>>");

                        int commend = int.Parse(Console.ReadLine());
                        if (commend == 1)
                        {
                            return job[command];
                        }
                        else if (commend == 2)
                        {
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                            Thread.Sleep(500);
                        }
                    }
                }

                //숫자가 아니라 문자로 입력을 잘못한 경우
                catch (FormatException)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500);
                }
            }
        }
    }
}
