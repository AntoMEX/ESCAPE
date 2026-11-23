using UnityEngine;

public enum EnemyState
{
    Stalking,
    Fleeing,
    Patroling
}
public class TacticBehaviour : MonoBehaviour
{
    public Transform player;

    [Header("Pesos")]
    public float weightPlayerDistance = 1f; //Importancia de la distancia del jugador al punto
    public float weightMyDistance = 1f;     //Importancia de mi distancia al punto
    public float weightCover = 1f;          //Importancia de la covertura del punto
    public float weightAggro = 1f;          //Importancia de la agresividad del punto

    float EvaluatePoint(Point point, EnemyState state)
    {
        float distanceToPlayer = Vector3.Distance(point.myTransform.position, player.position);
        float playerDistScore = Mathf.Clamp(10 - distanceToPlayer, 0, 10f) / 10f; //0= lejos, 1 = cerca

        float distanceToMe = Vector3.Distance(point.myTransform.position, transform.position);
        float distToMeScore = Mathf.Clamp(10 - distanceToMe, 0, 10f) / 10f; //0 = lejos, 1 = cerca

        float coverScore = point.coverValue; //0 = descubierto, 1 = muy cubierto
        coverScore = point.visibleForPlayer ? coverScore * 0.8f : coverScore * 1.2f;

        float aggroScore = point.aggroValue; //0 = no aggro, 1 = muy aggro
        aggroScore = point.visibleForPlayer ? aggroScore * 1.2f : aggroScore * 0.8f;

        switch (state) //En base al estado, se le da todavía mas o menos pesos a ciertos valores.
        {
            case EnemyState.Stalking:
                return (weightPlayerDistance * playerDistScore * 1.2f) +
                (weightMyDistance * distToMeScore * 0.7f) +
                (weightCover * coverScore * 0.5f) +
                (weightAggro * aggroScore);

            case EnemyState.Fleeing:
                return (weightPlayerDistance * (1 - playerDistScore) * 1.2f) +
                (weightMyDistance * distToMeScore * 0.7f) +
                (weightCover * coverScore * 1.2f) +
                (weightAggro * aggroScore * 0.3f);

            case EnemyState.Patroling:
                return (weightMyDistance * (1 - distToMeScore) * 0.8f) +
                (weightAggro * aggroScore * 0.8f) +
                (weightCover * coverScore * 0.4f);
        }

        return 0f;
    }

    public Point GetBestPoint(EnemyState state)
    {
        Point bestPoint = null;
        float bestScore = -Mathf.Infinity;

        foreach (Point point in PointsBoard.Instance.pointArray)
        {
            if (PointsBoard.Instance.IsPointBanned(point))
                continue;
            float score = EvaluatePoint(point, state);
            if (score > bestScore)
            {
                bestScore = score;
                bestPoint = point;
            }
        }

        PointsBoard.Instance.BannPoint(bestPoint);
        return bestPoint;
    }
}
//Script original hecho por Diego Jiménez Cruzado, para el proyecto final de Inteligencia Artifical Avanzada