using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 100f;

    void Update()
    {
        transform.Rotate(Vector3.right * _rotationSpeed * Time.deltaTime);
    }
}
