using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class spawnStraight : MonoBehaviour
{
    [SerializeField] GameObject BallPrefab;
    Rigidbody rb;
    public float speed = 5f;
    public float timer = 5f;

    public void inst_ball()
    {
        GameObject inst = Instantiate(BallPrefab,transform);
        rb = inst.GetComponent<Rigidbody>();
        rb.AddForce(speed * -transform.forward, ForceMode.Impulse);

        //inst.transform.position = transform.parent.position;
        //inst.transform.position

    }
    private void FixedUpdate()
    {
        if (timer <= 0)
        {
            timer = 5f;
            inst_ball();
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
