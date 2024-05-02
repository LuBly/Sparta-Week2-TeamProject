namespace ProjectNoName
{
    public class Minion : Monster
    {
        public Minion(int level, string name, float health, float attackPower)
        {
            monsterLv = level;
            monsterName = name;
            monsterHealth = health;
            monsterAttackPower = attackPower;
            //rewardExp = exp;
        }
    }
}
