using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class sortingObject : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Material hotMaterial, coldMaterial;
    [SerializeField] GameObject spawnPlace, LeftHand, RightHand;
    [SerializeField] Spawner spawner;
    [SerializeField] bool isCold;
    [SerializeField] int molecules = 20, hotMolecules, coldMolecules, correctColdCounter = 0, correctHotCounter = 0, wrongCounter = 0, velocity = 10;
    [SerializeField] int sortedCounter = 0;
    [SerializeField] Transform middleHotBox, middleColdBox, boxTransform;
    [SerializeField]
    TextMeshProUGUI sortedText;

    //------------ Sounds ---------//
    [SerializeField] EventReference catchColdObject;
    [SerializeField] EventReference catchHotObject;
    [SerializeField] EventReference throwObject;
    [SerializeField] EventReference hitColdCorrect;
    [SerializeField] EventReference hitHotCorrect;
    [SerializeField] EventReference hitHotFalse;
    public void Shoot(bool isLeft)
    {
        RuntimeManager.PlayOneShot(throwObject);
        GameObject hand = isLeft ? LeftHand: RightHand;
        Vector3 direction = hand.transform.forward;
        rb.AddForce(direction * velocity, ForceMode.Impulse);
    }
    // Start is called before the first frame update
    void Start()
    {
        spawner.Spawn(molecules);
        (coldMolecules, hotMolecules) = spawner.getMolecules(); 
        rb = GetComponent<Rigidbody>();
        boxTransform = middleHotBox;
    }
    public void ChangeTemperature(bool isHot)
    {
        if (isHot)
        {
            RuntimeManager.PlayOneShot(catchHotObject);
            GetComponent<MeshRenderer>().material = hotMaterial;
            boxTransform = middleHotBox;
        }
        else
        {
            RuntimeManager.PlayOneShot(catchColdObject);
            GetComponent<MeshRenderer>().material = coldMaterial;
            boxTransform = middleColdBox;
        }
        isCold = !isHot;
    }



    private void OnCollisionEnter(Collision collision)
    {
        transform.position = spawnPlace.transform.position;
        rb.velocity = rb.velocity * 0 ;
        transform.Rotate(0, 10 * 0, 0);

        if (collision.gameObject.tag == "molecule")
        {

            if (collision.gameObject.GetComponent<molecule>().getIsCold() == isCold)
            {
                if (isCold)
                { 
                    correctColdCounter++;
                    RuntimeManager.PlayOneShot(hitColdCorrect);

                }
                else
                {
                    correctHotCounter++;
                    RuntimeManager.PlayOneShot(hitHotCorrect);

                }
                    
            }
            else
            {
                wrongCounter++;
                RuntimeManager.PlayOneShot(hitColdCorrect);
            }
            spawner.sortMolecule(collision.gameObject.GetComponent<molecule>(),boxTransform);
            sortedCounter++;
            sortedText.text = "Sorted: " + sortedCounter.ToString() + "\\" + molecules.ToString();
            // ------ Todo Function that Changes The Color of Glass According to how many you sorted right ------ // 
        }
    }
}
