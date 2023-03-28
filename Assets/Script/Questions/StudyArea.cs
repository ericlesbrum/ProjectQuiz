using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class StudyArea
{
    public TypeOfStudyArea typeOfStudyArea;
    public List<Question> questions;
    public void Shuffle()
    {
        var count = questions.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = questions[i];
            questions[i] = questions[r];
            questions[r] = tmp;
        }
    }
}