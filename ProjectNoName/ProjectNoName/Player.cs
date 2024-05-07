using static ProjectNoName.Utill;

namespace ProjectNoName
{
    // 직업 종류
    // 차후 클래스 선택
    public enum ClassType
    {
        Warrior = 1,
        Archer,
        Magician,
        TypeEnd,
    }
    public class Player
    {
        public PlayerData Data = new PlayerData();
        
        public Skill skill = new Skill();
        // 이거 그냥 
        /*
         * public Skill skill; 가능
         * 그래서 다 바꿔야해요
         * 
         * ClassType;
         * switch(classType)
         *  case:classType.Warrior 
         *  skill = new WarriorSkill();
         * 
         * 캐릭터를 마법사를 만들었어요..
         * WarriorSkill, MagicianSkill << 얘네는 그냥 안쓰는 쓰레기 데이터가 됩니다.
         * 
         */
        public WarriorSkill warriorSkill = new WarriorSkill();
        public ArcherSkill archerSkill = new ArcherSkill();
        public MagicianSkill magicianSkill = new MagicianSkill();

        public List<int> levelUpData = new List<int>() 
        {
            0,
            10, // Lv 1 -> Lv 2
            35, // Lv 2 -> Lv 3
            65, // Lv 3 -> Lv 4
            100,// Lv 4 -> Lv 5
        };
        // 데이터 로드용 
        public Player()
        {

        }

        // 캐릭터 최초 생성용
        public Player(string playerName, ClassType selectClass)
        {
            Data.Level = 1;
            Data.Exp = 0;
            Data.Name = playerName;//플레이어 생성창에서 유저가 입력한 이름 값이 들어갈것
            Data.ClassType = selectClass;//플레이어 생성창에서 유저가 선택한 직업 타입이 들어갈것.
            Data.AttackPower = 10;
            Data.DefensePower = 50;
            Data.CriticalRate = 100; //치명타 확률
            Data.CriticalDamage = 50; //치명타 피해
            Data.EvasionRate = 0; // 회피율
            Data.MaxMana = 100; // 마나
            Data.CurMana = Data.MaxMana;
            Data.ManaAfterSkill = Data.CurMana; // 스킬 사용후 마나
            Data.MaxHealth = 100;
            Data.CurHealth = Data.MaxHealth;
            Data.Gold = 2500f;

            // idx맞추기용 더미데이터 입력.
            Data.Inventory.AddItem(new Item());
        }

        public void ShowStatus()
        {
            Console.WriteLine("[내 정보]");
            // Lv
            Console.WriteLine($"Lv. {Data.Level} [exp. " + ConsoleColors.Green + $"{Data.Exp}" + ConsoleColors.Reset + "]");
            // 직업
            switch (Data.ClassType)
            {
                case ClassType.Warrior:
                    Console.WriteLine($"{Data.Name} :" + ConsoleColors.Red + $" {Data.ClassType}" + ConsoleColors.Reset);
                    break;

                case ClassType.Archer:
                    Console.WriteLine($"{Data.Name} :" + ConsoleColors.Green + $" {Data.ClassType}" + ConsoleColors.Reset);
                    break;

                case ClassType.Magician:
                    Console.WriteLine($"{Data.Name} :" + ConsoleColors.Purple + $" {Data.ClassType}" + ConsoleColors.Reset);
                    break;
            }
            // 공격력
            Console.Write("공격력 : " + $"{Data.AttackPower}");
            if (Data.IncreaseAttack > 0)
            {
                Console.WriteLine($" + ("+ ConsoleColors.Red + $"{Data.IncreaseAttack}" + ConsoleColors.Reset + ")");
            }
            else
            {
                Console.WriteLine();
            }
            // 방어력
            Console.Write($"방어력 : {Data.DefensePower}");
            if (Data.IncreaseDefense > 0)
            {
                Console.WriteLine($" + ("+ ConsoleColors.Blue + $"{Data.IncreaseDefense}" + ConsoleColors.Reset + ")");
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
            Console.WriteLine($"체 력 : " + ConsoleColors.Green + $"{Data.CurHealth}" + ConsoleColors.Reset);

            // 마나
            Console.WriteLine($"마 나 : " + ConsoleColors.Cyan + $"{Data.CurMana}" + ConsoleColors.Reset);

            // 소유 gold
            Console.WriteLine($"Gold : " +ConsoleColors.Yellow+ $"{Data.Gold}" + ConsoleColors.Reset);

            Console.WriteLine();
            Console.WriteLine("[장착 장비]");
            // 착용중인 장비
            if(Data.Weapon != null)
            {
                ItemData weaponData = Data.Weapon.Data;
                Console.Write($"착용 무  기 : {weaponData.Name}");
                int originRow = Console.CursorTop;
                Console.SetCursorPosition(30, originRow);
                Console.WriteLine($"[공격력 + {weaponData.AttackPowerIncrease}]");
            }

            if (Data.Armor != null)
            {
                ItemData armorData = Data.Armor.Data;
                Console.Write($"착용 방어구 : {armorData.Name}");
                int originRow = Console.CursorTop;
                Console.SetCursorPosition(30, originRow);
                Console.WriteLine($"[방어력 + {armorData.DefencePowerIncrease}]");
            }

            // 직업 스킬
            Console.WriteLine();
            Console.WriteLine("[직업 스킬]");
            switch (Data.ClassType)
            {
                case ClassType.Warrior:
                    Console.WriteLine("1. Power Strike");
                    Console.WriteLine("2. Power Slam");
                    Console.WriteLine("3. Double Down");
                    break;
                case ClassType.Archer:
                    Console.WriteLine("1. Make it Rain");
                    Console.WriteLine("2. Ace in the Hole");
                    Console.WriteLine("3. Multi Shot");
                    break;
                case ClassType.Magician:
                    Console.WriteLine("1. Chain Lightning");
                    Console.WriteLine("2. Inferno Bomb");
                    Console.WriteLine("3. Frost Nova");
                    break;
            }
        }


        public void ShowBattleStatus()
        {
            Console.WriteLine("[내 정보]");
            Console.WriteLine($"Lv.{Data.Level} {Data.Name} ({Data.ClassType})");
            Console.WriteLine($"HP {Data.CurHealth} / {Data.MaxHealth}");
            Console.WriteLine($"MP {Data.CurMana} / {Data.MaxMana}");
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
                return random.Next((int)((GetPlayerAttack() - deviation) * (1 + Data.CriticalDamage / 100)), (int)((GetPlayerAttack() + deviation) * (1 + Data.CriticalDamage / 100)));
            }
            //치명타 실패
            else
            {
                Console.WriteLine();
                return random.Next((int)GetPlayerAttack() - deviation, (int)GetPlayerAttack() + deviation);
            }


        }

