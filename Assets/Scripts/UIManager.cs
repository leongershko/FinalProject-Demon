using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform head;
    [SerializeField] float spawnDistance = 2;
    [SerializeField] Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position * 2;
        transform.position = offset + head.position + new Vector3(head.forward.x, head.forward.y, 1) * spawnDistance;
        //transform.LookAt(head.position);
        //transform.forward *= -1;
    }
}
