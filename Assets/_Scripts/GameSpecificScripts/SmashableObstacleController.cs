using System.Collections;
using UnityEngine;

public class SmashableObstacleController : MonoBehaviour
{
    public GameObject[] smashableParts;
    public float force;
    public ForceMode forceMode = ForceMode.Impulse;

    private PlayerController player;
    private PlayerCanvasManager playerCanvas;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        playerCanvas = FindObjectOfType<PlayerCanvasManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            if (gameObject.CompareTag(Tags.BLACK_WALL))
            {
                if (playerCanvas.playerLevelCount.GetCurrentLevel() == 1)
                {
                    ReferenceManager.Instance.canPlatformsMove = false;
                    SmashObstacle(other.gameObject.transform);
                    GameManager.instance.LevelFail();
                }
                else
                {
                    player.DoScaleDown();
                    SmashObstacle(other.gameObject.transform);
                }
            }
            else
            {
                SmashObstacle(other.gameObject.transform);
                GameManager.instance.CollectGem(player.transform.position, 10);
            }
        }
    }

    private void SmashObstacle(Transform player)
    {
        foreach (GameObject smashables in smashableParts)
        {
            smashables.gameObject.SetActive(true);
            smashables.transform.parent = null;
            transform.gameObject.SetActive(false);
            transform.GetComponent<BoxCollider>().enabled = false;
            var forceDirection = smashables.transform.position - player.position;
            smashables.GetComponent<Transform>().parent = null;
            smashables.GetComponent<Rigidbody>().useGravity = true;
            smashables.GetComponent<Rigidbody>().AddForce(forceDirection * force, ForceMode.Impulse);
            smashables.GetComponent<Rigidbody>().AddTorque(forceDirection * force, ForceMode.Impulse);
            Destroy(smashables, 6f);
        }
    }
}
