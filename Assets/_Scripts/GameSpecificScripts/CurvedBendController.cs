using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmazingAssets.CurvedWorld;

public class CurvedBendController : MonoBehaviour
{
    private CurvedWorldController curvedWorldController;
    private CharacterMovement characterMovement;
    
    private float speed;
    private float timerForCurvatureChange;
    
    private Vector2 startCurvature;
    private Vector2 finalCurvature;
    
    void Start()
    {
        curvedWorldController = gameObject.GetComponent<CurvedWorldController>();
        characterMovement = FindObjectOfType<CharacterMovement>();

        timerForCurvatureChange = 1f;
        speed = 0.2f;
    }

    void Update()
    {
        if (characterMovement.isGameStarted && !GameManager.instance.isLevelCompleted)
        {
            timerForCurvatureChange += Time.deltaTime * speed;
            
            curvedWorldController.bendHorizontalSize = Mathf.Lerp(startCurvature.x, finalCurvature.x, timerForCurvatureChange);
            curvedWorldController.bendVerticalSize = Mathf.Lerp(startCurvature.y, finalCurvature.y, timerForCurvatureChange);

            if (timerForCurvatureChange >= 1f)
            {
                UpdateCurvature();
                timerForCurvatureChange = 0f;
            }
        }
    }

    private void UpdateCurvature()
    {
        startCurvature = new Vector2(curvedWorldController.bendHorizontalSize, curvedWorldController.bendVerticalSize);
        finalCurvature = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
    }
}
