using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHP : MonoBehaviour
{
    public Entity entity;
    public Canvas hpUI;
    public bool show = false;
    public bool alwayShow;
    [SerializeField] Slider hpBar;
    [SerializeField] Image hpBarColor;
    [SerializeField] Sprite hpEnemy;
    [SerializeField] Sprite hpAlly;

    public float timeDisplay = 2f;
    float timeCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        hpUI = GetComponent<Canvas>();
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
    }

    public void Set(Entity e, Defines.MonsterType monsterType)
    {
        entity = e;
        Collider2D child = e.gameObject.GetComponentsInChildren<Collider2D>(true)
                                     .FirstOrDefault(t => t.CompareTag("Enemy"));
        gameObject.transform.localPosition = new Vector3(child.transform.localPosition.x, child.transform.localPosition.y - 0.2f - child.bounds.size.y / 2, 0);
        switch (monsterType)
        {
            case Defines.MonsterType.Enemy:
                hpBarColor.sprite = hpEnemy;
                break;
            case Defines.MonsterType.Ally:
                hpBarColor.sprite = hpAlly;
                alwayShow = true;
                break;

        }
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
                hpBar.gameObject.transform.localScale = new Vector3(entity.gameObject.transform.localScale.x, 1, 1);
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
        hpBar.value = (float)entity.currentHP / entity.maxHP;
    }
}
