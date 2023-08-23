using UnityEngine;

public class batCapsuleFollower : MonoBehaviour
{
    private batCapsule _batFollower;
    private Rigidbody _rigidbody;
    private Vector3 _velocity;

    [SerializeField]
    private float _sensitivity = 100f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 destination = _batFollower.transform.position;
        _rigidbody.transform.rotation = transform.rotation;

        _velocity = (destination - _rigidbody.transform.position) * _sensitivity;

        _rigidbody.velocity = _velocity;
        transform.rotation = _batFollower.transform.rotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Vector3 direction = _batFollower.transform.position - other.gameObject.transform.position;
            Debug.Log("Hit The Baseball!");
            other.gameObject.GetComponent<Rigidbody>().AddForce(direction.normalized * _sensitivity);
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ball"))
    //    {
    //        Vector3 direction = _batFollower.transform.position - collision.gameObject.transform.position;
    //        Debug.Log("Hit!");
    //        collision.gameObject.GetComponent<Rigidbody>().AddForce(direction * _sensitivity);
    //    }
    //}

    public void SetFollowTarget(batCapsule batFollower)
    {
        _batFollower = batFollower;
    }
}