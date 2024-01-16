using UnityEngine;

public class TestCylinderController : MonoBehaviour
{
    public TestCylinderScaleData scaleData;
    public float distance;

    private PlayerController player;

    private void Awake()
    {
        transform.localScale = scaleData.DefaultScale;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= 2f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, scaleData.DesiredScale, Time.deltaTime * scaleData.LerpSpeed);
        }
        else if (distance > 2f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, scaleData.DefaultScale, Time.deltaTime * scaleData.LerpSpeed);
        }
    }
}
