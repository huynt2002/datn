using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    [SerializeField] Animator animator;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            animator.SetTrigger("start");
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
