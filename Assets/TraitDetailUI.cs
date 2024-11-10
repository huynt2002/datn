using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TraitDetailUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI traitNme;
    [SerializeField] TextMeshProUGUI description;
    public void Set(string name, string des)
    {
        traitNme.text = name;
        description.text = des;
    }
}
