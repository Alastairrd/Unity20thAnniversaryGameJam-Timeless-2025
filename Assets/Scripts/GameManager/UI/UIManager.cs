using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Assets.Scripts.Actions.BuildTrap;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{
    #region Singleton Pattern
    public static UIManager Instance { get; private set; }
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
    #endregion

    [TextArea]
    [SerializeField] public string startMessage = " ";
    [SerializeField] string currentText = string.Empty;
    [SerializeField] string newMessage = string.Empty;
    [SerializeField] public Queue<string> currentQueue = new Queue<string>();
    [SerializeField] TMP_Text ConsoleText;
    [SerializeField] TMP_InputField InputText;
    [SerializeField] TMP_Text PlaceHolder;

    [SerializeField] Image HealthBar;
    [SerializeField] Image BaseHealthBar;
    [SerializeField] TMP_Text TimeText;

    [SerializeField] float textSpeedMultiplier;
    public float textSpeed;

    [SerializeField]
    float blinkerCooldown = 1;
    float lastPlaceHolderSwitch = 0f;
    bool isToggledPlaceHolder = true;
    bool pressingSkip = false;
    private bool didFocus = false;

    [Header("Sound")]
    [SerializeField] GameObject enterInputSound;

    public Coroutine printRoutine;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
            pressingSkip = true;
        if (Input.GetKeyUp(KeyCode.DownArrow))
            pressingSkip = false;

        if (!didFocus)
        {
            InputText.ActivateInputField();  // gives focus + caret
            didFocus = true;
        }

        // If the input field ever loses focus, force focus again
        if (!InputText.isFocused)
            InputText.ActivateInputField();

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2)) 
            InputText.ActivateInputField();

        if (CanEnterText())
            UpdateResourceUI();
        else
            BlinkPlaceHolder();
    }

    bool CanEnterText()
    {
        return newMessage.Length == 0 && currentQueue.Count == 0 && InputText.text.Length != 0;
    }

    #region Blinker Animation
    void BlinkPlaceHolder()
    {
        if(lastPlaceHolderSwitch + blinkerCooldown < Time.time)
        {
            isToggledPlaceHolder = !isToggledPlaceHolder;
            lastPlaceHolderSwitch = Time.time;
        }

        if (isToggledPlaceHolder)
            PlaceHolder.text = "";
        else
            PlaceHolder.text = "type actions here";
    }
    #endregion

    public void ReadInput()
    {
        if(CanEnterText())
        {
            Destroy(Instantiate(enterInputSound), 1);
            InputManager.Instance.HandleInputLogic(InputText.text);
            InputText.text = string.Empty;
        }
    }

    #region Printing
        #region Print Message
    //------------------------- Message Handling ----------------------------//
    public void PrintMessage(string message)
    {
        InputQueue(new Queue<string>(new string[] { message }));
    }
    #endregion

        #region Queue Logic
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

        // Finished � mark the coroutine as stopped
        printRoutine = null;
    }

    public IEnumerator PrintCurrentQueue()
    {
        while (currentQueue.Count > 0)
        {
            yield return StartCoroutine(MessageAnimation(currentQueue.Dequeue()));
        }
    }
    #endregion

        #region Message Animation
    IEnumerator MessageAnimation(string text) //give it list of things it should update
    {
        if (text.Length > 0 && text[0] == '<') text = stripTags(text);
        
        newMessage = "\n" + text;
        currentText += tagStart;
        while (newMessage.Length > 0) 
        {
            currentText += newMessage[0];
            newMessage = newMessage.Substring(1, newMessage.Length - 1);
            ConsoleText.text = currentText;
            if(!pressingSkip)
                yield return new WaitForSeconds(textSpeed / textSpeedMultiplier);
        }

        currentText += tagEnd;
        tagStart = tagEnd = "";
        yield return null;
    }


    #region Rich Text
    private string tagStart = "";
    private string tagEnd = "";

    private string stripTags(string text)
    {
        string stripText = "";
        tagStart = tagEnd = "";
        bool tagClosed = false;

        foreach (char c in text)
        {
            tagClosed &= (c != '<');

            if (tagClosed) stripText += c;
            else if (stripText == "") tagStart += c;
            else tagEnd += c;

            tagClosed |= (c == '>');
        }
        return stripText;
    }
    #endregion
    #endregion

    #region TextSpeed
    //Returns a speed proportional to the length of the text
    public void IncreaseTextSpeed()
    {
        float speed = textSpeed;
        speed += .1f;
        if (speed > 1) speed = 1;
        textSpeed = speed;
    }

    public void DecreaseTextSpeed()
    {
        float speed = textSpeed;
        speed -= .1f;
        if (speed < 0) speed = 0;
        textSpeed = speed;
    }


    #endregion

    #endregion

    #region Update UI
    public void UpdateResourceUI()
    {
        //TO DO
    }
    #endregion //TO IMPLEMENT

    #region Sound
    
    public void IncreaseVolume() 
    {
        float volume = AudioListener.volume;
        volume += .1f;
        if (volume > 1) volume = 1;
        AudioListener.volume = volume;
    }

    public void DecreaseVolume()
    {
        float volume = AudioListener.volume;
        volume -= .1f;
        if (volume < 0) volume = 0;
        AudioListener.volume = volume;
    }

    #endregion
}
