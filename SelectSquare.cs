using UnityEngine;
using System.Collections;

public class SelectSquare{
    Vector2 m_vertex1; public Vector2 Vertex1 { get { return m_vertex1; } set { m_vertex1 = value; } }
    Vector2 m_vertex2; public Vector2 Vertex2 { get { return m_vertex2; } set { m_vertex2 = value; } }
    Vector2 m_vertex3; public Vector2 Vertex3 { get { return m_vertex3; } set { m_vertex3 = value; } }
    Vector2 m_vertex4; public Vector2 Vertex4 { get { return m_vertex4; } set { m_vertex4 = value; } }

    public SelectSquare()
    {
        m_vertex1 = new Vector2();
        m_vertex2 = new Vector2();
        m_vertex3 = new Vector2();
        m_vertex4 = new Vector2();
    }

    public SelectSquare(Vector2 vertex1, Vector2 vertex2, Vector2 vertex3, Vector2 vertex4)
    {
        m_vertex1 = vertex1;
        m_vertex2 = vertex2;
        m_vertex3 = vertex3;
        m_vertex4 = vertex4;
    }

    public bool Inside(Vector3 quarry)
    {
        Vector2 q = new Vector2(quarry.x, quarry.z);
        return ( ((m_vertex1 - q).x <= 0 && (m_vertex1 - q).y > 0) && (m_vertex4 - q).x > 0 && (m_vertex4 - q).y <= 0 ) || ( ((m_vertex1 - q).x > 0 && (m_vertex1 - q).y <= 0) && (m_vertex4 - q).x <= 0 && (m_vertex4 - q).y > 0 );
    }
}
