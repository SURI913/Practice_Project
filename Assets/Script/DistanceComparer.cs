using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceComparer : IComparer
{
    private Transform compare_transform;

    public DistanceComparer(Transform _compare_transform)
    {
        compare_transform = _compare_transform;
    }

    public int Compare(object x, object y)
    {
        Collider2D xcollider = x as Collider2D;
        Collider2D ycollider = y as Collider2D;
        Vector3  offset = xcollider.transform.position - compare_transform.transform.position;
        float xDistance = offset.sqrMagnitude;

        offset = ycollider.transform.position - compare_transform.transform.position;
        float yDistance = offset.sqrMagnitude;

        return xDistance.CompareTo(yDistance);
    }

    
}
