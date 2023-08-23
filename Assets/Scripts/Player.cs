using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    InputActionReference slowTime, eagleVision, pullMolecule, getMoleculeSpeed;
    [SerializeField] Spawner spawner;
    [SerializeField] GameObject firstCam, secondCam;
    [SerializeField] TextMeshProUGUI moleculeSpeedText, EnergyText;
    [SerializeField] int energy = 0;
    const string energyString = "Energy: ";
    bool eagleVisionFlag = false;



    // ------- Constants -------- //
    const float SLOW_TIME_WAIT = 5f, SLOW_FACTOR = 4f, EAGLE_VISION_WAIT = 4f, SHOW_SPEED_WAIT = 5f;
    const int SLOW_ENERGY = 10, SPEED_ENERGY = 5, PULL_M_ENERGY = 3, THIRD_VIEW_ENERGY = 1;

    // ------- Sounds -------- //
    [SerializeField] EventReference slowTimeSound;
    [SerializeField] EventReference unslowTimeSound;
    [SerializeField] EventReference eagleVisionSound;
    [SerializeField] EventReference unEagleVisionSound;
    [SerializeField] EventReference PullMoleculeSound;
    [SerializeField] EventReference GetMoleculeSpeedSound;

    private void OnEnable()
    {
        slowTime.action.performed += performSlowTime;
        if (!eagleVisionFlag) { eagleVision.action.performed += performEagleVision; }
        pullMolecule.action.performed += performPullMolecule;
        getMoleculeSpeed.action.performed += performGetMoleculeSpeed;
    }

    private void OnDisable()
    {
        slowTime.action.performed -= performSlowTime;
        eagleVision.action.performed -= performEagleVision;
        pullMolecule.action.performed -= performPullMolecule;
        getMoleculeSpeed.action.performed -= performGetMoleculeSpeed;
    }

    private void performGetMoleculeSpeed(InputAction.CallbackContext obj)
    {
        // ---- Show the speed for 5 seconds on the screen -----//
        if (Spawner.chosenMolecule == null ) { return; }
        RuntimeManager.PlayOneShot(GetMoleculeSpeedSound);
        energy += SPEED_ENERGY;
        EnergyText.text = energyString + energy.ToString(); 
        moleculeSpeedText.text = "Speed: " + Spawner.chosenMolecule.getInitSpeed().ToString();
        moleculeSpeedText.gameObject.SetActive(true);
        Invoke("unShowSpeed", SHOW_SPEED_WAIT);
    }
    private void performPullMolecule(InputAction.CallbackContext obj)
    {
        if (Spawner.chosenMolecule == null) { return; }
        RuntimeManager.PlayOneShot(PullMoleculeSound);
        energy += PULL_M_ENERGY;
        EnergyText.text = energyString + energy.ToString();
        Vector3 direction = transform.position - Spawner.chosenMolecule.transform.position;
        Spawner.chosenMolecule.setDirection(direction);
    }

    private void performEagleVision(InputAction.CallbackContext obj)
    {
        RuntimeManager.PlayOneShot(eagleVisionSound);
        energy += THIRD_VIEW_ENERGY;
        EnergyText.text = energyString + energy.ToString();
        secondCam.SetActive(true);
        firstCam.SetActive(false);
        eagleVisionFlag = true;
        Invoke("backToFirstCam", EAGLE_VISION_WAIT);
    }

    private void performSlowTime(InputAction.CallbackContext obj)
    {
        RuntimeManager.PlayOneShot(slowTimeSound);
        Debug.Log("Perform SlowTime");
        if (spawner.Slow(SLOW_FACTOR))
        {
            RuntimeManager.PlayOneShot(unslowTimeSound);
            StartCoroutine(spawner.Unslow(SLOW_TIME_WAIT));
            energy += SLOW_ENERGY;
            EnergyText.text = energyString + energy.ToString();
        }
    }
    private void unShowSpeed()
    {
        moleculeSpeedText.gameObject.SetActive(false);
    }

    private void backToFirstCam()
    {
        RuntimeManager.PlayOneShot(unEagleVisionSound);
        firstCam.SetActive(true);
        secondCam.SetActive(false);
        eagleVisionFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
