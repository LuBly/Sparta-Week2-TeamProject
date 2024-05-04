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
            Data.RewardGold = monsterData.RewardGold;
            Data.RewardExp = monsterData.RewardExp;
            Data.RewardItems = monsterData.RewardItems;
        }

        public override int CreateMonsterGoldReward()
        {
            return Data.RewardGold;
        }

        public override List<Item> CreateMonsterItemReward()
        {
            List<Item> rewardItemList = new List<Item>();
            Random random = new Random();
            int idx = random.Next(0, Data.RewardItems.Count);
            rewardItemList.Add(Data.RewardItems[idx]);
            return rewardItemList;
        }
    }
}
