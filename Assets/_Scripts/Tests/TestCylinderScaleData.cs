using UnityEngine;

[CreateAssetMenu(menuName = "Lights Out/CylinderScaleData", fileName = "Cylinder Scale Data")]
public class TestCylinderScaleData : ScriptableObject
{
    [SerializeField] Vector3 defaultScale = new Vector3(.5f, 1f, .5f);
    [SerializeField] Vector3 desiredScale = Vector3.one;
    [SerializeField] float lerpSpeed = 5f;

    public Vector3 DefaultScale { get => defaultScale; set => defaultScale = value; }
    public Vector3 DesiredScale { get => desiredScale; set => desiredScale = value; }
    public float LerpSpeed { get => lerpSpeed; set => lerpSpeed = value; }
}
