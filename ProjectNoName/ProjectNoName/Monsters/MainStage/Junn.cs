namespace ProjectNoName
{
    public class Junn : Monster
    {
        int monsterId = 4;
        public Junn()
        {
            Data = DataManager.Instance().MonsterDB[monsterId];
        }
    }
}
