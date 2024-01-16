using UnityEngine;
using DG.Tweening;

public class UpAndDownController : MonoBehaviour
{
    private GameObject player;
    private GameAreaSpawner areaSpawner;

    public BaseTweenData tweenDataPositive;
    public BaseTweenData tweenDataNegative;

    private Tween tweenPositive;
    private Tween tweenNegative;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        areaSpawner = FindObjectOfType<GameAreaSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            if (other.transform.position.y >= 4f)
            {
                OnPLayerDown();
            }
            else if (other.transform.position.y < 4f)
            {
                OnPlayerUp();
            }

            GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void OnPlayerUp()
    {
        ParticleSystem ps = player.gameObject.transform.GetChild(1).transform.GetComponent<ParticleSystem>();
        var main = ps.main;

        if (tweenPositive != null)
        {
            return;
        }

        DOTween.Kill("playerdowntween");

        tweenPositive = tweenDataPositive.GetTween(player.gameObject)
            .SetId("playeruptween")
            .OnStart(delegate
            {
                PlayerCanvasManager.Instance.playerLevelCount.GetLevelTextDown();

                player.gameObject.transform.GetChild(1).transform.DOLocalMoveY(0.45f, .7f).Play();
                main.gravityModifier = 0.1f;
            })
            .OnComplete(delegate
            {
                areaSpawner.SwitchColliders(true, false);
                player.GetComponent<PlayerController>().playerPos = PlayerPos.Top;
            })
            .OnKill(delegate
            {
                tweenPositive = null;
            })
            .Play();
    }

    public void OnPLayerDown()
    {
        ParticleSystem ps = player.gameObject.transform.GetChild(1).transform.GetComponent<ParticleSystem>();
        var main = ps.main;

        if (tweenNegative != null)
        {
            return;
        }

        DOTween.Kill("playeruptween");

        tweenNegative = tweenDataNegative.GetTween(player.gameObject)
             .SetId("playerdowntween")
             .OnStart(delegate
             {
                 PlayerCanvasManager.Instance.playerLevelCount.GetLevelTextUp();

                 player.gameObject.transform.GetChild(1).transform.DOLocalMoveY(-0.75f, 0.7f).Play();
                 main.gravityModifier = -0.1f;
             })
             .OnComplete(delegate
             {
                 areaSpawner.SwitchColliders(false, true);
                 player.GetComponent<PlayerController>().playerPos = PlayerPos.Down;
             })
             .OnKill(delegate
             {
                 tweenNegative = null;
             })
             .Play();
    }
}
