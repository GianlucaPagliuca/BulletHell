using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI mainMenuText;
    public GameObject[] gameButtons;

    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.z));

        mainMenuText.transform.position = new Vector3(0, screenBounds.y / 2, 0);

        gameButtons[0].transform.position = new Vector3(0, 0, 0);
        gameButtons[1].transform.position = new Vector3(0, (screenBounds.y * -1) / 2, 0);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}