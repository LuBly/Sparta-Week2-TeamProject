using System.Numerics;

namespace ProjectNoName
{
    public enum MenuType
    {
        Start,
        Status,
        Inventory,
        Store,
        Dungeon,
        Rest,
    }
    internal class GameManager
    {
        // 초기 화면 설정(맨 처음 실행했을 때)
        MenuType curMenu = MenuType.Start;
        Player player = DataManager.Instance().Player;
        // [추가 사항]
        //UIManager - 각 메뉴들을 호출, 꾸미기 기능
        //DataManager - 게임의 모든 정보들을 저장하고 있을 Manager (static)
        public void StartGame()
        {
            // 게임화면
            while (true)
            {
                // 각 씬별 동작
                switch (curMenu)
                {
                    case MenuType.Start:
                        LoadStartMenu();
                        break;

                    case MenuType.Status:
                        //LoadStatusMenu();
                        break;

                    case MenuType.Inventory:
                        //LoadInventoryMenu();
                        break;

                    case MenuType.Store:
                        //LoadStoreMenu();
                        break;

                    case MenuType.Dungeon:
                        //LoadDungeonMenu();
                        break;

                    case MenuType.Rest:
                        //LoadRestMenu();
                        break;

                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        curMenu = MenuType.Store;
                        break;
                }
                // 각 절차별 약간의 시간 부여
                Thread.Sleep(100);
            }
        }

        void LoadStartMenu()
        {
            Console.WriteLine("안녕?");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 정할 수 있어\n");

            Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 던전입장\n5. 휴식하기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            curMenu = (MenuType)int.Parse(Console.ReadLine());
        }

        void LoadStatusMenu()
        {
            player.ShowStatus();
            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            curMenu = (MenuType)int.Parse(Console.ReadLine());
        }

    }
}
