using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Question
{
    [TextArea]
    public string enunciation;
    [TextArea]
    public string[] answers = new string[4];
    public string correct;
    public string difficulty;
    public string typeOfStudyArea;    

    public int GetCorrectAnswer()
    {
        int temp = 0;
        for (int i = 0; i < answers.Length; i++)
        {
            if (answers[i].Equals(correct))
            {
                temp = i;
            }
        }
        return temp;
    }
}