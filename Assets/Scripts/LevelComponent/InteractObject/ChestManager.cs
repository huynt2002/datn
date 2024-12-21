using UnityEngine;

public class ChestManager : PlayerInteract
{
    bool open;
    [SerializeField] SpawnManager.ChestType chestType;
    void Start()
    {
        animator = GetComponent<Animator>();
        interactType = InteractType.Chest;
        open = false;
    }

    public void SpawnItem()
    {
        //animation
        SpawnManager.instance.SpawnItemFromChest(transform.position, chestType);
    }

    public override void Chest()
    {
        animator.SetTrigger("open");
        gameObject.GetComponent<Collider2D>().enabled = false;
        SoundManager.instance.PlayOpenChestSound(transform);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        InfoUIManager.instance.SetInfo(Helper.GetPos(gameObject, Helper.ObjPosition.Top), Color.white, Defines.InfoButText.Check);
    }
}
