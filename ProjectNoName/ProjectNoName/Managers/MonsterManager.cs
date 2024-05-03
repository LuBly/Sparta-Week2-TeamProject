namespace ProjectNoName
{
    public class MonsterManager
    {
        List<MonsterData> MonsterDB = DataManager.Instance().MonsterDB;

        // 몬스터 생성
        Monster CreateMonster(int id)
        {
            string curMonsterType = MonsterDB[id].MonsterType;
            
            switch(curMonsterType)
            {
                case "Minion":
                    return new Minion();
                case "Void":
                    return new Void();
                case "CannonMinion":
                    return new CannonMinion();
                case "Varon":
                    return new Varon();
                case "Junn":
                    return new Junn();
                case "JaeH":
                    return new JaeH();
                case "Krista":
                    return new Krista();
                default:
                    return new Monster();

            }
        }

        // 스테이지별 몬스터 생성
        // 1스테이지 0~2 중에 1~4마리선택
        // 2스테이지 1~3 중에 1~4마리선택
        // 3스테이지 2~4 중에 1~4마리선택
        // 4스테이지 3~5 중에 1~4마리선택
        // 5스테이지 보스(되면) BossMoster[stageIdx/5] 생성
        // n스테이지 n-1 ~ n+1 중에 선택
        public List<Monster> SetStageMonster(int stageidx)
        {
            List<Monster> battleMonsters = new List<Monster>()
            {
                // idx를 맞추기 위한 빈데이터 입력
                new Monster()
            };

            // 1~4마리의 몬스터를 소환
            Random random = new Random();
            int monsterCount = random.Next(1, 5);

            // monster List에 저장되어있는 몬스터중 1~4마리의 몬스터를 선택
            for (int i = 0; i < monsterCount; i++)
            {
                // 랜덤 인덱스 생성 (0부터 monster의 저장된 객체들의 수(3)만큼)
                int monsterId = random.Next(stageidx-1, stageidx+2);
                // Battle에서 사용할 몬스터 List에 추가
                battleMonsters.Add(CreateMonster(monsterId));
            }

            return battleMonsters;
        }
    }
}
