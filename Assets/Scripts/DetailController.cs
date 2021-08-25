using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailController : MonoBehaviour
{
    public GameObject destroyingParticle;
    public Vector3 endPosition { get; set; }
    public Vector3 rotation { get; set; }

    private Rigidbody thisRigidbody;
    private void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        thisRigidbody.AddForce(endPosition, ForceMode.Force);
        transform.rotation = transform.rotation * Quaternion.Euler(rotation);
    }
    public void Death()
    {
        GameObject particle =  Instantiate(destroyingParticle, transform.position, transform.rotation);
        Destroy(particle, 1.5f);
        Destroy(this.gameObject);
    }
}
