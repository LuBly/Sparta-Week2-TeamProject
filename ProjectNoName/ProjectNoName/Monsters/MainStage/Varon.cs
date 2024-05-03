namespace ProjectNoName
{
    public class Varon : Monster
    {
        int monsterId = 3;
        public Varon()
        {
            Data = DataManager.Instance().MonsterDB[monsterId];
        }
    }
}
