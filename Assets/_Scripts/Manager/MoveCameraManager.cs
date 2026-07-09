using DG.Tweening;
using UnityEngine;

public class MoveCameraManager : Singleton<MoveCameraManager>
{
    [SerializeField] private Camera mainCamera;

    protected override void Awake()
    {
        base.Awake();
    }

    public void MoveCamera()
    {
        mainCamera.transform.DOMove(new Vector3(SpawnLevelManager.Instance.currentX, 0, -10), 0.5f).OnComplete(SpawnLevelManager.Instance.SetUp);
    }
}
