using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Specialist
{
    [SerializeField] QuestionSpec questionSpec = new QuestionSpec();
    [SerializeField] string typeOfStudyArea;
    [SerializeField] string typeOfDifficulty;
    public int index;

    public void GetSpecialistResponse(Question question, string typeOfStudyArea)
    {
        this.typeOfStudyArea = questionSpec.TypeOfStudyArea[UnityEngine.Random.Range(0, 4)];
        typeOfDifficulty = questionSpec.TypeOfDifficulty[UnityEngine.Random.Range(0, 2)];

        if (this.typeOfStudyArea == typeOfStudyArea && question.difficulty == typeOfDifficulty)
        {
            index = question.GetCorrectAnswer();
        }
        else
        {
            index = UnityEngine.Random.Range(0, 4);
        }
    }
}