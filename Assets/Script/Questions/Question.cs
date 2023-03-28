using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Question
{
    public TypeOfDifficulty difficulty;
    [TextArea]
    public string enunciation;
    public Answer[] answers = new Answer[4];
    public int GetCorrectAnswer()
    {
        int temp = 0;
        for (int i = 0; i < answers.Length; i++)
        {
            if (answers[i].GetCorrect())
            {
                temp = i;
            }
        }
        return temp;
    }
}