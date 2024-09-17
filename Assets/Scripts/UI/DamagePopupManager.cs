using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopUpManager : MonoBehaviour
{
    public static DamagePopUpManager instance;
    [SerializeField] GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public DamagePopup Create(Vector3 position, float damageAmount, bool isCriticalHit = false, bool isHeal = false)
    {
        Transform damagePopupTransform = Instantiate(prefab.transform, position, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCriticalHit, isHeal);

        return damagePopup;
    }
}
