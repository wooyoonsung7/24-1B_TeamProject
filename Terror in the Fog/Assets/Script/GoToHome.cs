using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToHome : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            InsideInventory.Instance.CheckInventory_2();
            GameManager.currentMap = 1;
            SceneManager.LoadScene("Home");
        }
    }
}
