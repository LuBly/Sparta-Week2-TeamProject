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
        
        // MonsterData
        // + MonsterType : string
        // + Level : int
        // + Name : string
        // + Health : float
        // + AttackPower : float
        // + rewardExp : int
        public Monster CreateMonster(Monster monster)
        {
            Monster newMonster = new Monster();
            newMonster.monsterLv = monster.monsterLv;
            newMonster.monsterName = monster.monsterName;
            newMonster.monsterHealth = monster.monsterHealth;
            newMonster.monsterAttackPower = monster.monsterAttackPower;
            newMonster.monsterDefensePower = monster.monsterDefensePower;
            newMonster.rewardExp = monster.rewardExp;

            return newMonster;
        }

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


