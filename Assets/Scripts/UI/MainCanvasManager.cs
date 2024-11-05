using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainCanvasManager : MonoBehaviour
{
    GameManager gameManager;
    [Header("PlayerStatus")]
    [SerializeField] GameObject playerMainCanvas;
    [SerializeField] Slider HPBar;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI coinCount;
    [SerializeField] TextMeshProUGUI gemCount;
    [SerializeField] Image skillCD;

    [Header("Pause")]
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject resumeBut;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject settingUI;
    [SerializeField] GameObject backBut;
    [Header("PlayerStats")]
    [SerializeField] GameObject playerInfoCanvas;
    [SerializeField] TextMeshProUGUI hp;
    [SerializeField] TextMeshProUGUI dmg;
    [SerializeField] TextMeshProUGUI def;
    [Header("Inventory")]
    List<ItemStats> itemList;
    [SerializeField] List<GameObject> itemSlots;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemRare;
    [SerializeField] TextMeshProUGUI itemDescription;
    [Header("EndGame")]
    [SerializeField] GameObject EndGame;
    [SerializeField] GameObject Win;
    [SerializeField] GameObject returnbut1;
    [SerializeField] GameObject Lose;
    [SerializeField] GameObject returnbut2;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        ManagePauseCanvas();
        ManagePlayerInfoCanvas();
        ShowResult();
        if (GameManager.instance)
            GameManager.instance.lose += ShowResult;
    }

    void OnDestroy()
    {
        if (GameManager.instance)
            GameManager.instance.lose -= ShowResult;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayMainCanvas();
        if (gameManager.playerInfo)
        {
            DisplayItemDescription();
        }
    }

    void DisplayMainCanvas()
    {
        ManageHP();
        ManageCoin();
        ManageSkill();
    }

    void ManageHP()
    {
        HPBar.value = (float)PlayerStats.instance.CurrentHP / PlayerStats.instance.MaxHP;
        hpText.text = PlayerStats.instance.CurrentHP + "/" + PlayerStats.instance.MaxHP;
    }

    void ManageCoin()
    {
        coinCount.text = PlayerStats.instance.coin + "";
        gemCount.text = PlayerStats.instance.gem + "";
    }

    void ManageBuff()
    {

    }

    void ManageSkill()
    {


        skillCD.fillAmount = PlayerMovement.instance.cdCount / PlayerMovement.instance.skillCDTime;

    }

    public void ManagePauseCanvas()
    {
        if (gameManager.pause)
        {
            pauseCanvas.SetActive(true);
            EventSystem.current.SetSelectedGameObject(resumeBut);
        }
        else
        {
            pauseCanvas.SetActive(false);
            HideSettingUI();
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void ManagePlayerInfoCanvas()
    {
        if (gameManager.playerInfo)
        {
            itemList = InventoryManager.instance.items.ConvertAll(e => e.Key);
            DisplayStats();
            DisplayInventory();
            playerInfoCanvas.SetActive(true);
            EventSystem.current.SetSelectedGameObject(itemSlots[0]);
        }
        else
        {
            playerInfoCanvas.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    void DisplayStats()
    {
        hp.text = PlayerStats.instance.CurrentHP + "/" + PlayerStats.instance.MaxHP;
        dmg.text = PlayerStats.instance.Damage + "";
        def.text = PlayerStats.instance.DEF + "";
    }

    void DisplayInventory()
    {
        for (int i = 0; i < InventoryManager.instance.items.Count; i++)
        {

            itemSlots[i].GetComponent<Image>().sprite = itemList[i].icon;
        }
        for (int i = InventoryManager.instance.items.Count; i < 9; i++)
        {
            itemSlots[i].GetComponent<Image>().sprite = null;
        }
    }

    public void DisplayItemDescription()
    {
        itemName.text = "";
        itemDescription.text = "";
        itemRare.text = "";
        int i = GetItemIndex();
        if (i != -1)
        {
            itemName.text = itemList[i].ItemName;
            itemRare.text = itemList[i].itemType.ToString();
            itemDescription.text = itemList[i].description;
        }
    }

    public int GetItemIndex()
    {
        for (int i = 0; i < InventoryManager.instance.items.Count; i++)
        {
            if (EventSystem.current.currentSelectedGameObject == itemSlots[i])
            {
                return i;
            }
        }
        return -1;
    }

    public void ShowSettingUI()
    {
        settingUI.SetActive(true);
        pauseUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(backBut);
    }

    public void HideSettingUI()
    {
        settingUI.SetActive(false);
        pauseUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(resumeBut);
    }


    public void ShowResult()
    {
        if (GameManager.instance.isLose)
        {
            EndGame.SetActive(true);
            Lose.SetActive(true);
            Win.SetActive(false);
            EventSystem.current.SetSelectedGameObject(returnbut2);
            Time.timeScale = 0;
        }
        else if (GameManager.instance.isWin)
        {
            EndGame.SetActive(true);
            Lose.SetActive(false);
            Win.SetActive(true);
            EventSystem.current.SetSelectedGameObject(returnbut1);
            Time.timeScale = 0;
        }
        else
        {
            EndGame.SetActive(false);
            Lose.SetActive(false);
            Win.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            Time.timeScale = 1;
        }
    }
}
