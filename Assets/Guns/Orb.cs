using UnityEngine;

public class Orb : MonoBehaviour
{
    public Transform target;
    private ParticleSystem orbParticles;
    [SerializeField] private float fallSpeed;

    private void Start()
    {
        orbParticles = GetComponentInChildren<ParticleSystem>();
        GetComponent<Rigidbody>().linearVelocity = transform.up * -fallSpeed;
    }

    private void Update()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.gameObject.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.Hit(new Vector3(0, 0, 0), 2147483647);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, 0);
            orbParticles.Play();
            Invoke("DestroyOrb", 5);
        }
    }

    public void Initialize(Transform initTarget)
    {
        target = initTarget;
    }

    private void DestroyOrb()
    {
        Destroy(gameObject);
    }
}
