using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Collections;   

public class UIManager : MonoBehaviour
{
    public string currentText = string.Empty;
    public string newMessage = string.Empty;
    public float textSpeed = .1f;
    [SerializeField] TMP_Text ConsoleText;
    [SerializeField] TMP_InputField InputText;

    public bool canEnterText = true;


    private void Start()
    {
        currentText = ConsoleText.text;

        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
    }



    public void ReadInput()
    {
        if(!string.IsNullOrEmpty(InputText.text) && canEnterText)
        {
            InputText.text = string.Empty;
            canEnterText = false;
            HandleInputLogic(InputText.text);
        }
    }

    void HandleInputLogic(string input) 
    {
        if (input.ToLower() == "build wall")
        {
            StartCoroutine(PrintInput(InputText.text));
        }
        else
        {

        }
    }

    void HandleQueryLogic(Queue queue) 
    {
        
    }

    IEnumerator PrintInput(string text)
    {
        newMessage = "\n " + text;
        while (newMessage != string.Empty) 
        {
            currentText = currentText + newMessage[0];
            newMessage = newMessage.Substring(1, newMessage.Length - 1);
            OnTextChange();
            yield return new WaitForSeconds(textSpeed);
        }
        canEnterText = true;
        yield return null;
    }

    public void OnTextChange() 
    {
        ConsoleText.text = currentText;
    }
}
