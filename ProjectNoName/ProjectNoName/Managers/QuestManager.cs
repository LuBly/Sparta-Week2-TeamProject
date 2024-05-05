namespace ProjectNoName
{
    public class QuestManager
    {
        public List<Quest> QuestList = new List<Quest>()
        {
            new Quest(),
            new Quest(1, "낡은 검 구매하기", "낡은 검 구매하기", QuestType.Collect, 1, QuestProgress.NoStart, 5, 100, 4),
            new Quest(2, "미니언 쳐 부수기", "미니언 2마리를 나락보내보자", QuestType.Battle, 2, QuestProgress.NoStart, 50, 50, 0),
        };

        public List<Quest> OnGoingBattleQuest = new List<Quest>();
    }
}