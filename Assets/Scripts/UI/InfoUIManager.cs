using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
public class InfoUIManager : MonoBehaviour
{
    public static InfoUIManager instance;
    [SerializeField] GameObject infoCanvas;
    [SerializeField] TextMeshProUGUI butText;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Disable();
    }

    // Update is called once per frame
    public void SetInfo(
        Transform pos,
        Color c,
        string butText = null)
    {
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
        if (!infoCanvas) return;
        infoCanvas.SetActive(false);
    }
}
