using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_1 : MonoBehaviour
{

    //모든 오브젝트는 특정 지점을 정해서 이동

    //여러가지 이동 구현하기
    [Header("* 움직임 선택")]
    public int choice_move;
    public Transform target;
    [Space]

    //MoveTowrad  방식
    //Vector3.MoveTowards(현재 위치, 목표 위치, 속력)
    //직선으로 이동하는 방식 
    // movement will not overshoot the target
    //현재위치에서 목표위치 방향으로 속력만큼 움직인다
    [Header("* MoveTowrad 이동 변수")]
    public float speed;
    void MoveTowradMoving()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    //SmoothDamp 방식
    //Vector3.SmoothDamp(현재 위치, 목표 위치, 참조 속력, 소요 시간)
    //부드럽게 이동, 어느정도 포물선도 가능하다고 판단
    //일반적인 용도: 카메라를 매끄럽게 움직일 때
    [Header("* SmoothDamp 이동 변수")]
    public float smooth_time = 0.3f; //목표에 도달하는 데 걸리는 대략적인 시간
    private Vector3 velocity = Vector3.zero;
    void SmoothDampMoving()
    {
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 5, -10));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smooth_time);
        //ref velocity 호출할때 마다 함수에 의해 수정 됨
    }

    //Lerp(현재 위치, 목표 위치, 보간 간격)
}
