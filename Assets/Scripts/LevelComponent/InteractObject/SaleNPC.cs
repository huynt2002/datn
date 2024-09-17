using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaleNPC : PlayerInteract
{
    [SerializeField] List<Transform> itemPos;
    DialogManager dialogManager;
    GameObject[] item;
    // Start is called before the first frame update
    void Start()
    {
        dialogManager = GetComponent<DialogManager>();
        interactType = InteractType.NPC;
        item = new GameObject[itemPos.Count];
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void NPC()
    {
        dialogManager.Show(Defines.DialogNPCText.SaleNPC);
    }

    void Init()
    {
        for (int i = 0; i < itemPos.Count; i++)
        {
            if (item[i] == null)
            {
                Vector2 pos = new Vector2(itemPos[i].position.x, itemPos[i].position.y + 1f);
                item[i] = SpawnManager.instance.SpawnItem(pos);
                item[i].GetComponent<ItemManager>().SetSale();
            }
        }
    }
    public void Refresh()
    {
        for (int i = 0; i < itemPos.Count; i++)
        {
            if (item[i] != null)
            {
                //fix
                if (!item[i].GetComponent<ItemManager>().isSale)
                {
                    var newItem = item[i] as GameObject;
                }
                Destroy(item[i]);
                item[i] = null;

            }
        }
        Init();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        InfoUIManager.instance.SetInfo(gameObject.transform, Color.white, Defines.InfoButText.Talk);
    }
}
