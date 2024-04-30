using System.Numerics;

namespace ProjectNoName
{
    public enum ItemType
    {
        None = 0,
        Weapon,
        Armor,
    }

    internal class Item
    {
        //[각 아이템별 정보]
        public ItemData Data = new ItemData();
        /// <summary>
        /// 필요시 추가
        /// </summary>
        // 추가 효과(ex. 회복물약)
        // int? healthIncrease, ManaIncrease

        // index값을 맞추기 위한 더미데이터 생성용 OverLoading
        public Item() { }
        public Item(string name, ItemType itemType, int increaseData, string description, float price)
        {
            Data.Name = name;
            Data.ItemType = itemType;
            switch (itemType)
            {
                case ItemType.Weapon:
                    Data.AttackPowerIncrease = increaseData;
                    break;
                case ItemType.Armor:
                    Data.DefencePowerIncrease = increaseData;
                    break;
                // itemType별 적용 데이터 추가

            }
            Data.Description = description;
            Data.Price = price; 
        }

        // 데이터 저장 및 로드를 위한 함수
        public void ManageItem()
        {
            PlayerData playerData = DataManager.Instance().Player.Data;
            
            switch (Data.ItemType)
            {
                // 무기 장착
                case ItemType.Weapon:
                    // 장착된 오브젝트가 있다면 교체
                    if (playerData.Weapon != null)
                    {
                        // 지금 착용 중인 무기와 다른 무기라면 교체
                        if (playerData.Weapon != this)
                        {
                            // 교체 메세지 출력
                            Console.WriteLine($"{playerData.Weapon.Data.Name} => {Data.Name} 교체했습니다.");
                            // 기존 무기 장착 해제
                            playerData.Weapon.Data.IsEquiped = false;
                            // 신규 무기 장착
                            EquipItem(ItemType.Weapon);
                        }
                        // 지금 착용 중인 무기와 같은 무기라면 해제
                        else
                        {
                            // 기존 장착 무기 장착 해제
                            UnEquipItem(ItemType.Weapon);
                        }
                    }
                    // 장착된 오브젝트가 없다면 장착
                    else
                    {
                        EquipItem(ItemType.Weapon);
                        Console.WriteLine($"{Data.Name}을 장착했습니다.");
                    }
                    break;

                case ItemType.Armor:
                    // 장착된 오브젝트가 있다면 교체
                    if (playerData.Armor != null)
                    {
                        // 지금 착용 중인 방어구와 다른 방어구라면
                        if (playerData.Armor != this)
                        {
                            // 교체 메세지 출력
                            Console.WriteLine($"{playerData.Armor.Data.Name} => {Data.Name} 교체했습니다.");
                            // 기존 무기 장착 해제
                            playerData.Armor.Data.IsEquiped = false;
                            // 신규 방어구 장착
                            EquipItem(ItemType.Armor);
                        }
                        // 지금 착용 중인 방어구와 같은 방어구라면 해제
                        else
                        {
                            // 기존 방어구 장착 해제
                            UnEquipItem(ItemType.Armor);
                        }
                    }
                    // 장착된 오브젝트가 없다면 장착
                    else
                    {
                        EquipItem(ItemType.Armor);
                        Console.WriteLine($"{Data.Name}을 장착했습니다.");
                    }
                    break;
            }
        }

        // 장비 장착
        void EquipItem(ItemType itemType)
        {
            Player player = DataManager.Instance().Player;
            switch (itemType)
            {
                case ItemType.Weapon:
                    Data.IsEquiped = true;
                    player.Data.Weapon = this;
                    player.Data.IncreaseAttack = Data.AttackPowerIncrease;
                    break;

                case ItemType.Armor:
                    Data.IsEquiped = true;
                    player.Data.Armor = this;
                    player.Data.IncreaseDefense = Data.DefencePowerIncrease;
                    break;
            }
        }

        void UnEquipItem(ItemType itemType)
        {
            Player player = DataManager.Instance().Player;
            switch (itemType)
            {
                case ItemType.Weapon:
                    // 기존 무기 장착 해제
                    Data.IsEquiped = false;
                    player.Data.IncreaseAttack = 0;
                    player.Data.Weapon = null;
                    Console.WriteLine($"{Data.Name}을 해제하였습니다.");
                    break;

                case ItemType.Armor:
                    // 기존 방어구 장착 해제
                    Data.IsEquiped = false;
                    player.Data.IncreaseDefense = 0;
                    player.Data.Armor = null;
                    Console.WriteLine($"{Data.Name}을 해제하였습니다.");
                    break;
            }
        }
        /// <summary>
        /// MenuType에 따른 Item 표현 방식을 구분
        /// </summary>
        /// <param name="menuType">
        /// Item을 보여주는 MenuType이 무엇인지에 따라 보여지는 정보가 다르다.
        /// </param>
        public void ShowItem(MenuType menuType)
        {
            int originRow = Console.CursorTop;
            // 장착중인 아이템이라면 인벤토리에 [E] 표시
            if (Data.IsEquiped)
            {
                Console.Write("[E]");
            }
            // 이름
            Console.Write($"{Data.Name}");
            // 수치
            Console.SetCursorPosition(20, originRow);
            switch (Data.ItemType)
            {
                case ItemType.Weapon:
                    Console.Write($"| 공격력 + {Data.AttackPowerIncrease} ");
                    break;
                case ItemType.Armor:
                    Console.Write($"| 방어력 + {Data.DefencePowerIncrease} ");
                    break;

            }
            // 설명
            Console.SetCursorPosition(35, originRow);
            Console.WriteLine($"| {Data.Description}");

            // 스토어에서 구매 및 판매결정을 할 때만 해당 내용 출력
            if (menuType == MenuType.Store)
            {
                Console.SetCursorPosition(100, originRow);
                if (Data.IsPurchased)
                {
                    Console.WriteLine("| 구매완료");
                }
                else
                    Console.WriteLine($"| {Data.Price} G");
            }
        }

        public bool CanBuy()
        {
            return DataManager.Instance().Player.Data.Gold >= Data.Price;
        }


        // 구매 관련
        public void BuyItem()
        {
            Data.IsPurchased = true;
            DataManager.Instance().Player.Data.Gold -= Data.Price;
            DataManager.Instance().Player.Data.Inventory.AddItem(this);
            Console.WriteLine($"{Data.Name}를 구매했습니다.");
        }

        // 판매 관련
        public void SellItem()
        {
            Data.IsPurchased = false;
            DataManager.Instance().Player.Data.Gold += Data.Price;
            DataManager.Instance().Player.Data.Inventory.RemoveItem(this);
            Console.WriteLine($"{Data.Name}를 {Data.Price * 0.85f}에 판매했습니다.");
        }
    }
}
