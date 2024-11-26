using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class InsideInventory : MonoBehaviour
{
    public static InsideInventory Instance = null;

    //인벤토리의 슬롯들에게 명령을 내리기위한 변수
    [SerializeField]
    private GameObject go_InsideInventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;

    private Slot[] slots;

    //인멘토리를 열었을 때, 플레이어조작을 멈추기 위한 변수
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private GameObject staminaSlider;
    [SerializeField]
    private Text itemText;
    public bool isFull = false;

    //퀵슬롯의 기능
    private int i_index  = 0;
    private GameObject[] checkImages;

    private bool OneTime = true;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(this.gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        checkImages = new GameObject[6];
    }

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetColor(0);
            checkImages[i] = slots[i].transform.GetChild(1).gameObject;
            checkImages[i].SetActive(false);
        }
    }

    private void Update()
    {
        FadeOut();
    }
    public void AcuquireItem(IItem _item)
    {

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item);
                return;
            }
        }
    }

    public void CheckSlotFull()
    {
        int count = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                count++;
            }
        }
        if (count >= slots.Length)
        {
            isFull = true;
            Debug.Log("인벤토리가 가득 찼읍니다");
        }
        else
        {
            isFull = false;
        }
    }

    public void CheckCanUse(int index)
    {
        if (index != i_index)
        {
            OneTime = true;
        }

        i_index = index;

        if (OneTime && slots[i_index].item != null) //아이템이름표시
        {
            Debug.Log("된다.");
            itemText.text = slots[i_index].item.itemName;
            FadeIn();
            OneTime = false;
        }
        
        for (int i = 0; i < slots.Length; i++)
        {
            if (i != index)                            //인벤토리의 사용가능표시, 아이템사용가능 초기화
            {
                checkImages[i].SetActive(false);

                if (slots[i].item == null)
                {
                    //Debug.Log("확인 2");
                    slots[i].isCanUse = false;
                }
            }
        }
        checkImages[i_index].SetActive(true);

        if (slots[index].item != null)
        {
            //Debug.Log("확인 3");
            slots[index].isCanUse = true;
        }
    }

    public void UsingItem()
    {
        int index = i_index;
        if (slots[index].isCanUse && slots[index].item != null)
        {
            slots[index].item.Use(playerController.gameObject);
            UsedItem(index);
        }
        else
        {
            //Debug.Log("아이템이 없읍니다");
        }
    }

    private void UsedItem(int index)
    {
        if (slots[index].item.isCanUse)
        {
            slots[index].ClearSlot();
        }
    }

    public void ClearAllItem()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ClearSlot();
        }
    }

    private void FadeIn()
    {
        //SetColor((int)currentTime);
        itemText.DOFade(1f, 0.2f).SetEase(Ease.InOutQuad).SetAutoKill(false).OnComplete(() => StartCoroutine(FadeOut()));
    }
    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2f);

        itemText.DOFade(0f, 0.2f).SetEase(Ease.InOutQuad).SetAutoKill(false);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        playerController = FindObjectOfType<PlayerController>();
        staminaSlider = GameObject.Find("StaminaSlider");
        itemText = GameObject.Find("ItemNameText").GetComponent<Text>();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
