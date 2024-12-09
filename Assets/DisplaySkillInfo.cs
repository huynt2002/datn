using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplaySkillInfo : MonoBehaviour
{
    public AttackSkill skill;
    [SerializeField] TextMeshProUGUI skillName;
    [SerializeField] TextMeshProUGUI skillDescription;
    private void OnEnable()
    {
        if (skill == null)
        {
            skillName.text = skill.skillName;
            skillDescription.text = skill.skillDescription;
        }
    }
}
