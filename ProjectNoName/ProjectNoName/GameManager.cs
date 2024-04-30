using System.Numerics;
using static System.Formats.Asn1.AsnWriter;

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
        Quit = 100,
    }
    internal class GameManager
    {
        // 초기 화면 설정(맨 처음 실행했을 때)
        MenuType curMenu = MenuType.Start;
        Player player = DataManager.Instance().Player;
        Store store = DataManager.Instance().Store;
        Dungeon dungeon = DataManager.Instance().Dungeon;
        // [추가 사항]
        //DataManager - 게임의 모든 정보들을 저장하고 있을 Manager (static)
        public void StartGame()
        {
            bool isGameOver = false;
            // 게임화면
            while (!isGameOver)
            {
                Console.Clear();
                // 각 씬별 동작
                switch (curMenu)
                {
                    case MenuType.Start:
                        LoadStartMenu();
                        break;

                    case MenuType.Status:
                        LoadStatusMenu();
                        break;

                    case MenuType.Inventory:
                        LoadInventoryMenu();
                        break;

                    case MenuType.Store:
                        LoadStoreMenu();
                        break;

                    case MenuType.Dungeon:
                        LoadDungeonMenu();
                        break;

                    case MenuType.Quit:
                        DataManager.Instance().GetData();
                        DataManager.Instance().SaveData();
                        isGameOver = true;
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

        // StartMenu
        void LoadStartMenu()
        {
            Console.WriteLine("안녕?");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 정할 수 있어\n");

            Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 던전입장");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            curMenu = (MenuType)int.Parse(Console.ReadLine());
        }
        
        // Status 메뉴
        void LoadStatusMenu()
        {
            player.ShowStatus();
            curMenu = (MenuType)Utill.EndMenu();
        }

        // 인벤토리 기본 메뉴
        void LoadInventoryMenu()
        {
            // Inventory 초기 화면
            player.Inventory.ShowInventory();
            while (true)
            {
                bool isContinue = true;
                int choiceIdx = int.Parse(Console.ReadLine());
                switch (choiceIdx)
                {
                    case 0:
                        isContinue = false;
                        curMenu = 0;
                        break;
                    case 1:
                        LoadEquipMenu();
                        break;
                }

                if (!isContinue) break;
            }
        }

        // 인벤토리 세부 매뉴
        void LoadEquipMenu()
        {
            player.Inventory.EquipInventory();
        }

        // 상점 메뉴
        void LoadStoreMenu()
        {
            store.ShowStore();
            while (true)
            {
                bool isContinue = true;
                StoreType choiceIdx = (StoreType)int.Parse(Console.ReadLine());
                switch (choiceIdx)
                {
                    case 0:
                        isContinue = false;
                        curMenu = 0;
                        break;
                    case StoreType.Buy:
                        store.UseStore(StoreType.Buy);
                        break;
                    case StoreType.Sell:
                        store.UseStore(StoreType.Sell);
                        break;
                }

                if (!isContinue) break;
            }
        }

        void LoadDungeonMenu()
        {
            while (true)
            {
                int choiceIdx = dungeon.ShowDungeon();
                bool isContinue = true;
                switch (choiceIdx)
                {
                    case 0:
                        isContinue = false;
                        curMenu = 0;
                        break;
                    default:
                        dungeon.ShowStage(choiceIdx);
                        break;
                }

                if (!isContinue) break;
            }
        }
    }
}
