using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField]
    private BackgroundBlockSpawner backgroundBlockSpawner;
    [SerializeField]
    private BackgroundBlockSpawner foregroundBlockSpawner;
    [SerializeField]
    private DragBlockSpawner dragBlockSpawner;
    [SerializeField]
    private BlockArrangeSystem blockArrangeSystem;
    [SerializeField]
    private UIController uIController;

    public int CurrentSocre { private set; get; }
    public int HighScore { private set; get; }

    private BackgroundBlock[] backgroundBlocks;
    private int currentDragBlockCount;

    private readonly Vector2Int blockCount = new Vector2Int(10, 10);
    private readonly Vector2 blockHalf = new Vector2(0.5f, 0.5f);
    private readonly int maxDragBlockCount=3;

    private List<BackgroundBlock> filledBlockList;

    private void Awake()
    {
        //���� ���� �ʱ�ȭ
        CurrentSocre = 0;
        HighScore = PlayerPrefs.GetInt("HighScore");

        filledBlockList = new List<BackgroundBlock>();
        //�� ������� ���Ǵ� ��� ����� ����
        backgroundBlockSpawner.SpawnBlocks(blockCount, blockHalf);

        //�巡�� ����� ��ġ�� �� ������ ����Ǵ� ��� ����� ����
        backgroundBlocks = new BackgroundBlock[blockCount.x * blockCount.y];
        backgroundBlocks = foregroundBlockSpawner.SpawnBlocks(blockCount, blockHalf);

        blockArrangeSystem.Setup(blockCount, blockHalf, backgroundBlocks, this);
        //�巡�� ��� ����
        StartCoroutine(SpawnDragBlocks());
    }
    public void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            if (Input.GetKeyDown(KeyCode.Home))

            {
                Application.Quit();
            }
        }
    }
    private IEnumerator SpawnDragBlocks()
    {
        //�巡�� ��� ������ �ִ�(3)���� ����
        currentDragBlockCount = maxDragBlockCount;
        //�巡�� ��� ����
        dragBlockSpawner.SpawnBlocks();

        yield return new WaitUntil(() => IsCompleteSpawnBlocks());
    }
    private bool IsCompleteSpawnBlocks()
    {
        int count = 0;
        for (int i = 0; i < dragBlockSpawner.BlockSpawnPoints.Length; i++)
        {
            if(dragBlockSpawner.BlockSpawnPoints[i].childCount!=0 && dragBlockSpawner.BlockSpawnPoints[i].GetChild(0).localPosition==Vector3.zero)
            {
                count++;
            }
        }
        return count == dragBlockSpawner.BlockSpawnPoints.Length;
    }
    public void AfterBlockArrangement(DragBlock block)
    {
        StartCoroutine("OnAfterBlockArrangement", block);
    }
    private IEnumerator OnAfterBlockArrangement(DragBlock block)
    {
        //��ġ�� �Ϸ�� �巡�� ��� ����
        Destroy(block.gameObject);
        //�ϼ��� ���� �ִ��� �˻��ϰ�, �ϼ��� ���� ��ϵ��� ������ ����
        int filledLineCount = CheckFilldeLine();
        //�ϼ��� ���� ������ 0��, �ϼ��� ���� ������ 2�� filledLineCount��*10��(10,20,40,80...)
        int lineScore = filledLineCount == 0 ? 0 : (int)Mathf.Pow(2, filledLineCount - 1) * 10;
        //���� ���
        CurrentSocre += block.ChildBlocks.Length + lineScore;

        yield return StartCoroutine(DestroyFilledBlocks(block));

        currentDragBlockCount--;

        if (currentDragBlockCount == 0)
        {
           yield return StartCoroutine(SpawnDragBlocks());
        }

        yield return new WaitForEndOfFrame();

        if (IsGameOver())
        {
            if(CurrentSocre>HighScore)
            {
                PlayerPrefs.SetInt("HighScore", CurrentSocre);
            }
            uIController.GameOver();
        }
    }
    private int CheckFilldeLine()
    {
        int filledLineCount = 0;
        filledBlockList.Clear();
        //���� �� �˻�
        for (int y = 0 ; y < blockCount.y; ++y)
        {
            int fillBlockCount = 0;
            for (int x = 0; x < blockCount.x; ++x)
            {
                if (backgroundBlocks[y * blockCount.x + x].BlockState == BlockState.Fill)
                    fillBlockCount++;
            }
            if(fillBlockCount==blockCount.x)
            {
                for (int x = 0; x < blockCount.x; ++x)
                {
                    filledBlockList.Add(backgroundBlocks[y * blockCount.x + x]);
                }
                filledLineCount++;
            }
        }
        //���� �� �˻�
        for (int x = 0; x < blockCount.x; ++x)
        {
            int fillBlockCount = 0;
            for (int y = 0; y < blockCount.y; ++y)
            {
                if (backgroundBlocks[y * blockCount.x + x].BlockState == BlockState.Fill)
                    fillBlockCount++;
            }
            if (fillBlockCount == blockCount.y)
            {
                for (int y = 0; y < blockCount.y; ++y)
                {
                    filledBlockList.Add(backgroundBlocks[y * blockCount.x + x]);
                }
                filledLineCount++;
            }
        }
        return filledLineCount;
    }
    private IEnumerator DestroyFilledBlocks(DragBlock block)
    {
        filledBlockList.Sort((a, b) =>
                (a.transform.position - block.transform.position).sqrMagnitude.
                CompareTo((b.transform.position - block.transform.position).sqrMagnitude));
        for (int i = 0; i < filledBlockList.Count; i++)
        {
            filledBlockList[i].EmptyBlock();

            yield return new WaitForSeconds(0.01f);
        }
        filledBlockList.Clear();
    }
    private bool IsGameOver()
    {
        int dragBlcokCont = 0;
        //��ġ ������ �巡�� ����� �������� ��
        for (int i = 0; i < dragBlockSpawner.BlockSpawnPoints.Length; ++i)
        {
            if(dragBlockSpawner.BlockSpawnPoints[i].childCount!=0)
            {
                dragBlcokCont++;
                if (blockArrangeSystem.IsPossibleArrangement(
                    dragBlockSpawner.BlockSpawnPoints[i].GetComponentInChildren<DragBlock>()))
                {
                    return false;
                }
            }
        }
        return dragBlcokCont != 0;
    }
}
