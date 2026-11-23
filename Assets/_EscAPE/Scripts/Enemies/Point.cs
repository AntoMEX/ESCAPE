using UnityEngine;

public class Point : MonoBehaviour
{
    public float coverValue; //0-1 donde 0 = no cobertura, y 1 = hay cobertura
    public float aggroValue; //0-1 donde 0 = no sirve para atacar, 1 es muy bueno para atacar
    public bool visibleForPlayer;
    public Transform player;
    public Transform myTransform;
    public float detectionRange = 50f;
    [SerializeField] Collider trigger;

    private void Awake()
    {
        if (!myTransform) myTransform = transform;
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        EvaluateCover();
    }

    void EvaluateCover()
    {
        Vector3 direccion = (player.position - transform.position).normalized;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direccion, out hit, detectionRange))
        {
            if (hit.transform == player)
                visibleForPlayer = true;
        }
        else
            visibleForPlayer = false;
    }

    private void OnDrawGizmos()
    {
        if (player == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
//Script original hecho por Diego Jiménez Cruzado, para el proyecto final de Inteligencia Artifical Avanzada