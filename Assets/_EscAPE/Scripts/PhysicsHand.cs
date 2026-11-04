using UnityEngine;
using UnityEngine.UIElements;

public class PhysicsHand : MonoBehaviour
{
    [Header("PID")]
    [SerializeField] float frecuency = 50f;
    [SerializeField] float damping = 0.5f;
    [SerializeField] float rotFrecuency = 100f;
    [SerializeField] float rotDamping = 0.9f;
    [SerializeField] private Rigidbody playerRigidBody;
    private Rigidbody _rigidbody;
    [SerializeField] private Transform target;
    [Space]
    [Header("HookesLaw")]
    [SerializeField] float climbingForce = 1000f;
    [SerializeField] float climbDragForce = 500f;
    Vector3 _previousPosition;
    bool isColliding = false;
    void Start()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.maxAngularVelocity = float.PositiveInfinity;
        _previousPosition = transform.position;
    }

    void FixedUpdate()
    {
        PIDMovement();
        PIDRotation();
        if(isColliding) HookesLaw();
    }

    void PIDMovement()
    {
        float kp = (6f * frecuency) * (6f * frecuency) * 0.25f;
        float kd = 4.5f * frecuency * damping;
        float g = 1f / (1f + kd * Time.fixedDeltaTime + kp * Time.fixedDeltaTime * Time.fixedDeltaTime);
        float ksg = kp * g;
        float kdg = (kd + kp * Time.fixedDeltaTime) * g;
        Vector3 force = (target.position - transform.position) * ksg + (playerRigidBody.linearVelocity - _rigidbody.linearVelocity) * kdg;
        _rigidbody.AddForce(force, ForceMode.Acceleration);
    }

    void PIDRotation()
    {
        float kp = (6f * rotFrecuency) * (6f * rotFrecuency) * 0.25f;
        float kd = 4.5f * rotFrecuency * rotDamping;
        float g = 1f / (1f + kd * Time.fixedDeltaTime + kp * Time.fixedDeltaTime * Time.fixedDeltaTime);
        float ksg = kp * g;
        float kdg = (kd + kp * Time.fixedDeltaTime) * g;
        Quaternion q = target.rotation * Quaternion.Inverse(transform.rotation);
        if(q.w < 0)
        {
            q.x = -q.x;
            q.y = -q.y;
            q.z = -q.z;
            q.w = -q.w;
        }
        q.ToAngleAxis(out float angle, out Vector3 axis);
        axis.Normalize();
        axis *= Mathf.Deg2Rad;
        Vector3 torque = ksg * axis * angle + -_rigidbody.angularVelocity * kdg;
        _rigidbody.AddTorque(torque, ForceMode.Acceleration);
    }

    void HookesLaw()
    {
        Vector3 displaysmentFromResting = transform.position - target.position;
        Vector3 force = displaysmentFromResting * climbingForce;
        float drag = GetDrag();

        playerRigidBody.AddForce(force, ForceMode.Acceleration);
        playerRigidBody.AddForce(drag * -playerRigidBody.linearVelocity * climbDragForce, ForceMode.Acceleration);
    }

    float GetDrag()
    {
        Vector3 handVelocity = (target.localPosition - _previousPosition) / Time.fixedDeltaTime;
        float drag = 1 /handVelocity.magnitude + 0.01f;
        drag = drag > 1 ? 1 : drag;
        drag = drag < 0.03f ? 0.03f : drag;
        _previousPosition = transform.position;
        return drag;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isColliding = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }
}
