namespace ProjectNoName
{
    public class JaeH : Monster
    {
        int monsterId = 5;
        public JaeH()
        {
            Data = DataManager.Instance().MonsterDB[monsterId];
        }
    }
}
