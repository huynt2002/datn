using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI traitLevel;
    [SerializeField] Image traitImage;
    MainCanvasManager mainCanvasManager;
    void Start()
    {
        mainCanvasManager = GameObject.FindWithTag("GameManager").GetComponent<MainCanvasManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnSelect(BaseEventData eventData)
    {

    }

    public void OnDeselect(BaseEventData eventData)
    {


    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
