using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockArrangeSystem : MonoBehaviour
{
    private Vector2Int blockCount;
    private Vector2 blockHalf;
    private BackgroundBlock[] backgroundBlocks;
    private StageController stageController;

    public void Setup(Vector2Int blockCount,Vector2 blockHalf,
                      BackgroundBlock[] backgroundBlocks,StageController stageController)
    {
        this.blockCount = blockCount;
        this.blockHalf = blockHalf;
        this.backgroundBlocks = backgroundBlocks;
        this.stageController = stageController;
    }
    /// <summary>
    /// �Ű������� �޾ƿ� block�� ��ġ�� �� �ִ��� �˻��ϰ�,
    /// ��ġ�� �����ϸ� ��� ��ġ, �� �ϼ� �˻�, ���� ��� ó��
    /// </summary>
    public bool TryArrangementBlock(DragBlock block)
    {
        //��� ��ġ�� �������� �˻�
        for (int i = 0; i < block.ChildBlocks.Length; ++i)
        {
            //�ڽ� ����� ���� ��ġ(�θ��� ���� ��ǥ+�ڽ��� ���� ��ǥ)
            Vector3 position = block.transform.position + block.ChildBlocks[i];
            //����� �� ���ο� ��ġ�ϰ� �ִ���?
            if (!IsBlockInsideMap(position))
                return false;
            //���� ��ġ�� �̹� �ٸ� ����� ��ġ�Ǿ� �ִ���?
            if (!IsOtherBlockInThisBlock(position))
                return false;
        }
        //��� ��ġ
        for (int i = 0; i < block.ChildBlocks.Length; ++i)
        {
            //�ڽ� ����� ���� ��ġ (�θ��� ���� ��ǥ+ �ڽ��� ���� ��ǥ)
            Vector3 position = block.transform.position + block.ChildBlocks[i];
            //�ش� ��ġ�� �ִ� ��� ����� ������ �����ϰ�, ä�����κ���
            backgroundBlocks[PositionToIndex(position)].FillBlock(block.Color);
        }

        stageController.AfterBlockArrangement(block);
        return true;
    }
    /// <summary>
    /// �Ű������� �޾ƿ� ��ġ(position)�� ��� ������� �ٱ����� �˻�
    /// ������� �ٱ��̸� false,�����̸� true ��ȯ
    /// </summary>
    private bool IsBlockInsideMap(Vector2 position)
    {
        if (position.x < -blockCount.x * 0.5f + blockHalf.x || position.x > blockCount.x * 0.5f - blockHalf.x ||
            position.y < -blockCount.y * 0.5f + blockHalf.y || position.y > blockCount.y * 0.5f - blockHalf.y)
        {
            return false;
        }
        return true;
    }
    /// <summary>
    /// �Ű������� �޾ƿ� ��ġ(position) ������ �������� �ʿ� ��ġ�� ����� ����(index)�� ����ؼ� ��ȯ
    /// </summary>
    private int PositionToIndex(Vector2 position)
    {
        float x = blockCount.x * 0.5f - blockHalf.x + position.x;
        float y = blockCount.y * 0.5f - blockHalf.y - position.y;

        return (int)(y * blockCount.x + x);
    }
    /// <summary>
    ///  ���� ��ġ(position)�� �ִ� ����� ����ִ��� �˻� �� ��� ��ȯ
    ///  ����� ��������� true, ��������� false
    /// </summary>
    private bool IsOtherBlockInThisBlock(Vector2 position)
    {
        int index = PositionToIndex(position);
        if (backgroundBlocks[index].BlockState == BlockState.Fill)
        {
            return false;
        }
        return true;
    }

    public bool IsPossibleArrangement(DragBlock block)
    {
        for (int y = 0; y < blockCount.y; ++y)
        {
            for (int x = 0; x < blockCount.x; ++x)
            {
                int count = 0;
                Vector3 position = new Vector3(-blockCount.x * 0.5f + blockHalf.x + x, blockCount.y * 0.5f - blockHalf.y - y, 0);

                position.x = block.BlockCount.x % 2 == 0 ? position.x + 0.5f : position.x;
                position.y = block.BlockCount.y % 2 == 0 ? position.y - 0.5f : position.y;
                for (int i = 0; i < block.ChildBlocks.Length; i++)
                {
                    Vector3 blockPosition = block.ChildBlocks[i] + position;

                    if (!IsBlockInsideMap(blockPosition))
                        break;
                    if (!IsOtherBlockInThisBlock(blockPosition))
                        break;
                    count++;
                }
                if(count==block.ChildBlocks.Length)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
