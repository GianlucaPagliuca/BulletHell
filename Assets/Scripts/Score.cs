using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    private GameObject cam;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }


    // Update is called once per frame
    void Update()
    {
        TextChange();



    }
    public void TextChange()
    {
        scoreText.text = cam.GetComponent<GameManager>().Score.ToString();

    }
}
