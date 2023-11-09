using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] private Line path;
    [SerializeField] private LineRenderer line;
    [SerializeField] private GameObject pathFollower;

    private float currentPosition;
    private float pathLenght;

    private void OnDrawGizmos()
    {
        pathLenght = path.Length;
        
        DrawLine();
        MoveObject();
    }

    private void DrawLine()
    {
        line.positionCount = path.points.Length;
        for (int i = 0; i < line.positionCount; i++)
        {
            line.SetPosition(i, path.points[i]);
        }
    }

    private void MoveObject()
    {
        currentPosition += Time.deltaTime * 10;

        var data = path.GetValue(currentPosition);
        pathFollower.transform.position = data.point;

        if (data.end) currentPosition = 0;
    }

    [System.Serializable]
    public struct Line
    {
        public Vector3[] points;
        public float Length { get; private set; }

        public ValueData GetValue(float point)
        {
            var distanceTraveled = 0.0f;
            
            for (int i = 0; i < this.points.Length - 1; i++)
            {
                distanceTraveled += Vector3.Distance(points[i], points[i + 1]);
                if (point > distanceTraveled) continue;

                float distance = Vector3.Distance(points[i], points[i + 1]);
                float currentSpeed = 1 / distance;

                var pos = Vector3.Lerp(points[i], points[i + 1], 1 - (distanceTraveled - point) * currentSpeed);
                return new ValueData(pos, false);
            }

            return new ValueData(points.Last(), true);
        }

        public struct ValueData
        {
            public readonly Vector3 point;
            public readonly bool end;

            public ValueData(Vector3 point, bool end)
            {
                this.point = point;
                this.end = end;
            }
        }
    }
}
