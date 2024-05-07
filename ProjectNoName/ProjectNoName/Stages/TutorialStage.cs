using System;

namespace ProjectNoName
{
    public class TutorialStage : Stage
    {
        TutorialMonster monsterManage = new TutorialMonster();
        Player player = DataManager.Instance().Player;
        public bool playerTurn = true;

        public TutorialStage(string name, int recommendedDefense, float clearReward)
        {
            stageName = name;
            stageRecommendedDefense = recommendedDefense;
            stageClearReward = clearReward;
        }


        /// <summary>
        /// 이 곳에 전투 관련 함수를 작성해주시면 됩니다.
        /// 
        //Console.WriteLine(monsterList[0]);
        // Stage내부의 함수(virtual만 가능)를 수정하고 싶으시면 이런식으로 수정해주시면 됩니다.
        protected override bool CheckClear()
        {
            bool isClear = false;
            return isClear;
        }

        public override void StartBattle()
        {
            BattleLogic();
            //플레이어가 던전에서 패배하고 나왔을 경우
            if (player.Data.CurHealth <= 0)
            { player.Data.CurHealth = 1; }
        }

        //등장 몬스터 new TutorialMonster
        public TutorialMonster[] generateMonster = new TutorialMonster[18]
        {
            //1번그룹
            new TutorialMonster(1, "슬라임", 10, 10, 10, 1, 1),
            new TutorialMonster(1, "동굴박쥐", 15, 15, 20, 3, 1),
            new TutorialMonster(2, "고블린", 20, 20, 30, 3, 2),
            new TutorialMonster(3, "동굴거미", 20, 20, 30, 4, 3),
            new TutorialMonster(4, "하피", 35, 35, 50, 2, 4),
            new TutorialMonster(5, "오크", 50, 50, 50, 5, 5),
            //2번그룹
            new TutorialMonster(1, "슬라임", 10, 10, 10, 1, 1),
            new TutorialMonster(1, "동굴박쥐", 15, 15, 20, 3, 1),
            new TutorialMonster(2, "고블린", 20, 20, 30, 3, 2),
            new TutorialMonster(3, "동굴거미", 20, 20, 30, 4, 3),
            new TutorialMonster(4, "하피", 35, 35, 50, 2, 4),
            new TutorialMonster(5, "오크", 50, 50, 50, 5, 5),
            //3번그룹
            new TutorialMonster(1, "슬라임", 10, 10, 10, 1, 1),
            new TutorialMonster(1, "동굴박쥐", 15, 15, 20, 3, 1),
            new TutorialMonster(2, "고블린", 20, 20, 30, 3, 2),
            new TutorialMonster(3, "동굴거미", 20, 20, 30, 4, 3),
            new TutorialMonster(4, "하피", 35, 35, 50, 2, 4),
            new TutorialMonster(5, "오크", 50, 50, 50, 5, 5),
        };

        //랜덤 몬스터 저장용 임시리스트        
        public List<TutorialMonster> monsterList = new List<TutorialMonster>();

        //랜덤 몬스터 등장
        public void MonsterRandomSlot()
        {
            monsterList.Clear();
            Random random = new Random();

            TutorialMonster monsterClone1 = new TutorialMonster(0, " ", 0, 0, 0, 0, 0);
            TutorialMonster monsterClone2 = new TutorialMonster(0, " ", 0, 0, 0, 0, 0);
            TutorialMonster monsterClone3 = new TutorialMonster(0, " ", 0, 0, 0, 0, 0);

            monsterClone1 = generateMonster[random.Next(0, 6)];
            monsterClone2 = generateMonster[random.Next(6, 12)];
            monsterClone3 = generateMonster[random.Next(12, 18)];

            monsterList.Add(monsterClone1);
            monsterList.Add(monsterClone2);
            monsterList.Add(monsterClone3);
        }

