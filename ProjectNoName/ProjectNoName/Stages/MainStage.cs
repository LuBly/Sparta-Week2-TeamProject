namespace ProjectNoName
{
    enum BattleMenuType
    {
        Attack = 1,
        // 추가
    }

    public class MainStage : Stage
    {
        public MainStage(string name, int recommendedDefense, float clearReward)
        {
            stageName = name;
            stageRecommendedDefense = recommendedDefense;
            stageClearReward = clearReward;
        }
        MonsterManager monsterManager = new MonsterManager();
        Player player = DataManager.Instance().Player;

        // battle에서 사용할 몬스터 List
        List<Monster> battleMonsters; 

        // Stage에서 구현된 StartBattle 함수를 override하여 작성
        public override void StartBattle()
        {
            battleMonsters = monsterManager.SetStageMonster(1);
            bool isPlayerTurn = true;
            // Battle 시작
            while (true)
            {
                // 콘솔 초기화
                Console.Clear();
                // battle이 끝났는지 체크 == while 탈출 조건
                if (CheckBattleEnd())
                {
                    Console.WriteLine(isPlayerWin);
                    break;
                }

                // 플레이어 턴이라면
                if (isPlayerTurn)
                {
                    ShowPlayerTurn();
                    isPlayerTurn = false;
                }
                
                // Enemy 턴이라면
                else
                {
                    // EnemyTurn 보여주기
                    ShowEnemyTurn();
                    isPlayerTurn = true;
                }
            }
            // 결과창 출력
            ShowStageResult();
        }
        //Player Turn 관련 함수
        void ShowPlayerTurn()
        {
            Console.Clear();
            Console.WriteLine("Battle!");
            Console.WriteLine();
            Console.WriteLine("[PlayerTurn]");
            Console.WriteLine();
            // 몬스터 정보 출력 _ 턴이 끝난뒤로 계속 변동
            Console.WriteLine("[몬스터 정보]");
            for(int i = 1; i < battleMonsters.Count; i++)
            {
                battleMonsters[i].ShowMonsterData();
            }

            Console.WriteLine();
            player.ShowBattleStatus();

            Console.WriteLine("\n1. 공격 \n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            BattleMenuType choiceIdx = (BattleMenuType)int.Parse(Console.ReadLine());

            switch (choiceIdx)
            {
                case BattleMenuType.Attack:
                    // Attack 페이지 오픈
                    ShowPlayerAttack();
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 ㄱㄱ");
                    Thread.Sleep(500);
                    ShowPlayerTurn();
                    break;
            }
        }

        void ShowPlayerAttack()
        {
            Console.Clear();
            Console.WriteLine("Battle!\n");

            // 몬스터 정보 출력 _ idx 포함
            Console.WriteLine("[몬스터 정보]");
            for(int i = 1; i < battleMonsters.Count; i++)
            {
                Console.Write($"{i} ");
                battleMonsters[i].ShowMonsterData();
            }

            Console.WriteLine();
            player.ShowBattleStatus();

            Console.WriteLine("\n0. 취소 \n");
            Console.WriteLine("대상을 선택해주세요.");
            Console.Write(">>");
            int monsterIdx = int.Parse(Console.ReadLine());
            // 취소일경우
            if(monsterIdx == 0)
            {
                // 다시 PlayerTurn을 보여주는 함수 실행
                ShowPlayerTurn();
            }
            // 범위 내의 값을 선택한 경우
            else if (monsterIdx > 0 && monsterIdx < battleMonsters.Count)
            {
                AttackMonster(monsterIdx);
            }
            // 범위 밖의 값을 선택한 경우
            else
            {
                Console.WriteLine("\n[잘못된 선택입니다!]");
                Thread.Sleep(500);
                ShowPlayerAttack();
            }
        }

        void AttackMonster(int monsterIdx)
        {
            Console.Clear();
            Monster curMonster = battleMonsters[monsterIdx];
            Console.WriteLine($"{player.Data.Name} 의 공격!");
            int playerDamage = player.GetPlayerDamage();  // *치명타 문구 출력*
            Console.WriteLine($"Lv.{curMonster.Data.Level} {curMonster.Data.Name} 을(를) 맞췄습니다. [데미지 : {playerDamage}]");
            Console.WriteLine();
            Console.WriteLine($"Lv.{curMonster.Data.Level} {curMonster.Data.Name}");
            
            Console.Write($"HP {curMonster.Data.Health} -> ");
            // 데미지 처리 이후 체력이 0 이하라면 사망 처리
            if (curMonster.TakeDamage(playerDamage) <= 0)
            {
                Console.WriteLine("Dead");
            }
            //아니라면 체력 표시
            else
            {
                Console.WriteLine(curMonster.Data.Health);
            }
            Utill.ShowNextPage();
        }

        //Enemy Turn 관련 함수
        void ShowEnemyTurn()
        {
            for (int i = 1; i < battleMonsters.Count; i++)
            {
                // 살아있는 몬스터만 캐릭터를 공격
                if (battleMonsters[i].Data.Health > 0)
                    AttackPlayer(battleMonsters[i]);
            }
        }

        void AttackPlayer(Monster monster)
        {
            Player player = DataManager.Instance().Player;
            Console.Clear();
            Console.WriteLine("Battle!");
            Console.WriteLine();
            Console.WriteLine("[EnemyTurn]");
            Console.WriteLine();
            Console.WriteLine($"Lv.{monster.Data.Level} {monster.Data.Name} 의 공격!");
            player.TakeDamage(monster.Data.AttackPower);
            Utill.ShowNextPage();
        }

        // battle이 끝났는지 체크
        bool CheckBattleEnd()
        {
            bool isEnd = false;
            // 플레이어의 체력이 0 이하로 떨어짐 == Fail
            if(player.Data.CurHealth <= 0)
            {
                isEnd = true;
                isPlayerWin = false;
            }
            // battleMonsters 체력이 모두 0 이하로 떨어짐 == success
            else if (CheckAllMonsterDie())
            {
                isEnd = true;
                isPlayerWin = true;
            }
            return isEnd;
        }
        // Monster가 모두 죽었는지 체크
        bool CheckAllMonsterDie()
        {
            bool isAllDie = true;
            for(int i = 1; i < battleMonsters.Count; i++)
            {
                if (battleMonsters[i].Data.Health > 0)
                {
                    isAllDie = false;
                    break;
                }
            }
            return isAllDie;
        }

        protected override void StageClear()
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            Console.WriteLine("Victory");
            Console.WriteLine();
            Console.WriteLine($"{stageName}에서 몬스터 {battleMonsters.Count - 1}마리를 잡았습니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Data.Level} {player.Data.Name}");
            Console.WriteLine($"HP {originHealth} -> {player.Data.CurHealth}");
            // 보상 출력 추가
            Utill.ShowNextPage();
        }

        protected override void StageFail()
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            Console.WriteLine("You Lose");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Data.Level} {player.Data.Name}");
            Console.WriteLine($"HP {originHealth} -> {player.Data.CurHealth}");
            Utill.ShowNextPage();
        }
    }
}
