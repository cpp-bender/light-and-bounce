using System.Collections.Generic;
using UnityEngine;

public class RunnerGameArea : MonoBehaviour
{
    public List<GameObject> topCircles;
    public List<GameObject> bottomCircles;

    public GameAreaSpawner spawner;
    public CircleManager bottomCircleManager;
    public CircleManager aboveCircleManager;

    private CharacterMovement characterMovement;
    private ReferenceManager referenceManager;

    void Start()
    {
        characterMovement = FindObjectOfType<CharacterMovement>();
        referenceManager = FindObjectOfType<ReferenceManager>();

    }
    
    void Update()
    {
        if (characterMovement.isGameStarted && referenceManager.canPlatformsMove)
        {
            transform.position += spawner.moveDirection * spawner.movingSpeed * Time.deltaTime;
        }
        
        if (transform.position.z < spawner.destoryZone)
        {
            spawner.RespawnPlatform(transform);
        }
    }
}