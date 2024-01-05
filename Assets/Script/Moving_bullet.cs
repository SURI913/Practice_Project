using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Moving_bullet : MonoBehaviour
{
    public Transform target;
    [Space]
    [Header("* 호 크기")]
    [Range(-100, 5)]
    public float offset;
    [Header("* Slerp 이동 변수")]
    public float SL_count = 1000; //
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
        foreach (var point in SlerpMoving(transform.position, target.position, offset))
        {
            
            transform.position = point;
        }
    }
}
