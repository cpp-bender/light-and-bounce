using UnityEngine;
using System;

[Serializable]
public class ItemPicker
{
    public int platformIndex = 0;
    public GameObject platformPrefab = null;
    public float posX;

    [HideInInspector]
    public float posZ;
}
