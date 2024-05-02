namespace ProjectNoName
{
    public class Void : Monster
    {
        public Void(int level, string name, float health, float attackPower)
        {
            monsterLv = level;
            monsterName = name;
            monsterHealth = health;
            monsterAttackPower = attackPower;
            //rewardExp = exp;
        }
    }
}
