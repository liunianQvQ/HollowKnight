using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public int index;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        index = SceneManager.GetActiveScene().buildIndex;
        SelectScene();
    }

    void SelectScene()
    {
        if(index == 0 && Input.anyKeyDown)
        {
            SceneManager.LoadScene(index + 1);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (index == 1)
        {
            SceneManager.LoadScene(index + 1);
        }
    }
}
