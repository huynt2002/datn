using UnityEngine;
public class SkillProvider : PlayerInteract
{
    bool canProvide;
    void Start()
    {
        interactType = InteractType.SkillProvide;
        canProvide = true;
    }

    public override void SkillProvide()
    {
        if (canProvide)
        {
            canProvide = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!canProvide) { return; }
        if (PlayerMovement.instance.skill1)
        {
            InfoUIManager.instance.SetInfo(Helper.GetPos(gameObject, Helper.ObjPosition.Top), Color.white, Defines.InfoButText.Focus);
            return;
        }
        InfoUIManager.instance.SetInfo(Helper.GetPos(gameObject, Helper.ObjPosition.Top), Color.white, Defines.InfoButText.Change);
    }
}