using UnityEngine;

public class OrbSpawner : Gun
{
    public override bool AttemptFire()
    {
        if (!base.AttemptFire())
            return false;

        var b = Instantiate(bulletPrefab, gunBarrelEnd.transform.position, gunBarrelEnd.rotation);
        b.GetComponent<Projectile>().Initialize(0, 10, 5, 0, null);

        anim.SetTrigger("shoot");
        elapsed = 0;
        ammo -= 1;

        return true;
    }
}
