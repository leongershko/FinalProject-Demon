using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{

    [SerializeField] int maxSpeed = 30;
    [SerializeField] int minSpeed = 5;
    [SerializeField] int seperatingSpeed, coldCounter=0,hotCounter=0;
    [SerializeField] List<molecule> moleculeList = new List <molecule> ();
    

 
    [SerializeField] GameObject molecule;
    [SerializeField] GameObject xMax;
    [SerializeField] GameObject xMin;
    [SerializeField] GameObject yMax;
    [SerializeField] GameObject yMin;
    [SerializeField] GameObject zMax;
    [SerializeField] GameObject zMin;

    public static molecule chosenMolecule;
    private bool slowTimeFlag = false;


    public int SampleGaussian(System.Random random, int mean,  int stddev)
    {
        double x1 = 1 - random.NextDouble();
        double x2 = 1 - random.NextDouble();
        double y1 = Math.Sqrt(-2 * Math.Log(x1)) * Math.Cos(2 * Math.PI * x2);
        return (int) (y1 * stddev + mean);
    }


    void spawnSingleMolecule(int speed, Vector3 direction, Vector3 position)
    {
        bool cold = speed <= seperatingSpeed;
        GameObject instancedMulecule = Instantiate(molecule);
        molecule m = instancedMulecule.GetComponent<molecule>();

        instancedMulecule.transform.position = position;
        m.setInitSpeed(speed);
        m.setDirection(direction);
        m.setIsCold(cold);

        moleculeList.Add(m);

        if (cold) coldCounter++;
        else hotCounter++;
    }
    

    public void Spawn(int molecules)
    {
        seperatingSpeed = (maxSpeed + minSpeed) / 2;

        int xmax = (int)xMax.transform.position.x;
        int xmin = (int)xMin.transform.position.x;
        int ymax = (int)yMax.transform.position.y;
        int ymin = (int)yMin.transform.position.y;
        int zmax = (int)zMax.transform.position.z;
        int zmin = (int)zMin.transform.position.z;
        int mean = (maxSpeed + minSpeed) / 2;
        int std = 5;
        int i = 0;
        System.Random rand = new System.Random();
        while (i++ < molecules)
        {
            var randomSpeed = SampleGaussian(rand, mean, std);
            randomSpeed = randomSpeed > minSpeed ? randomSpeed : minSpeed;
            randomSpeed = randomSpeed < maxSpeed ? randomSpeed : maxSpeed;
          
            Debug.Log(randomSpeed + "\n");
            Vector3 spawnPosition = new Vector3(rand.Next(xmin, xmax), rand.Next(ymin, ymax), rand.Next(zmin, zmax));
            Vector3 spawnDirection = new Vector3(rand.Next(-10, 10), rand.Next(-10, 10), rand.Next(-10, 10));

            spawnSingleMolecule(randomSpeed, spawnDirection, spawnPosition);

        }
    }
    public (int, int) getMolecules()
    {
        return (coldCounter, hotCounter);
    }

    public bool Slow(float speedFactor)
    {
        if (slowTimeFlag) return false;
        slowTimeFlag = true;
        Debug.Log("SlowTime");
        foreach (molecule mol in moleculeList)
        {
            mol.setSpeed(Math.Max(1,mol.getSpeed() / speedFactor));
            
        }
        return true;
    }

    public IEnumerator Unslow(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("UNSlowTime");
        foreach (molecule mol in moleculeList)
        {
            mol.setSpeed(mol.getInitSpeed());
        }
        slowTimeFlag = false;
    }
    public static void resetChosenMolecule() { chosenMolecule = null; }
    public void sortMolecule(molecule mol, Transform middleOfBox)
    {
        mol.setIsSorted();
        moleculeList.Remove(mol);
        mol.transform.position = middleOfBox.position;
    }
}
