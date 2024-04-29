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
