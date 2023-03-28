using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour
{
    [SerializeField] TypeOfStudyArea typeOfStudyArea;
    [SerializeField] TypeOfDifficulty typeOfDifficulty;
    public int index;
    private void Start()
    {
        typeOfStudyArea = (TypeOfStudyArea)UnityEngine.Random.Range(0, 4);
        typeOfDifficulty = (TypeOfDifficulty)UnityEngine.Random.Range(0, 2);

    }
    public void GetAudienceResponse(Question question, TypeOfStudyArea typeOfStudyArea)
    {
        if (this.typeOfStudyArea == typeOfStudyArea && question.difficulty == typeOfDifficulty)
        {
            index = question.GetCorrectAnswer();
        }
        else
        {
            bool temp = false;
            do
            {
                index = UnityEngine.Random.Range(0, 4);
                if (index != question.GetCorrectAnswer())
                {
                    temp = true;
                }
            } while (!temp);
        }
    }
}
