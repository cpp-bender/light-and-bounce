using UnityEngine;

public class LevelEndController : MonoBehaviour
{
    [Header("Children Data"), Space(5f)]
    public GameObject[] levelEndObstacles;
    public float offsetZ = 5f;

    [Header("Move Data")]
    public Vector3 moveDir = Vector3.back;
    public float moveSpeed = 5f;
    public bool canMove = false;

    private void Start()
    {

    }

    private void Update()
    {
        if (canMove)
        {
            transform.Translate(moveDir * moveSpeed * Time.deltaTime);
        }
    }

    private void OnValidate()
    {
        for (int i = 0; i < levelEndObstacles.Length; i++)
        {
            Vector3 localPos = new Vector3(transform.position.x, transform.position.y, (i * offsetZ));
            levelEndObstacles[i].transform.localPosition = localPos;
        }
    }
}
