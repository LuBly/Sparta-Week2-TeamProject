namespace ProjectNoName
{
    public class CannonMinion : Monster
    {
        public CannonMinion(int level, string name, float health, float attackPower)
        {
            monsterLv = level;
            monsterName = name;
            monsterHealth = health;
            monsterAttackPower = attackPower;
            //rewardExp = exp;
        }
    }
}
