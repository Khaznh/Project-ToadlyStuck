using UnityEngine;

public class LevelChooseData : Singleton<LevelChooseData>
{
    public int levelIndex;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
