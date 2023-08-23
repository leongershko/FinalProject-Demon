using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class molecule : MonoBehaviour
{
    private float rotationSpeed;
    [SerializeField]
    private float speed, initSpeed;
    private Rigidbody rb;
    private bool isCold;
    private bool isSorted = false;

    // ------- Sounds -------- //
    [SerializeField] EventReference moleculeCollision;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // ------ Getters And Settt
    public float getSpeed()
    {
        return speed;
    }
    public float getInitSpeed()
    {
        return initSpeed;
    }
    public bool getIsCold()
    {
        return isCold;
    }
    public void setSpeed(float speed)
    {
        this.speed = speed;
        rotationSpeed = speed;
        rb.velocity = rb.velocity.normalized * speed;
    }

    public void setInitSpeed(float speed)
    {
        this.initSpeed = speed;
        this.speed = speed;
        rotationSpeed = speed;
    }
    public void setIsCold(bool cold)
    {
        this.isCold = cold;
    }

    public void setIsSorted()
    {
        isSorted = true;
    }




    private void OnCollisionEnter(Collision collision)
    {
        // Apply the new direction to the object's velocity
        rb.velocity = rb.velocity.normalized * speed;
        //transform.Rotate(0, 10 * rotationSpeed, 0);
        if (collision.gameObject.tag == "molecule" && !isSorted) { RuntimeManager.PlayOneShot(moleculeCollision);
            Debug.Log("I Collided");
        }
        
        
    }
    public void returnMolecule()
    {
        Spawner.chosenMolecule = this;
    }

    public void setDirection(Vector3 direction)
    {
        rb.velocity = direction.normalized * speed;
    }
}
