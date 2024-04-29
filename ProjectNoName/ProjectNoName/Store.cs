namespace ProjectNoName
{
    public enum StoreType
    {
        Buy = 1,
        Sell = 2,
    }
    internal class Store
    {
        bool isFirst = true;
        Inventory storeInventory = new Inventory();

        // . . . .호출
        void D_addStoreItem()
        {
            // [임시]DataBase
            List<Item> items = new List<Item>() {
                new Item(),
                new Item("수련자 갑옷", ItemType.Armor, 5, "수련에 도움을 주는 갑옷입니다.", 1000),
                new Item("무쇠갑옷", ItemType.Armor, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1500),
                new Item("스파르타의 갑옷", ItemType.Armor, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500),
                new Item("낡은 검", ItemType.Weapon, 2, "쉽게 볼 수 있는 낡은 검 입니다. ", 600),
                new Item("청동 도끼", ItemType.Weapon, 5, "어디선가 사용됐던거 같은 도끼입니다.", 1500),
                new Item("스파르타의 창", ItemType.Weapon, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 4000),
                new Item("AK47", ItemType.Weapon, 47, "전설의 외할머니가 사용하던 무기입니다.", 4747),
            };

            foreach (Item item in items)
            {
                storeInventory.AddItem(item);
            }
        }
        // ShowStore(Player)
        public void ShowStore()
        {
            //Inventory storeInventory = DataManager.Instance().StoreInventory;
            // 최초 방문 시 Item List를 inventory에 저장
            if (isFirst)
            {
                D_addStoreItem();
                isFirst = false;
            }
            Console.Clear();
            // Player 골드 출력
            Console.WriteLine("[상점]\n");
            Console.WriteLine("[보유 골드]");
            DataManager.Instance().Player.GetPlayerGold();

            // Store에 들어갈 Item List 불러오기
            storeInventory.ShowItemList(InventoryType.noneIdx, MenuType.Store);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
        }

        public void UseStore(StoreType storeType)
        {
            switch (storeType)
            {
                // Buy
                case StoreType.Buy:
                    // 아이템 구매 함수 불러오기
                    BuyItem();
                    break;
                // Sell
                case StoreType.Sell:
                    // 아이템 판매 함수 불러오기
                    SellItem();
                    break;
            }
        }

        // Inventory의 아이템을 구매
        public void BuyItem()
        {
            Player player = DataManager.Instance().Player;
            // 장착 메세지 출력
            while (true)
            {
                // 기존 Inventory 내용 삭제
                Console.Clear();

                // Player 골드 출력
                Console.WriteLine("[상점]\n");
                Console.WriteLine("[보유 골드]");
                player.GetPlayerGold();
                storeInventory.ShowItemList(InventoryType.idx, MenuType.Store);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("구매하시려는 아이템을 선택해주세요 (0 : 뒤로가기)");
                Console.Write(">> ");
                int choiceIdx = int.Parse(Console.ReadLine());
                // 범위 밖의 번호를 선택했을 때
                if (choiceIdx > storeInventory.CountInventory() || choiceIdx < 0)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500);
                    continue;
                }
                // 0을 선택했을 때
                else if (choiceIdx == 0)
                {
                    // 초기 페이지 재호출
                    ShowStore();
                    break;
                }
                // 장비를 선택했을 때
                else
                {
                    Item curItem = storeInventory.ChooceItem(choiceIdx);
                    // 이미 구매한 아이템이라면
                    if (curItem.isBuy())
                    {
                        Console.WriteLine($"이미 구매한 아이템입니다.");
                    }
                    // 장비 가격보다 보유 gold가 많으면 add
                    else if (curItem.canBuy())
                    {
                        curItem.BuyItem();
                    }
                    // 장비 가격보다 보유 gold가 적으면 패스
                    else
                    {
                        Console.WriteLine("골드가 부족합니다.");
                    }
                }

                // 약간의 Delay 부여
                Thread.Sleep(500);
            }
        }

        public void SellItem()
        {
            //Player 정보와 PlayerInventory 호출
            Player player = DataManager.Instance().Player;
            Inventory playerInventory = DataManager.Instance().Player.Inventory;
            // 장착 메세지 출력
            while (true)
            {
                // 기존 Inventory 내용 삭제
                Console.Clear();

                // Player 골드 출력
                Console.WriteLine("[상점]\n");
                Console.WriteLine("[보유 골드]");
                player.GetPlayerGold();
                playerInventory.ShowItemList(InventoryType.idx, MenuType.Store);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("판매하시려는 아이템을 선택해주세요 (0 : 뒤로가기)");
                Console.Write(">> ");
                int choiceIdx = int.Parse(Console.ReadLine());
                // 범위 밖의 번호를 선택했을 때
                if (choiceIdx > playerInventory.CountInventory() || choiceIdx < 0)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500);
                    continue;
                }
                // 0을 선택했을 때
                else if (choiceIdx == 0)
                {
                    // 초기 페이지 재호출
                    ShowStore();
                    break;
                }
                // 장비를 선택했을 때
                else
                {
                    Item curItem = playerInventory.ChooceItem(choiceIdx);
                    curItem.SellItem();
                }

                // 약간의 Delay 부여
                Thread.Sleep(500);
            }
        }
    }
}
