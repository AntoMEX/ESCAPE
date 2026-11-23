using System.Collections.Generic;
using UnityEngine;

public class PointsBoard : MonoBehaviour
{
    public static PointsBoard Instance { private set; get; }
    public Point[] pointArray;
    public List<Point> bannedPoints;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Object.Destroy(this);
    }
    public Point ProvideRandomPoint()
    {
        int randIndex = Random.Range(0, pointArray.Length - 1);
        bannedPoints.Add(pointArray[randIndex]);
        return pointArray[randIndex];
    }
    public bool IsPointBanned(Point point)
    {
        return bannedPoints.Contains(point);
    }
    public void BannPoint(Point point)
    {
        bannedPoints.Add(point);
    }
    public void UnBannPoint(Point point)
    {
        if (bannedPoints.Contains(point) == false)
        {
            Debug.Log($"El punto {point.gameObject.name} no está en la lista de baneados");
            return;
        }
        bannedPoints.Remove(point);
    }
}
//Script original hecho por Diego Jiménez Cruzado, para el proyecto final de Inteligencia Artifical Avanzada