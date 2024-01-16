using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPlatformController : MonoBehaviour
{
    private GameObject staticItemsOnPlatforms;

    public void StaticItemHandler(List<GameObject> prefabList)
    {
        int prefabIndex = Random.Range(0, 3);
        staticItemsOnPlatforms = Instantiate(prefabList[prefabIndex], transform.position, Quaternion.Euler(0, 0, 0));
        staticItemsOnPlatforms.transform.parent = gameObject.transform;
        staticItemsOnPlatforms.transform.position = new Vector3(Random.Range(-2f, 2f), transform.position.y, transform.position.z + Random.Range(1f, 5f));
    }

    public void DestroyStaticItems()
    {
        if (staticItemsOnPlatforms != null)
        {
            Destroy(staticItemsOnPlatforms);
        }
    }
}
