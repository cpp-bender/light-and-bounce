using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public GameAreaSpawner gameAreaSpawner;
    public int index = 0;
    public bool canSpawn = true;

    private GameObject itemsOnPlatform;

    private GameObject itemObject;
    private ReferenceManager referenceManager;
    private float itemPosX;
    private float itemPosZ;

    private void Awake()
    {
        referenceManager = FindObjectOfType<ReferenceManager>();
    }

    public void PlatformLevelDesign()
    {
        for (int i = 0; i < referenceManager.platformsItems[referenceManager.currentPlatformIndex].platformItems.Count; i++)
        {
            if (index == referenceManager.platformsItems[referenceManager.currentPlatformIndex].platformItems[i].platformIndex)
            {
                itemObject = referenceManager.platformsItems[referenceManager.currentPlatformIndex].platformItems[i].platformPrefab;
                itemPosX = referenceManager.platformsItems[referenceManager.currentPlatformIndex].platformItems[i].posX;
                itemPosZ = referenceManager.platformsItems[referenceManager.currentPlatformIndex].platformItems[i].posZ;
                itemPosZ = Random.Range(1f, 2.8f);
                ItemHandler();
                break;
            }
        }
    }

    private void ItemHandler()
    {
        itemsOnPlatform = Instantiate(itemObject, itemObject.transform.position, Quaternion.Euler(0, 0, 0));
        itemsOnPlatform.transform.parent = gameObject.transform;
        itemsOnPlatform.transform.position = new Vector3(itemPosX, transform.position.y, transform.position.z + itemPosZ);
    }

    public void DestroyPrevItem()
    {
        if (itemsOnPlatform != null)
        {
            Destroy(itemsOnPlatform);
        }
    }
}
