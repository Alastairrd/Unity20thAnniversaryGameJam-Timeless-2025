using System.Collections.Generic;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] string startMessage = " ";
    [SerializeField] string currentText = string.Empty;
    [SerializeField] string newMessage = string.Empty;
    [SerializeField] Queue<string> currentQueue;
    [SerializeField] TMP_Text ConsoleText;
    [SerializeField] TMP_InputField InputText;

    

    [SerializeField] float minSpeed = .01f;
    [SerializeField] float maxSpeed = .3f;
       

    private void Start()
    {
        currentText = ConsoleText.text;

        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
    }



    public void ReadInput()
    {
        if(!string.IsNullOrEmpty(InputText.text) && CanEnterText())
        {
            HandleInputLogic(InputText.text);
            InputText.text = string.Empty;
        }
    }

    void HandleInputLogic(string input) 
    {
        if (input.ToLower() == "build wall" || input == "1")
        {
            StartCoroutine(Print("wall was build hehehhe"));
        }
        else
        {
            string concatString = input + " is not a valid option";
            StartCoroutine(Print(concatString));
        }
    }

    public void HandleQueueLogic(Queue<string> queue) 
    {
        currentQueue = queue;
        while (queue.Peek() != null)
        {
            StartCoroutine(Print(queue.Peek().ToString()));
            queue.Dequeue();    
        }

    }

    IEnumerator Print(string text)
    {
        newMessage = "\n " + text;
        while (newMessage != string.Empty) 
        {
            currentText = currentText + newMessage[0];
            newMessage = newMessage.Substring(1, newMessage.Length - 1);
            OnTextChange();
            yield return new WaitForSeconds(TextSpeed(newMessage));
        }
        yield return null;
    }

    public void OnTextChange() 
    {
        ConsoleText.text = currentText;
    }

    float TextSpeed(string text)
    {

        return  Mathf.Clamp(1 / text.Length, minSpeed, maxSpeed);
    }

    bool CanEnterText()
    {
        if (string.IsNullOrEmpty(newMessage) && currentQueue.Peek() == null)
            return true;
        else
            return false;
    }
}
