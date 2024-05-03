namespace ProjectNoName
{
    public class CannonMinion : Monster
    {
        int monsterId = 2;
        public CannonMinion()
        {
            Data = DataManager.Instance().MonsterDB[monsterId];
        }
    }
}
