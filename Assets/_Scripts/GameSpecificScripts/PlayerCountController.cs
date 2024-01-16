using DG.Tweening;
using UnityEngine;
using TMPro;

public class PlayerCountController : MonoBehaviour
{
    private PlayerController player;
    private TextMeshProUGUI levelText;
    private RectTransform rectTransform;
    private int currentLevel;

    private void Awake()
    {
        levelText = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        InitLevelText();
    }

    public void InitLevelText()
    {
        currentLevel = 1;
        levelText.text = currentLevel.ToString();
    }

    public void GetLevelTextDown()
    {
        rectTransform.DOLocalMoveY(-.5f, 1f).SetEase(Ease.InOutBack).SetRelative().Play();
    }

    public void GetLevelTextUp()
    {
        rectTransform.DOLocalMoveY(.5f, 1f).SetEase(Ease.InOutBack).SetRelative().Play();
    }

    public void IncreaseLevelTextBy(int addCount = 1)
    {
        currentLevel += addCount;
        currentLevel = Mathf.Clamp(currentLevel, 1, 10);
        levelText.text = currentLevel.ToString();
    }

    public void DecreaseLevelTextBy(int addCount = 1)
    {
        currentLevel -= addCount;
        currentLevel = Mathf.Clamp(currentLevel, 1, 10);
        levelText.text = currentLevel.ToString();
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
