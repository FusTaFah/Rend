using UnityEngine;
using System.Collections;

public class SelectSquare{
    Vector3 m_vertex1; public Vector3 Vertex1 { get { return m_vertex1; } set { m_vertex1 = value; } }
    Vector3 m_vertex4; public Vector3 Vertex4 { get { return m_vertex4; } set { m_vertex4 = value; } }

    public SelectSquare()
    {
        m_vertex1 = new Vector3();
        m_vertex4 = new Vector3();
    }

    public SelectSquare(Vector3 vertex1, Vector3 vertex4)
    {
        m_vertex1 = vertex1;
        m_vertex4 = vertex4;
    }

    public bool Inside(Vector3 quarry)
    {
        Vector3 q = quarry;
        return (((m_vertex1 - q).x <= 0 && (m_vertex1 - q).z > 0) && ((m_vertex4 - q).x > 0 && (m_vertex4 - q).z <= 0)) ||
            (((m_vertex1 - q).x > 0 && (m_vertex1 - q).z <= 0) && ((m_vertex4 - q).x <= 0 && (m_vertex4 - q).z > 0)) ||
            (((m_vertex1 - q).x > 0 && (m_vertex1 - q).z > 0) && ((m_vertex4 - q).x <= 0 && (m_vertex4 - q).z <= 0)) ||
            (((m_vertex1 - q).x <= 0 && (m_vertex1 - q).z <= 0) && ((m_vertex4 - q).x > 0 && (m_vertex4 - q).z > 0));
    }
}
