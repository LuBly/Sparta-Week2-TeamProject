using ProjectNoName.Data;

namespace ProjectNoName
{
    public enum StoreType
    {
        Buy = 1,
        Sell = 2,
    }
    internal class Store
    {
        public StoreData Data = new StoreData();

        // ShowStore(Player)
        public void ShowStore()
        {
            //Inventory storeInventory = DataManager.Instance().StoreInventory;
            // 최초 방문 시 Item List를 inventory에 저장
            Console.Clear();
            // Player 골드 출력
            Console.WriteLine("[상점]\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{DataManager.Instance().Player.Data.Gold} G\n");
            ;

            // Store에 들어갈 Item List 불러오기
            Data.StoreInventory.ShowItemList(InventoryType.noneIdx, MenuType.Store);

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
                Console.WriteLine($"{player.Data.Gold} G\n");
                Data.StoreInventory.ShowItemList(InventoryType.idx, MenuType.Store);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("구매하시려는 아이템을 선택해주세요 (0 : 뒤로가기)");
                Console.Write(">> ");
                int choiceIdx = int.Parse(Console.ReadLine());
                // 범위 밖의 번호를 선택했을 때
                if (choiceIdx > Data.StoreInventory.CountInventory() || choiceIdx < 0)
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
                    Item curItem = Data.StoreInventory.ChooceItem(choiceIdx);
                    // 이미 구매한 아이템이라면
                    if (curItem.Data.IsPurchased)
                    {
                        Console.WriteLine($"이미 구매한 아이템입니다.");
                    }
                    // 장비 가격보다 보유 gold가 많으면 add
                    else if (curItem.CanBuy())
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
            // 장착 메세지 출력
            while (true)
            {
                // 기존 Inventory 내용 삭제
                Console.Clear();

                // Player 골드 출력
                Console.WriteLine("[상점]\n");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Data.Gold} G\n");
                player.Data.Inventory.ShowItemList(InventoryType.idx, MenuType.Store);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("판매하시려는 아이템을 선택해주세요 (0 : 뒤로가기)");
                Console.Write(">> ");
                int choiceIdx = int.Parse(Console.ReadLine());
                // 범위 밖의 번호를 선택했을 때
                if (choiceIdx > player.Data.Inventory.CountInventory() || choiceIdx < 0)
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
                    Item curItem = player.Data.Inventory.ChooceItem(choiceIdx);
                    curItem.SellItem();
                }

                // 약간의 Delay 부여
                Thread.Sleep(500);
            }
        }
    }
}
