using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DialogManager : MonoBehaviour
{
    public GameObject canvas;
    public TextMeshProUGUI text;
    public GameObject but;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
