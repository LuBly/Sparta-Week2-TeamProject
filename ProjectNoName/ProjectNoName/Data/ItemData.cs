namespace ProjectNoName
{
    internal class ItemData
    {
        // 구매 여부
        public bool IsUsable;
        // 장착 여부
        public bool IsEquiped;
        // 이름
        public string Name;
        // 가격
        public float Price;
        // 장비 타입 (무기, 방어구, +회복(제작 시 enum 추가 필요)
        public ItemType ItemType;
        // 무기 공격력 증가(방어구일 경우 null 값 가능)
        public float AttackPowerIncrease = 0;
        // 방어력 증가(무기일 경우 null값 가능)
        public float DefencePowerIncrease = 0;
        // 설명
        public string Description;
    }
}
