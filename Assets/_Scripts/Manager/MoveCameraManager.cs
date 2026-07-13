using DG.Tweening;
using UnityEngine;

public class MoveCameraManager : Singleton<MoveCameraManager>
{
    [SerializeField] private Camera mainCamera;
    private bool isMoving = false;

    protected override void Awake()
    {
        base.Awake();
    }

    public void MoveCamera()
    {
        if (isMoving) return;

        isMoving = true;
        mainCamera.transform.DOMove(new Vector3(SpawnLevelManager.Instance.currentX, 0, -10), 0.5f).OnComplete(() =>
        {
            SpawnLevelManager.Instance.SetUp();
            isMoving = false;
        });
    }
}
