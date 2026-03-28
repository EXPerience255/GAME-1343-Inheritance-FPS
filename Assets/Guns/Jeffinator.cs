using UnityEngine;

public class Jeffinator : Gun
{
    [SerializeField] protected GameObject jeff;

    public override bool AttemptFire()
    {
        if (!base.AttemptFire())
            return false;

        var b = Instantiate(bulletPrefab, gunBarrelEnd.transform.position, gunBarrelEnd.rotation);
        b.GetComponent<Projectile>().Initialize(-3, 20, 5, 0, SpawnJeff);

        anim.SetTrigger("shoot");
        elapsed = 0;
        ammo -= 1;

        return true;
    }

    void SpawnJeff(HitData hd)
    {
        Instantiate(jeff, hd.location + new Vector3(Random.Range(-5, 5), 5, Random.Range(-5, 5)), Quaternion.Euler(0, 0, 0));
    }
}
