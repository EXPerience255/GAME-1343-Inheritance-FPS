using UnityEngine;

public class AutoRifle : Gun
{
    public override bool AttemptFire()
    {
        if (!base.AttemptFire())
            return false;

        var b = Instantiate(bulletPrefab, gunBarrelEnd.transform.position, gunBarrelEnd.rotation * Quaternion.Euler(Random.Range(-3, 3), Random.Range(-3, 3), 0));
        b.GetComponent<Projectile>().Initialize(1, Random.Range(70, 90), Random.Range(1, 2), 2, null);

        anim.SetTrigger("shoot");
        elapsed = 0;
        ammo -= 1;

        return true;
    }
}
