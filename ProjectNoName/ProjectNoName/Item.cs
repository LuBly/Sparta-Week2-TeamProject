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
        // 최초 생성 시 false이므로 false로 초기화 
        // 차후 Save, Load 제작 시 해당 부분 변경 가능
        
        // 구매 여부
        bool isUsable = false;
        // 장착 여부
        bool isEquiped = false;
        // 이름
        string name;
        // 가격
        float price;
        // 장비 타입 (무기, 방어구, +회복(제작 시 enum 추가 필요)
        ItemType itemType;
        // 무기 공격력 증가(방어구일 경우 null 값 가능)
        float attackPowerIncrease = 0;
        // 방어력 증가(무기일 경우 null값 가능)
        float defencePowerIncrease = 0;
        // 설명
        string description;


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
            this.name = name;
            this.itemType = itemType;
            switch (itemType)
            {
                case ItemType.Weapon:
                    attackPowerIncrease = increaseData;
                    break;
                case ItemType.Armor:
                    defencePowerIncrease = increaseData;
                    break;
                // itemType별 적용 데이터 추가

            }
            this.description = description;
            this.price = price; 
        }

        // 데이터 저장 및 로드를 위한 함수
        public ItemData GetItemData()
        {
            Data.IsUsable = isUsable;
            Data.IsEquiped = isEquiped;
            Data.Name = name;
            Data.Price = price;
            Data.ItemType = itemType;
            Data.AttackPowerIncrease = attackPowerIncrease;
            Data.DefencePowerIncrease = defencePowerIncrease;
            Data.Description = description;

            return Data;
        }

        public void EquipItem()
        {
            Player player = DataManager.Instance().Player;
            
            switch (itemType)
            {
                // 무기 장착
                case ItemType.Weapon:
                    // 장착된 오브젝트가 있다면 교체
                    if (player.Weapon != null)
                    {
                        // 기존 장착 무기 장착 해제
                        player.Weapon.isEquiped = false;

                        // 기존 장착 무기 장착 해제
                        player.Weapon = this;
                        break;
                    }
                    // 장착된 오브젝트가 없다면 장착
                    else
                    {
                        player.Weapon = this;
                    }

                    // 공통 작업(수치 변경)
                    player.SetIncreaseAttack(attackPowerIncrease);
                    break;

                case ItemType.Armor:
                    // 장착된 오브젝트가 있다면 교체
                    if (player.Armor != null)
                    {
                        // 기존 방어구 장착 해제
                        player.Armor.isEquiped = false;

                        // 신규 방어구 장착
                        player.Armor = this;
                        break;
                    }

                    // 장착된 오브젝트가 없다면 장착
                    else
                    {
                        player.Armor = this;
                    }
                    
                    // 공통 작업 (수치 변경)
                    player.SetIncreaseDefense(defencePowerIncrease);
                    
                    break;
            }

            // 장착 중일 때
            if (isEquiped)
            {
                Console.WriteLine($"{this.name}을 해제하였습니다.");
                isEquiped = false;
            }
            // 장착 중이 아닐 때
            else
            {
                Console.WriteLine($"{this.name}을 장착했습니다.");
                isEquiped = true;
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
            if (isEquiped)
            {
                Console.Write("[E]");
            }
            // 이름
            Console.Write($"{name}");
            // 수치
            Console.SetCursorPosition(20, originRow);
            switch (itemType)
            {
                case ItemType.Weapon:
                    Console.Write($"| 공격력 + {attackPowerIncrease} ");
                    break;
                case ItemType.Armor:
                    Console.Write($"| 방어력 + {defencePowerIncrease} ");
                    break;

            }
            // 설명
            Console.SetCursorPosition(35, originRow);
            Console.WriteLine($"| {description}");

            // 스토어에서 구매 및 판매결정을 할 때만 해당 내용 출력
            if (menuType == MenuType.Store)
            {
                Console.SetCursorPosition(100, originRow);
                if (isUsable)
                {
                    Console.WriteLine("| 구매완료");
                }
                else
                    Console.WriteLine($"| {price} G");
            }
        }

        // 상점 관련
        public bool isBuy()
        {
            return isUsable;
        }

        public bool canBuy()
        {
            return DataManager.Instance().Player.GetPlayerGold() >= price;
        }


        // 구매 관련
        public void BuyItem()
        {
            isUsable = true;
            DataManager.Instance().Player.UseGold(price);
            DataManager.Instance().Player.Inventory.AddItem(this);
            Console.WriteLine($"{name}를 구매했습니다.");
        }

        // 판매 관련
        public void SellItem()
        {
            isUsable = false;
            DataManager.Instance().Player.EarnGold(price);
            DataManager.Instance().Player.Inventory.RemoveItem(this);
            Console.WriteLine($"{name}를 {price * 0.85f}에 판매했습니다.");
        }
    }
}
