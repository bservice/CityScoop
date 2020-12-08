using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    private bool clicked;
    public bool walkButton;
    public bool portalButton;

    Vector2 cursorPosition;

    public string sceneName;

    private PauseTest pauseMenu;
    private Inventory inventory;

    public bool Clicked
    {
        get { return clicked; }
        set
        {
            clicked = value;
        }
    }

    public bool Walk
    {
        get { return walkButton; }
    }
    public bool Portal
    {
        get { return portalButton; }
    }

    public string Scene
    {
        get { return sceneName; }
    }
    // Start is called before the first frame update
    void Start()
    {
        clicked = false;
        pauseMenu = FindObjectOfType<PauseTest>();
        inventory = FindObjectOfType<Inventory>();
        if(pauseMenu == null)
        {
            pauseMenu = new PauseTest();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!pauseMenu.Paused)
        {
            switch(sceneName)
            {
                case "Bathroom":
                    if (inventory.HaveItem("Bathroom Key"))
                    {
                        CheckForClick();
                    }
                    break;
                case "Office":
                    CheckForClick();
                    break;
                case "OfficeFront":
                    CheckForClick();
                    break;
                case "CentralPark1":
                    CheckForClick();
                    break;
                case "CentralPark2":
                    CheckForClick();
                    break;
                case "CentralPark3":
                    CheckForClick();
                    break;
                case "Street":
                    CheckForClick();
                    break;
                case "Gardner'sShed":
                    CheckForClick();
                    break;
                case "N-CentralPark1":
                    CheckForClick();
                    break;
                case "N-CentralPark2":
                    CheckForClick();
                    break;
                case "N-CentralPark3":
                    CheckForClick();
                    break;
                case "N-Bathroom":
                    CheckForClick();
                    break;
                case "Subway":
                    CheckForClick();
                    break;
                case "N-Subway":
                    CheckForClick();
                    break;
                case "Pizzeria":
                    CheckForClick();
                    break;
            }            
        }
        
    }

    public void CheckForClick()
    {
        //Grab vector2 for cursor to use in AABB math
        cursorPosition = Input.mousePosition;
        cursorPosition = Camera.main.ScreenToWorldPoint(cursorPosition);

        //Selection for objects
        if (Input.GetMouseButtonDown(0))
        {
            //AABB collision test for cursor
            if (cursorPosition.x < this.GetComponent<BoxCollider2D>().bounds.max.x && cursorPosition.x > this.GetComponent<BoxCollider2D>().bounds.min.x)
            {
                //Potential collision!
                //Check the next condition in a nested if statement, just to not have a ton of &'s and to be more efficient
                if (cursorPosition.y > this.GetComponent<BoxCollider2D>().bounds.min.y && cursorPosition.y < this.GetComponent<BoxCollider2D>().bounds.max.y)
                {
                    //Collision!
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(210f / 255f, 198f / 255f, 140f / 255f);
                    clicked = true;
                    //SceneManager.LoadScene(sceneName);
                }
            }
        }
    }
}
