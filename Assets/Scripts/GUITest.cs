using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUITest : MonoBehaviour
{
    // Fields
    private string scene;
   
    public GUIStyle style;
    public Font font;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene().name;
        DontDestroyOnLoad(this);
        style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.font = font;
        style.fontSize = 2000;
    }

    // Update is called once per frame
    void Update()
    {
        scene = SceneManager.GetActiveScene().name;
        style.fontSize = 50;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 50, 19), "Hecken' heck!", style);
    }
}