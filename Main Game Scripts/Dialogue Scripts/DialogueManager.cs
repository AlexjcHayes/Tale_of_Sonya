using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject nameText;
    public GameObject dialogueText;

    GameObject gameManager;
    GameManagement gameManage;
    private Queue<string> sentences;
    private Queue<string> names; // added this
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
    }

    // Update is called once per frame
    public void StartDialogue(Dialogue dialogue)
    {

        //Debug.Log("Starting conversation with " + dialogue.name); // debug
        names.Clear();
        sentences.Clear();
        foreach (string name in dialogue.name)
        {
            names.Enqueue(name);
        }

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence.ToString());
        }
        displayNextSentence();
    }

    public void displayNextSentence()
    {
        if (sentences.Count == 0)
        {
            endDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        string name = names.Dequeue();
        nameText.GetComponent<TMPro.TextMeshProUGUI>().text = name;
        //dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        //Debug.Log(sentence); // debug
    }

    IEnumerator TypeSentence(string sentence){
        dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text ="";
        foreach(char letter in sentence.ToCharArray()){
            dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text += letter;
            yield return new WaitForSeconds((float).01);
        }
    }
    void endDialogue()
    {
        gameManager = GameObject.Find("GameManager"); // finds the game manager game object
        gameManage = gameManager.GetComponent<GameManagement>(); // gets the GameManagement Script 
        gameManage.DialogueTextBox.SetActive(false); // disables the text box UI
        Debug.Log("End of conversation");
    }
}
