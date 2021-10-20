using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public Plane PlaneObj;
    public float Score;
    public GameObject MenuPanel;

    private bool IsPaused;

    // Start is called before the first frame update
    void Start()
    {
        MenuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // reference to pause and unpause games - https://gamedevbeginner.com/the-right-way-to-pause-the-game-in-unity/
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsPaused)
            {
                PauseGame();
                //MenuPanel.SetActive(true);
            }
            else
            {
                ResumeGame();
            }
        }

        if (!PlaneObj)
        {
            // player died. show menu to restart
        }
    }

    public void StartGame()
    {
        MenuPanel.SetActive(false);
    }

    public void RestartGame()
    {

    }

    public void PauseGame()
    {
        print("LOG:: PAUSE");
        Time.timeScale = 0;
        IsPaused = true;
    }

    public void ResumeGame()
    {
        print("LOG:: UN-PAUSE");
        //Time.timeScale = 1;
        MenuPanel.SetActive(false);
        IsPaused = false;
    }
}
