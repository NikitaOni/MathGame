using UnityEngine;

public class RotateLight : MonoBehaviour
{
    private float rotateSpeed = -30.0f;

    void Update()
    {
        transform.Rotate(0f, 0f ,rotateSpeed * Time.deltaTime);
    }
}
