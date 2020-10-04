using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    Vector3 m_targetPosition = Vector3.zero;
    NavMeshAgent m_agent = null;

    [SerializeField] float m_nextPointDst = 1f;

    Transform m_transform = null;
    public float totalDistance = 0;
    Vector3 m_lastPosition;

    [SerializeField] float m_baseAgentSpeed = 100;
    [SerializeField] float m_dirtMultiplier = .5f;
    int m_dirtMaskId = 8;

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

        NavMeshHit navMeshHit;
        m_agent.SamplePathPosition(NavMesh.AllAreas, 0f, out navMeshHit);
        if (navMeshHit.mask == m_dirtMaskId)
        {
            m_agent.speed = m_baseAgentSpeed * m_dirtMultiplier;
        }
        else m_agent.speed = m_baseAgentSpeed;
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