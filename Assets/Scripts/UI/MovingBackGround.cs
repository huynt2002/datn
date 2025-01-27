using UnityEngine;

public class MovingBackGround : MonoBehaviour
{
    [SerializeField] Vector2 originPos;
    [SerializeField] Vector2 finishPos;
    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < finishPos.x)
        {
            transform.position = originPos;
        }
        transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }
}
