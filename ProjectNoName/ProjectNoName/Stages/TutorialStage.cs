namespace ProjectNoName
{
    public class TutorialStage : Stage
    {
        public TutorialStage(string name, int recommendedDefense, float clearReward)
        {
            stageName = name;
            stageRecommendedDefense = recommendedDefense;
            stageClearReward = clearReward;
        }


        /// <summary>
        /// 이 곳에 전투 관련 함수를 작성해주시면 됩니다.
        /// 
        
        // Stage내부의 함수(virtual만 가능)를 수정하고 싶으시면 이런식으로 수정해주시면 됩니다.
        protected override bool CheckClear()
        {
            bool isClear = false;
            return isClear;
        }

        public void CreateStage()
        {
            
        }
        //스테이지 진입
        /*
        public void EnterStage()
        {
            Dungeon dungeon = DataManager.Instance().Dungeon;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("진입하시겠습니까?\n");
                Console.WriteLine("쉬운 던전\n");
                Console.WriteLine("일반 던전\n");
                Console.WriteLine("0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                int stageChoiceIdx = int.Parse(Console.ReadLine());
                if (stageChoiceIdx > 2() || stageChoiceIdx < 0)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500);
                    continue;
                }
                else
                {
                    switch (stageChoiceIdx)
                    {
                        case 0:
                            break;
                        case 1:
                            BattleScene()
                            break;
                        case 2:
                            BattleScene()
                            break;
                    }
                }
            }
        }
        
        //전투 페이지
        public void BattleScene()
        {
            Console.Clear();
            Console.WriteLine("[Battle!!]\n");
            Player player = DataManager.Instance().Player;
            //몬스터출력
            Monster.EncounterMonster();

            //플레이어 정보 출력
            Console.WriteLine("[내정보]\n");
            Console.WriteLine($"Lv.{player.level}  {player.stageName} ({player.classType})");
            Console.WriteLine($"HP 100/{player.health}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 방어");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
        }

        //플레이어 전투시 선택지
        public enum Action
        {
            Exit = 0,
            Attack = 1,
            Defense = 2
        }

        //전투 메커니즘 *
        public float BattleLogic()
        {
            while (true)
            {
                BattleScene();
                //몬스터 인덱스 번호 필요 + 추가로 입력값이 인덱스번호와 매칭되게

                int batlleChoiceIdx = int.Parse(Console.ReadLine());
                if (batlleChoiceIdx > 2() || batlleChoiceIdx < 0)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500);
                    continue;
                }
                else
                {
                    switch (batlleChoiceIdx)
                    {
                        case Action.Exit:
                            break;
                        case Action.Attack:
                            ChoiceEnemy();
                            AttackEnemy();
                            break;
                        case Action.Defense:
                            Defend();
                            break;
                    }
                }
                //전투진행여부 판단
                //{1.최종결과창
                //2.중간결산창-몬스터반격(공격함수)로직}
                ContinueBattle()

                    //플레이어 턴을 확인하는 변수 필요
            }
        }

        //방어함수
        public int Defend()
        {
            //몬스터 정보 불러오기
            Player player = DataManager.Instance().Player;
            int hurtValue = player.GetPlayerHealth() - Monster monster[n].CounterAttack();
            return hurtValue;
        }

        //공격함수
        public int AttackEnemy()
        {
            Player player = DataManager.Instance().Player;
            Monster monster[].monsterDefensePower;
            int Damagevalue = Monster monster[].monsterHealth;
            return Damagevalue -= Math.Ceiling(player.GetPlayerAttack())

        }

        //(적 선택지) *
        public void ChoiceEnemy(int target)
        {
            Console.WriteLine("[Battle!!]\n");
            //몬스터출력
            for (int j = 1; j < 4; j++)
            {
                Console.Write("-");
                Console.Write($" {j} ");
                stageEncounters[];
            }
            //플레이어 정보 출력
            Console.WriteLine("[내정보]\n");
        }

        //전투 진행 여부 파악
        public bool ContinueBattle()
        {
            //플레이어가 체력을 전부 소진 && 적이 하나라도 생존해있다면 게임 진행
            Player player = DataManager.Instance().Player;
            if (player.GetPlayerHealth() > 0 && enemy.count > 0)) //카운트변수 선언할당 해야함.
            {
                CalculateMiddleResult();
                return true;
                continue;
            }
            else
            {
                CalculateFinalResult();
                return false;
                //작동안하면 break; ?
            }
        }

        //중간결산창
        public void CalculateMiddleResult()
        {
            Console.Clear();
            Player player = DataManager.Instance().Player;
            Random random = new Random();
            if (playerTurn == true)
            {
                Console.WriteLine("[Battle!!]\n");

                Console.WriteLine($"{player.stageName}의 공격!\n");
                Console.WriteLine($"Lv.{unknownLevel} {unknownName}을(를) 맞췄습니다. [데미지 : {random.Next(player.GetPlayerAttack() * 0.9, player.GetPlayerAttack() * 1.1)}]";
                Console.WriteLine($"Lv.{unknownLevel} {unknownName}\n");
                Console.WriteLine($"HP {unKnownMaxHealth}/{unknownHealth}");
            }
            else
            {
                Console.WriteLine("[Battle!!]\n");

                Console.WriteLine($"{unknownName}의 공격!\n");
                Console.WriteLine($"Lv.{unknownLevel} {unknownName}을(를) 맞췄습니다. [데미지 : {.}])");
                Console.WriteLine($"Lv.{unknownLevel} {unknownName}\n");
                Console.WriteLine($"HP {unKnownMaxHealth}/{unknownHealth}");
            }
            Console.WriteLine("0. 다음\n");
            Console.WriteLine();
            Console.WriteLine();
            NextButton()
        }

        //최종결산창 - 경험치 요소 추가
        public void CalculateFinalResult()
        {
            Console.Clear();
            Player player = DataManager.Instance().Player;

            //승리
            if (player.GetPlayerHealth > 0 || enemy.count <= 0)
            {
                Console.WriteLine("[Battle!!]\n");

                Console.WriteLine("Victory\n");
                Console.WriteLine($"던전에서 몬스터 {enemyCount}를 잡았습니다.");
                Console.WriteLine($"Lv.{player.level} {player.stageName}\n");
                Console.WriteLine($"HP 100/{player.GetPlayerHealth()}");
                Console.WriteLine("0. 다음\n");
                Console.WriteLine();
                Console.WriteLine();
                NextButton()
            }
            //패배
            else
            {
                Console.WriteLine("[Battle!!]\n");

                Console.WriteLine("You Lose\n");

                Console.WriteLine($"Lv.{player.level} {player.stageName}\n");
                Console.WriteLine($"HP 100/{player.GetPlayerHealth()}");
                Console.WriteLine("0. 다음\n");
                Console.WriteLine();
                Console.WriteLine();
                NextButton()
            }
        }

        //결산창 넘기기
        public void NextButton()
        {
            while (true)
            {
                int nextButton = int.Parse(Console.ReadLine());
                if (nextButton == 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500);
                    continue;
                }
            }
        }
        */
    }
}
