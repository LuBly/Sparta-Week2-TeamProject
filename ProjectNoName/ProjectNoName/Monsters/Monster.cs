namespace ProjectNoName
{
    public class Monster
    {
        // ������ ���� ������
        // ��ӹ޴� ���� Class������ ���� �����ϵ��� Protected�� ����
        
        public int monsterLv { get; set; }
        public string monsterName { get; set; }
        public float monsterHealth { get; set; }
        public float monsterAttackPower { get; set; }
        public float monsterDefensePower { get; set; }
        public int rewardExp { get; set; }
        public MonsterData Data = new MonsterData();

        public Monster() { }

        // ���� �� ��ü�� ������ ����ϴ� �Լ�
        public void ShowMonsterData()
        {
            int originRow = Console.CursorTop;
            Console.Write($"Lv.{Data.Level} {Data.Name}");
            Console.SetCursorPosition(18,originRow);
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
    }


}


