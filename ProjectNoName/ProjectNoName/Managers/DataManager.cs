using Newtonsoft.Json;

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
        string playerDataPath = @"..\..\..\Data\SaveData\PlayerData.json";
        string storeDataPath = @"..\..\..\Data\SaveData\StoreData.json";
        string originStoreDataPath = @"..\..\..\Data\InitData\OriginStoreData.json";

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

        public void SaveData()
        {
            // playerData
            string playerJson = JsonConvert.SerializeObject(Player.Data, Formatting.Indented);
            File.WriteAllText(playerDataPath, playerJson);

            // storeData 저장
            string storeJson = JsonConvert.SerializeObject(Store.Data, Formatting.Indented);
            File.WriteAllText(storeDataPath, storeJson);
        }

        // 게임이 실행될때 무조건 실행되어야 하는 함수
        public void InitData()
        {
            string originStoreJson = File.ReadAllText(originStoreDataPath);
            StoreData store = JsonConvert.DeserializeObject<StoreData>(originStoreJson);
            Store.Data.StoreInventory = store.StoreInventory;
        }
        // 데이터 불러오기 함수
        public void LoadData()
        {
            // 파일이 있는지 찾아보고 있으면 가고
            if (!File.Exists(playerDataPath) || !File.Exists(storeDataPath))
            {
                Console.WriteLine("저장된 데이터가 없습니다.");
                Thread.Sleep(1000);
            }
            else
            {
                string? playerJson = File.ReadAllText(playerDataPath);
                PlayerData? playerData = JsonConvert.DeserializeObject<PlayerData>(playerJson);
                string? storeJson = File.ReadAllText(storeDataPath);
                StoreData? store = JsonConvert.DeserializeObject<StoreData>(storeJson);

                // 저장된 데이터가 있다
                if (playerData != null && store != null)
                {
                    Player = new Player();
                    Player.Data = playerData;
                    Store.Data.StoreInventory = store.StoreInventory;
                }
            }
        }
    }
}
