using UnityEngine;

public class OrbSpawner : Gun
{
    [SerializeField] protected GameObject orbPrefab;

    public override bool AttemptFire()
    {
        if (!base.AttemptFire())
            return false;

        var b = Instantiate(bulletPrefab, gunBarrelEnd.transform.position, gunBarrelEnd.rotation);
        b.GetComponent<Projectile>().Initialize(0, 10, 5, 0, SpawnOrb);

        anim.SetTrigger("shoot");
        elapsed = 0;
        ammo -= 1;

        return true;
    }

    void SpawnOrb(HitData hd)
    {
        var orb = Instantiate(orbPrefab, hd.location + new Vector3(0, 200, 0), Quaternion.Euler(0, 0, 0));
        orb.GetComponent<Orb>().Initialize(hd.target.transform);
    }
}
