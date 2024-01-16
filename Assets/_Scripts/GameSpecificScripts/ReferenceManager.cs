using DG.Tweening.Core.Enums;
using DG.Tweening;
using UnityEngine;

public class ReferenceManager : SingletonMonoBehaviour<ReferenceManager>
{
    public ItemPickerData[] platformsItems;
    public Material circleScaleUpMat;
    public Material circleScaleDownMat;
    public Material pulseScaleUpMat;
    public Material pulseScaleDownMat;
    public bool canPlatformsMove = true;
    public int currentPlatformIndex;
    public int lastPlatformIndex;

    protected override void Awake()
    {
        base.Awake();
        InitDOTween();
        currentPlatformIndex = (GameManager.instance.currentLevel - 1) % platformsItems.Length;
        SetLastPlatformIndex();
    }

    private void SetLastPlatformIndex()
    {
        int x = 0;
        for (int i = 0; i < platformsItems[currentPlatformIndex].platformItems.Count; i++)
        {
            if (platformsItems[currentPlatformIndex].platformItems[i].platformIndex > x)
            {
                lastPlatformIndex = platformsItems[currentPlatformIndex].platformItems[i].platformIndex;
            }
        }
    }

    public void InitDOTween()
    {
        //Default All DOTween Global Settings
        DOTween.Init(true, true, LogBehaviour.Default);
        DOTween.defaultAutoPlay = AutoPlay.None;
        DOTween.maxSmoothUnscaledTime = .15f;
        DOTween.nestedTweenFailureBehaviour = NestedTweenFailureBehaviour.TryToPreserveSequence;
        DOTween.showUnityEditorReport = false;
        DOTween.timeScale = 1f;
        DOTween.useSafeMode = true;
        DOTween.useSmoothDeltaTime = false;
        DOTween.SetTweensCapacity(1000, 50);

        //Default All DOTween Tween Settings
        DOTween.defaultAutoKill = false;
        DOTween.defaultEaseOvershootOrAmplitude = 1.70158f;
        DOTween.defaultEasePeriod = 0f;
        DOTween.defaultEaseType = Ease.Linear;
        DOTween.defaultLoopType = LoopType.Restart;
        DOTween.defaultRecyclable = false;
        DOTween.defaultTimeScaleIndependent = false;
        DOTween.defaultUpdateType = UpdateType.Normal;
    }
}
