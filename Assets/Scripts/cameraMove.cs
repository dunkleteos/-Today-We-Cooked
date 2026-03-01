using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 8f;

    float fixedZ;

    void Start()
    {
        fixedZ = transform.position.z; // genelde -10
    }

    void LateUpdate()
{
    if (target == null) return;

    transform.position = new Vector3(
        target.position.x,
        target.position.y,
        fixedZ
    );
}

    }

