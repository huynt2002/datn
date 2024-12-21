using UnityEngine;

public class MultiTimeEffect : ItemEffect
{
    public float effectCD = 5f;
    protected float countCD = 0;
    public virtual bool Check()
    {
        if (countCD < effectCD)
        {
            countCD += Time.deltaTime;
            return false;
        }
        return true;
    }

    private void Start()
    {
        entity = gameObject.GetComponentInParent<Entity>();
    }

    public void ResetCD(float cd = 0f)
    {
        countCD = cd;
    }
    public override void ApplyEffect()
    {

    }

    public override void DestroyEffect()
    {

    }

    void Update()
    {
        if (Check())
        {
            ApplyEffect();
        }
    }
}
