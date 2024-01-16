using DG.Tweening;
using UnityEngine;

public class PulseWaveController : MonoBehaviour
{
    public BaseTweenData scaleUpTweenData;

    private PlayerController player;
    private Tween scaleUpTween;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.CIRCLE))
        {
            other.GetComponent<CircleController>().PlayerHitEnter();
        }
    }

    public void DoScaleUp(Transform parent)
    {
        #region SCALE UP TWEEN
        if (scaleUpTween != null)
        {
            return;
        }
        scaleUpTween = scaleUpTweenData.GetTween(gameObject)
            .OnStart(delegate
            {
                int upTweenCount = DOTween.Kill("up", true);
                int downTweenCount = DOTween.Kill("down", true);
                int colorTweenCount = DOTween.Kill("color", true);
                // player.GetComponent<SphereCollider>().enabled = false;
                player.gameObject.layer = 9;
            })
            .OnKill(delegate
            {
                // player.GetComponent<SphereCollider>().enabled = true;
                player.gameObject.layer = 7;
                scaleUpTween = null;
                Destroy(gameObject);
            }).Play();
        #endregion
    }
}
