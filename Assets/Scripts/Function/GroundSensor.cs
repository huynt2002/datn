using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public bool isGrounded;
    [SerializeField] Transform groundSensor;
    [SerializeField] Vector2 groundSensorSize;
    [SerializeField] LayerMask groundLayer;
    public Collider2D platformCollider { get; private set; }
    private void FixedUpdate()
    {
        CheckGround();
    }

    public void CheckGround()
    {
        var x = Physics2D.OverlapBox(groundSensor.position, groundSensorSize, 0, groundLayer);
        if (Physics2D.OverlapBox(groundSensor.position, groundSensorSize, 0, groundLayer))
        {
            isGrounded = true;
            if (x.tag == Defines.Tag.OneWayPlatform)
            {
                platformCollider = x.gameObject.GetComponent<Collider2D>();

            }
            else
            {
                platformCollider = null;
            }
        }
        else
        {
            isGrounded = false;
            platformCollider = null;
        }

    }
}
