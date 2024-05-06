namespace ProjectNoName
{
    public enum MenuType
    {
        Start,
        Status,
        Inventory,
        Store,
        Dungeon,
        Quest,
        Quit,//저장하는 타입
        Load,//불러오는 타입
        StoreBuy,
        StoreSell,
        //Consumable, //소비창
    }
    public class GameManager
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

                    case MenuType.Quest:
                        LoadQuestMenu();
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
            while(true)
            {
                Console.Clear();
                Console.WriteLine("안녕?");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 정할 수 있어\n");

                Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 던전입장\n5. 저장하기");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                int inputIdx = int.TryParse(Console.ReadLine(), out inputIdx) ? inputIdx : -1;
                if (inputIdx > System.Enum.GetValues(typeof(MenuType)).Length || inputIdx < 0)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500);
                    continue;
                }
                else if (inputIdx == 0)
                {
                    LoadStartMenu();
                    break;
                }
                else
                {
                    curMenu = (MenuType)inputIdx;
                    break;
                }
            }
        }

        // Status 메뉴
        void LoadStatusMenu()
        {
            while (true)
            {
                Console.Clear();

                DataManager.Instance().Player.ShowStatus();
                int input = Utill.EndMenu();

                if (input != 0)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500);
                    continue;
                }
                else
                {
                    LoadStartMenu();
                    break;
                }
            }
        }

        // 인벤토리 기본 메뉴
        void LoadInventoryMenu()
        {
            while (true)
            {
                // Inventory 초기 화면
                DataManager.Instance().Player.Data.Inventory.ShowInventory();

                int choiceIdx = int.TryParse(Console.ReadLine(), out choiceIdx) ? choiceIdx : -1;
                if(choiceIdx == 0)
                {
                    curMenu = 0;
                    break;
                }
                else if (choiceIdx == 1)
                {
                    LoadEquipMenu();
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500);
                    continue;
                }
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
            while (true)
            {
                store.ShowStore();
                bool isContinue = true;
                StoreType choiceIdx = (StoreType)(int.TryParse(Console.ReadLine(), out int inputIdx) ? inputIdx : -1);
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
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Thread.Sleep (500);
                        continue;
                }

                if (!isContinue) break;
            }
        }
        
        // 던전 메뉴
        void LoadDungeonMenu()
        {
            while (true)
            {
                int choiceIdx = dungeon.ShowDungeon();
                if(choiceIdx > dungeon.dungeonStage.Count || choiceIdx < 0)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500);
                    continue;
                }
                else if (choiceIdx == 0)
                {
                    curMenu = 0;
                    break ;
                }
                else
                {
                    dungeon.ShowStage(choiceIdx);
                    break;
                }
            }
        }

        // Quest 메뉴
        void LoadQuestMenu()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("퀘스트 목록");

            // 전체 퀘스트 목록을 가져옴
            List<Quest> questList = DataManager.Instance()._QuestManager.QuestList;

            // 퀘스트 목록 출력
            for (int i = 1; i < questList.Count; i++)
            {
                Console.Write($"{questList[i].Data.QuestId}. {questList[i].Data.QuestName}");
                switch (questList[i].Data.QuestProgress)
                {
                    case QuestProgress.OnGoing:
                        Console.WriteLine(" [진행중]");
                        break;
                    case QuestProgress.Completion:
                        Console.WriteLine(" [완료스]");
                        break;
                    default:
                        Console.WriteLine();
                        break;
                }
            }
            Console.WriteLine("\n원하시는 퀘스트를 선택해주세요.");
            Console.WriteLine("0. 돌아가기");
            Console.Write(">> ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int choiceIdx))
            {
                if (choiceIdx >= 0 && choiceIdx < questList.Count)
                {
                    if (questList[choiceIdx].Data.QuestProgress == QuestProgress.Completion)
                    {
                        Console.WriteLine("해당 퀘스트는 이미 완료하였습니다.");
                        Thread.Sleep(500);
                    }
                    else
                    {
                        switch (choiceIdx)
                        {
                            case 0:
                                curMenu = MenuType.Start;
                                break;
                            default:
                                DetailQuest(questList[choiceIdx]);
                                // 선택한 퀘스트로 이동하는 로직 추가
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 범위 내의 숫자를 입력해주세요.");
                    LoadQuestMenu(); // 재입력 요구
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 숫자를 입력해주세요.");
                LoadQuestMenu(); // 재입력 요구
            }
        }
        void DetailQuest(Quest quest)
        {
            Console.Clear();
            Console.WriteLine($"퀘스트 이름 : {quest.Data.QuestName}");
            Console.WriteLine($"\n{quest.Data.QuestDescription}");
            switch (quest.Data.QuestType)
            {
                case QuestType.Collect:
                    Console.WriteLine($"\n{quest.GetInventoryItemCount()} / {quest.Data.CompletCondition}");
                    break;
                case QuestType.Battle:
                    Console.WriteLine($"\n{quest.KillCount} / {quest.Data.CompletCondition}");
                    break;
            }

            // 보상 정보 출력
            Console.WriteLine($"\n보상");
            Console.WriteLine($"Gold : {quest.Data.RewardGold}");
            Console.WriteLine($"Exp : {quest.Data.RewardExp}\n");
            // 수락 거절 보상받기
            switch (quest.Data.QuestProgress)
            {
                case QuestProgress.NoStart:
                    Console.WriteLine("1. 수락");
                    if(quest.Data.QuestType == QuestType.Battle)
                        quest.AcceptBattleQuest();
                    break;
                default:
                    if (quest.GetInventoryItemCount() < quest.Data.CompletCondition)
                    {
                        Console.WriteLine("1. 완료하기");
                    }
                    else
                    {
                        Console.WriteLine("1. 완료하기");
                    }
                    break;
            }

            Console.WriteLine("0. 뒤로가기");
            Console.Write(">> ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int choice))
            {
                switch (choice)
                {
                    case 0:
                        LoadQuestMenu();
                        break;
                    case 1:
                        switch (quest.Data.QuestProgress)
                        {
                            case QuestProgress.NoStart:
                                Console.WriteLine("퀘스트를 수락합니다.");
                                // 퀘스트 진행 상태를 변경하고 다른 작업 수행
                                quest.Data.QuestProgress = QuestProgress.OnGoing;
                                Thread.Sleep(500);
                                break;
                            default:
                                if (quest.Data.QuestType == QuestType.Collect)
                                {
                                    if (quest.GetInventoryItemCount() < quest.Data.CompletCondition)
                                    {
                                        Console.WriteLine("퀘스트를 진행중입니다.");
                                        Thread.Sleep(500);
                                    }
                                    else
                                    {
                                        Console.WriteLine("퀘스트 클리어!");
                                        // 퀘스트 완료 상태로 변경하고 다른 작업 수행
                                        quest.ClearQuest();
                                        Thread.Sleep(500);
                                    }
                                }
                                else
                                {
                                    if (quest.KillCount < quest.Data.CompletCondition)
                                    {
                                        Console.WriteLine("퀘스트를 진행중입니다.");
                                        Thread.Sleep(500);
                                    }
                                    else
                                    {
                                        Console.WriteLine("퀘스트 클리어!");
                                        // 퀘스트 완료 상태로 변경하고 다른 작업 수행
                                        quest.ClearQuest();
                                        Thread.Sleep(500);
                                    }
                                }

                                break;
                                
                        }
                        break;
                }
            }
        }
    }
}
