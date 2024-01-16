using Random = UnityEngine.Random;
using UnityEngine;

public class GameAreaSpawner : MonoBehaviour
{
    public PlayerController player;
    public GameObject platform;
    public GameObject[] platforms;
    public Vector3 moveDirection;
    public int currentPlatformIndex;
    public int platformCount;
    public float movingSpeed;
    public float gameAreaLength;
    public float destoryZone;

    [HideInInspector]
    public int random;
    [HideInInspector]
    public float randomPosX;
    [HideInInspector]
    public float randomPosZ;

    private LevelEndController levelEnd;
    private ReferenceManager referenceManager;
    private GameObject lastChunk;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        referenceManager = FindObjectOfType<ReferenceManager>();
        levelEnd = FindObjectOfType<LevelEndController>();
        currentPlatformIndex = 1;
        CreatePlatforms();
        SwitchColliders(false, true);
    }

    private void CreatePlatforms()
    {
        platforms = new GameObject[platformCount + 1];
        platforms[0] = platform;

        for (int i = 0; i < platformCount; i++)
        {
            GameObject platformGO = Instantiate(platform, new Vector3(0f, 0f, (i + 1) * 3f) + transform.position, platform.transform.rotation, transform);
            platformGO.GetComponent<PlatformController>().index = currentPlatformIndex;
            platformGO.GetComponent<PlatformController>().PlatformLevelDesign();
            currentPlatformIndex++;
            platforms[i + 1] = platformGO;
        }
        platforms[0].gameObject.SetActive(false);
    }

    public void SwitchColliders(bool above, bool bottom)
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].GetComponent<RunnerGameArea>().aboveCircleManager.SwitchSphereColliders(above);
            platforms[i].GetComponent<RunnerGameArea>().bottomCircleManager.SwitchSphereColliders(bottom);
        }
    }

    public void RandomCalls()
    {
        random = Random.Range(0, 11);
        randomPosZ = Random.Range(1f, 2.8f);
        randomPosX = Random.Range(-2f, 2f);
    }

    public void RespawnPlatform(Transform platformToRespawn)
    {
        CheckForLevelEnd();

        Vector3 newPos = platformToRespawn.transform.position;
        newPos.z += gameAreaLength;

        lastChunk = platformToRespawn.gameObject;
        lastChunk.transform.position = newPos;

        lastChunk.GetComponent<PlatformController>().DestroyPrevItem();
        lastChunk.GetComponent<PlatformController>().index += platforms.Length;
        lastChunk.GetComponent<PlatformController>().PlatformLevelDesign();
    }

    private void CheckForLevelEnd()
    {
        if (levelEnd.canMove)
        {
            return;
        }


        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i].GetComponent<PlatformController>().index == referenceManager.lastPlatformIndex + 15)
            {
                player.DOMoveForLevelEnd();
                levelEnd.canMove = true;
                return;
            }
        }
    }
}
