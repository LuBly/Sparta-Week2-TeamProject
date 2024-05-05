namespace ProjectNoName
{
    public enum QuestType
    {
        Collect,
        Battle
    }

    public enum QuestProgress
    {
        NoStart,
        OnGoing,
        Completion
    }
    //[ Quest [Data] ]
    public class Quest
    {
        public QuestData Data = new QuestData();
        public int KillCount = 0;

        public Quest() { }
        
        public Quest(int questId, string questName, string questDescription, QuestType questType, int CompleteCondition, QuestProgress questProgress, int rewardExp, int rewardGold , int targetId)
        {
            Data.QuestId = questId;
            Data.QuestName = questName;
            Data.QuestDescription = questDescription;
            Data.QuestType = questType;
            Data.CompletCondition = CompleteCondition;
            Data.QuestProgress = questProgress;
            Data.RewardExp = rewardExp;
            Data.RewardGold = rewardGold;
            
            switch(questType) 
            {
                case QuestType.Collect:
                    Data.TargetItemId = targetId;
                    break;
                case QuestType.Battle:
                    Data.TargetMonsterId = targetId;
                    break;
            }
        }
        // 수락
        public void AcceptBattleQuest()
        {
            Data.QuestProgress = QuestProgress.OnGoing;
            DataManager.Instance()._QuestManager.OnGoingBattleQuestList.Add(this);
        }

        public void UpdateKillCount(int monsterId)
        {
            if(monsterId == Data.TargetMonsterId)
                KillCount++;
        }

        public void ClearQuest()
        {
            Data.QuestProgress = QuestProgress.Completion;
            RewardQuest();
            if(Data.QuestType == QuestType.Battle)
                DataManager.Instance()._QuestManager.OnGoingBattleQuestList.Remove(this);
        }

        void RewardQuest()
        {
            DataManager.Instance().Player.Data.Exp += Data.RewardExp;
            DataManager.Instance().Player.Data.Gold += Data.RewardGold;
        }

        public int GetInventoryItemCount()
        {
            return DataManager.Instance().Player.Data.Inventory.CountItems(Data.TargetItemId);
        }
        

        /*
        1. QuestAccept
            1. Quest 내부에 KillCount(count) 생성
            
        2. Stage진행중에 Id에 해당하는 몬스터가 죽을때 
            1. 몬스터 사망시에 "진행중"인 Quest가 있는지 체크
            2. Id 체크
            3. Quest에 있는 Id라면 count++

        3. QuestPage를 열었을 때 현재 킬카운트 호출 및 출력.

        Quest도 저장?? 'ㄷ'
         */

    }
}
