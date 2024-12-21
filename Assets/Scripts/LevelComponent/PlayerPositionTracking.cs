using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public Transform pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = PlayerStats.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        pos = PlayerStats.instance.transform;
        transform.position = pos.position;
    }
}
