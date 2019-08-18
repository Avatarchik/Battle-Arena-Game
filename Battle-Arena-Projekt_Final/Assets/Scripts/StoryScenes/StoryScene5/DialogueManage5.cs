﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManage5 : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public Dialogue5 dialogue5;
    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
        StartDialogue(dialogue5);
    }

    public void StartDialogue (Dialogue5 dialogue5)
    {
        Debug.Log("Starting conversation with " + dialogue5.name);

        animator.SetBool("IsOpen", true);
        nameText.text = dialogue5.name;
        sentences.Clear();

        foreach(string sentence in dialogue5.sentences)
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
            yield return new WaitForSeconds(0.05f);
        }
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation.");
        animator.SetBool("IsOpen", false);
        SceneManager.LoadScene("StoryScene6");
    }
}
