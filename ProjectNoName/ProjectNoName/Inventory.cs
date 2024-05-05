using System.Runtime.CompilerServices;

namespace ProjectNoName
{
    public enum InventoryType
    {
        noneIdx,
        idx,
    }
    public class Inventory
    {
        // inventory에 보유하고 있는 아이템 
        public InventoryData Data = new InventoryData();

        // Inventory 관련 함수 모음
        public int CountInventory()
        {
            return Data.InventoryItems.Count;
        }

        public void AddItem(Item item)
        {
            // 현재 선택한 아이템이 현재 인벤토리에 있는지 Find
            Item? curItem = Data.InventoryItems.Find(i => i.Data.Id == item.Data.Id);

            // 가지고 있지 않던 아이템이라면 인벤토리에 Add
            if(curItem == null)
            {
                item.Data.ItemCount++;
                Data.InventoryItems.Add(item);
            }
            
            // 가지고 있던 아이템이라면
            else
            {
                curItem.Data.ItemCount++;
            }
            
        }

        public void RemoveItem(Item item)
        {
            // Player에서 count가 0이면 List에서 삭제
            // Store의 경우 count가 0 이상일때만 구매가능하게.
            item.Data.ItemCount--;
            if (item.Data.ItemCount <= 0)
            {
                item.Data.ItemCount = 0;
                Data.InventoryItems.Remove(item);
            }
        }

        public Item ChooceItem(int idx)
        {
            return Data.InventoryItems[idx];
        }

        /// <summary>
        /// 페이지 출력 관련 함수
        /// </summary>
        /// 
        // 인벤토리 페이지 보여주기
        public void ShowInventory()
        {
            Console.Clear();

            ShowItemList(InventoryType.noneIdx, MenuType.Inventory);
            //Console.WriteLine("\n2. 아이템 사용");
            Console.WriteLine("\n1. 장착관리/아이템 사용");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
        }

        // 장착 페이지 보여주기 
        public void ShowEquipInventory()
        {
            // 장착 메세지 출력
            while (true)
            {
                // 기존 Inventory 내용 삭제
                Console.Clear();

                ShowItemList(InventoryType.idx, MenuType.Inventory);

                Console.WriteLine("\n장착할 장비 또는 사용할 아이템을 선택해주세요.(0 : 뒤로가기)");
                Console.Write(">> ");
                int equipIdx = int.Parse(Console.ReadLine());
                // 범위 밖의 번호를 선택했을 때
                if (equipIdx > CountInventory() || equipIdx < 0)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500);
                    continue;
                }
                // 0을 선택했을 때
                else if (equipIdx == 0)
                {
                    ShowInventory();
                    break;
                }
                // 장비를 선택했을 때
                else
                {
                    // 장비 장착 및 해제
                    Data.InventoryItems[equipIdx].ManageItem();
                }

                // 약간의 Delay 부여
                Thread.Sleep(500);
            }
        }

        ////소비창
        //public void ConsumableInventory()
        //{
        //    while(true)
        //    {
        //        Console.Clear();

        //        ShowConsumableItemList(InventoryType.idx, MenuType.Consumable);

        //        Console.WriteLine("\n사용할 아이템을 선택해주세요. (0 : 뒤로가기)");
        //        Console.Write(">> ");
        //        int consumeIdx = int.Parse(Console.ReadLine());
        //        if(consumeIdx > CountInventory() || consumeIdx < 0)
        //        {
        //            Console.WriteLine("잘못된 입력입니다.");
        //            Thread.Sleep(500);
        //            continue;
        //        }
        //        // 0을 선택했을 때
        //        else if (consumeIdx == 0)
        //        {
        //            ShowInventory();
        //            break;
        //        }
        //        // 장비를 선택했을 때
        //        else
        //        {
        //            Data.InventoryItems[consumeIdx].ManageItem();
        //        }

        //        Thread.Sleep(500);
        //    }
        //}

        // 장비 장착 / 해제
        public void EquipInventory()
        {
            // 장착 메세지 출력
            while (true)
            {
                // 기존 Inventory 내용 삭제
                Console.Clear();

                ShowItemList(InventoryType.idx, MenuType.Inventory);

                Console.WriteLine("\n장착할 장비 또는 사용할 아이템을 선택해주세요.(0 : 뒤로가기)");
                Console.Write(">> ");
                int equipIdx = int.Parse(Console.ReadLine());
                // 범위 밖의 번호를 선택했을 때
                if (equipIdx > CountInventory() || equipIdx < 0)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500);
                    continue;
                }
                // 0을 선택했을 때
                else if (equipIdx == 0)
                {
                    ShowInventory();
                    break;
                }
                // 장비를 선택했을 때
                else
                {
                    // 장비 장착 및 해제
                    Data.InventoryItems[equipIdx].ManageItem();
                }

                // 약간의 Delay 부여
                Thread.Sleep(500);
            }
        }

        // ItemList 출력
        public void ShowItemList(InventoryType type, MenuType menuType)
        {
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine("");
            Utill.ShowLine();
            // 아이템 리스트 표기
            for (int i = 1; i < CountInventory(); i++)
            {
                Console.Write("-");
                if (type == InventoryType.idx)
                    Console.Write($" {i} ");
                Data.InventoryItems[i].ShowItem(menuType);
            }
        }

        //// 소비창 ItemList 출력
        //public void ShowConsumableItemList(InventoryType type, MenuType menuType)
        //{
        //    Console.WriteLine("\n[아이템 목록]");

        //    // 아이템 리스트 표기
        //    for (int i = 1; i < CountInventory(); i++)
        //    {
        //        if (Data.InventoryItems[i].Data.ItemType == ItemType.HealthPotion || Data.InventoryItems[i].Data.ItemType == ItemType.ManaPotion)
        //        {
        //            Console.Write("-");
        //            if (type == InventoryType.idx)
        //                Console.Write($" {i} ");
        //            Data.InventoryItems[i].ShowItem(MenuType.Consumable);
        //        }
        //    }
        //}
    }
}
