﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EpilogueSceneManager1 : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public EpilogueScene1 epilogue1;
    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
        StartDialogue(epilogue1);
    }

    public void StartDialogue (EpilogueScene1 epilogue1)
    {
        Debug.Log("Starting conversation with " + epilogue1.name);

        animator.SetBool("IsOpen", true);
        nameText.text = epilogue1.name;
        sentences.Clear();

        foreach(string sentence in epilogue1.sentences)
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
        StartCoroutine(TypeSenctence(sentence));
    }

    IEnumerator TypeSenctence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation.");
        animator.SetBool("IsOpen", false);
        SceneManager.LoadScene("Epilogue1_5");
    }
}
