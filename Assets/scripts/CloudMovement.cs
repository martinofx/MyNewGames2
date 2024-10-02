using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    private float speed;

    public void SetSpeed(float cloudSpeed)
    {
        speed = cloudSpeed;
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
