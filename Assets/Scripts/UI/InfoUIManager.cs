using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class InfoUIManager : MonoBehaviour
{
    public static InfoUIManager instance;
    [SerializeField] GameObject infoCanvas;
    [SerializeField] TextMeshProUGUI butText;
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] TextMeshProUGUI rareRate;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] Image traitImage;
    [SerializeField] TextMeshProUGUI traitName;
    [SerializeField] Image infoPanel;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Disable();
    }

    // Update is called once per frame
    public void SetInfo(Transform pos, Color c, string butText = null, ItemStats item = null)
    {
        if (item == null)
        {
            infoPanel.gameObject.SetActive(false);
        }
        else
        {
            this.name.text = item.itemName;
            this.rareRate.text = item.itemType.ToString();
            this.description.text = item.description;

            if (item.trait)
            {
                this.traitName.text = item.trait.traitName;
                this.traitImage.sprite = item.trait.icon;
            }
            SetTraitVisible(item.trait);

            infoPanel.gameObject.SetActive(true);
        }
        if (butText == null)
        {
            this.butText.gameObject.SetActive(false);
        }
        else
        {
            this.butText.text = butText;
            this.butText.color = c;
            this.butText.gameObject.SetActive(true);
        }
        infoCanvas.GetComponent<RectTransform>().position = pos.position;
        infoCanvas.SetActive(true);
    }

    void SetTraitVisible(bool visible)
    {
        traitImage.gameObject.SetActive(visible);
        traitName.gameObject.SetActive(visible);
    }

    public void Disable()
    {
        infoCanvas.SetActive(false);
    }
}
