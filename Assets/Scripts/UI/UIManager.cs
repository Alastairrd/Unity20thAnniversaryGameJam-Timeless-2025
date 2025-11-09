using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;
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

    [SerializeField] List<string> PossibleOutcomes;


    [SerializeField] float speedToLengthRatio = 10;
    [SerializeField] float minSpeed = 1;
    [SerializeField] float maxSpeed = 3;

    Dictionary<string, string> ActionDictionary = new Dictionary<string, string>()
    {
        {"BuildWall", "Build a Wall" },
        {"Scavenge", "Scavenge" },
        {"BuildTrap", "Build a Trap" }
    };


    Dictionary<string, string> InputDictionary = new Dictionary<string, string>()
    {
        {"Build Wall"       ,"BuildWall"},
        {"Build a Wall"     ,"BuildWall"},
        {"Build Walls"      ,"BuildWall"},
        {"Scavenge"         ,"Scavenge"},
        {"Build Trap"       ,"BuildTrap"},
        {"Build a Trap"     ,"BuildTrap"},
    };


    bool pressingSkip = false;
    private Coroutine printRoutine;

    private void Start()
    {
        
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

        PrintMessage(startMessage);
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

    //------------------------- Input Handling ----------------------------//
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
        
        if(input.ToLower() == "reset")
        {
            GameController.Instance.Reset();
            return;
        }

        if (input.ToLower() == "0" && PossibleOutcomes.Count == 0)
        {
            GameController.Instance.Reset();
            return;
        }

        if (int.TryParse(input, out int number))
        { 
            if(number > 0 && number <= PossibleOutcomes.Count)
            {
                ChosenAction(number - 1);
                return;
            }
        }


        for (int i = 0; i < PossibleOutcomes.Count; i++)
        {
            if (input.ToLower() == PossibleOutcomes[i].ToLower()) 
            {
                ChosenAction(i);
                return;
            }
        }

        string MappedInput;
        if(InputDictionary.TryGetValue(input, out MappedInput))
        {
            for (int i = 0; i < PossibleOutcomes.Count; i++)
            {
                if(MappedInput.ToLower() == PossibleOutcomes[i].ToLower())
                {
                    ChosenAction(i);
                    return;
                }
            }
            PrintMessage("You find yourself unable to " + GetActionFromDictionary(MappedInput));
            TakePossibleActions(PossibleOutcomes);
            return;
        }

        PrintMessage("You are unable to perfom that action");
        //TakePossibleActions(PossibleOutcomes);
    }

    

    void ChosenAction(int actionChosen)
    {
        string action = PossibleOutcomes[actionChosen];
        GameController.Instance.SimulateAction(action);
    }


    public void TakePossibleActions(List<string> possibleActions)
    {
        PossibleOutcomes = possibleActions;
        InputQueue(CreateQueueFromActions(PossibleOutcomes));
    }

    Queue<string> CreateQueueFromActions(List<string> possibleActions)
    {
        Queue<string> actionQueue = new Queue<string>();

        actionQueue.Enqueue("Please choose one of the following actions: \n");
        for (int i = 0; i < possibleActions.Count; i++)
        {
            actionQueue.Enqueue((i + 1).ToString() + ". " + GetActionFromDictionary(possibleActions[i]) + "\n");
        }

        return actionQueue;
    }

    string GetActionFromDictionary(string actionKey)
    {
        if(ActionDictionary.TryGetValue(actionKey, out string actionDescription)) { 
            return actionDescription;
        }
        return "Unknown action."; //This should never happen
    }

    //------------------------- Message Handling ----------------------------//
    public void PrintMessage(string message)
    {
        InputQueue(new Queue<string>(new string[] { message }));
    }

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
            else if(stripText == "") tagStart += c;
            else tagEnd += c;
            
            tagClosed |= (c == '>');
        }
        return stripText;
    }
    IEnumerator MessageAnimation(string text)
    {
        if (text.Length > 0 && text[0] == '<') text = stripTags(text);
        
        newMessage = "\n" + text;
        currentText += tagStart;
        while (newMessage.Length > 0) 
        {
            //Debug.Log(newMessage);
            float textSpeed = TextSpeed(newMessage);
            currentText += newMessage[0];
            newMessage = newMessage.Substring(1, newMessage.Length - 1);
            OnTextChange();
            yield return new WaitForSeconds(textSpeed);
        }

        currentText += tagEnd;
        tagStart = tagEnd = "";
        yield return null;
    }


    //Returns a speed proportional to the length of the text
    float TextSpeed(string text)
    {
        if (pressingSkip) 
        {
            //Debug.Log("Skipping text speed");
            return 0;
        }
        else 
        {
            return Mathf.Clamp(speedToLengthRatio / text.Length, 1 / maxSpeed, 1 / minSpeed);
        }
            
    }

    public void OnTextChange()
    {
        ConsoleText.text = currentText;
    }

}
