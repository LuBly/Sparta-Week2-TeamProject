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
        
        public Player Player = new Player();
        public Store Store = new Store();
        public Dungeon Dungeon = new Dungeon();

        string playerDataPath = @"D:\스파르탄\Sparta-Week2-TeamProject\ProjectNoName\ProjectNoName\Data\SaveData\PlayerData.json";
        string storeDataPath = @"D:\스파르탄\Sparta-Week2-TeamProject\ProjectNoName\ProjectNoName\Data\SaveData\StoreData.json";

        // 데이터 저장 함수
        // PlayerData 
        //  - 스텟, 골드
        //  - Player Inventory Data (List)
        // Store Inventory Data (List)

        // Private로 선언된 객체 내부의 자료들을 public 변수들로 이뤄진 Data클래스로 전환
        // json에 저장할 수 있는 형태로 전환
        public void GetData()
        {
            Player.GetPlayerData();
            Store.GetStoreData();
        }
        
        public void SaveData()
        {
            // playerData
            string playerJson = JsonConvert.SerializeObject(Player.Data, Formatting.Indented);
            File.WriteAllText(playerDataPath, playerJson);

            string storeJson = JsonConvert.SerializeObject(Store.Data, Formatting.Indented);
            File.WriteAllText(storeDataPath, storeJson);
        }

        // 데이터 불러오기 함수
        // PlayerData
        // PlayerInventory 
        public void LoadData()
        {

        }
    }
}
