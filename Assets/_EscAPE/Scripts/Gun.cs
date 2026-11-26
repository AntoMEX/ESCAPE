using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Gun : InteractableObject
{
    [SerializeField] private float shootCooldown;
    private float lastShotTime = 0f;

    [SerializeField] private int ammo;
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private GameObject ammoDisplay;

    [SerializeField] private Transform muzzle;
    [SerializeField] private float range;
    [SerializeField] private int dmg;

    [Header("Debug")]
    [SerializeField] bool debuggActive = true;
    public Vector3 impactPoint {  get; private set; }
    [SerializeField] float pointRad = 0.1f;
    private void Start()
    {
        ammoDisplay.SetActive(false);
        UpdateAmmoUI();
    }

    private void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = ammo.ToString();
        }
    }

    public virtual void Shoot()
    {
        if (ammo > 0 && lastShotTime + shootCooldown < Time.time) //Si hay balas y ha pasado el cooldown
        {
            ammo--;

            UpdateAmmoUI();

            lastShotTime = Time.time;
            //Disparar
            Debug.Log("Pew Pew");
            if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hit, range))
            {
                Debug.Log("Hit: " + hit.collider.name);
                if(hit.collider.TryGetComponent(out HealthComponent healthComp))
                {
                    healthComp.RecibeDamage(dmg);
                }
                if(debuggActive) impactPoint = hit.point;
            }
        }
    }

    public override void OnGrabbed()
    {
        base.OnGrabbed();
        ammoDisplay.SetActive(true);
    }

    public override void OnReleased()
    {
        base.OnReleased();
        ammoDisplay.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        if (debuggActive && Grabbed)
        { 
            Gizmos.color = Color.red;
            Gizmos.DrawRay(muzzle.position, muzzle.forward * range);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(impactPoint, pointRad);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(muzzle.position, pointRad);
        }
    }

}

