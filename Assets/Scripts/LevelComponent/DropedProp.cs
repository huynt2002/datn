using UnityEngine;

public class DropedProp : MonoBehaviour
{
    public enum DropType
    {
        None, HP, Coin, Gem
    }
    public float HPAmount;
    [SerializeField] DropType dropType;

    void Awake()
    {
        LevelManager.instance.clearOnLoadLevel += DestroyProp;
    }

    private void OnDestroy()
    {
        LevelManager.instance.clearOnLoadLevel -= DestroyProp;
    }

    void DestroyProp()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (dropType)
        {
            case DropType.HP:
                HP(other.transform.parent.GetComponent<PlayerStats>());
                break;
            // case DropType.Coin:
            //     Coin(other.transform.parent.GetComponent<PlayerStats>());
            //     break;
            // case DropType.Gem:
            //     Gem(other.transform.parent.GetComponent<PlayerStats>());
            //     break;
            default:
                break;
        }

    }

    void HP(PlayerStats e)
    {
        e?.HealHP(HPAmount);
        Destroy(gameObject);
    }

    void Coin()
    {
        PlayerStats e = PlayerStats.instance;
        var amount = Random.Range(100, 300);
        e.AddCoin(amount);
        SoundManager.instance.PlayCoinSound(gameObject.transform);
        Destroy(gameObject);
    }

    void Gem()
    {
        PlayerStats e = PlayerStats.instance;
        var amount = Random.Range(10, 50);
        e.AddGem(amount);
        Destroy(gameObject);
    }
}
