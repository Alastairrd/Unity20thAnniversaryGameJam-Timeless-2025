using System.Collections.Generic;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Assets.Scripts.Actions.BuildTrap;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] string startMessage = " ";
    [SerializeField] string currentText = string.Empty;
    [SerializeField] string newMessage = string.Empty;
    [SerializeField] public Queue<string> currentQueue = new Queue<string>();
    [SerializeField] TMP_Text ConsoleText;
    [SerializeField] TMP_InputField InputText;


    [SerializeField] float speedToLengthRatio = 10;
    [SerializeField] float minSpeed = 1;
    [SerializeField] float maxSpeed = 3;
       
    bool pressingSkip = false;
    private Coroutine printRoutine;

    private void Start()
    {
        PrintMessage(startMessage);
        currentText = ConsoleText.text;
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pressingSkip = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            pressingSkip = false;
        }
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
            //play sound for enter
        }
        //play for error
    }

    void HandleInputLogic(string input) 
    {
        if (input.ToLower() == "build wall" || input == "1")
        {
            PrintMessage("wall was build hehehhe");
        }
        else
        {
            string concatString = input + " is not a valid option";
            PrintMessage(concatString);
        }
    }

    //public void HandleQueueLogic(Queue<string> queue) 
    //{
    //    currentQueue = queue;
    //    while (!currentQueue.IsUnityNull())
    //    {
    //        PrintMessage(queue.Dequeue());
    //    }
    //}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queue"></param>
    public void InputQueue(Queue<string> queue)
    {
        while (queue.Count > 0)
        {
            currentQueue.Enqueue(queue.Dequeue());
        }

        // Only start the coroutine if it's not already running
        if (printRoutine == null)
        {
            printRoutine = StartCoroutine(PrintCurrentQueue_TEST());
        }
    }
    private IEnumerator PrintCurrentQueue_TEST()
    {
        while (currentQueue.Count > 0)
        {
            yield return StartCoroutine(MessageAnimation(currentQueue.Dequeue()));
        }

        // Finished – mark the coroutine as stopped
        printRoutine = null;
    }
    /// <summary>
    /// /
    /// </summary>
    /// <returns></returns>
    public IEnumerator PrintCurrentQueue() 
    {
        while (currentQueue.Count > 0)
        {
            yield return StartCoroutine(MessageAnimation(currentQueue.Dequeue()));
        }
    }
    public void PrintMessage(string text) 
    {
        StartCoroutine(MessageAnimation(text));
    }

    IEnumerator MessageAnimation(string text)
    {
        newMessage = "\n" + text;
        while (newMessage.Length > 0) 
        {
            Debug.Log(newMessage);
            float textSpeed = TextSpeed(newMessage);
            currentText += newMessage[0];
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
        if (pressingSkip) 
        {
            Debug.Log("Skipping text speed");
            return 0;
        }
        else 
        {
            return Mathf.Clamp(speedToLengthRatio / text.Length, 1 / maxSpeed, 1 / minSpeed);
        }
            
    }

}
