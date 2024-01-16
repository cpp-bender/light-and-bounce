using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public bool canMoveForward;
    public bool canMoveSideways;
    public bool isGameStarted;
    public float leftLimit = -2f;
    public float rightLimit = 2f;
    public float forwardSpeed = 0f;

    private float swipeSensivity;
    private float maximumSensivity = 100f;

    private Vector3 targetPos;
    private PlayerController player;

    private bool lockLeft;
    private bool lockRight;

    private void Start()
    {
        TouchManager.instance.onTouchBegan += TouchBegan;
        TouchManager.instance.onTouchMoved += TouchMoved;
        TouchManager.instance.onTouchEnded += TouchEnded;
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (!isGameStarted && Input.GetMouseButtonDown(0))
        {
            isGameStarted = true;
            canMoveForward = true;
            canMoveSideways = true;
        }
        Movement();
    }

    private void TouchBegan(TouchInput touch)
    {
        targetPos = transform.position;
    }

    private void TouchEnded(TouchInput touch)
    {
        targetPos = transform.position;
        swipeSensivity = 0f;
    }

    private void TouchMoved(TouchInput touch)
    {
        if (!canMoveSideways)
        {
            targetPos = new Vector3(0f, targetPos.y, targetPos.z);
            return;
        }

        swipeSensivity = Mathf.Abs(touch.DeltaScreenPosition.x);

        if (swipeSensivity > maximumSensivity)
        {
            swipeSensivity = maximumSensivity;
        }

        if (touch.DeltaScreenPosition.x > 0)
        {
            if (rightLimit < transform.position.x - (swipeSensivity / 1000f))
            {
                targetPos = new Vector3(transform.position.x + (swipeSensivity / 1000f), transform.position.y, transform.position.z);
            }
            else
            {
                targetPos = new Vector3(rightLimit, transform.position.y, transform.position.z);
            }

        }
        else if (touch.DeltaScreenPosition.x < 0)
        {
            if (leftLimit > transform.position.x + (swipeSensivity / 1000f))
            {
                targetPos = new Vector3(transform.position.x + (((swipeSensivity) / -1000f)), transform.position.y, transform.position.z);
            }
            else
            {
                targetPos = new Vector3(leftLimit, transform.position.y, transform.position.z);
            }

        }
        else
        {
            targetPos = transform.position;
        }
    }

    private void Movement()
    {
        if (!canMoveForward)
        {
            return;
        }

        if (isGameStarted)
        {
            if ((transform.position.x - targetPos.x < 0 && !lockRight) || (transform.position.x - targetPos.x > 0 && !lockLeft))
            {
                if (Time.timeScale >= 0.5f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * swipeSensivity / 2f);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.unscaledDeltaTime * swipeSensivity / 2f);
                }
            }

            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f), Time.deltaTime * forwardSpeed);

        }
    }
}