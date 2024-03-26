using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.Animations;

public class Moving_bullet : MonoBehaviour
{
    Transform target;
    [Space]
    [Header("* 호 크기")]
    [Range(-100, 5)]
    public float offset;
    [Header("* Slerp 이동 변수")]
    public float SL_count = 1000;


    //----------------------DoTween
    //땅에 닫기까지 걸리는 시간
    [Header("* 점프하는 힘 / 시간 / 그래프")]
    public float force;
    public float timer;
    public Ease ease_type;
    IEnumerable<Vector3> SlerpMoving( Vector3 start, Vector3 end, float center_offset)
    {
        var center_pivot = (start + end) * 0.5f;

        center_pivot -= new Vector3(0, -center_offset);

        var start_relative_center = start - center_pivot;
        var end_relative_center = end - center_pivot;

        var f = 1f / SL_count;

        for (var i = 0f; i < 1 + f; i += f)
        {
            yield return Vector3.Slerp(start_relative_center, end_relative_center, 0.05f) + center_pivot;
        }
        
    }

    void DoTweenMoving()
    {
        transform.DOLocalJump( target.position, force, 1, timer).SetEase(ease_type).SetLoops(-1, LoopType.Restart);

        //transform.DOMove(Vector3 목표값, float 변화시간, (bool 정수단위 이동여부));
        //.SetEase(Ease easeType / AnimationCurve animCurve / EaseFunction customEase)
        //.SetLoops(int loops, LoopType loopType = LoopType.Restart) : 변환을 반복하는 기능
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            transform.position = Vector3.zero;
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var point in SlerpMoving(transform.position, target.position, offset))
        {
            Gizmos.DrawSphere(point, 0.1f);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position-target.position*0.5f, 0.2f);
    }

    private void Update()
    {
        target = my_parent.colliders[0].transform;
        foreach (var point in SlerpMoving(transform.position, target.position, offset))
        {

            transform.position = point;
        }
    }
    private Test my_parent;
    private void Start()
    {
        my_parent = GetComponentInParent<Test>();
        //DoTweenMoving();
    }
}
