using UnityEngine;

public class PlayerCanvasManager : SingletonMonoBehaviour<PlayerCanvasManager>
{
    public PlayerCountController playerLevelCount;

    protected override void Awake()
    {
        base.Awake();
    }
}
