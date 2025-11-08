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
    [SerializeField] Queue<string> currentQueue = new Queue<string>();
    [SerializeField] TMP_Text ConsoleText;
    [SerializeField] TMP_InputField InputText;


    [SerializeField] float speedToLengthRatio = 10;
    [SerializeField] float minSpeed = 1;
    [SerializeField] float maxSpeed = 3;
       

    private void Start()
    {
        StartCoroutine(PrintMessage(startMessage));
        currentText = ConsoleText.text;
    }
    bool CanEnterText()
    {
        if (newMessage.Length == 0 && currentQueue.Count == 0 && InputText.text.Length != 0)
            return true;
        else
            return false;
    }

    public void ReadInput()
    {
        if(CanEnterText())
        {
            HandleInputLogic(InputText.text);
            InputText.text = string.Empty;
        }
    }

    void HandleInputLogic(string input) 
    {
        if (input.ToLower() == "build wall" || input == "1")
        {
            StartCoroutine(PrintMessage("wall was build hehehhe"));
        }
        else
        {
            string concatString = input + " is not a valid option";
            StartCoroutine(PrintMessage(concatString));
        }
    }

    public void HandleQueueLogic(Queue<string> queue) 
    {
        currentQueue = queue;
        while (!currentQueue.IsUnityNull())
        {
            StartCoroutine(PrintMessage(queue.Peek().ToString()));
            queue.Dequeue();    
        }
    }

    IEnumerator PrintMessage(string text)
    {
        newMessage = "\n " + text;
        while (newMessage != string.Empty) 
        {
            float textSpeed = TextSpeed(newMessage);
            currentText = currentText + newMessage[0];
            newMessage = newMessage.Substring(1, newMessage.Length - 1);
            OnTextChange();
            yield return new WaitForSeconds(textSpeed);
        }
        yield return null;
    }

    public void OnTextChange() 
    {
        ConsoleText.text = currentText;
    }

    float TextSpeed(string text)
    {
        return  Mathf.Clamp(speedToLengthRatio / text.Length, 1 / maxSpeed , 1 / minSpeed);
    }

}
