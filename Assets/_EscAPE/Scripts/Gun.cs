using UnityEngine;

public class Gun : InteractableObject
{
    [SerializeField] private float shootCooldown;
    private float lastShotTime = 0f;
    [SerializeField] private int ammo;
    //SHOOT
    public override void DoAction()
    {
        if (ammo > 0 && lastShotTime + shootCooldown < Time.time)
        {
            Shoot();
        }
    }

    protected virtual void Shoot()
    {
        ammo--;
        lastShotTime = Time.time;
        Debug.Log("Pew Pew");
    }
}
