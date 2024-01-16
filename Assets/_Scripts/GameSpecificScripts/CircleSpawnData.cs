using UnityEngine;

[CreateAssetMenu(menuName = "Lights Out/Circle Spawn Data", fileName = "Circle Spawn Data")]
public class CircleSpawnData : ScriptableObject
{
    [SerializeField] GameObject circlePrefab;
    [SerializeField] Vector3 initialScale = Vector3.one * 2f;
    [SerializeField] Vector3 startPoint = Vector3.zero;
    [SerializeField] Vector3 initialRot = Vector3.zero;
    [SerializeField] float offsetRow = 1f;
    [SerializeField] float offsetColumn = 1f;
    [SerializeField] float hexagonalOffset = .25f;
    [SerializeField] int circleRowCount = 10;
    [SerializeField] int circleColumnCount = 10;

    public GameObject CirclePrefab { get => circlePrefab; set => circlePrefab = value; }
    public Vector3 StartPoint { get => startPoint; set => startPoint = value; }
    public Vector3 InitialRot { get => initialRot; set => initialRot = value; }
    public int CircleRowCount { get => circleRowCount; set => circleRowCount = value; }
    public int CircleColumnCount { get => circleColumnCount; set => circleColumnCount = value; }
    public float OffsetRow { get => offsetRow; set => offsetRow = value; }
    public float OffsetColumn { get => offsetColumn; set => offsetColumn = value; }
    public float HexagonalOffset { get => hexagonalOffset; set => hexagonalOffset = value; }
    public Vector3 InitialScale { get => initialScale; set => initialScale = value; }
}
