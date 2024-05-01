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
        public PlayerData Data = new PlayerData();
        
        // 데이터 로드용 
        public Player()
        {

        }

        // 캐릭터 최초 생성용
        public Player(string playerName, ClassType selectClass)
        {
            Data.Level = 1;
            Data.LevelPoint = 0;
            Data.Name = playerName;//플레이어 생성창에서 유저가 입력한 이름 값이 들어갈것
            Data.ClassType = selectClass;//플레이어 생성창에서 유저가 선택한 직업 타입이 들어갈것.
            Data.AttackPower = 10;
            Data.DefensePower = 5;
            Data.Health = 100;
            Data.Gold = 2500f;

            // idx맞추기용 더미데이터 입력.
            Data.Inventory.AddItem(new Item());
        }

        public void ShowStatus()
        {
            Console.WriteLine("[상태 보기]");
            // Lv
            Console.WriteLine($"Lv. {Data.Level}");
            // 직업
            Console.WriteLine($"{Data.Name} : {Data.ClassType}");
            // 공격력
            Console.Write($"공격력 : {Data.AttackPower}");
            if (Data.IncreaseAttack > 0)
            {
                Console.WriteLine($" + ({Data.IncreaseAttack})");
            }
            else
            {
                Console.WriteLine();
            }
            // 방어력
            Console.Write($"방어력 : {Data.DefensePower}");
            if (Data.IncreaseDefense > 0)
            {
                Console.WriteLine($" + ({Data.IncreaseDefense})");
            }
            else
            {
                Console.WriteLine();
            }
            // 체력
            Console.WriteLine($"체 력 : {Data.Health}");
            
            // 마나
            Console.WriteLine($"마 나 : {Data.Mana}");

            // 소유 gold
            Console.WriteLine($"Gold : {Data.Gold}");
        }

        public float GetPlayerAttack()
        {
            return Data.AttackPower + Data.IncreaseAttack;
        }

        public float GetPlayerDefence()
        {
            return Data.AttackPower + Data.IncreaseDefense;
        }

        // 기타 함수
        /// 전투 매커니즘에 따라 함수 변형필요
        public float TakeDamage()
        {
            Data.Health /= 2;
            return Data.Health;
        }

        // 체력 회복
        public float RecoverHealth(float healthRecovered)
        {
            Data.Health += healthRecovered;
            Console.WriteLine($"체력을 {healthRecovered} 회복하였습니다.");
            return Data.Health;
        }

        // 마나 획복
        public float RecoverMana(float manaRecovered)
        {
            Data.Mana += manaRecovered;
            Console.WriteLine($"마나를 {manaRecovered} 회복하였습니다.");
            return Data.Mana;
        }
    }
}
