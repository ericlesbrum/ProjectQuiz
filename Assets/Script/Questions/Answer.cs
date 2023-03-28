using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Answer
{
    [TextArea]
    public string text;
    [SerializeField] bool isCorrect;
    public bool GetCorrect()
    {
        return isCorrect;
    }
}
