using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectedUI : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemIcon;
    [SerializeField] List<Image> imageSelecteds;
    [SerializeField] Color selectedColor;

    [SerializeField] Color normalColor;
    // Start is called before the first frame update
    void Start()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            OnUISelected();
        }
        else
        {
            OnUIDeselected();
        }
    }

    void OnUISelected()
    {
        foreach (var image in imageSelecteds)
        {
            image.color = selectedColor;
        }
    }

    void OnUIDeselected()
    {
        foreach (var image in imageSelecteds)
        {
            image.color = normalColor;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        OnUISelected();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        OnUIDeselected();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnUISelected();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnUIDeselected();
    }
}
