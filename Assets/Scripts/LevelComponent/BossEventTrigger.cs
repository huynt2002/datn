using Cinemachine;
using UnityEngine;

public class BossEventTrigger : MonoBehaviour
{
    [SerializeField] GameObject timeline;
    [SerializeField] Transform target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isBossFight)
        {
            timeline?.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.instance.isBossFight = true;
        SoundManager.instance.PlayBossFightMusic();
        timeline?.SetActive(true);
        GameObject.FindWithTag("MainCamera").GetComponent<CinemachineVirtualCamera>().m_Follow = target;
        gameObject.SetActive(false);
    }

    public void OnBossDefeated()
    {
        GameManager.instance.isBossFight = false;
        SoundManager.instance.PlayMainMusic();
    }
}
