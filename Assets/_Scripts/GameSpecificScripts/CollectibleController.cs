using UnityEngine;
using System;

public class CollectibleController : MonoBehaviour
{
    public GameObject pulseWaveObjectPrefab;

    private Action OnPlayerHitEnter;
    private PlayerController player;

    private void Start()
    {
        OnPlayerHitEnter = PlayerHitEnter;
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            OnPlayerHitEnter?.Invoke();
        }
    }

    private void PlayerHitEnter()
    {
        Vector3 worldPos = Vector3.zero;

        if (player.playerPos == PlayerPos.Down)
        {
            worldPos = new Vector3(transform.position.x, .6f, transform.position.z);
        }
        else if (player.playerPos == PlayerPos.Top)
        {
            worldPos = new Vector3(transform.position.x, 9.5f, transform.position.z);
        }

        var pulseWaveObject = Instantiate(pulseWaveObjectPrefab, worldPos, Quaternion.identity);
        pulseWaveObject.GetComponent<PulseWaveController>().DoScaleUp(transform);
        player.DoScaleUp();
        gameObject.SetActive(false);
    }
}
