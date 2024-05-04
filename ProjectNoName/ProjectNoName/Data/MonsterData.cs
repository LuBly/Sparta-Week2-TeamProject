namespace ProjectNoName
{
    public class MonsterData
    {
        public int MonsterId;
        public string MonsterType;
        public int Level;
        public string Name;
        public float Health;
        public float AttackPower;
        public int RewardExp;
        public int RewardGold;
        public List<Item> RewardItems = new List<Item>();
    }
}
