namespace ProjectNoName
{
    public class Monster
    {
        // 몬스터의 공통 데이터
        // 상속받는 하위 Class에서만 접근 가능하도록 Protected로 선언
        
        public int monsterLv { get; set; }
        public string monsterName { get; set; }
        public float monsterHealth { get; set; }
        public float monsterAttackPower { get; set; }
        public float monsterDefensePower { get; set; }
        public int rewardExp { get; set; }
        public MonsterData Data = new MonsterData();

        protected int monsterId;
        public Monster() { }

        // 몬스터 각 개체의 정보를 출력하는 함수
        public void ShowMonsterData()
        {
            int originRow = Console.CursorTop;
            Console.Write($"Lv.{Data.Level} {Data.Name}");
            Console.SetCursorPosition(25,originRow);
            if(Data.Health <= 0 )
            {
                Console.WriteLine($"Dead");
            }
            else
            {
                Console.WriteLine($"HP : {Data.Health}");
            }
        }

        public float TakeDamage(float damage)
        {
            Data.Health -= damage;
            return Data.Health;
        }

        // 각 하위 개체별로 드랍 아이템 및 확률에 따른 드랍
        public virtual int CreateMonsterGoldReward()
        {
            return Data.RewardGold;
        }

        public virtual List<Item> CreateMonsterItemReward()
        {
            List<Item> rewardItemList = new List<Item>();
            Random random = new Random();
            int idx = random.Next(0, Data.RewardItems.Count);
            rewardItemList.Add(Data.RewardItems[idx]);
            return rewardItemList;
        }
    }
}


