using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshSale : PlayerInteract
{
    [SerializeField] SaleNPC npc;
    bool canRefresh = true;
    void Start()
    {
        interactType = InteractType.NPC;
    }
    public override void NPC()
    {
        if (PlayerStats.instance.coin >= 100 && canRefresh)
        {
            canRefresh = false;
            npc.Refresh();
            PlayerStats.instance.SetCoin(PlayerStats.instance.coin - 100);
            Invoke(nameof(ResetRefresh), 1f);
        }
        else
        {
            return;
        }

    }

    void ResetRefresh()
    {
        canRefresh = true;
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        InfoUIManager.instance.SetInfo(gameObject.transform, Color.yellow, Defines.InfoButText.Refresh + "(100)");
    }
}
