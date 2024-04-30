namespace ProjectNoName
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShowStart();

            GameManager gameManager = new GameManager();
            gameManager.StartGame();
        }

        static void ShowStart()
        {
            Console.WriteLine("NoName\n");
            Console.WriteLine("\n1.게임 시작\n2.게임 종료");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            string input = Console.ReadLine();

            //숫자 입력이 맞는지 확인
            if (int.TryParse(input, out int choice))
            {
                //입력된 값에 따라 게임 시작 및 종료
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("게임을 시작합니다!");
                        break;

                    case 2:
                        Console.WriteLine("게임을 종료합니다.");
                        Environment.Exit(0);
                        break;

                        //지정되지 않은 숫자값이 입력되었을 때 재입력
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                        Console.WriteLine("아무 키나 누르십시오...");
                        Console.ReadKey(true);
                        Console.Clear();
                        ShowStart();
                        break;
                }
            }
            else
            {
                // 숫자 이외의 값이 입력될 시 재입력
                Console.WriteLine("잘못된 입력입니다. 숫자를 입력해주세요.");
                Console.WriteLine("아무 키나 누르십시오...");
                Console.ReadKey(true);
                Console.Clear();
                ShowStart();
            }
        }
    }
}