using UnityEngine;

public class Shotgun : Gun
{
    [SerializeField] protected int bulletCount;

    public override bool AttemptFire()
    {
        if (!base.AttemptFire())
            return false;

        for (var i = 0; i < bulletCount; i++)
        {
            var b = Instantiate(bulletPrefab, gunBarrelEnd.transform.position, gunBarrelEnd.rotation * Quaternion.Euler(Random.Range(-5, 5), Random.Range(-5, 5), 0));
            b.GetComponent<Projectile>().Initialize(4, Random.Range(60, 120), Random.Range(0.75f, 1.25f), 3, null);
        }

        anim.SetTrigger("shoot");
        elapsed = 0;
        ammo -= 1;

        return true;
    }
}
