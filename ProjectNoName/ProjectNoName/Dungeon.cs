namespace ProjectNoName
{
    internal class Dungeon
    {
        List<Stage> dungeonStage;
        bool isFirst = true;
        // 던전 스테이지 정보 입력 DB 생성
        void CreateStage()
        {
            dungeonStage = new List<Stage>() {
                new Stage(),
                new TutorialStage("튜토리얼 던전", 5, 1000f), // << 준호님 담당
                new MainStage("일반 던전", 11, 1700f), // << 제가 담당해서 만들어 보는것
            };
        }
        public int ShowDungeon()
        {
            Console.Clear();
            if (isFirst)
            {
                CreateStage();
                isFirst = false;
            }

            Console.WriteLine("[던전입장]\n");

            for(int i = 1; i < dungeonStage.Count; i++)
            {
                Console.Write($"{i}. ");
                dungeonStage[i].ShowStageInfo();
            }

            return Utill.EndMenu();
        }

        public void ShowStage(int idx)
        {
            dungeonStage[idx].StartBattle();            
            Utill.EndMenu();
        }
    }
}
