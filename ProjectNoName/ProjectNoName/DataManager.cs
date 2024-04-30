namespace ProjectNoName
{
    // 싱글톤으로 생성하여 어느 클래스에서든 접근 가능
    internal class DataManager
    {
        // 필요한 정보들 생성

        // [필수 사항]
        // Player - Status, 전투에 활용
        // Store - Inventory에 활용
        // Dungeon
        private static DataManager staticDataManager;
        
        public static DataManager Instance()
        {
            if(staticDataManager == null)
            {
                staticDataManager = new DataManager();
            }
            return staticDataManager;
        }

        
        public Player Player; 
        public Store Store = new Store();
        public Dungeon Dungeon = new Dungeon();

        // 플레이어 생성
        public void CreatePlayer()
        {
            Console.Clear();
            Console.WriteLine("플레이어를 생성합니다.\n");

            Console.Write("플레이어 이름을 입력하세요: ");
            string playerName = Console.ReadLine();

            Console.WriteLine("\n직업을 선택하세요:");
            Console.WriteLine("\n1. 전사\n2. 궁수\n3. 마법사\n");
            Console.Write(">> ");
            int jobChoice;
            if (int.TryParse(Console.ReadLine(), out jobChoice) && jobChoice >= 1 && jobChoice <= 3)
            {
                ClassType selectedClass = (ClassType)(jobChoice);
                Console.WriteLine($"플레이어 {playerName}이(가) {selectedClass}로 생성되었습니다!");
                Thread.Sleep(2000);
                // Player 클래스에 이름과 직업을 전달
                Player = new Player(playerName, selectedClass);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                Thread.Sleep(1000);
                CreatePlayer();
            }
        }

        // dataManager에 store랑 player를 꽃아놨는데
        // store 초기화될때 또 dataManager를 불러와서 초기화를 한다.

        // 데이터 저장 함수
        public void SaveData()
        {
            
        }

        // 데이터 불러오기 함수
        public void LoadData()
        {

        }
    }
}