        //전투 페이지
        public void BattleScene()
        {
            Console.Clear();
            Console.WriteLine("[Battle!!]\n");

            for (int i = 0; i < monsterList.Count; i++)
            {
                //몬스터 사망 처리
                if (monsterList[i].tutorialMonsterHealth <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"Lv.{monsterList[i].tutorialMonsterLv} {monsterList[i].tutorialMonsterName} ");
                    Console.WriteLine("[DEAD]");
                    Console.ResetColor();
                }
                else
                { Console.WriteLine($"Lv.{monsterList[i].tutorialMonsterLv} {monsterList[i].tutorialMonsterName}  HP {monsterList[i].tutorialMonsterHealth}"); }
            }

            //플레이어 정보 출력
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Data.Level}  {player.Data.Name} ({player.Data.ClassType})");
            Console.WriteLine($"HP {player.Data.CurHealth} / {player.Data.MaxHealth}"); // Hp : curHp / maxHp<< maxHp
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 방어");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
        }

        //전투 메커니즘 *
        public void BattleLogic()
        {
            MonsterRandomSlot();
            bool loop = true;
            while (loop)
            {
                BattleScene();

                int batlleChoiceIdx = int.TryParse(Console.ReadLine(), out batlleChoiceIdx) ? batlleChoiceIdx : -1;
                if (batlleChoiceIdx > 2 || batlleChoiceIdx < 0)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500);
                    continue;
                }
                else
                {
                    switch (batlleChoiceIdx)
                    {
                        case 0: //Exit:
                            loop = false;
                            break;
                        case 1:  //Attack:
                            ChoiceEnemy();
                            break;
                        case 2:  //Defense:                            
                            if ((player.Data.CurHealth > 0) && (monsterList.Count > 0))
                            {
                                MonsterCalculateMiddleResult("def");
                                if (player.Data.CurHealth <= 0)
                                { CalculateFinalResult(); }
                            }
                            else
                            {
                                CalculateFinalResult();
                            }
                            break;
                    }
                }
                if (player.Data.CurHealth <= 0 || monsterList.Count <= 0)
                { loop = false; }
            }

        }

        //적 선택지 + 전투 진행 여부 파악
        public void ChoiceEnemy()
        {
            //몬스터 생존 여부 판단
            bool[] monsterSurvive = new bool[monsterList.Count];
            for (int j = 0; j < monsterList.Count; j++)
            {
                monsterSurvive[j] = false;   //몬스터 생존시 false               
            }

            int countNum = monsterList.Count;
            int enemyChoiceIdx;
            int minAtkValue = (int)Math.Ceiling(player.GetPlayerAttack() * 0.9f);
            int maxAtkValue = (int)Math.Ceiling(player.GetPlayerAttack() * 1.1f);
            bool loop = true;
            Random random = new Random();
            while (loop)
            {
                Console.Clear();
                Console.WriteLine("[Battle!!]\n");
                for (int i = 0; i < monsterList.Count; i++)
                {
                    //몬스터 사망 처리
                    if (monsterList[i].tutorialMonsterHealth <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"Lv.{monsterList[i].tutorialMonsterLv} {monsterList[i].tutorialMonsterName} ");
                        Console.WriteLine("[DEAD]");                       
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write($"{i + 1} ");
                        Console.WriteLine($"Lv.{monsterList[i].tutorialMonsterLv} {monsterList[i].tutorialMonsterName}  HP {monsterList[i].tutorialMonsterHealth}");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("[내정보]");
                Console.WriteLine($"Lv.{player.Data.Level}  {player.Data.Name} ({player.Data.ClassType})");
                Console.WriteLine($"HP {player.Data.CurHealth} / {player.Data.MaxHealth} ");
                Console.WriteLine("\n0. 나가기\n");
                Console.WriteLine("대상을 선택하세요.");
                Console.Write(">> ");
                
                int resultAtkValue = random.Next(minAtkValue, maxAtkValue + 1);

                enemyChoiceIdx = int.TryParse(Console.ReadLine(), out enemyChoiceIdx) ? enemyChoiceIdx : -1;
                if (enemyChoiceIdx > 3 || enemyChoiceIdx < 0)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500);
                    continue;
                }
                else
                {
                    if (enemyChoiceIdx == 0)
                    { loop = false; }
                    else
                    {
                        if (monsterList[enemyChoiceIdx - 1].tutorialMonsterHealth <= 0)
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                            Thread.Sleep(500);
                            continue;
                        }
                        else
                        {      
                            switch (enemyChoiceIdx)
                            {
                                case 1:
                                    monsterList[0].TakeHit(resultAtkValue); //player.GetPlayerAttack()
                                    break;
                                case 2:
                                    monsterList[1].TakeHit(resultAtkValue);
                                    break;
                                case 3:
                                    monsterList[2].TakeHit(resultAtkValue);
                                    break;
                            }
                        }
                        PlayerCalculateMiddleResult(enemyChoiceIdx - 1, monsterList[enemyChoiceIdx - 1].TakeHit(resultAtkValue));
                        MonsterCalculateMiddleResult("atk");

                        for (int i = 0; i < monsterList.Count; i++)
                        {
                            if (monsterList[i].tutorialMonsterHealth <= 0)
                            {
                                if (monsterSurvive[i] == true)
                                {
                                    continue;
                                }
                                monsterSurvive[i] = true;
                                countNum--;
                            }
                        }
                    }
                }

                if (enemyChoiceIdx != 0)
                {
                    if (countNum <= 0)
                    {
                        if (player.Data.CurHealth > 0)
                        {
                            monsterList.Clear();
                            CalculateFinalResult();
                        }
                        loop = false;
                    }
                    else
                    {
                        if (player.Data.CurHealth <= 0)
                        {
                            CalculateFinalResult();
                            loop = false;
                        }
                    }
                }
            }
        }

        //중간결산창 - 플레이어 턴
        public void PlayerCalculateMiddleResult(int n, float m)  //(타겟몬스터의 인덱스, 데미지)
        {
            Console.Clear();

            Console.WriteLine("[Battle!!]\n");
            Console.WriteLine($"{player.Data.Name}의 공격!");
            Console.WriteLine($"Lv.{monsterList[n].tutorialMonsterLv} {monsterList[n].tutorialMonsterName} 을(를) 맞췄습니다. [데미지 : {m}]");
            Console.WriteLine();
            Console.WriteLine($"Lv.{monsterList[n].tutorialMonsterLv} {monsterList[n].tutorialMonsterName}");
            if (monsterList[n].tutorialMonsterHealth - m <= 0)
            {
                monsterList[n].tutorialMonsterHealth = 0;
                Console.WriteLine($"HP {monsterList[n].tutorialMonsterMaxHealth} -> 0 ");
                //player.Data.LevelPoint += monsterList[n].tutorialMonsterrewardExp;
            }
            else
            { Console.WriteLine($"HP {monsterList[n].tutorialMonsterMaxHealth} -> {monsterList[n].tutorialMonsterHealth -= m}"); }
            Console.WriteLine("0. 다음\n");
            Console.WriteLine();
            Console.WriteLine();
            NextButton();
        }

        //중간결산창 - 몬스터 턴
        public void MonsterCalculateMiddleResult(string choice)
        {
            //플레이어 공격시
            if (choice == "atk")
            {
                for (int i = 0; i < monsterList.Count; i++)
                {
                    Console.Clear();
                    if (monsterList[i].tutorialMonsterHealth <= 0)
                    { continue; }
                    else
                    {
                        Console.WriteLine("[Battle!!]\n");
                        Console.WriteLine($"Lv{monsterList[i].tutorialMonsterLv}. {monsterList[i].tutorialMonsterName}의 공격!");
                        Console.WriteLine($"Lv.{player.Data.Level} {player.Data.Name} 을(를) 맞췄습니다. [데미지 : {monsterList[i].CounterAttack(choice)}])");
                        Console.WriteLine();
                        Console.WriteLine($"Lv.{player.Data.Level} {player.Data.Name}");
                        Console.WriteLine($"HP 100 ->{player.Data.CurHealth - monsterList[i].CounterAttack(choice)}");
                        player.Data.CurHealth -= monsterList[i].CounterAttack(choice);
                        if (player.Data.CurHealth <= 0)
                        {
                            player.Data.CurHealth = 0;
                            break;
                        }
                    }
                    Console.WriteLine("0. 다음\n");
                    Console.WriteLine();
                    Console.WriteLine();
                    NextButton();
                }
            }

            //플레이어 방어시
            if (choice == "def")
            {
                for (int i = 0; i < monsterList.Count; i++)
                {
                    Console.Clear();
                    if (monsterList[i].tutorialMonsterHealth <= 0)
                    { continue; }
                    else
                    {
                        Console.WriteLine("[Battle!!]\n");
                        Console.WriteLine($"Lv{monsterList[i].tutorialMonsterLv}. {monsterList[i].tutorialMonsterName}의 공격!");
                        Console.WriteLine($"Lv.{player.Data.Level} {player.Data.Name} 을(를) 맞췄습니다. [데미지 : {monsterList[i].CounterAttack(choice)}])");
                        Console.WriteLine();
                        Console.WriteLine($"Lv.{player.Data.Level} {player.Data.Name}");
                        Console.WriteLine($"HP 100 ->{player.Data.CurHealth - monsterList[i].CounterAttack(choice)}");
                        player.Data.CurHealth -= monsterList[i].CounterAttack(choice);
                        if (player.Data.CurHealth <= 0)
                        {
                            player.Data.CurHealth = 0;
                            break;
                        }
                    }
                    Console.WriteLine("0. 다음\n");
                    Console.WriteLine();
                    Console.WriteLine();
                    NextButton();
                }
            }

        }
        //최종결산창
        public void CalculateFinalResult()
        {
            Console.Clear();

            //승리
            if (player.Data.CurHealth > 0 || monsterList.Count <= 0)
            {
                Console.WriteLine("[Battle!!] - Result\n");
                Console.WriteLine("Victory\n");
                Console.WriteLine($"던전에서 몬스터 3마리를 잡았습니다.");
                Console.WriteLine($"Lv.{player.Data.Level} {player.Data.Name}\n");
                //if(player.Data.LevelPoint  ==  필요경험치)
                //{Console.WriteLine("축하합니다 레벨업하셨습니다.\n");
                //Console.WriteLine($"Lv.{player.Data.Level} -> Lv.{player.Data.Level += 1}\n");
                //player.Data.CurHealth = 100;} //레벨업 어드밴티지 체력완전회복
                Console.WriteLine($"HP {player.Data.CurHealth} / {player.Data.MaxHealth} ");
                Console.WriteLine("보상: 1000G \n");
                Console.WriteLine($"{player.Data.Gold}G -> {player.Data.Gold += 1000f}G\n");
                Console.WriteLine("0. 다음\n");
                Console.WriteLine();
                Console.WriteLine();
                NextButton();
            }
            //패배
            else
            {
                Console.WriteLine("[Battle!!]\n");

                Console.WriteLine("You Lose\n");

                Console.WriteLine($"Lv.{player.Data.Level} {player.Data.Name}\n");
                Console.WriteLine($"HP 100/0");
                Console.WriteLine("0. 다음\n");
                Console.WriteLine();
                Console.WriteLine();
                NextButton();
            }
        }

        //결산창 넘기기
        public void NextButton()
        {
            while (true)
            {
                int nextButton = int.TryParse(Console.ReadLine(), out nextButton) ? nextButton : -1;
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


    }
}