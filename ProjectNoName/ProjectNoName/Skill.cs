using System;

namespace ProjectNoName
{
    public class Skill
    {
        Player Player = DataManager.Instance().Player;

        public int SkillMana;

        public int PlayerDamage()
        {
            return DataManager.Instance().Player.GetPlayerDamage();
        }

        // 광역 공격
        public virtual int AOEAttack()
        {
            SkillMessage("범위 공격");
            SkillMana = 40;
            return CastSkill(1.2f);
        }

        // 일반 공격보다 강한 단일 공격
        public virtual int EmpoweredAttack()
        {
            SkillMessage("강화 공격");
            SkillMana = 10;
            return CastSkill(1.4f);
        }

        // 더 강한 단일 공격
        public virtual int DoubleAttack()
        {
            SkillMessage("이중 공격");
            SkillMana = 20;
            return CastSkill(2);
        }

        // 스킬 시전 메세지
        public void SkillMessage(string skillName)
        {
            Player player = DataManager.Instance().Player;
            Console.WriteLine($"{player.Data.Name}이(가) {skillName}을(를) 시전했다!");
        }

        // 스킬 마나 사용
        public int CastSkill(float damageRatio)
        {
            Player player = DataManager.Instance().Player;
            // 스킬 시전 성공; 마나 소모
            if ((player.Data.ManaAfterSkill -= SkillMana) > 0)
            {
                return (int)(PlayerDamage() * damageRatio);
            }
            // 스킬 시전에 필요한 마나 부족
            else
            {
                Console.WriteLine("마나가 부족합니다. 스킬을 사용할 수 없습니다.");
                return 0;
            }
        }
    }

    public class WarriorSkill : Skill
    {
        public override int AOEAttack()
        {
            SkillMessage("Power Strike");
            SkillMana = 40;
            return CastSkill(1.2f);
        }

        // 차별화 스킬
        public override int EmpoweredAttack()
        {
            SkillMessage("Power Slam");
            SkillMana = 25;
            return CastSkill(3);
        }

        public override int DoubleAttack()
        {
            SkillMessage("Double Down");
            SkillMana = 20;
            return CastSkill(2);
        }
    }

    public class ArcherSkill : Skill
    {
        public override int AOEAttack()
        {
            SkillMessage("Make it Rain");
            SkillMana = 40;
            return CastSkill(1.2f);
        }

        public override int EmpoweredAttack()
        {
            SkillMessage("Ace in the Hole");
            SkillMana = 10;
            return CastSkill(1.4f);
        }

        // 차별화 스킬
        public override int DoubleAttack()
        {
            SkillMessage("Multi Shot");
            SkillMana = 30;
            return CastSkill(4);
        }
    }

    public class MagicianSkill : Skill
    {
        // 차별화 스킬
        public override int AOEAttack()
        {
            SkillMessage("Chain Lightning");
            SkillMana = 50;
            return CastSkill(2);
        }

        public override int EmpoweredAttack()
        {
            SkillMessage("Inferno Bomb");
            SkillMana = 10;
            return CastSkill(1.4f);
        }

        public override int DoubleAttack()
        {
            SkillMessage("Frost Nova");
            SkillMana = 20;
            return CastSkill(2);
        }
    } 
}
