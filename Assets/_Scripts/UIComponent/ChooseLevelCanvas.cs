using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ChooseLevelCanvas : MonoBehaviour
{
    private const int CONTENT_X_DISTANCE = 1500;

    [SerializeField] private int numberOfLevels = 30;

    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private List<GameObject> contentContainer;

    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;

    private int workSpace = 0;

    private int showContentIndex = 0;
    private bool isMoving = false;

    private void OnEnable()
    {
        SpawnLevelButtons();
    }

    private void Update()
    {
        left.SetActive(showContentIndex > 0);
        right.SetActive(showContentIndex < contentContainer.Count - 1);
    }

    private void SpawnLevelButtons()
    {
        workSpace = 0;

        for (int i = 0; i < contentContainer.Count; i++)
        {
            foreach (Transform child in contentContainer[i].transform)
            {
                Destroy(child.gameObject);
            }
        }

        for (int i = 0; i < numberOfLevels; i++)
        {
            int temp = i + 1;

            if (temp % 17 == 0)
            {
                workSpace++;
            }

            GameObject levelButtonPrefabIns = Instantiate(levelButtonPrefab, contentContainer[workSpace].transform);
            levelButtonPrefabIns.GetComponent<LevelButton>().Init(temp);
        }
    }

    public void MoveToRight()
    {
        if (!isMoving && showContentIndex < contentContainer.Count - 1)
        {
            showContentIndex++;
            MoveContainers(-CONTENT_X_DISTANCE);
        }
    }

    public void MoveToLeft()
    {
        if (!isMoving && showContentIndex > 0)
        {
            showContentIndex--;
            MoveContainers(CONTENT_X_DISTANCE);
        }
    }

    private void MoveContainers(float distance)
    {
        isMoving = true;

        for (int i = 0; i < contentContainer.Count; i++)
        {
            float targetX = contentContainer[i].transform.localPosition.x + distance;

            if (i == 0)
            {
                contentContainer[i].transform.DOLocalMoveX(targetX, 0.5f)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() => isMoving = false);
            }
            else
            {
                contentContainer[i].transform.DOLocalMoveX(targetX, 0.5f)
                    .SetEase(Ease.OutQuad);
            }
        }
    }
}
