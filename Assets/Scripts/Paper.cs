using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    Vector3 m_target;
    [SerializeField] float flySpeed = 1;
    [SerializeField] float flyRotSpeed = 1;

    public void Init(Vector3 targetPosition)
    {
        m_target = targetPosition;
    }

    void Update()
    {
        Vector3 dir = m_target - transform.position;

        if (dir.sqrMagnitude < .01f) this.enabled = false;

        transform.position += (dir.normalized * flySpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * flyRotSpeed * Time.deltaTime);
    }
}