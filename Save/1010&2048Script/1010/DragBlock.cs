using System.Collections;
using UnityEngine;

public class DragBlock : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curveMovement;//�̵� ���� �׷���
    [SerializeField]
    private AnimationCurve curveScale;//ũ�� ���� �׷���

    private BlockArrangeSystem blockArrangeSystem;

    private float appearTime = 0.5f;//����� ������ �� �ҿ�Ǵ� �ð�
    private float returnTime = 0.1f;//����� ���� ��ġ�� ���ư� �� �ҿ�Ǵ� �ð�
    [field: SerializeField]
    public Vector2Int BlockCount { private set; get; }//�ڱ� ��� ���� (����,����)

    public Color Color { private set; get; }
    public Vector3[] ChildBlocks { private set; get; }

    public void Setup(BlockArrangeSystem blockArrangeSystem, Vector3 parentPosition)
    {
        this.blockArrangeSystem = blockArrangeSystem;

        Color = GetComponentInChildren<SpriteRenderer>().color;

        ChildBlocks = new Vector3[transform.childCount];

        for (int i = 0; i < ChildBlocks.Length; ++i)
        {
            ChildBlocks[i] = transform.GetChild(i).localPosition;
        }
        StartCoroutine(OnMoveTo(parentPosition, appearTime));
    }

    /// <summary>
    /// �ش� ������Ʈ�� Ŭ���� ��
    /// </summary>
    private void OnMouseDown()
    {
        //�巡�� ������ ����� ó�� 0.5�� ũ��� �����Ǵµ�
        //��� ����� ũŰ�� 1�̱� ������ ��� ����� ũ��� ������ 1�� Ȯ��
        StopCoroutine("OnScaleTo");
        StartCoroutine("OnScaleTo", Vector3.one);
    }
    /// <summary>
    /// �ش� ������Ʈ�� �巡���� �� �� ������ ȣ��
    /// </summary>
    private void OnMouseDrag()
    {
        //���� ��� ����� Pivot�� ��ϼ��� ���߾����� �����Ǿ� �ֱ� ������ x��ġ�� �״�� ����ϰ�,
        //y��ġ�� y�� ��� ������ ����(BlockCount.y * 0.5f)�� gap��ŭ �߰��� ��ġ�� ���

        //Camera.main.ScreenToWorldPoint()�� Vector3 ��ǥ�� ���ϸ� z���� ī�޶��� ��ġ�� -10�� ������ ������
        //gap���� z ���� +10 ����� ���� ������Ʈ���� ��ġ�Ǿ� �ִ� z=0���� �����ȴ�.
        Vector3 gap = new Vector3(0, BlockCount.y * 0.5f + 1, 10);
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + gap;
    }
    /// <summary>
    /// �ش� ������Ʈ�� Ŭ���� ������ �� 1ȸ ȣ��
    /// </summary>
    private void OnMouseUp()
    {
        //�ڽ� ��� ������ Ȧ��, ¦�� �� �� �ٸ��� ���
        //���� �ݿø��ϴ� Mathf.RoundToInt()�� �̿��� ����� ������ǿ� ����(Snap)�ؼ� ��ġ
        float x = Mathf.RoundToInt(transform.position.x - BlockCount.x % 2 * 0.5f) + BlockCount.x % 2 * 0.5f;
        float y = Mathf.RoundToInt(transform.position.y - BlockCount.y % 2 * 0.5f) + BlockCount.y % 2 * 0.5f;

        transform.position = new Vector3(x, y, 0);

        bool isSuccess = blockArrangeSystem.TryArrangementBlock(this);

        if (isSuccess==false)
        {
            ////���� ũ�⿡�� 0.5ũ��� ���
            StopCoroutine("OnScaleTo");
            StartCoroutine("OnScaleTo", Vector3.one * 0.5f);
            // ���� ��ġ���� �θ� ������Ʈ ��ġ�� �̵�
            StartCoroutine(OnMoveTo(transform.parent.position, returnTime));
        }
    }
    /// <summary>
    /// ���� ��ġ���� end ��ġ���� time �ð����� �̵�
    /// </summary>
    IEnumerator OnMoveTo(Vector3 end, float time)
    {
        Vector3 start = transform.position;
        float current = 0;
        float percent = 0;
        while(percent<1)
        {
            current += Time.deltaTime;
            percent = current / time;
            transform.position = Vector3.Lerp(start, end, curveMovement.Evaluate(percent));
            yield return null;
        }
    }
    /// <summary>
    /// ���� ũ�⿡�� end ũ����� scaleTime �ð����� Ȯ�� or ���
    /// </summary>
    IEnumerator OnScaleTo(Vector3 end)
    {
        Vector3 start = transform.localScale;
        float current = 0;
        float percent = 0;
        while(percent<1)
        {
            current += Time.deltaTime;
            percent = current / returnTime;

            transform.localScale = Vector3.Lerp(start, end, curveScale.Evaluate(percent));

            yield return null;
        }
    }
}
