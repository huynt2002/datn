using UnityEngine;
public class SkillProvider : PlayerInteract
{
    int cost;
    void Start()
    {
        interactType = InteractType.SkillProvide;
        cost = 100;
    }

    public override void SkillProvide()
    {
        if (!PlayerMovement.instance.skill1)
        {
            return;
        }
        PlayerStats.instance.AddCoin(-cost);
        var x = cost * 0.3f;
        cost += (int)x;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!PlayerMovement.instance.skill1)
        {
            InfoUIManager.instance.SetInfo(Helper.GetPos(gameObject, Helper.ObjPosition.Top), Color.white, Defines.InfoButText.Focus + "(0)");
            return;
        }
        InfoUIManager.instance.SetInfo(Helper.GetPos(gameObject, Helper.ObjPosition.Top), Color.white, Defines.InfoButText.Change + "(" + cost + ")");
    }
}