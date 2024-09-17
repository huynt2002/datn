using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InfoUIManager : MonoBehaviour
{
    public static InfoUIManager instance;
    [SerializeField] GameObject infoCanvas;
    [SerializeField] TextMeshProUGUI butText;
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] TextMeshProUGUI rareRate;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] Image infoPanel;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Disable();
    }

    // Update is called once per frame
    public void SetInfo(Transform pos, Color c, string butText = null, string name = null, string rare = "", string description = "")
    {
        if (name == null)
        {
            infoPanel.gameObject.SetActive(false);
        }
        else
        {
            this.name.text = name;
            this.rareRate.text = rare;
            this.description.text = description;
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

    public void Disable()
    {
        infoCanvas.SetActive(false);
    }
}
