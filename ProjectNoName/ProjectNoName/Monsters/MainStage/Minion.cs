namespace ProjectNoName
{
    public class Minion : Monster
    {
        int monsterId = 0;
        public Minion()
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
