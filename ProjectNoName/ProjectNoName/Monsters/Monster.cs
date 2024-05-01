namespace ProjectNoName
{
    public class Monster
    {
        // 몬스터의 공통 데이터
        // 상속받는 하위 Class에서만 접근 가능하도록 Protected로 선언
        protected int monsterLv { get; set; }
        protected string monsterName { get; set; }
        protected float monsterHealth { get; set; }
        protected float monsterAttackPower { get; set; }
        protected float monsterDefensePower { get; set; }
        protected int monsterExp { get; set; }

        public Monster() { }

        
    }


}


