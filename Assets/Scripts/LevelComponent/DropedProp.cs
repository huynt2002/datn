using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropedProp : MonoBehaviour
{
    public enum DropType
    {
        None, HP, Coin, Gem
    }
    public float HPAmount;
    [SerializeField] DropType dropType;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake()
    {
        LevelManager.instance.clearOnLoadLevel += DestroyProp;
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {

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
        //how much coin and gem
        e.SetCoin(e.coin + amount);
        SoundManager.instance.PlayCoinSound(gameObject.transform);
        Destroy(gameObject);
    }

    void Gem()
    {
        PlayerStats e = PlayerStats.instance;
        var amount = Random.Range(10, 50);
        //how much coin and gem
        e.SetGem(e.gem + amount);
        Destroy(gameObject);
    }
}
