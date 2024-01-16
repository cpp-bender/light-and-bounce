using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(menuName = "Lights Out/Player Scale Data", fileName = "Player Scale Data")]
public class PlayerScaleData : ScriptableObject
{
    [Header("Scale Data"), Space(5f)]
    [Range(.1f, 10f)] public float scaleOffset = 1f;
    [Range(1f, 10f)] public float minScale = 1f;
    [Range(1f, 10f)] public float maxScale = 3f;

    [Header("Prefabs")]
    public GameObject fakePlayerPrefab;

    [Header("Materials")]
    public Material scaleBaseMat;
    public Material scaleFakeMat;

    [Header("Colors")]
    public Color scaleUpColor = Color.green;
    public Color scaleDownColor = Color.red;

    [Header("Sphere Collider Radius Offset")]
    public float minRadius = .35f;
    public float maxRadius = .75f;
    public float radiusOffset = .1f;

    [Header("Tween Data"), Space(5f)]
    public Ease easeType = Ease.Linear;
    public bool autoKill = true;
    public bool recyclable = true;
    public float duration = .5f;
    public string scaleUpTweenID = "playerScaleUp";
    public string scaleDownTweenID = "playerScaleDown";
}
