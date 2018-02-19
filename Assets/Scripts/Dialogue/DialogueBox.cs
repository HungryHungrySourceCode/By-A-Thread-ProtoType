using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour {
    public Text text;
    public bool readingText;
    public TextScrollField scrollField;

    const float defaultStartTime = 0.2f;
    const float defaultTimeBetweenLetters = 0.06f;
    const float defaultPauseAtEndTime = 3f;

    public bool breakDialogue = false;


    [TextArea(2, 30)]
    public List<string> texts;
    public bool textBeingDisplayed = false;
    public string[] startingDialogue;

    private void Start()
    {
        for (int i = 0; i < startingDialogue.Length; i++)
        {
            texts.Add(startingDialogue[i]);
        }
        StartCoroutine(DisplayFirstTextInQue());
    }

    public void BreakDialogueAndRead(string _text)
    {
        scrollField.SetDragValue(-1f);
        breakDialogue = true;
        texts.Clear();
        texts.Add(_text);
        StartCoroutine(DisplayFirstTextInQue());
    }

    public void BreakDialogueAndDisplay(string _text)
    {
        scrollField.SetDragValue(1f);
        texts.Clear();
        breakDialogue = true;
        text.text = _text;
    }
    public void BreakDialogueAndDisplay(string _text, int fontSize)
    {
        text.fontSize = fontSize;
        texts.Clear();
        breakDialogue = true;
        text.text = _text;
    }


    public void AddTextToQue(string text)
    {
        texts.Add(text);
    }

    public void ClearAllText(string text)
    {
        texts.Remove(text);
    }

    public void ReadNextLine()
    {
        if (textBeingDisplayed == true)
            return;

        Debug.Log(texts.Count);
        int count = texts.Count;
        if (count > 0)
        {
            Debug.Log(texts[0]);
            StartCoroutine(DisplayFirstTextInQue());
        }
    }

    public IEnumerator DisplayFirstTextInQue()
    {
        Debug.Log("DisplayText");
        float t = 0;
        int currentCharacter = 0;
        float currentTime = defaultTimeBetweenLetters;
        bool started = false;
        textBeingDisplayed = true;
        int addedTextSize = 0;

        while (true)
        {
            if (breakDialogue == true)
            {
                textBeingDisplayed = false;
                scrollField.isTextBeingRead = false;
                breakDialogue = false;
                yield break;
            }
            scrollField.isTextBeingRead = true;

            if (started == false)
            {
                started = true;
                yield return new WaitForSeconds(defaultStartTime);
            }

            t += currentTime;

            if (currentCharacter < texts[0].Length) //+ addedTextSize)
            {
                if (texts[0].ToCharArray()[currentCharacter].ToString() == "+")
                {
                    currentTime += 0.04f;
                    currentCharacter++;
                }
                else if (texts[0].ToCharArray()[currentCharacter].ToString() == "-")
                {
                    currentTime -= 0.04f;
                    currentCharacter++;
                }
                else if (texts[0].ToCharArray()[currentCharacter].ToString() == "_")
                {
                    currentCharacter++;
                    if (texts[0].ToCharArray()[currentCharacter].ToString() == "+")
                    {
                        currentCharacter++;
                        float power;
                        float smallNum;

                        if (texts[0].ToCharArray()[currentCharacter].ToString() == "+")
                        {
                        currentCharacter++;

                        float.TryParse(texts[0].ToCharArray()[currentCharacter].ToString(), out power);
                        power *= 10f;
                        currentCharacter++;

                        float.TryParse(texts[0].ToCharArray()[currentCharacter].ToString(), out smallNum);
                        power += smallNum;
                        Debug.Log(power);

                            //currentCharacter++;

                        text.text = text.text.ToString() + "<size=" + power + ">";
                        addedTextSize += 9;
                            while (texts[0].ToCharArray()[currentCharacter].ToString() != " ") 
                            {
                                currentCharacter++;
                                text.text = text.text.ToString() + texts[0].ToCharArray()[currentCharacter].ToString();
                            }
                        text.text = text.text.ToString() + "</size>";
                        addedTextSize += 7;
                        }
                        else
                        {

                            float.TryParse(texts[0].ToCharArray()[currentCharacter].ToString(), out power);
                            power *= 10f;

                            currentCharacter++;
                            float.TryParse(texts[0].ToCharArray()[currentCharacter].ToString(), out smallNum);
                            power += smallNum;
                            Debug.Log(power);

                            text.text = text.text.ToString() + "<size=" + power + ">";
                            addedTextSize += 9;
                            currentCharacter++;
                            text.text = text.text.ToString() + texts[0].ToCharArray()[currentCharacter].ToString();
                            text.text = text.text.ToString() + "</size>";
                            addedTextSize += 7;
                            currentCharacter++;
                        }


                    }
                }
                else
                {
                    text.text = text.text.ToString() + texts[0].ToCharArray()[currentCharacter].ToString();
                    currentCharacter++;
                }

                //if (currentCharacter++ <= texts[0].ToCharArray().Length)

                
            }
            else
            {
                textBeingDisplayed = false;
                texts.RemoveAt(0);
                scrollField.isTextBeingRead = false;
                yield return new WaitForSeconds(defaultPauseAtEndTime);
                text.text = "";
                ReadNextLine();
                yield break;
            }

            yield return new WaitForSeconds(currentTime);
        }
    }
}
