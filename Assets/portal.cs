using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal : MonoBehaviour
{
    SceneManager sceneManager;
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("main");
    }
}
