namespace ProjectNoName
{
    public class Void : Monster
    {
        int monsterId = 1;
        public Void()
        {
            Data = DataManager.Instance().MonsterDB[monsterId];
        }
    }
}
