using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Need to add this

public class DialogueManager : MonoBehaviour
{
    // The Text Itself
    public Text nameText;
    public Text dialogueText;
    public Queue<string> names;
    public Queue<string> sentences;
    public GameObject[] sprites;

    // Animates the Text-box
    public Animator animator;

    // The Inventory
    private Inventory inv;

    // All of the lookable objects
    private LookableManager look;
    // All of the buttons.
    private Button[] buttons;


    // Cursor Controls
    public Texture2D specialTexture;
    public Texture2D normalTexture;
    public CursorMode cursorMode = CursorMode.ForceSoftware;
    public Vector2 hotSpot = Vector2.zero;

    //Pause menu
    public PauseTest pauseMenu;

    // Whether or not dialogue is happening...
    public bool inDialogue;

    private PickUp[] items;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
        inv = FindObjectOfType<Inventory>();
        look = FindObjectOfType<LookableManager>();
        buttons = FindObjectsOfType<Button>();
        pauseMenu = FindObjectOfType<PauseTest>();
        inDialogue = false;
        items = FindObjectsOfType<PickUp>();
    }

    void Update()
    {
        if (!pauseMenu.Paused)
        {
            if (inDialogue)
            {
                // Changes the cursor to the dialogue one.
                Cursor.SetCursor(specialTexture, hotSpot, cursorMode);
            }
        }
        
        if(inDialogue)
        {
            for(int i = 0; i < items.Length; i++)
            {
                items[i].talking = true;
            }
        }
        else
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].talking = false;
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (!pauseMenu.Paused)
        {
            // It's dialogue time.
            inDialogue = true;

            // Makes inventory items invisible when the text-box pops up.
            foreach (PickUp p in inv.inventory)
            {
                p.GetComponent<SpriteRenderer>().enabled = false;
                p.GetComponent<BoxCollider2D>().enabled = false;
            }

            // Disables things that the player can search while in dialogue.
            foreach (Lookable l in look.lookables)
            {
                l.GetComponent<BoxCollider2D>().enabled = false;
            }
            foreach (NPC n in look.npcs)
            {
                n.GetComponent<BoxCollider2D>().enabled = false;
            }

            // Disables all buttons on screen.
            foreach(Button b in buttons)
            {
                b.GetComponent<SpriteRenderer>().enabled = false;
                b.GetComponent<BoxCollider2D>().enabled = false;
            }

            animator.SetBool("IsOpen", true);

            nameText.text = dialogue.names[0];

            // Empties out the variables.
            sentences.Clear();
            names.Clear();

            // Gets all of the names and dialogue (always needs to be the same amount!).
            for(int i = 0; i < dialogue.sentences.Length; i++)
            {
                sentences.Enqueue(dialogue.sentences[i]);
                names.Enqueue(dialogue.names[i]);
            }

            // Get all of the sprites for this dialogue.
            sprites = dialogue.people;
            foreach (GameObject sprite in sprites)
            {
                sprite.GetComponent<SpriteRenderer>().enabled = true;
            }

            // If the dialogue should change a conditional...
            if(dialogue.conditionToChange != -1)
            {
                // Change it!
                FindObjectOfType<GameManager>().conditionalBools[dialogue.conditionToChange] = true;
            }

            DisplayNextSentence();
        }
    }

    // Displaces the next sentence in the queue.
    public void DisplayNextSentence()
    {
        // Won't allow text to continue if paused.
        if (!pauseMenu.Paused)
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            // Change the text
            nameText.text = names.Dequeue();
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));

            // Change the talking sprite.
            foreach(GameObject sprite in sprites)
            {
                if(nameText.text == sprite.name)
                {
                    sprite.GetComponent<SpriteRenderer>().color = Color.white;
                }
                else
                {
                    sprite.GetComponent<SpriteRenderer>().color = Color.grey;
                }
            }
        }
    }

    // Slowly types out the sentence.
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);

        // Enabling the items and searchable areas.
        foreach (PickUp p in inv.inventory)
        {
            p.GetComponent<SpriteRenderer>().enabled = true;
            p.GetComponent<BoxCollider2D>().enabled = true;
        }
        foreach (Lookable l in look.lookables)
        {
            l.GetComponent<BoxCollider2D>().enabled = true;
        }
        foreach (NPC n in look.npcs)
        {
            n.GetComponent<BoxCollider2D>().enabled = true;
        }
        foreach (Button b in buttons)
        {
            b.GetComponent<SpriteRenderer>().enabled = true;
            b.GetComponent<BoxCollider2D>().enabled = true;
        }

        // Disabling the character portraits.
        foreach (GameObject sprite in sprites)
        {
            sprite.GetComponent<SpriteRenderer>().enabled = false;
        }

        // Dialogue done.
        inDialogue = false;

        // Changes the cursor to the normal one.
        Cursor.SetCursor(normalTexture, hotSpot, cursorMode);
    }
}
