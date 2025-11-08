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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            //ReadInput();
            Debug.Log("ClickedENter");
        }
    }

    public void OnTextChange() 
    {
        currentText = ConsoleText.text;
    }

    void ReadInput()
    {
        if(!string.IsNullOrEmpty(InputText.text) && canEnterText)
        {
            StartCoroutine(PrintInput(InputText.text));
            InputText.text = string.Empty;
            Debug.Log("EnteredSOemting");
        }
    }

    IEnumerator PrintInput(string text)
    {
        canEnterText = false;
        newMessage = text;
        while (newMessage != string.Empty) 
        {
            currentText = currentText + newMessage[0];
            newMessage.Substring(1, newMessage.Length - 1);
            OnTextChange();
            yield return new WaitForSeconds(textSpeed);
        }
    }

}
