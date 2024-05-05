using System.Security.Cryptography;

namespace ProjectNoName
{
    enum BattleMenuType
    {
        Attack = 1,
        // 추가
    } 

    public class MainStage : Stage
    {
        public MainStage(string name)
        {
            stageName = name;
        }
        MonsterManager monsterManager = new MonsterManager();
        Player player = DataManager.Instance().Player;

        // battle에서 사용할 몬스터 List
        List<Monster> battleMonsters;
        int stageIdx;
        // Player 딴에서 현재 갈수있는 최상 stage를 가지고 있어야 한다.
        // Stage에서 구현된 StartBattle 함수를 override하여 작성

        public override void ShowStageInfo()
        {
            int originRow = Console.CursorTop;
            // 이름
            Console.Write($"{stageName}");
            // 권장 방어력
            Console.SetCursorPosition(20, originRow);
            Console.WriteLine($"| [ 최상층 : {player.Data.MaxStage} ]");
        }

        public override void StartBattle()
        {
            // int stageIdx = Player에서 조절
            stageIdx = SelectStageIdx();
            battleMonsters = monsterManager.SetStageMonster(stageIdx);
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

        int SelectStageIdx()
        {
            int stageIdx = player.Data.MaxStage;
            bool isSelect = false;
            while(true)
            {
                if (isSelect)
                    break;

                ShowPhase("단계 선택");
                Console.Write($"방향키로 층을 선택해주세요 ");

                // 데이터 가공
                if (stageIdx < 1) stageIdx = 1;
                else if (stageIdx > player.Data.MaxStage) stageIdx = player.Data.MaxStage;
                
                // 출력
                if(stageIdx == 1)
                {
                    Console.WriteLine($"{stageIdx} > ");
                }
                else if(stageIdx == player.Data.MaxStage)
                {
                    Console.WriteLine($"< {stageIdx} ");
                }
                else
                {
                    Console.WriteLine($"< {stageIdx} > ");
                }
                
                // 키입력
                ConsoleKeyInfo inputKeyInfo = Console.ReadKey(true);
                switch(inputKeyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        stageIdx--;
                        break;
                    case ConsoleKey.RightArrow: 
                        stageIdx++;
                        break;
                    case ConsoleKey.Enter:
                        isSelect = true;
                        break;
                }
            }
            return stageIdx;
        }

        //Player Turn 관련 함수
        void ShowPlayerTurn()
        {
            ShowPhase("내 차례");
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
            // 입력한 값이 숫자형태라면 그대로 입력, 아니라면 -1 << default로 갈 수 있는 값
            int choiceIdx = (int.TryParse(Console.ReadLine(), out choiceIdx)) ? choiceIdx : -1;

            switch ((BattleMenuType)choiceIdx)
            {
                case BattleMenuType.Attack:
                    // Attack 페이지 오픈
                    ShowPlayerAttack();
                    break;

                default:
                    Console.WriteLine("[잘못된 선택입니다!]");
                    Thread.Sleep(500);
                    ShowPlayerTurn();
                    break;
            }
        }

        void ShowPlayerAttack()
        {
            ShowPhase("누굴 때릴까?");
            // 몬스터 정보 출력 _ idx 포함
            Console.WriteLine("[몬스터 정보]");
            for(int i = 1; i < battleMonsters.Count; i++)
            {
                Console.Write($"[{i}] ");
                battleMonsters[i].ShowMonsterData();
            }

            Console.WriteLine();
            player.ShowBattleStatus();

            Console.WriteLine("\n0. 취소 \n");
            Console.WriteLine("대상을 선택해주세요.");
            Console.Write(">>");
            int monsterIdx = (int.TryParse(Console.ReadLine(), out monsterIdx)) ? monsterIdx : -1;
            // 취소일경우
            if (monsterIdx == 0)
            {
                // 다시 PlayerTurn을 보여주는 함수 실행
                ShowPlayerTurn();
            }
            // 범위 내의 값을 선택한 경우
            else if (monsterIdx > 0 && monsterIdx < battleMonsters.Count)
            {
                if (battleMonsters[monsterIdx].Data.Health > 0)
                    AttackMonster(monsterIdx);
                else
                {
                    Console.WriteLine("\n[이미 죽은 대상입니다!]");
                    Thread.Sleep(500);
                    ShowPlayerAttack();
                }
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
            ShowPhase("이것도 피해 보시지");
            Console.WriteLine("[전투정보]");
            Monster curMonster = battleMonsters[monsterIdx];
            Console.Write($"{player.Data.Name} 의 공격!");
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
            ShowPhase("적 차례");
            Console.WriteLine("[전투정보]");
            for (int i = 1; i < battleMonsters.Count; i++)
            {
                // 살아있는 몬스터만 캐릭터를 공격
                if (battleMonsters[i].Data.Health > 0)
                    AttackPlayer(battleMonsters[i]);
            }

            Utill.ShowNextPage();
        }

        void AttackPlayer(Monster monster)
        {
            Player player = DataManager.Instance().Player;
            Console.WriteLine();
            Console.WriteLine($"Lv.{monster.Data.Level} {monster.Data.Name} 의 공격!");
            player.TakeDamage(monster.Data.AttackPower);
            Thread.Sleep(500);
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
            ShowPhase("결과");
            Console.WriteLine("[이겼콩ㅋ]");
            Console.WriteLine();
            Console.WriteLine($"{stageName} [{stageIdx}]에서 몬스터 {battleMonsters.Count - 1}마리를 잡았습니다.");
            Console.WriteLine();
            Console.WriteLine("[캐릭터 정보]");
            Console.WriteLine($"Lv.{player.Data.Level} {player.Data.Name}");
            Console.WriteLine($"HP {originHealth} -> {player.Data.CurHealth}");
            // 보상 함수 실행
            CreateStageReward();
            Utill.ShowNextPage();
        }

        void CreateStageReward()
        {
            int totalRewardExp = 0;
            int totalRewardGold = 0;
            List<Item> rewardItems = new List<Item>();

            for (int i = 1; i < battleMonsters.Count; i++)
            {
                totalRewardExp += battleMonsters[i].Data.RewardExp;
                totalRewardGold += battleMonsters[i].CreateMonsterGoldReward();
                rewardItems.AddRange(battleMonsters[i].CreateMonsterItemReward());
            }
            Console.WriteLine($"EXP : {player.Data.Exp} -> {player.Data.Exp += totalRewardGold}");
            Console.WriteLine();
            Console.WriteLine("[획득 아이템]");
            Console.WriteLine($"{totalRewardGold} Gold");
            player.Data.Gold += totalRewardGold;
            foreach(var item in rewardItems)
            {
                player.Data.Inventory.AddItem(item);
                Console.WriteLine(item.Data.Name);
            }
        }

        protected override void StageFail()
        {
            ShowPhase("결과");
            Console.WriteLine("[졌콩 ㅠㅠ]");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Data.Level} {player.Data.Name}");
            Console.WriteLine($"HP {originHealth} -> {player.Data.CurHealth}");
            Utill.ShowNextPage();
        }

        // 출력
        void ShowPhase(string phase)
        {
            Console.Clear();
            Console.WriteLine("Battle!");
            Console.WriteLine();
            Console.WriteLine($"[{phase}]");
            Console.WriteLine();
        }
    }
}
