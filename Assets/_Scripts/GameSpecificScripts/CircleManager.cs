using UnityEngine;

public class CircleManager : MonoBehaviour
{
    public SphereCollider[] sphereColliders;

    private void Awake()
    {
        sphereColliders = GetComponentsInChildren<SphereCollider>();
    }

    public void SwitchSphereColliders(bool active)
    {
        for (int i = 0; i < sphereColliders.Length; i++)
        {
            sphereColliders[i].enabled = active;
        }
    }
}
