using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_1 : MonoBehaviour
{

    //��� ������Ʈ�� Ư�� ������ ���ؼ� �̵�

    //�������� �̵� �����ϱ�
    [Header("* ������ ����")]
    public int choice_move;
    public Transform target;
    [Space]

    //MoveTowrad  ���
    //Vector3.MoveTowards(���� ��ġ, ��ǥ ��ġ, �ӷ�)
    //�������� �̵��ϴ� ��� 
    // movement will not overshoot the target
    //������ġ���� ��ǥ��ġ �������� �ӷ¸�ŭ �����δ�
    [Header("* MoveTowrad �̵� ����")]
    public float speed;
    void MoveTowradMoving()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    //SmoothDamp ���
    //Vector3.SmoothDamp(���� ��ġ, ��ǥ ��ġ, ���� �ӷ�, �ҿ� �ð�)
    //�ε巴�� �̵�, ������� �������� �����ϴٰ� �Ǵ�
    //�Ϲ����� �뵵: ī�޶� �Ų����� ������ ��
    [Header("* SmoothDamp �̵� ����")]
    public float smooth_time = 0.3f; //��ǥ�� �����ϴ� �� �ɸ��� �뷫���� �ð�
    private Vector3 velocity = Vector3.zero;
    void SmoothDampMoving()
    {
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 5, -10));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smooth_time);
        //ref velocity ȣ���Ҷ� ���� �Լ��� ���� ���� ��
    }

    //Lerp(���� ��ġ, ��ǥ ��ġ, ���� ����)
}
