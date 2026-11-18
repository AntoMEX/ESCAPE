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
    [SerializeField] private float dmg;

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
                    healthComp.UpdateHP(-dmg);
                }
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

}

