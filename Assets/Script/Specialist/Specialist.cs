using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Specialist : MonoBehaviour
{
    [SerializeField] TypeOfStudyArea typeOfStudyArea;
    [SerializeField] TypeOfDifficulty typeOfDifficulty;
    public int index;
    private void Start()
    {
        typeOfStudyArea = (TypeOfStudyArea)UnityEngine.Random.Range(0, 4);
        typeOfDifficulty = (TypeOfDifficulty)UnityEngine.Random.Range(0, 2);
    }
    public void GetSpecialistResponse(Question question, TypeOfStudyArea typeOfStudyArea)
    {
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