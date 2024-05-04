namespace ProjectNoName
{
    public class Void : Monster
    {
        int monsterId = 1;
        public Void()
        {
            MonsterData monsterData = DataManager.Instance().MonsterDB[monsterId];
            Data.MonsterId = monsterId;
            Data.MonsterType = monsterData.MonsterType;
            Data.Level = monsterData.Level;
            Data.Name = monsterData.Name;
            Data.Health = monsterData.Health;
            Data.AttackPower = monsterData.AttackPower;
            Data.RewardGold = monsterData.RewardGold;
            Data.RewardExp = monsterData.RewardExp;
            Data.RewardItems = monsterData.RewardItems;
        }
    }
}