        // 기타 함수

        // 데미지 받을 때
        public float TakeDamage(float damage)
        {
            Random random = new Random();
            //회피 성공, 받는 데미지 0
            if (random.Next(1, 100) <= Data.EvasionRate)
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
            Data.CurMana += manaRecovered;
            Console.WriteLine($"마나를 {manaRecovered} 회복하였습니다.");
            return Data.CurMana;
        }

        // 스킬 선택창
        public void SelectSkill()
        {
            switch (Data.ClassType)
            {
                case ClassType.Warrior:
                    Console.WriteLine("1. Power Strike(소모MP: 40)");
                    Console.WriteLine("2. Power Slam(소모MP: 25)");
                    Console.WriteLine("3. Double Down(소모MP: 20)");
                    break;
                case ClassType.Archer:
                    Console.WriteLine("1. Make it Rain(소모MP: 40)");
                    Console.WriteLine("2. Ace in the Hole(소모MP: 10)");
                    Console.WriteLine("3. Multi Shot(소모MP: 30)");
                    break;
                case ClassType.Magician:
                    Console.WriteLine("1. Chain Lightning(소모MP: 50)");
                    Console.WriteLine("2. Inferno Bomb(소모MP: 10)");
                    Console.WriteLine("3. Frost Nova(소모MP: 20)");
                    break;
            }
            Console.WriteLine($"\nMP {Data.CurMana}/{Data.MaxMana}");
        }

        // 스킬 데미지
        public int GetSkillDamage(int skillIndex)
        {
            int skillDamage = 0;

            switch (Data.ClassType)
            {
                case ClassType.Warrior:
                    switch (skillIndex)
                    {
                        case 1:
                            skillDamage = warriorSkill.AOEAttack();
                            break;
                        case 2:
                            skillDamage = warriorSkill.EmpoweredAttack();
                            break;
                        case 3:
                            skillDamage = warriorSkill.DoubleAttack();
                            break;
                    }
                    break;
                case ClassType.Archer:
                    switch (skillIndex)
                    {
                        case 1:
                            skillDamage = archerSkill.AOEAttack();
                            break ;
                        case 2:
                            skillDamage = archerSkill.EmpoweredAttack();
                            break;
                        case 3:
                            skillDamage = archerSkill.DoubleAttack();
                            break;
                    }
                    break;
                case ClassType.Magician:
                    switch (skillIndex)
                    {
                        case 1:
                            skillDamage = magicianSkill.AOEAttack();
                            break;
                        case 2:
                            skillDamage = magicianSkill.EmpoweredAttack();
                            break;
                        case 3:
                            skillDamage = magicianSkill.DoubleAttack();
                            break;
                    }
                    break;
            }
            return skillDamage;
        }

        // LevelUp 체크
        public bool CheckLevelUp()
        {
            bool isLevelUp = false;
            // 현재 Exp가 레벨업에 필요한 Exp보다 클 때
            while(Data.Exp > levelUpData[Data.Level])
            {
                // 설정 최대 레벨에 도달했을 때 더 레벨업 못하게
                if (Data.Level == levelUpData.Count) break;
                isLevelUp = true;
                // 레벨업 했을 때
                Data.Exp -= levelUpData[Data.Level++];
                Data.AttackPower += 0.5f;
                Data.DefensePower += 1f;
            }
            return isLevelUp;
        }
    }
}
