using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Assets.Scripts.Actions.BuildTrap;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [TextArea()]
    [SerializeField] string startMessage = " ";
    [SerializeField] string currentText = string.Empty;
    [SerializeField] string newMessage = string.Empty;
    [SerializeField] public Queue<string> currentQueue = new Queue<string>();
    [SerializeField] TMP_Text ConsoleText;
    [SerializeField] TMP_InputField InputText;
    [SerializeField] TMP_Text PlaceHolder;

    [SerializeField] GameObject ResourceUI;

    [SerializeField] Image HealthBar;
    [SerializeField] Image BaseHealthBar;
    [SerializeField] TMP_Text TimeText;
    [SerializeField] TMP_Text Wood;
    [SerializeField] TMP_Text Metal;
    [SerializeField] TMP_Text Medicine;

    [SerializeField] List<string> PossibleOutcomes;

    [Range(0f, 1f)]
    [SerializeField] float textSpeed = .1f;
    [Range(1f, 40f)]
    [SerializeField] float textSpeedMultiplier = 10;

    [SerializeField] GameObject enterInputSound;


    private bool didFocus = false;



    Dictionary<string, string> ActionDictionary = new Dictionary<string, string>()
    {
        {"BuildWall", "Build a Wall" },
        {"Scavenge", "Scavenge" },
        {"BuildTrap", "Build a Trap" },
        {"HealPlayer", "First-Aid" },
        {"PassTime", "Pass the time" }
    };


    Dictionary<string, string> InputDictionary = new Dictionary<string, string>()
    {
        {"Build Wall"       ,"BuildWall"},
        {"Build a Wall"     ,"BuildWall"},
        {"Build Walls"      ,"BuildWall"},
        {"Wall"             ,"BuildWall"},
        {"Scavenge"         ,"Scavenge"},
        {"Build Trap"       ,"BuildTrap"},
        {"Build a Trap"     ,"BuildTrap"},
        {"Trap"             ,"BuildTrap"},
        {"First-Aid"        ,"HealPlayer"},
        {"First Aid"        ,"HealPlayer"},
        {"Heal"             ,"HealPlayer"},
        {"Firstaid"         ,"HealPlayer"},
        {"pass time"        ,"PassTime"},
        {"pass"             ,"PassTime"},
        {"wait"             ,"PassTime"},
        {"skip"             ,"PassTime"},
    };

    [SerializeField] GameObject typingSoundPrefab;
    [SerializeField] GameObject typingSoundInstance;
    float lastTimeTyped = 0;
    float cooldown = .5f;

    [SerializeField] GameObject printingSoundPrefab;
    [SerializeField] GameObject printingSoundInstance;

    [SerializeField] GameObject beepUISound;


    bool pressingSkip = false;
    private Coroutine printRoutine;

    float lastPlaceHolderSwitch = 0f;
    [SerializeField]
    float blinkerCooldown = 1;

    bool isToggledPlaceHolder = true;

    private void Start()
    {
        //UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        UpdateResourceUI();
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


        if (!CanEnterText())
            BlinkPlaceHolder();

        if(lastTimeTyped + cooldown < Time.time && typingSoundInstance) 
        {
            Destroy(typingSoundInstance);
        }

        if (!NoMessagesQueued()) 
        {
            if(!printingSoundInstance)
                printingSoundInstance = Instantiate(printingSoundPrefab);
        }
        else 
        {
            if (printingSoundInstance)
                Destroy(printingSoundInstance);
        }

    }

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


    //------------------------- Input Handling ----------------------------//
    bool CanEnterText()
    {
        return NoMessagesQueued() && InputText.text.Length != 0;
    }

    bool NoMessagesQueued() 
    {
        return newMessage.Length == 0 && currentQueue.Count == 0;
    }

    public void ReadInput()
    {
        if(CanEnterText())
        {
            Destroy(Instantiate(enterInputSound), 1);
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

    public void UpdateResourceUI() 
    {
        
            HealthBar.fillAmount = GameController.Instance.playerHealth / 100f;
            BaseHealthBar.fillAmount = GameController.Instance.baseHealth / 250f;
            Metal.text = GameController.Instance.metal.ToString();
            Medicine.text = GameController.Instance.medicine.ToString();
            Wood.text = GameController.Instance.wood.ToString();
            TimeText.text = (24 - GameController.Instance.hoursLeftToday).ToString() + ":00";

        //Debug.Log("Updated UI");
    }

    void ChosenAction(int actionChosen)
    {
        string action = PossibleOutcomes[actionChosen];
        GameController.Instance.SimulateAction(action);
    }


    public void TakePossibleActions(List<string> possibleActions)
    {
        UpdateResourceUI();
        PossibleOutcomes = possibleActions;
        InputQueue(CreateQueueFromActions(PossibleOutcomes));
    }

    Queue<string> CreateQueueFromActions(List<string> possibleActions)
    {
        Queue<string> actionQueue = new Queue<string>();

        actionQueue.Enqueue("\n Please choose one of the following actions: \n");
        for (int i = 0; i < possibleActions.Count; i++)
        {
            actionQueue.Enqueue((i + 1).ToString() + ". " + GetActionFromDictionary(possibleActions[i]));
        }
        actionQueue.Enqueue("\n");

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
            currentText += newMessage[0];
            newMessage = newMessage.Substring(1, newMessage.Length - 1);
            OnTextChange();
                if(!pressingSkip)
                    yield return new WaitForSeconds(textSpeed / textSpeedMultiplier);
        }

        currentText += tagEnd;
        tagStart = tagEnd = "";
        yield return null;
    }

    public void OnTextChange()
    {
        ConsoleText.text = currentText;
    }

    public void CloseResourceUI() 
    {
        ResourceUI.SetActive(false);
        Destroy(Instantiate(beepUISound), 1f);
    }

    public void OpenResourceUI()
    {
        ResourceUI.SetActive(true);
        Destroy(Instantiate(beepUISound), 1f);
    }


    public void PlayerTyping() 
    {
        lastTimeTyped = Time.time;
        if (!typingSoundInstance)
        {
            typingSoundInstance = Instantiate(typingSoundPrefab);
        }
    }

}
