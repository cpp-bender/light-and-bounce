using DG.Tweening;
using UnityEngine;

[SelectionBase]
public class CircleController : MonoBehaviour
{
    public BaseTweenData scaleUpTweenData;
    public BaseTweenData scaleDownTweenData;

    private ReferenceManager referenceManager;
    private CharacterMovement characterMovement;
    public Tween scaleUpTween;
    public Tween scaleDownTween;

    private MeshRenderer meshRenderer;
    private Tween colorTween;

    public Color highlightColor;
    public Color defaultColor;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        referenceManager = FindObjectOfType<ReferenceManager>();
        characterMovement = FindObjectOfType<CharacterMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            PlayerHitEnter();
        }
    }

    public void PlayerHitEnter()
    {
        if (scaleUpTween != null || !characterMovement.isGameStarted)
        {
            return;
        }

        scaleUpTween = scaleUpTweenData.GetTween(gameObject)
            .SetId("up")
            .OnStart(delegate
            {
                ChangeColor(highlightColor);
            })
            .OnKill(delegate
            {
                scaleUpTween = null;
                PlayerHitExit();
            })
            .Play();
    }

    public void PlayerHitExit()
    {
        scaleDownTween = scaleDownTweenData.GetTween(gameObject)
            .SetId("down")
            .OnStart(delegate
            {
                ChangeColor(defaultColor);
            })
            .OnKill(delegate
            {
                scaleDownTween = null;
            })
            .Play();
    }

    public void ChangeColor(Color color)
    {
        colorTween = meshRenderer.material.DOColor(color, .15f)
            .SetId("color")
            .SetAutoKill(true)
            .SetRecyclable(true)
            .OnStart(delegate
            {
                colorTween = null;
            })
            .OnKill(delegate
            {
                colorTween = null;
            })
            .Play();
    }
}
