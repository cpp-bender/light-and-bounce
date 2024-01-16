using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerPos playerPos = PlayerPos.Down;
    public PlayerScaleData scaleData;
    public BaseTweenData levelEndTweenData;

    private PlayerCountController playerCount;
    private GameAreaSpawner areaSpawner;
    private MeshRenderer meshRenderer;
    private SphereCollider selfCollider;
    private Tween scaleUpTween;
    private Tween scaleDownTween;
    private Tween levelEndMoveTween;
    private GameObject fakePlayer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        selfCollider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        playerCount = PlayerCanvasManager.Instance.playerLevelCount;
        areaSpawner = FindObjectOfType<GameAreaSpawner>();
    }

    public void DOMoveForLevelEnd()
    {
        if (levelEndMoveTween != null)
        {
            return;
        }

        if (playerPos == PlayerPos.Down)
        {
            return;
        }

        ParticleSystem ps = gameObject.transform.GetChild(1).transform.GetComponent<ParticleSystem>();
        var main = ps.main;

        levelEndMoveTween = levelEndTweenData.GetTween(gameObject)
            .OnStart(delegate
            {
                PlayerCanvasManager.Instance.playerLevelCount.GetLevelTextUp();
            })
            .OnKill(delegate
            {
                areaSpawner.SwitchColliders(false, true);
                GetComponent<PlayerController>().playerPos = PlayerPos.Down;
                gameObject.transform.GetChild(1).transform.localPosition = new Vector3(0f, -0.75f, 0f);
                main.gravityModifier = 0.1f;
                levelEndMoveTween = null;
            })
            .Play();
    }

    public void DoScaleUp()
    {
        if (scaleUpTween != null)
        {
            return;
        }

        DOTween.Kill(scaleData.scaleDownTweenID, false);

        var scaleX = Mathf.Clamp(transform.localScale.x + scaleData.scaleOffset, scaleData.minScale, scaleData.maxScale);
        var scaleY = Mathf.Clamp(transform.localScale.y + scaleData.scaleOffset, scaleData.minScale, scaleData.maxScale);
        var scaleZ = Mathf.Clamp(transform.localScale.z + scaleData.scaleOffset, scaleData.minScale, scaleData.maxScale);
        var newRadius = Mathf.Clamp(selfCollider.radius - scaleData.radiusOffset, scaleData.minRadius, scaleData.maxRadius);

        scaleUpTween = transform.DOScale(new Vector3(scaleX, scaleY, scaleZ), scaleData.duration)
            .SetEase(scaleData.easeType)
            .SetAutoKill(scaleData.autoKill)
            .SetRecyclable(scaleData.recyclable)
             .SetId(scaleData.scaleUpTweenID)
             .OnStart(delegate
             {
                 fakePlayer = Instantiate(scaleData.fakePlayerPrefab);
                 fakePlayer.transform.position = transform.position;
                 fakePlayer.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
                 fakePlayer.GetComponent<MeshRenderer>().material = scaleData.scaleFakeMat;
                 ChangeColor(scaleData.scaleUpColor);
                 playerCount.IncreaseLevelTextBy(1);
                 selfCollider.radius = newRadius;
             })
             .OnKill(delegate
             {
                 meshRenderer.material = scaleData.scaleBaseMat;
                 Destroy(fakePlayer);
                 scaleUpTween = null;
             }).Play();
    }

    public void DoScaleDown()
    {
        if (scaleDownTween != null)
        {
            return;
        }

        DOTween.Kill(scaleData.scaleUpTweenID, false);

        var scaleX = Mathf.Clamp(transform.localScale.x - scaleData.scaleOffset, scaleData.minScale, scaleData.maxScale);
        var scaleY = Mathf.Clamp(transform.localScale.y - scaleData.scaleOffset, scaleData.minScale, scaleData.maxScale);
        var scaleZ = Mathf.Clamp(transform.localScale.z - scaleData.scaleOffset, scaleData.minScale, scaleData.maxScale);
        var newRadius = Mathf.Clamp(selfCollider.radius + scaleData.radiusOffset, scaleData.minRadius, scaleData.maxRadius);

        scaleDownTween = transform.DOScale(new Vector3(scaleX, scaleY, scaleZ), scaleData.duration)
            .SetEase(scaleData.easeType)
            .SetAutoKill(scaleData.autoKill)
            .SetRecyclable(scaleData.recyclable)
            .SetId(scaleData.scaleDownTweenID)
              .OnStart(delegate
              {
                  fakePlayer = Instantiate(scaleData.fakePlayerPrefab);
                  fakePlayer.transform.position = transform.position;
                  fakePlayer.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
                  fakePlayer.GetComponent<MeshRenderer>().material = scaleData.scaleBaseMat;
                  meshRenderer.material = scaleData.scaleFakeMat;
                  ChangeColor(scaleData.scaleDownColor);
                  playerCount.DecreaseLevelTextBy(1);
                  selfCollider.radius = newRadius;
              })
              .OnKill(delegate
              {
                  meshRenderer.material = scaleData.scaleBaseMat;
                  Destroy(fakePlayer);
                  scaleDownTween = null;
              }).Play();
    }

    private void ChangeColor(Color newColor)
    {
        meshRenderer.material.DOColor(newColor, .3f)
            .Play();
    }
}