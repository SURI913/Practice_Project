using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Moving_1 : MonoBehaviour
{
    enum MoveType{
        MoveTowad, SmoothDamp, Lerp, Slerp
    }

    //��� ������Ʈ�� Ư�� ������ ���ؼ� �̵�

    //�������� �̵� �����ϱ�
    [Header("* ������ ����")]
    public int choice_move = 999;
    public Transform target;
    [Space]

    //MoveTowrad  ���
    //Vector3.MoveTowards(���� ��ġ, ��ǥ ��ġ, �ӷ�)
    //�������� �̵��ϴ� ��� 
    // movement will not overshoot the target
    //������ġ���� ��ǥ��ġ �������� �ӷ¸�ŭ �����δ�
    [Header("* MoveTowrad �̵� ����")]
    public float speed = 999;

    void MoveTowradMoving()
    {
        if(speed == 999) { Debug.LogError("MoveTowrad�� speed �� ����"); return; }
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    //SmoothDamp ���
    //Vector3.SmoothDamp(���� ��ġ, ��ǥ ��ġ, ���� �ӷ�, �ҿ� �ð�)
    //�ε巴�� �̵�, ������� �������� �����ϴٰ� �Ǵ�
    //�Ϲ����� �뵵: ī�޶� �Ų����� ������ ��
    [Header("* SmoothDamp �̵� ����")]
    public float smooth_time = 999; //��ǥ�� �����ϴ� �� �ɸ��� �뷫���� �ð�
    private Vector3 velocity = Vector3.zero;
    void SmoothDampMoving()
    {
        if (smooth_time == 999) { Debug.LogError("SmoothDamp�� smooth_time �� ����"); return; }

        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smooth_time);
        //ref velocity ȣ���Ҷ� ���� �Լ��� ���� ���� ��
    }

    //Lerp(���� ��ġ, ��ǥ ��ġ, ���� ����) Linear interpolation ���� ����!
    //�����(0~1)�̹Ƿ� ���������� 0�̸� ������ġ, 1�̸� ��ǥ��ġ��
    //���� ������ ���ؼ� �������� �ε巴�� ����ų�, ������ �̵��� ���� �� ����
    //���⼭ ���� ������ Time.deltaTime�� ���⵵ ��

    //�� t ���� Time.deltaTime���� �޾ƿ� ��� b ��ǥ��(1)�� ������ �������� ���ϰ� 0.9999999....���� ���� ���߰� �ȴ�.
    //���� ��ǥ���� �����ߴ��� ���θ� �ľ��Ϸ��� " c == 1 "�� �ƴ�,
    //" c >= 0.99999f " �� ���� �ּ����� ������ �����Ͽ� �Ǵ���

    //������ �̵��� ����� �� ������ӿ��� ��Ʈ�� ���� �� ���

    // �Ʒ� ��ó�� ���
    //currentTime += Time.deltaTime;
    //Vector3.Lerp(transform.position, target.position, currentTime / 2);
    //t ���� 100%�̸� �Ϻ��ϰ� ������ ���� �ǹ��Ѵ�.
    //��, currentTime�� 0, 1, 2ó�� ��� �÷��ְ� / 2 ���ָ� 1, �ᱹ 100%�� �ȴ�.
    //// ���ĺ���
    //c=(1 - a) * s + a* T
    //���̵��� ���̵� �ƿ��� �������� ���� �� �̷��� ������������ ���� �ٲ���
    //c(color), a(alpha)
    //��� color = color1 * weight1 + color2*weight2; -�� ������ �����ϰ� ���� ����
    //// 1-a �� 0�̸� �⺻ ��, ���� Ŀ���� ����
    //�ִϸ��̼� �����ӿ��� �� �Լ� ����� �پ��� �̵� ǥ��

    [Header("* Lerp �̵� ����")]
    public float L_interpolation_length = 999; //���� ���� �������� �ε巯�� ������

    void LerpMoving()
    {
        if (L_interpolation_length == 999) { Debug.LogError("Lerp�� L_interpolation_length �� ����"); return; }

        transform.position = Vector3.Lerp(transform.position, target.position, 0.001f);
    }

    //Slerp (���� ��ġ, ��ǥ ��ġ, ���� ����) - ���� ���� ���� �Ѹ��� ���󰣴� �����ϸ��
    //���ΰ�꿡 ���� �������ε� ���� ����
    [Header("* Slerp �̵� ����")]
    public float SL_interpolation_length = 999; //���� ���� �������� �ε巯�� ������
    void SlerpMoving()
    {
        if (SL_interpolation_length == 999) { Debug.LogError("Slerp�� SL_interpolation_length �� ����"); return; }

        transform.position = Vector3.Slerp(transform.position, target.position, 0.01f);
    }

    private void Update()
    {
        if (choice_move == 999) { Debug.LogError("choice_move �� ����"); return; }
        if (target == null) { Debug.LogError("target �� ����"); return; }

        switch (choice_move)
        {
            case (int)MoveType.MoveTowad:
                MoveTowradMoving();
                break;
            case (int)MoveType.SmoothDamp:
                SmoothDampMoving();
                break;
            case (int)MoveType.Lerp:
                LerpMoving();
                break;
            case (int)MoveType.Slerp:
                SlerpMoving();
                break;
        }
    }
}
