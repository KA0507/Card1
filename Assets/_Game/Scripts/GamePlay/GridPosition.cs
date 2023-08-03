using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Vector3EqualityComparer : IEqualityComparer<Vector3>
{
    public bool Equals(Vector3 v1, Vector3 v2)
    {
        return Mathf.Approximately(v1.x, v2.x) && Mathf.Approximately(v1.z, v2.z);
    }

    public int GetHashCode(Vector3 vector)
    {
        unchecked
        {
            int hashCode = vector.x.GetHashCode();
            hashCode = (hashCode * 397) ^ vector.y.GetHashCode();
            hashCode = (hashCode * 397) ^ vector.z.GetHashCode();
            return hashCode;
        }
    }
}

public class GridPosition : MonoBehaviour
{
    public LayerMask groundLayer;
    public Vector3 point;
    public List<Vector3> gridPositions = new List<Vector3>();

    float radius = 2f; // Bán kính hình tổ ong
    public void OnInit(Vector3 pos)
    {
        gridPositions.Clear();
        point = pos;
        radius = 2f;
        for (int layer = 0; layer <= 6; layer++)
        {
            if (layer == 0)
            {
                gridPositions.Add(new Vector3(pos.x, 1.5f, pos.z));
                continue;
            }
            float angleIncrement = Mathf.PI * 2f / (layer * 6); // Góc giữa các đối tượng trên mỗi lớp

            for (int i = 0; i < layer * 6; i++)
            {
                float angle = i * angleIncrement;
                float xPos = - Mathf.Cos(angle) * radius;
                float zPos = Mathf.Sin(angle) * radius;

                Vector3 gridPosition = new Vector3(point.x + xPos, 1.5f, point.z + zPos);
                gridPositions.Add(gridPosition);
            }
            radius += 2f;
        }
    }
}
