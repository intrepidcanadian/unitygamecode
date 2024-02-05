using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeGameScreen : MonoBehaviour

{
    public GameObject prefabInstance;


   public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ShowPrefab()
    {
        if (prefabInstance != null)
        {
            prefabInstance.SetActive(true);
        }
    }

        public void ClosePrefab()
    {
        if (prefabInstance != null)
        {
            prefabInstance.SetActive(false);
        }
    }

}
