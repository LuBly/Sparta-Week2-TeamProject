using System.Numerics;

namespace ProjectNoName
{
    // 직업 종류
    // 차후 클래스 선택
    public enum ClassType
    {
        Warrior,
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
        public Item Waepon;
        public Item Armor;


        public Inventory Inventory = new Inventory();

        // 최초 선언
        public Player()
        {
            level = 1;
            levelPoint = 0;
            name = "Player";//플레이어 생성창에서 유저가 입력한 이름 값이 들어갈것
            classType = ClassType.Warrior;//플레이어 생성창에서 유저가 선택한 직업 타입이 들어갈것
            attackPower = 10;
            defensePower = 5;
            health = 100;
            gold = 2500f;
            // idx맞추기용 더미데이터 입력.
            Inventory.AddItem(new Item());
        }

        // Player 정보를 입력해주는 함수
        public void SetPlayer(/*매개변수 입력*/)
        {
            
        }

        public void ShowStatus()
        {
            Console.WriteLine("[상태 보기]");
            // Lv
            Console.WriteLine($"Lv. {level}");
            // 직업
            Console.WriteLine($"Chad : {classType}");
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
