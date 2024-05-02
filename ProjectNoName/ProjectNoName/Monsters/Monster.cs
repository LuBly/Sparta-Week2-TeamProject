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

        public Monster() { }
        
        // ���� �� ��ü�� ������ ����ϴ� �Լ�
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


