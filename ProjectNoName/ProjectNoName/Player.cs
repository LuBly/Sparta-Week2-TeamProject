using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Threading;

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
    public class Player
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
            Data.DefensePower = 50;
            Data.CriticalRate = 100; //치명타 확률
            Data.CriticalDamage = 50; //치명타 피해
            Data.EvasionRate = 0; // 회피율
            Data.MaxHealth = 100;
            Data.CurHealth = Data.MaxHealth;
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
            // 치명타 확률
            Console.WriteLine($"치명타 확률 : {Data.CriticalRate}%");

            // 치명타 피해
            Console.WriteLine($"치명타 피해 : {Data.CriticalDamage}%");

            // 회피율
            Console.WriteLine($"회피율: {Data.EvasionRate}%");

            // 체력
            Console.WriteLine($"체 력 : {Data.CurHealth}");
            
            // 마나
            Console.WriteLine($"마 나 : {Data.Mana}");

            // 소유 gold
            Console.WriteLine($"Gold : {Data.Gold}");

            // 내 장착 아이템
            // ex) [장착 무기] 스파르타의 창 (공격력 + 7)
            // [장착 방어구] 스파르타의 갑옷 (방어력 + 9)
            /*
             {Data.Weapon.Data.Name}
             */
        }


        public void ShowBattleStatus()
        {
            Console.WriteLine("[내 정보]");
            Console.WriteLine($"Lv.{Data.Level} {Data.Name} ({Data.ClassType})");
            Console.WriteLine($"HP {Data.CurHealth} / {Data.MaxHealth}");
        }

        public float GetPlayerAttack()
        {
            return Data.AttackPower + Data.IncreaseAttack;
        }

        public float GetPlayerDefence()
        {
            return Data.DefensePower + Data.IncreaseDefense;
        }

        public int GetPlayerDamage()
        {
            Random random = new Random();
            //데미지 편차
            int deviation = (int)Math.Ceiling(GetPlayerAttack() * 0.1f);
            //치명타 성공
            if (random.Next(1, 100) <= Data.CriticalRate)
            {
                Console.Write(" [ CRITICAL! ]");
                Console.WriteLine();
                return random.Next((int)((GetPlayerAttack() - deviation) * (1 + Data.CriticalDamage/100)), (int)((GetPlayerAttack() + deviation) * (1 + Data.CriticalDamage/100)));
            }
            //치명타 실패
            else
            {
                Console.WriteLine();
                return random.Next((int)GetPlayerAttack() - deviation, (int)GetPlayerAttack() + deviation);
            }
                
            
        }
        
        // 기타 함수
        /// 전투 매커니즘에 따라 함수 변형필요
        public float TakeDamage(float damage)
        {
            Random random = new Random();
            //회피 성공, 받는 데미지 0
            if (random.Next(1,100) <= Data.EvasionRate)
            {
                damage = 0;
                Console.WriteLine($"Miss! [데미지 : {damage}]");
            }
            //회피 실패, 방어도에 따라 받는 데미지 감소
            else
            {
                damage = (int)(damage * (GetPlayerDefence() / (50 + GetPlayerDefence())));
                Console.WriteLine($"{Data.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
            }
            Console.WriteLine();
            Console.WriteLine($"Lv.{Data.Level} {Data.Name}");
            Console.WriteLine($"HP {Data.CurHealth} -> {Data.CurHealth - damage}");
            Data.CurHealth -= damage;
            if (Data.CurHealth < 0) Data.CurHealth = 0;
            return Data.CurHealth;
        }

        // 체력 회복
        public float RecoverHealth(float healthRecovered)
        {
            Data.CurHealth += healthRecovered;
            Console.WriteLine($"체력을 {healthRecovered} 회복하였습니다.");
            return Data.CurHealth;
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
