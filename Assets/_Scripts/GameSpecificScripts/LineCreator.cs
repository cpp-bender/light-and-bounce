using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : MonoBehaviour
{
    public GameObject platform;

    public int lineCount;

    private void Awake()
    {
        CreateLines();
    }
    
    void Start()
    {
        
    }

    private void CreateLines()
    {
        for (int i = 0; i < lineCount; i++)
        {
            GameObject platformGO = Instantiate(platform, new Vector3(0f, 0f, (i + 1) * 3f) + transform.position, platform.transform.rotation, transform);
        }
    }
}
