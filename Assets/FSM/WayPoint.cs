using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour {

    static Color linkColor = Color.green;
    public Color waypointColor = Color.blue;
    public float radius = 0.1F;
    public Transform[] Links;

    public Mesh Seta;

    /*public void OnDrawGizmos()
    {


        Gizmos.color = waypointColor;
        Gizmos.DrawSphere(transform.position, radius);
        for (int i = 0; i < Links.Length; i++)
        {
            if (Links[i] != null)
            {
                Gizmos.color = linkColor;
                Gizmos.DrawLine(transform.position, Links[i].position);

                

                Gizmos.DrawMesh(Seta, 0.5f * (transform.position + Links[i].position), Quaternion.LookRotation(Links[i].position - transform.position));
            }
        }
        
    }*/

}
