using System.Runtime.CompilerServices;

namespace ProjectNoName
{
    public enum InventoryType
    {
        noneIdx,
        idx,
    }
    internal class Inventory
    {
        // inventory에 보유하고 있는 아이템 
        // List -> Dictionary 필요
        //List<Item> inventoryItems = new List<Item>();
        Dictionary<int, Item> inventoryItems = new Dictionary<int, Item>();
        public int CountInventory()
        {
            return inventoryItems.Count;
        }
        public void AddItem(int keyIdx, Item item)
        {
            inventoryItems.Add(keyIdx, item);
        }
        public void RemoveItem(int keyIdx)
        {
            inventoryItems.Remove(keyIdx);
        }
        public Item ChooceItem(int keyIdx)
        {
            return inventoryItems[keyIdx];
        }
        public void ShowInventory()
        {
            Console.Clear();

            ShowItemList(InventoryType.noneIdx, MenuType.Inventory);
            Console.WriteLine("\n1. 장착관리");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
        }

        // Inventory 장착 
        public void ShowEquipInventory()
        {
            // 장착 메세지 출력
            while (true)
            {
                // 기존 Inventory 내용 삭제
                Console.Clear();

                ShowItemList(InventoryType.idx, MenuType.Inventory);

                Console.WriteLine("\n장착할 장비를 선택해주세요.(0 : 뒤로가기)");
                Console.Write(">> ");
                int equipIdx = int.Parse(Console.ReadLine());
                // 범위 밖의 번호를 선택했을 때
                if (equipIdx > inventoryItems.Count || equipIdx < 0)
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
                    inventoryItems[equipIdx].EquipItem();
                }

                // 약간의 Delay 부여
                Thread.Sleep(500);
            }
        }

        // ItemList 출력
        public void ShowItemList(InventoryType type, MenuType menuType)
        {
            Console.WriteLine("\n[아이템 목록]");

            // 아이템 리스트 표기
            foreach(var item in inventoryItems)
            {
                Console.Write("- ");
                if (type == InventoryType.idx)
                    Console.Write($"{item.Key} ");
                item.Value.ShowItem(menuType);
            }
        }
    }
}
