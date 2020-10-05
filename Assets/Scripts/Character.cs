using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : SquareZone
{
    [SerializeField] Mesh[] m_characterMesh = null;

    bool m_rotating = false;
    float m_time = -1;
    [SerializeField] float m_rotateTime = 1.5f;
    [SerializeField] float m_rotateSpeed = 250f;

    protected override void Start()
    {
        base.Start();
        GetComponent<MeshFilter>().mesh = m_characterMesh[Random.Range(0, m_characterMesh.Length)];
        gameObject.SetActive(true);
    }
    void Update()
    {
        if ((m_time - m_rotateTime / 2) < Time.time && Inside(PlayerPosition.inst.position))
        {
            m_time = m_rotateTime + Time.time;
            m_rotating = true;

            SoundManager.inst.PlayBell();
        }
        if (m_rotating)
        {
            float delta = m_time - Time.time;
            if (delta < 0)
            {
                m_rotating = false;
            }
            else
            {
                float speed = Mathf.Lerp(0, m_rotateTime, delta / m_rotateTime) * m_rotateSpeed;
                transform.Rotate(Vector3.up * speed * Time.deltaTime);
            }
        }
    }
}