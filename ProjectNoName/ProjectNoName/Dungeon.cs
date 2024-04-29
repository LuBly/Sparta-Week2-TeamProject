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
                new Stage("쉬운 던전", 5, 1000f),
                new Stage("일반 던전", 11, 1700f),
                new Stage("어려운 던전", 17, 2500f),
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

            int idx = 1;
            foreach (Stage stage in dungeonStage)
            {
                Console.Write($"{idx++}. ");
                stage.ShowStageInfo();
            }

            return Utill.EndMenu();
        }

        public void ShowStage(int idx)
        {
            dungeonStage[idx - 1].GetStageResult();
            Utill.EndMenu();
        }
    }
}
