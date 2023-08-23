using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class book : MonoBehaviour
{

    [SerializeField] GameObject canvas;
    [SerializeField] bool flag = false;

    // ------- Sounds -------- //
    [SerializeField] EventReference OpenSound;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !flag ) 
        {
            flag = true;
            canvas.SetActive(true);
            RuntimeManager.PlayOneShot(OpenSound);
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player" && !flag)
    //    {
            
    //    }
    //}
}
