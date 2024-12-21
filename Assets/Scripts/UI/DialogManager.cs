using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogManager : MonoBehaviour
{
    public GameObject canvas;
    public TextMeshProUGUI text;
    public GameObject but;

    public void Show(string text = "")
    {
        GameManager.instance.Dialog();
        this.text.text = text;
        canvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(but);
    }

    public void Hide()
    {
        GameManager.instance.DisableDialog();
        this.text.text = "";
        canvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
