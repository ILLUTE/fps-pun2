using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoPoint : MonoBehaviour
{
    [Range(0.1f, 100f)]
    public float radius = 1;
    public Color Color;

  /*  private void OnDrawGizmos()
    {
        Gizmos.color = Color;

        Gizmos.DrawWireSphere(transform.position, radius);
    }*/
}