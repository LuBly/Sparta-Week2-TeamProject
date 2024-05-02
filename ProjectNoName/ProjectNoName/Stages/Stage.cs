namespace ProjectNoName
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

        public Stage() { }

        // 아래 내용들은 하위 클래스에 생성
        /*
        public Stage(string stageName, int stageRecommendedDefense, float stageClearReward)
        {
            this.stageName = stageName;
            this.stageRecommendedDefense = stageRecommendedDefense;
            this.stageClearReward = stageClearReward;
        }
        */

        public void ShowStageInfo()
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


