using UnityEngine;

public class RunnerCameraFollow : MonoBehaviour
{
    public GameObject target;
    public Vector3 followOffset;
    public float followSpeed;
    public bool canFollow;

    private void Start()
    {
        target = FindObjectOfType<PlayerController>().gameObject;
        canFollow = true;
    }

    private void LateUpdate()
    {
        if (canFollow)
        {
            Follow();
        }
    }

    private void Follow()
    {
        Vector3 desiredPos = new Vector3(0f, 0f, target.transform.position.z) - followOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * followSpeed);
    }
}
