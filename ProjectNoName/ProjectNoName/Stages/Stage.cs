﻿namespace ProjectNoName
{
    public class Stage
    {
        // 필요한 정보
        // 이름
        public string stageName;
        // 권장 방어력
        public int stageRecommendedDefense;
        // 클리어 보상
        public float stageClearReward;

        // 승부 여부를 저장
        protected bool isPlayerWin;
        // 기존 체력 저장
        protected float originHealth = DataManager.Instance().Player.Data.CurHealth;
        // 기존 마나 저장
        protected float originMana = DataManager.Instance().Player.Data.CurMana;

        public Stage() { }

        public virtual void ShowStageInfo()
        {
            int originRow = Console.CursorTop;
            // 이름
            Console.Write($"{stageName}");
            // 권장 방어력
            Console.SetCursorPosition(20, originRow);
            Console.WriteLine($"| 방어력 {stageRecommendedDefense} 이상 권장");

            Console.SetCursorPosition(50, originRow);
            Console.WriteLine($"| 보상 {stageClearReward} G");
        }

        public virtual void StartBattle()
        {

        }

        public void ShowStageResult()
        {
            // 던전 클리어 
            if (isPlayerWin)
            {
                StageClear();
            }

            // 던전 실패
            else
            {
                StageFail();
            }
        }

        // 클리어 여부 판단 (방어력)
        protected virtual bool CheckClear()
        {
            bool isClear = true;
            return isClear;
        }

        /// <summary>
        ///  아래 함수는 전부 변경 필요
        ///  참고해서 변경해도 좋고, 아예 새로 작성하셔도 좋습니다.
        ///  다만 Player의 경우 모두 DataManager에서 불러오는식으로 작성해주시면 됩니다.
        /// </summary>
        // 성공시 실행 함수
        protected virtual void StageClear()
        {
            Player player = DataManager.Instance().Player;
            Console.Clear();
            Console.WriteLine("축하합니다!!");
            Console.WriteLine($"{stageName}을 클리어 하였습니다.");


            Console.WriteLine("\n[탐험 결과]");
            Console.WriteLine($"체력 {player.Data.CurHealth} -> {ChangeHealth()}");
            Console.WriteLine($"골드 {player.Data.Gold} -> {ChangeGold(player)}");
            //player.EditLevel();
        }

        // 실패시 실행 함수
        protected virtual void StageFail()
        {
            Player player = DataManager.Instance().Player;
            Console.Clear();
            Console.WriteLine("힝잉잉!!");
            Console.WriteLine($"{stageName}에 실패했습니다.");


            Console.WriteLine("\n[탐험 결과]");
            //Console.WriteLine($"체력 {player.Data.CurHealth} -> {player.TakeDamage()}");
        }

        // stage별 hp 변화량 계산
        float ChangeHealth()
        {
            Player player = DataManager.Instance().Player;
            // 기본 체력 감소량
            Random random = new Random();
            // 20 ~ 35 랜덤
            float minusHp = random.Next(20, 36);
            float playerDeffence = player.GetPlayerDefence();
            float changeValue = playerDeffence - stageRecommendedDefense;
            float damage = minusHp + changeValue;
            player.Data.CurHealth -= damage;

            return player.Data.CurHealth;
        }

        // 보상 골드 변화량 계산
        float ChangeGold(Player player)
        {
            Random random = new Random();
            float playerAttack = player.GetPlayerAttack();
            int plusGoldRate = random.Next((int)playerAttack, (int)playerAttack * 2);
            stageClearReward += stageClearReward * (plusGoldRate / 100f);
            player.Data.Gold += stageClearReward;
            return player.Data.Gold;
        }
    }
}


