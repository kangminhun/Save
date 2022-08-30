using UnityEngine;
using System.Collections;

public enum BlockState {Empty=0,Fill=1 }

public class BackgroundBlock : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; //��� ����� ���� ��� ���� ������Ʈ

    public BlockState BlockState { private set; get; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        BlockState = BlockState.Empty;
    }
    /// <summary>
    /// �巡�� ����� ��� ��Ͽ� ��ġ���� ��
    /// ��� ����� ������ �巡�� ��ϰ� �����ϰ� ����
    /// </summary>
    public void FillBlock(Color color)
    {
        BlockState = BlockState.Fill;
        spriteRenderer.color = color;
    }
    public void EmptyBlock()
    {
        BlockState = BlockState.Empty;
        StartCoroutine("ScaleTo", Vector3.zero);
    }
    /// <summary>
    /// 0.15�� ���� ����� ũ�⸦ 1���� 0���� ����ϰ�,
    /// ����� ����� ũ�� �缳��
    /// </summary>
    private IEnumerator ScaleTo(Vector3 end)
    {
        Vector3 start = transform.localScale;
        float current = 0;
        float percent = 0;
        float time = 0.15f;

        while(percent<1)
        {
            current += Time.deltaTime;
            percent = current / time;
            transform.localScale = Vector3.Lerp(start, end, percent);
            yield return null;
        }
        spriteRenderer.color = Color.white;
        transform.localScale = Vector3.one;
    }
}
