using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemTraitInfoUI : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI traitLevel;
    [SerializeField] Image traitImage;
    public ItemTrait itemTrait { get; private set; }
    MainCanvasManager mainCanvasManager;
    void Start()
    {
        mainCanvasManager = GameObject.FindWithTag("GameManager").GetComponent<MainCanvasManager>();
        GetTraitInfo();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetTrait(ItemTrait trait)
    {
        itemTrait = trait;
    }

    void GetTraitInfo()
    {
        if (itemTrait)
        {
            string str = InventoryManager.instance.itemTraitCount[itemTrait].ToString() + "->  ";
            for (int i = 0; i < itemTrait.effects.Count; i++)
            {
                string levelNum = itemTrait.effects[i].levelNum.ToString();
                if (itemTrait.effects[i].levelNum <= itemTrait.currentLevel)
                {
                    levelNum = "<b>" + levelNum + "</b>";
                }
                if (i == itemTrait.effects.Count - 1)
                {
                    str += levelNum;
                    break;
                }
                str += levelNum + "  /  ";
            }
            traitLevel.text = str;
            traitImage.sprite = itemTrait.icon;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        mainCanvasManager.UpdateTraitDetail(true, itemTrait);
    }

    public void OnDeselect(BaseEventData eventData)
    {

        mainCanvasManager.UpdateTraitDetail(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mainCanvasManager.UpdateTraitDetail(true, itemTrait);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mainCanvasManager.UpdateTraitDetail(false);
    }
}
