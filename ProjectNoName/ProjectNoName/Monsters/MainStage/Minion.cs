namespace ProjectNoName
{
    public class Minion : Monster
    {
        int monsterId = 0;
        public Minion()
        {
            Data = DataManager.Instance().MonsterDB[monsterId];
        }
    }
}
