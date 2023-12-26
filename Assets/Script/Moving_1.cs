using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Moving_1 : MonoBehaviour
{
    enum MoveType{
        MoveTowad, SmoothDamp, Lerp, Slerp
    }

    //모든 오브젝트는 특정 지점을 정해서 이동

    //여러가지 이동 구현하기
    [Header("* 움직임 선택")]
    public int choice_move = 999;
    public Transform target;
    [Space]

    //MoveTowrad  방식
    //Vector3.MoveTowards(현재 위치, 목표 위치, 속력)
    //직선으로 이동하는 방식 
    // movement will not overshoot the target
    //현재위치에서 목표위치 방향으로 속력만큼 움직인다
    [Header("* MoveTowrad 이동 변수")]
    public float speed = 999;

    void MoveTowradMoving()
    {
        if(speed == 999) { Debug.LogError("MoveTowrad에 speed 값 없음"); return; }
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    //SmoothDamp 방식
    //Vector3.SmoothDamp(현재 위치, 목표 위치, 참조 속력, 소요 시간)
    //부드럽게 이동, 어느정도 포물선도 가능하다고 판단
    //일반적인 용도: 카메라를 매끄럽게 움직일 때
    [Header("* SmoothDamp 이동 변수")]
    public float smooth_time = 999; //목표에 도달하는 데 걸리는 대략적인 시간
    private Vector3 velocity = Vector3.zero;
    void SmoothDampMoving()
    {
        if (smooth_time == 999) { Debug.LogError("SmoothDamp에 smooth_time 값 없음"); return; }

        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smooth_time);
        //ref velocity 호출할때 마다 함수에 의해 수정 됨
    }

    //Lerp(현재 위치, 목표 위치, 보간 간격) Linear interpolation 선형 보간!
    //백분율(0~1)이므로 보간간격이 0이면 현재위치, 1이면 목표위치임
    //보간 간격을 통해서 움직임을 부드럽게 만들거나, 일정한 이동을 만들 수 있음
    //여기서 보간 간격을 Time.deltaTime을 쓰기도 함

    //※ t 값을 Time.deltaTime으로 받아올 경우 b 목표값(1)에 영원히 도달하지 못하고 0.9999999....에서 값이 멈추게 된다.
    //따라서 목표값에 도달했는지 여부를 파악하려면 " c == 1 "이 아닌,
    //" c >= 0.99999f " 와 같이 최소한의 오차를 포함하여 판단함

    //일정한 이동을 만드는 건 리듬게임에서 노트를 찍을 때 사용

    // 아래 식처럼 사용
    //currentTime += Time.deltaTime;
    //Vector3.Lerp(transform.position, target.position, currentTime / 2);
    //t 값이 100%이면 완벽하게 도달한 것을 의미한다.
    //즉, currentTime을 0, 1, 2처럼 계속 늘려주고 / 2 해주면 1, 결국 100%가 된다.
    //// 알파블렌딩
    //c=(1 - a) * s + a* T
    //페이드인 페이드 아웃시 색상으로 만들 때 이렇게 선형보간으로 색을 바꿔줌
    //c(color), a(alpha)
    //결과 color = color1 * weight1 + color2*weight2; -두 색상을 적절하게 섞는 블랜딩
    //// 1-a 가 0이면 기본 색, 점점 커지면 섞임
    //애니메이션 움직임에도 이 함수 사용해 다양한 이동 표현

    [Header("* Lerp 이동 변수")]
    public float L_interpolation_length = 999; //보간 값이 작을수록 부드러운 움직임

    void LerpMoving()
    {
        if (L_interpolation_length == 999) { Debug.LogError("Lerp에 L_interpolation_length 값 없음"); return; }

        transform.position = Vector3.Lerp(transform.position, target.position, 0.001f);
    }

    //Slerp (현재 위치, 목표 위치, 보간 간격) - 구면 보간 구의 겉면을 따라간다 생각하면됨
    //내부계산에 따라 선형으로도 가니 주의
    [Header("* Slerp 이동 변수")]
    public float SL_interpolation_length = 999; //보간 값이 작을수록 부드러운 움직임
    void SlerpMoving()
    {
        if (SL_interpolation_length == 999) { Debug.LogError("Slerp에 SL_interpolation_length 값 없음"); return; }

        transform.position = Vector3.Slerp(transform.position, target.position, 0.01f);
    }

    private void Update()
    {
        if (choice_move == 999) { Debug.LogError("choice_move 값 없음"); return; }
        if (target == null) { Debug.LogError("target 값 없음"); return; }

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
