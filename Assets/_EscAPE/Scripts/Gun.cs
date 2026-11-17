using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Gun : InteractableObject
{
    [SerializeField] private float shootCooldown;
    private float lastShotTime = 0f;
    [SerializeField] private int ammo;

    public virtual void Shoot()
    {
        if (ammo > 0 && lastShotTime + shootCooldown < Time.time) //Si hay balas y ha pasado el cooldown
        {
            ammo--;
            lastShotTime = Time.time;
            //Disparar
            Debug.Log("Pew Pew");
        }
    }
}
