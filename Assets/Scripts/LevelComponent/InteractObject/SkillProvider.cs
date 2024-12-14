using UnityEngine;
public class SkillProvider : PlayerInteract
{
    int cost;
    bool canProvide;
    void Start()
    {
        interactType = InteractType.SkillProvide;
        cost = 100;
        canProvide = true;
    }

    public override void SkillProvide()
    {
        if (!canProvide)
        {
            return;
        }
        var skills = PlayerMovement.instance.skills;
        if (skills.Count == 0)
        {
            Debug.LogError("No skills");
            return;
        }
        int randSkill;
        do
        {
            randSkill = Random.Range(0, skills.Count);
        } while (
            PlayerMovement.instance.skill1 == skills[randSkill]
        );

        if (!PlayerMovement.instance.skill1)
        {
            canProvide = false;
        }
        else
        {
            if (PlayerStats.instance.coin < cost)
            {
                return;
            }
            PlayerStats.instance.AddCoin(-cost);
            var x = cost * 0.3f;
            cost += (int)x;
        }
        PlayerMovement.instance.skill1 = skills[randSkill];
        PlayerStats.instance.SetSkill(skills[randSkill].skillId);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!canProvide) { return; }
        if (!PlayerMovement.instance.skill1)
        {
            InfoUIManager.instance.SetInfo(Helper.GetPos(gameObject, Helper.ObjPosition.Top), Color.white, Defines.InfoButText.Focus + "(0)");
            return;
        }
        InfoUIManager.instance.SetInfo(Helper.GetPos(gameObject, Helper.ObjPosition.Top), Color.white, Defines.InfoButText.Change + "(" + cost + ")");
    }
}