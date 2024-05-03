namespace ProjectNoName
{
    public class CannonMinion : Monster
    {
        int monsterId = 2;
        public CannonMinion()
        {
            MonsterData monsterData = DataManager.Instance().MonsterDB[monsterId];
            Data.MonsterId = monsterId;
            Data.MonsterType = monsterData.MonsterType;
            Data.Level = monsterData.Level;
            Data.Name = monsterData.Name;
            Data.Health = monsterData.Health;
            Data.AttackPower = monsterData.AttackPower;
            Data.RewardExp = monsterData.RewardExp;
            Data.RewardItems = monsterData.RewardItems;
        }
    }
}
