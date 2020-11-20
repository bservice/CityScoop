using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Need to add this

public class DialogueManager : MonoBehaviour
{
    // The Text Itself
    public Text nameText;
    public Text dialogueText;
    public Queue<string> sentences;

    // Animates the Text-box
    public Animator animator;
    // Animates Vic
    public Animator animatorVic;
    private bool mouthOpen = false;
    public GameObject mouthClosed;

    // The Inventory
    private Inventory inv;

    // All of the lookable objects
    private LookableManager look;

    // Cursor Controls
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.ForceSoftware;
    public Vector2 hotSpot = Vector2.zero;

    // Whether or not dialogue is happening...
    public bool inDialogue;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        inv = FindObjectOfType<Inventory>();
        look = FindObjectOfType<LookableManager>();
        inDialogue = false;
    }

    void Update()
    {
        if(inDialogue)
        {
            // Changes the cursor to the dialogue one.
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        // It's dialogue time.
        inDialogue = true;

        // Makes inventory items invisible when the text-box pops up.
        foreach (PickUp p in inv.inventory)
        {
            p.GetComponent<SpriteRenderer>().enabled = false;
        }

        foreach(Lookable l in look.lookables)
        {
            l.GetComponent<BoxCollider2D>().enabled = false;
        }

        animator.SetBool("IsOpen", true);
        animatorVic.SetBool("IsIn", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            mouthClosed.SetActive(!mouthOpen);
            mouthOpen = !mouthOpen;
            dialogueText.text += letter;
            yield return null;
        }
        mouthOpen = false;
        mouthClosed.SetActive(true);
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        animatorVic.SetBool("IsIn", false);

        foreach (PickUp p in inv.inventory)
        {
            p.GetComponent<SpriteRenderer>().enabled = true;
        }

        foreach (Lookable l in look.lookables)
        {
            l.GetComponent<BoxCollider2D>().enabled = true;
        }

        mouthOpen = false;
        mouthClosed.SetActive(true);
        // Dialogue done.
        inDialogue = false;
    }
}
