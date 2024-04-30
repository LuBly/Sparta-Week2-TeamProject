namespace ProjectNoName
{
    // 직업 종류
    // 차후 클래스 선택
    public enum ClassType
    {
        Warrior = 1,
        Archar,
        Magician
    }
    internal class Player
    {
        // Player 객체 내에서만 수정해야할 자료들
        int level;
        int levelPoint;
        string name;
        ClassType classType;
        float attackPower;
        float defensePower;
        float health;
        float gold;

        // 장비 장착 class에서 사용
        float IncreaseAttack;
        float IncreaseDefense;

        // 장착하고 있는 무기, 방어구
        // 한개씩만 장착할 수 있다.
        public Item Weapon;
        public Item Armor;
        public Inventory Inventory = new Inventory();

        public PlayerData Data = new PlayerData();
        // 최초 선언
        public Player(string playerName, ClassType selectClass)
        {
            level = 1;
            levelPoint = 0;
            name = playerName;//플레이어 생성창에서 유저가 입력한 이름 값이 들어갈것
            classType = selectClass;//플레이어 생성창에서 유저가 선택한 직업 타입이 들어갈것.
            attackPower = 10;
            defensePower = 5;
            health = 100;
            gold = 2500f;
            // idx맞추기용 더미데이터 입력.
            Inventory.AddItem(new Item());
        }

        public void ShowStatus()
        {
            Console.WriteLine("[상태 보기]");
            // Lv
            Console.WriteLine($"Lv. {level}");
            // 직업
            Console.WriteLine($"{name} : {classType}");
            // 공격력
            Console.Write($"공격력 : {attackPower}");
            if (IncreaseAttack > 0)
            {
                Console.WriteLine($" + ({IncreaseAttack})");
            }
            else
            {
                Console.WriteLine();
            }
            // 방어력
            Console.Write($"방어력 : {defensePower}");
            if (IncreaseDefense > 0)
            {
                Console.WriteLine($" + ({IncreaseDefense})");
            }
            else
            {
                Console.WriteLine();
            }
            // 체력
            Console.WriteLine($"체 력 : {health}");
            // 소유 gold
            Console.WriteLine($"Gold : {gold}");
        }

        // 데이터 저장 및 로드를 위한 함수
        public void GetPlayerData()
        {
            Data.Level = level;
            Data.LevelPoint = levelPoint;
            Data.Name = name;
            Data.ClassType = classType;
            Data.AttackPower = attackPower;
            Data.DefensePower = defensePower;
            Data.Health = health;
            Data.Gold = gold;
            Data.IncreaseAttack = IncreaseAttack;
            Data.IncreaseDefense = IncreaseDefense;

            // 아이템 객체들에 대한 public화가 필요하다.
            if(Weapon != null)
                Data.Weapon = Weapon.GetItemData();
            if(Armor != null)
                Data.Armor = Armor.GetItemData();
            if(Inventory.CountInventory() != 0)
                Data.Inventory = Inventory.GetInventoryData();
        }


        // Get 호출 함수
        public float GetPlayerGold()
        {
            Console.WriteLine($"{gold} G\n");

            return gold;
        }

        public float GetPlayerAttack()
        {
            return attackPower + IncreaseAttack;
        }

        public float GetPlayerDefence()
        {
            return defensePower + IncreaseDefense;
        }

        public float GetPlayerHealth()
        {
            return health;
        }

        // Set 저장 함수

        //[GOLD]
        public void UseGold(float price)
        {
            gold -= price;
        }

        public void EarnGold(float price)
        {
            gold += price;
        }




        // 기타 함수

        // Player의 health가 변동됐을 때
        /// 전투 매커니즘에 따라 함수 변형필요
        public float TakeDamage()
        {
            health /= 2;
            return health;
        }

        public float TakeDamageValue(float damage)
        {
            health -= damage;
            return health;
        }

        public void SetIncreaseAttack(float value)
        {
            IncreaseAttack = value;
        }

        public void SetIncreaseDefense(float value)
        {
            IncreaseDefense = value;
        }
    }
}
