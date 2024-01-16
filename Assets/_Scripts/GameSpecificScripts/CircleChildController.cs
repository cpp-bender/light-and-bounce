using UnityEngine;
using DG.Tweening;

public class CircleChildController : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Tween colorTween;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeMaterialTo(Material newMat)
    {
        if (colorTween != null)
        {
            return;
        }

        colorTween = meshRenderer.material.DOColor(newMat.color, .2f)
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
