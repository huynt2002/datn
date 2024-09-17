using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHP : MonoBehaviour
{
    public Entity entity;
    public Canvas hpUI;
    public bool show = false;
    public bool alwayShow;
    [SerializeField] Slider hpBar;

    public float timeDisplay = 2f;
    float timeCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        hpUI = GetComponent<Canvas>();
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
    }

    public void Set(Entity e, Transform p)
    {
        entity = e;
        gameObject.transform.parent = p;
        gameObject.transform.localPosition = new Vector3(0, -0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (entity != null)
        {
            Display();
        }
    }

    void Display()
    {
        if (alwayShow)
        {
            ManageHP();
        }
        else
        {
            if (show)
            {
                hpUI.gameObject.transform.localScale = new Vector3(-transform.parent.localScale.x, 1, 1);
                ManageHP();
                hpUI.enabled = true;
                timeCount = 0;
            }
            else
            {
                timeCount += Time.deltaTime;
                if (timeCount > timeDisplay)
                {
                    hpUI.enabled = false;
                    show = false;
                }
            }
        }
    }
    void ManageHP()
    {
        hpBar.value = (float)entity.CurrentHP / entity.MaxHP;
    }
}
