using UnityEngine;
using System;
using Unity.Mathematics;

public class Test : MonoBehaviour
{
    public Vector2 check_size;
    public LayerMask checkLayers;
    public Collider2D[] colliders { get; set; }
    private void Update()
    {
        colliders = Physics2D.OverlapBoxAll(transform.position, check_size, 0f, checkLayers);
        Array.Sort(colliders, new DistanceComparer(transform));

        foreach (Collider2D item in colliders)
        {
            Debug.Log(item.name);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, check_size);
    }
}
