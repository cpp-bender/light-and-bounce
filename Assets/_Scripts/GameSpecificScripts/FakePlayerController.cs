using UnityEngine;

public class FakePlayerController : MonoBehaviour
{
    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        transform.position = player.transform.position;
    }
}
