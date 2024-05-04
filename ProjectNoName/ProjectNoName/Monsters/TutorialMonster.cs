using System.Diagnostics.Metrics;
using System;

namespace ProjectNoName
{
    public class TutorialMonster : Monster
    {
        public TutorialMonster() { }

        public int tutorialMonsterLv { get; set; }
        public string tutorialMonsterName { get; set; }
        public float tutorialMonsterMaxHealth { get; set; }
        public float tutorialMonsterHealth { get; set; }
        public float tutorialMonsterAttackPower { get; set; }
        public float tutorialMonsterDefensePower { get; set; }
        public int tutorialMonsterrewardExp { get; set; }

        public TutorialMonster(int lv, string name, float maxHealth, float health, float attackPower, float defensePower, int exp)
        {
            tutorialMonsterLv = lv;
            tutorialMonsterName = name;
            tutorialMonsterMaxHealth = maxHealth;
            tutorialMonsterHealth = health;
            tutorialMonsterAttackPower = attackPower;
            tutorialMonsterDefensePower = defensePower;
            tutorialMonsterrewardExp = exp;
        }        

        //몬스터 공격          
        public float CounterAttack()
        {
            return tutorialMonsterAttackPower * 0.1f;
        }

        //몬스터 피해 - 몬스터의 방어력에 영향받음
        public float TakeHit(float takeDamage)
        {
            if (tutorialMonsterDefensePower >= takeDamage)
            {                
                return 1;
            }
            else
            {                
                return takeDamage - tutorialMonsterDefensePower;
            }
        }
    }
}
