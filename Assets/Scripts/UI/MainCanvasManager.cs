using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class MainCanvasManager : MonoBehaviour
{
    GameManager gameManager;
    [Header("PlayerStatus")]
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
    [Header("PlayerInfoCanvas")]
    [SerializeField] GameObject playerInfoCanvas;
    [Header("PlayerStats")]
    [SerializeField] GameObject playerStatsInfoObject;
    [SerializeField] GameObject playerAvtSelectedObj;
    [SerializeField] TextMeshProUGUI hp;
    [SerializeField] TextMeshProUGUI dmg;
    [Header("SkillInfo")]
    [SerializeField] GameObject skillInfoObject;
    [SerializeField] GameObject skillSelectedObj;
    [SerializeField] TextMeshProUGUI skillName;
    [SerializeField] TextMeshProUGUI skillDes;
    [Header("Inventory")]
    [SerializeField] GameObject itemInfoObject;
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
        DisplayPlayerInfo();
    }

    void DisplayPlayerInfo()
    {
        if (gameManager.playerInfo)
        {
            DisplayItemDescription();
            DisplayStats();
            DisplaySkillInfo();
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
        if (!PlayerStats.instance) return;
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
            DisplayInventory();
            playerInfoCanvas.SetActive(true);
            EventSystem.current.SetSelectedGameObject(playerAvtSelectedObj);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            playerInfoCanvas.SetActive(false);
        }
    }

    void DisplayStats()
    {
        if (EventSystem.current.currentSelectedGameObject == playerAvtSelectedObj)
        {
            hp.text = PlayerStats.instance.CurrentHP + "/" + PlayerStats.instance.MaxHP;
            dmg.text = PlayerStats.instance.Damage + "";
            playerStatsInfoObject.SetActive(true);
            return;
        }
        playerStatsInfoObject.SetActive(false);
    }

    void DisplaySkillInfo()
    {
        if (EventSystem.current.currentSelectedGameObject == skillSelectedObj)
        {
            skillName.text = "Firey";
            skillDes.text = "Breath a fireball that damage enemy on its way!";
            skillInfoObject.SetActive(true);
            return;
        }
        skillInfoObject.SetActive(false);
    }

    void DisplayInventory()
    {
        for (int i = 0; i < InventoryManager.instance.items.Count; i++)
        {
            var icon = itemSlots[i].GetComponent<SelectedSkillUI>().skillIcon;
            icon.sprite = itemList[i].icon;
            icon.gameObject.SetActive(true);
        }
        for (int i = InventoryManager.instance.items.Count; i < 9; i++)
        {
            var icon = itemSlots[i].GetComponent<SelectedSkillUI>().skillIcon;
            icon.gameObject.SetActive(false);
        }
    }


    void DisplayItemDescription()
    {
        if (itemSlots.Contains(EventSystem.current.currentSelectedGameObject))
        {
            itemName.text = "";
            itemDescription.text = "";
            itemRare.text = "";
            int i = GetItemIndex();
            if (i != -1)
            {
                itemName.text = itemList[i].itemName;
                itemRare.text = itemList[i].itemType.ToString();
                itemDescription.text = itemList[i].description;
                itemInfoObject.SetActive(true);
                return;
            }
        }
        itemInfoObject.SetActive(false);
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
