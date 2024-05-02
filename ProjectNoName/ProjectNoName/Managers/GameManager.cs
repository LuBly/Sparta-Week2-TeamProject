namespace ProjectNoName
{
    public enum MenuType
    {
        Start,
        Status,
        Inventory,
        Store,
        Dungeon,
        Quit,//저장하는 타입
        Load,//불러오는 타입
        //Consumable, //소비창
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
            ShowStart();
            
            DataManager.Instance().InitData();
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
                        DataManager.Instance().SaveData();
                        isGameOver = true;
                        break;

                    case MenuType.Load:
                        DataManager.Instance().LoadData();
                        LoadStartMenu();
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

        void ShowStart()
        {
            Console.Clear();
            Console.WriteLine("NoName\n");
            Console.WriteLine("\n1. 새로운 게임\n2. 계 속 하 기\n3. 게 임 종 료");
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
                        DataManager.Instance().CreatePlayer();
                        Thread.Sleep(1000);
                        break;

                    case 2:
                        Console.WriteLine("게임을 불러옵니다!");
                        Thread.Sleep(1000);
                        curMenu = MenuType.Load;
                        break;

                    case 3:
                        Console.WriteLine("게임을 종료합니다.");
                        Environment.Exit(0);
                        break;

                    //지정되지 않은 숫자값이 입력되었을 때 재입력
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                        Thread.Sleep(1000);
                        ShowStart();
                        break;
                }
            }
            else
            {
                // 숫자 이외의 값이 입력될 시 재입력
                Console.WriteLine("잘못된 입력입니다. 숫자를 입력해주세요.");
                Thread.Sleep(1000);
                ShowStart();
            }
        }

        // StartMenu
        void LoadStartMenu()
        {
            Console.WriteLine("안녕?");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 정할 수 있어\n");

            Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 던전입장\n5. 저장하기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            curMenu = (MenuType)int.Parse(Console.ReadLine());
        }
        
        // Status 메뉴
        void LoadStatusMenu()
        {
            DataManager.Instance().Player.ShowStatus();
            curMenu = (MenuType)Utill.EndMenu();
        }

        // 인벤토리 기본 메뉴
        void LoadInventoryMenu()
        {
            // Inventory 초기 화면
            DataManager.Instance().Player.Data.Inventory.ShowInventory();
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
                    //// 소비창
                    //case 2:
                    //    LoadConsumableMenu();
                    //    break;
                }

                if (!isContinue) break;
            }
        }

        //// 소비창 메뉴
        //void LoadConsumableMenu()
        //{
        //    DataManager.Instance().Player.Data.Inventory.ConsumableInventory();
        //}

        // 인벤토리 세부 매뉴
        void LoadEquipMenu()
        {
            DataManager.Instance().Player.Data.Inventory.EquipInventory();
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
