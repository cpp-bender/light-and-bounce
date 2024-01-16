using DG.Tweening;
using UnityEngine;

public class LevelEndObsacleController : MonoBehaviour
{
    public GameObject[] smashableParts;
    public float force;
    public ForceMode forceMode = ForceMode.Impulse;
    public int text = 1;

    [Header("VFXS")]
    public ParticleSystem leftConfetti;
    public ParticleSystem rightConfetti;

    private PlayerCanvasManager playerCanvas;
    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        playerCanvas = FindObjectOfType<PlayerCanvasManager>();
        leftConfetti.gameObject.SetActive(false);
        rightConfetti.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            if (playerCanvas.playerLevelCount.GetCurrentLevel() == 1)
            {
                ReferenceManager.Instance.canPlatformsMove = false;
                GameManager.instance.LevelComplete(text);
            }
            else
            {
                player.DoScaleDown();
                ChangeColor();
                //SmashObstacle(other.gameObject.transform);
                if (text == 10)
                {
                    GameManager.instance.LevelComplete(text);
                }
            }
        }
    }

    private void ChangeColor()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().material.DOColor(Color.green, .5f)
            .OnStart(delegate 
            {
                leftConfetti.gameObject.SetActive(true);
                rightConfetti.gameObject.SetActive(true);
            })
            .Play();
    }

    private void SmashObstacle(Transform player)
    {
        foreach (var smashablePart in smashableParts)
        {
            smashablePart.gameObject.SetActive(true);
            Vector3 forceDirection = smashablePart.transform.position - player.position;
            smashablePart.GetComponent<Transform>().parent = null;
            smashablePart.GetComponent<Rigidbody>().useGravity = true;
            smashablePart.GetComponent<Rigidbody>().AddForce(forceDirection * force, ForceMode.Impulse);
            smashablePart.GetComponent<Rigidbody>().AddTorque(forceDirection * force, ForceMode.Impulse);
            transform.gameObject.SetActive(false);
            Destroy(smashablePart, 2f);
        }
    }
}
