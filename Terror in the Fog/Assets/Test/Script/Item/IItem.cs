using Unity.VisualScripting;
using UnityEngine;

// 아이템 타입들이 반드시 구현해야하는 인터페이스
public interface IItem {
    // 입력으로 받는 target은 아이템 효과가 적용될 대상
    void Use(GameObject target);

    public enum ItemType
    {
        Used,
        interacted,
        ETC
    }
    public ItemType type { get; set; }
    public string itemName { get; set; }
    public Sprite itemImage {  get; set; }
    public GameObject itemPrefab { get; set; }
}