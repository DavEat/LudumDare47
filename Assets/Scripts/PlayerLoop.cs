using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TreeEditor;
using UnityEngine;

public class PlayerLoop : Singleton<PlayerLoop>
{
    int m_currentIndex = 0;
    int m_size = 0;
    [SerializeField] List<LoopPoint> m_points = new List<LoopPoint>();

    [SerializeField] LoopPoint m_prefab_poopPoint = null;

    void Start()
    {
        SetSize(m_points.Count);
    }
    public int GetSize()
    {
        return m_size;
    }
    void SetSize(int size)
    {
        m_size = size;
    }
    public Vector3 GetCurrentPoint()
    {
        return m_points[m_currentIndex].GetPosition();
    }
    public Vector3 GetNextPoint()
    {
        if (++m_currentIndex >= m_size)
            m_currentIndex = 0;

        return m_points[m_currentIndex].GetPosition();
    }
    public LoopPoint GetPointAt(int index)
    {
        return m_points[index];
    }
    public void AddPoint(LoopPoint point, Vector3 position)
    {
        int index = m_points.IndexOf(point);

        m_size++;

        if (m_currentIndex > index)
            m_currentIndex++;

        m_points.Insert(index+1, Instantiate(m_prefab_poopPoint).Init(position, transform));
    }
    public void RemovePoint(LoopPoint point)
    {
        int index = m_points.IndexOf(point);

        m_size--;

        if (m_currentIndex > index)
            m_currentIndex--;

        Destroy(m_points[index].gameObject);
        m_points.RemoveAt(index);
    }
    public ref List<LoopPoint> GetLoopPoints()
    {
        return ref m_points;
    }
}