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

        public Monster() { }
        
        // 몬스터 각 개체의 정보를 출력하는 함수
        public void ShowMonsterData()
        {
            int originRow = Console.CursorTop;
            Console.Write($"Lv.{monsterLv} {monsterName}");
            Console.SetCursorPosition(18,originRow);
            if(monsterHealth <= 0 )
            {
                Console.WriteLine($"Dead");
            }
            else
            {
                Console.WriteLine($"HP : {monsterHealth}");
            }
        }

        public float TakeDamage(float damage)
        {
            monsterHealth -= damage;
            return monsterHealth;
        }
    }


}


