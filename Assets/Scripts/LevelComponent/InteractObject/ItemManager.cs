using UnityEngine;

public class ItemManager : PlayerInteract
{
    public ItemStats itemStats;
    public bool isSale { get; private set; }
    public int cost = 0;
    void Awake()
    {
        if (LevelManager.instance)
            LevelManager.instance.clearOnLoadLevel += DestroyItem;
    }

    void Start()
    {
        interactType = InteractType.Item;
        ApplyData();
    }

    void OnDestroy()
    {
        if (LevelManager.instance)
            LevelManager.instance.clearOnLoadLevel -= DestroyItem;
    }

    void Update()
    {

    }
    public void ApplyData()
    {
        GetComponent<SpriteRenderer>().sprite = itemStats.icon;
    }
    public override void Item()
    {
        if (isSale)
        {
            var coin = PlayerStats.instance.coin;
            if (coin >= cost)
            {
                PlayerStats.instance.AddCoin(-cost);
                isSale = false;
                GetPickedUp(InventoryManager.instance);
            }
            else
            {
                return;
            }
        }
        else
        {
            GetPickedUp(InventoryManager.instance);
        }
    }

    void GetPickedUp(InventoryManager inventoryManager)
    {
        if (inventoryManager.full)
        {
            return;
        }
        inventoryManager.AddItem(itemStats);
        Destroy(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isSale)
        {
            InfoUIManager.instance.SetInfo(Helper.GetPos(gameObject, Helper.ObjPosition.Top), Color.white, Defines.InfoButText.PickUp);
        }
        else
        {
            InfoUIManager.instance.SetInfo(Helper.GetPos(gameObject, Helper.ObjPosition.Top), Color.yellow,
            Defines.InfoButText.Buy + "(" + cost + ")");
        }
    }

    public void SetSale(int cost = 500)
    {
        this.cost = cost;
        isSale = true;
    }

    void DestroyItem()
    {
        Destroy(gameObject);
    }
}
