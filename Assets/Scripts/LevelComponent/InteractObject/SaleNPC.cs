using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaleNPC : PlayerInteract
{
    [SerializeField] List<Transform> itemPos;
    DialogManager dialogManager;
    GameObject[] saleItems;
    List<GameObject> purchasedItems;
    int cost = 500;
    // Start is called before the first frame update
    void Start()
    {
        dialogManager = GetComponent<DialogManager>();
        interactType = InteractType.NPC;
        saleItems = new GameObject[itemPos.Count];
        purchasedItems = new List<GameObject>();
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void NPC()
    {
        //dialogManager.Show(Defines.DialogNPCText.SaleNPC);
    }

    void Init()
    {
        for (int i = 0; i < itemPos.Count; i++)
        {
            if (saleItems[i] == null)
            {
                Vector2 pos = new Vector2(itemPos[i].position.x, itemPos[i].position.y + 1f);
                saleItems[i] = SpawnManager.instance.SpawnItemForSale(pos);
                var itemManager = saleItems[i].GetComponent<ItemManager>();
                float h = 1;
                switch (itemManager.itemStats.itemType)
                {
                    case ItemStats.ItemType.Rare:
                        h = 1.5f;
                        break;
                    case ItemStats.ItemType.Legend:
                        h = 2f;
                        break;
                }
                itemManager.SetSale((int)(h * cost));
            }
        }
    }
    public void Refresh()
    {
        for (int i = 0; i < itemPos.Count; i++)
        {
            if (saleItems[i] != null)
            {
                //fix
                if (!saleItems[i].GetComponent<ItemManager>().isSale)
                {
                    purchasedItems.Add(saleItems[i]);
                }
                saleItems[i] = null;
            }
        }
        Init();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        InfoUIManager.instance.SetInfo(Helper.GetPos(gameObject, Helper.ObjPosition.Top), Color.white, Defines.InfoButText.Talk);
    }
}
