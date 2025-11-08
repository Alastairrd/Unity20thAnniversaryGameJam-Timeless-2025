using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
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

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            ReadInput();
        }
    }


    void ReadInput()
    {
        if(!string.IsNullOrEmpty(InputText.text) && canEnterText)
        {
            StartCoroutine(PrintInput(InputText.text));
            InputText.text = string.Empty;
            canEnterText = false;
            Debug.Log("Input: " + InputText.text);
        }
    }

    IEnumerator PrintInput(string text)
    {
        newMessage = "\n " + text;
        Debug.Log("StartCourutine for: " + newMessage);
        while (newMessage != string.Empty) 
        {
            currentText = currentText + newMessage[0];
            Debug.Log("CurrentText: " + currentText);
            newMessage = newMessage.Substring(1, newMessage.Length - 1);
            Debug.Log("new message shortened: " + newMessage);
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
