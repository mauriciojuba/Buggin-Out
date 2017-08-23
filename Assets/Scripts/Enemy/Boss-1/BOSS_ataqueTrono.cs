using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS_ataqueTrono : MonoBehaviour
{
    MeshRenderer rend;

    CapsuleCollider col;
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        rend.enabled = false;

        col = GetComponent<CapsuleCollider>();
        col.enabled = false;
    }
    public void EnableCollider()
    {
        rend.enabled = true;

        col.enabled = true;
        Invoke("DisableCollider", 1f);
    }
    void DisableCollider()
    {
        rend.enabled = false;

        col.enabled = false;
    }
}