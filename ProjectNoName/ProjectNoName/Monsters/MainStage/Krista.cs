namespace ProjectNoName
{
    public class Krista : Monster
    {
        int monsterId = 6;
        public Krista()
        {
            Data = DataManager.Instance().MonsterDB[monsterId];
        }
    }
}
