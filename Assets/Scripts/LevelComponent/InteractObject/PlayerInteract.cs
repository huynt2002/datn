using UnityEngine;

public abstract class PlayerInteract : MonoBehaviour
{
    public enum InteractType
    {
        None, Gate, Chest, NPC, Item, SkillProvide
    }
    public InteractType interactType { get; protected set; }
    protected Animator animator;
    public virtual void Gate() { }
    public virtual void Chest() { }
    public virtual void NPC() { }
    public virtual void Item() { }
    public virtual void SkillProvide() { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerStats.instance.GetComponent<PlayerMovement>().playerInteract = this;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (PlayerStats.instance.GetComponent<PlayerMovement>().playerInteract == this)
        {
            PlayerStats.instance.GetComponent<PlayerMovement>().playerInteract = null;
        }
        InfoUIManager.instance?.Disable();
    }
}
