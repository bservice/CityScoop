using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAudioManager : MonoBehaviour
{
    //Arrays to hold the buttons
    public Button[] sceneButtons;
    private PausePlay[] pauseButtons;

    //Array to hold the managers, this will be used to keep it at one
    private ButtonAudioManager[] allManagers;

    //Hold the audio info
    private AudioSource soundEffect;
    public AudioClip button;
    public AudioClip walk;

    // Start is called before the first frame update
    void Start()
    {
        //Limit to one audio manager in the game
        allManagers = FindObjectsOfType<ButtonAudioManager>();
        if (allManagers.Length > 1)
        {
            Destroy(allManagers[1]);
            Destroy(allManagers[1].gameObject);
        }

        //Don't let it get destroyed, unless there is more than one which is handled above
        DontDestroyOnLoad(this);

        //Find the initial buttons
        sceneButtons = FindObjectsOfType<Button>();
        pauseButtons = FindObjectsOfType<PausePlay>();

        //Set the sound effect
        soundEffect = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Only loop through scenes that are not the game scene (the game scene won't have any of this type of button)
        //if (SceneManager.GetActiveScene().name != "BrandonTests")
        //{
            //Keep finding all of the buttons in the scene
            sceneButtons = FindObjectsOfType<Button>();

            //Loop through and check if they were clicked, if so, play sound
            for (int i = 0; i < sceneButtons.Length; i++)
            {
                if (sceneButtons[i].Clicked)
                {                    
                    sceneButtons[i].Clicked = false;

                if(sceneButtons[i].Walk)
                {
                    soundEffect.PlayOneShot(walk);
                }
                else
                {
                    soundEffect.PlayOneShot(button);
                }

                    SceneManager.LoadScene(sceneButtons[i].Scene);
                }
            }

            //Only check for pause buttons in the game scene

            
            //Keep finding all of the buttons in the scene
            pauseButtons = FindObjectsOfType<PausePlay>();

            //Loop through and check if they were clicked, if so, play sound
            for (int i = 0; i < pauseButtons.Length; i++)
            {
                if (pauseButtons[i].Clicked2)
                {
                    soundEffect.PlayOneShot(button);
                    //pauseButtons[i].Clicked2 = false;
                }
            }
        //}
        
    }
}
