namespace ProjectNoName
{
    // Data Save 및 Load를 위해 사용하는 객체들
    internal class PlayerData
    {
        // Player 객체 내에서만 수정해야할 자료들
        public int Level;
        public int LevelPoint;
        public string Name;
        public ClassType ClassType;
        public float AttackPower;
        public float DefensePower;
        public float Health;
        public float Gold;

        // 장비 장착 class에서 사용
        public float IncreaseAttack;
        public float IncreaseDefense;

        // 장착하고 있는 무기, 방어구
        // 한개씩만 장착할 수 있다.
        public ItemData? Weapon;
        public ItemData? Armor;
        public InventoryData? Inventory;


    }
}
