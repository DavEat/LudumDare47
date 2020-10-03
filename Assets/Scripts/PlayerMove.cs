using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    Vector3 m_targetPosition = Vector3.zero;
    NavMeshAgent m_agent = null;

    [SerializeField] float m_nextPointDst = 1f;

    Transform m_transform = null;
    public float totalDistance = 0;
    Vector3 m_lastPosition;

    void Start()
    {
        m_targetPosition = PlayerLoop.inst.GetCurrentPoint();
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.SetDestination(m_targetPosition);

        LoopEditor.inst.endMove += SetDestiantion;

        m_transform = GetComponent<Transform>();
        m_lastPosition = m_transform.position;
    }

    void Update()
    {
        if ((m_targetPosition - transform.position).sqrMagnitude < m_nextPointDst * m_nextPointDst)
        {
            m_targetPosition = PlayerLoop.inst.GetNextPoint();
            m_agent.SetDestination(m_targetPosition);
        }
        UpdateTravelDst();
    }
    public void SetDestiantion()
    {
        m_targetPosition = PlayerLoop.inst.GetCurrentPoint();
        m_agent.SetDestination(m_targetPosition);
    }
    void UpdateTravelDst()
    {
        totalDistance += (m_transform.position - m_lastPosition).magnitude;
        m_lastPosition = m_transform.position;
    }
}