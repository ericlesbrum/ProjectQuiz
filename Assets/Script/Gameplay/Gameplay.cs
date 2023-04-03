using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

[System.Serializable]
public class Quiz
{
    public List<Question> questions;
}

public class Gameplay : MonoBehaviour
{
    private const int MAX = 10;
    [SerializeField] int[] prizeValues = new int[MAX];
    [SerializeField] int index, currentPrize, currentPlayerAnswer;
    [SerializeField] string playerName;
    [SerializeField] float[] percentAnswers = new float[4], percentSpecialistValue = new float[4];
    [SerializeField] TextAsset textJson;
    [SerializeField] Question currentQuestion;
    [SerializeField] List<Question> questions;
    [SerializeField] List<Audience> audiences = new List<Audience>();
    [SerializeField] List<Specialist> specialists = new List<Specialist>();
    [SerializeField] Button[] helpButtons;
    [SerializeField] GameObject prizePrefab;
    [SerializeField] Transform prizeLocation;
    [SerializeField] TextMeshProUGUI enunciationText;
    [SerializeField] TextMeshProUGUI[] answersText;
    [SerializeField] GameObject checkAnswerScreen, correctScreen, victoryScreen, stopScreen, gameOverScreen, percentAudience, percentSpecialist;
    [SerializeField] Animator hud;
    [SerializeField] TextMeshProUGUI[] audiencePecent, specialistPercent;
    [SerializeField] Slider[] audienceSliders, specalistsSlider;
    [SerializeField] TextMeshProUGUI victoryText, stopText, gameOverText;
    private int jumpQuestion = 3, audienceCount = 2, indexAudience, indexSpecialist;

    private void Start()
    {
        GameManager.Instance.ChangeFont();
        playerName = GameManager.Instance.PlayerName;
        currentPrize = prizeValues[0];
        GameManager.Instance._Gameplay = this;
        string json = textJson.text;
        Quiz quiz = JsonConvert.DeserializeObject<Quiz>(json);
        questions = quiz.questions;

        GameManager.Instance.Shuffle(questions);

        for (int i = 0; i < 50; i++)
        {
            audiences.Add(new Audience());
        }
        for (int i = 0; i < 3; i++)
        {
            specialists.Add(new Specialist());
        }

        GetQuestion();
    }
    public void GetQuestion()
    {
        if (index >= MAX)
            return;
        else
        {
            currentQuestion = questions[0];
            questions.Remove(questions[0]);
            if (questions.Count == 0)
            {
                questions.Remove(questions[0]);
            }
            foreach (var item in audiences)
            {
                item.GetAudienceResponse(currentQuestion, questions[0].typeOfStudyArea);
            }
            if (audienceCount > 0 && !helpButtons[1].interactable)
                helpButtons[1].interactable = true;
            enunciationText.text = currentQuestion.enunciation;
            for (int i = 0; i < answersText.Length; i++)
            {
                answersText[i].text = currentQuestion.answers[i];
            }
            foreach (var item in specialists)
            {
                item.GetSpecialistResponse(currentQuestion, questions[0].typeOfStudyArea);
            }
            PrizeManipulation();
        }
    }
    public void PrizeManipulation()
    {
        GameObject temp = null;
        for (int i = 0; i < prizeLocation.childCount; i++)
            Destroy(prizeLocation.GetChild(i).gameObject);
        if (index == 0)
        {
            temp = Instantiate(prizePrefab, prizeLocation, false);
            temp.GetComponentInChildren<TextMeshProUGUI>().text = "0";
            temp = Instantiate(prizePrefab, prizeLocation, false);
            temp.GetComponentInChildren<TextMeshProUGUI>().text = prizeValues[index].ToString();
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                temp = Instantiate(prizePrefab, prizeLocation, false);
                switch (i)
                {
                    case 0:
                        temp.GetComponentInChildren<TextMeshProUGUI>().text = (prizeValues[index] / 4).ToString();
                        break;
                    case 1:
                        temp.GetComponentInChildren<TextMeshProUGUI>().text = (prizeValues[index] / 2).ToString();
                        break;
                    default:
                        temp.GetComponentInChildren<TextMeshProUGUI>().text = prizeValues[index].ToString();
                        break;
                }
            }
        }
    }
    public void JumpToNextQuestion()
    {
        jumpQuestion--;
        if (jumpQuestion <= 0)
        {
            helpButtons[0].interactable = false;
        }
        else
        {
            GetQuestion();
        }
    }
    public void AudienceHelp()
    {
        audienceCount--;
        indexAudience = 0;
        for (int i = 0; i < percentAnswers.Length; i++)
        {
            percentAnswers[i] = 0;
            audiencePecent[i].text = "0.0%";
        }
        AudienceManipulation();
        helpButtons[1].interactable = false;
        percentAudience.SetActive(true);
    }
    public void AudienceManipulation()
    {
        int[] temp = new int[4];
        for (int i = 0; i < percentAnswers.Length; i++)
        {
            foreach (var item in audiences)
            {
                if (i == item.index)
                    temp[i]++;
            }
            percentAnswers[i] = ((float)temp[i] / audiences.Count * 1.0f);
            //audiencePecent[i].text = percentAnswers[i].ToString("F1");
            StartCoroutine(SliderPercent(percentAnswers[i], audiencePecent[i], audienceSliders[i], 0));
        }
    }
    public void SpecialistManipulation()
    {
        int[] temp = new int[4];
        for (int i = 0; i < temp.Length; i++)
        {
            foreach (var item in specialists)
            {
                if (i == item.index)
                    temp[i]++;
            }
            percentSpecialistValue[i] = ((float)temp[i] / temp.Length * 1.0f);
            //audiencePecent[i].text = percentAnswers[i].ToString("F1");
            StartCoroutine(SliderPercent(percentSpecialistValue[i], specialistPercent[i], specalistsSlider[i], 1));
        }
    }
    public void SpecialistHelp()
    {
        helpButtons[2].interactable = false;
        indexSpecialist = 0;
        for (int i = 0; i < percentSpecialistValue.Length; i++)
        {
            percentSpecialistValue[i] = 0;
            specialistPercent[i].text = "0.0%";
        }
        SpecialistManipulation();
        percentSpecialist.SetActive(true);
    }
    public void SetPlayerAnswer(int index)
    {
        currentPlayerAnswer = index;
        checkAnswerScreen.SetActive(true);
    }
    public void CheckAnswer()
    {
        if (currentQuestion.answers[currentPlayerAnswer].Equals(currentQuestion.correct))
        {
            currentPrize = prizeValues[index];
            index++;
            if (index >= MAX)
            {
                victoryScreen.SetActive(true);
                victoryText.text = $"Parabéns {playerName}! Você ganhou o prêmio total: {currentPrize}$";
                return;
            }
            GetQuestion();
            correctScreen.SetActive(true);
        }
        else
        {
            gameOverScreen.SetActive(true);
            currentPrize /= 4;
            gameOverText.text = $"Que pena {playerName}! Você errou, o seu o prêmio total é: {currentPrize}$";
        }
        hud.SetTrigger("Hide");
    }
    IEnumerator SliderPercent(float value, TextMeshProUGUI textMeshPro, Slider slider, int valueOp)
    {
        textMeshPro.text = "";
        slider.value = 0;
        var waitTimer = new WaitForSeconds(0.01f);
        for (slider.value = 0; slider.value <= value;)
        {
            slider.value += Time.deltaTime;
            textMeshPro.text = (Mathf.RoundToInt(slider.value * 100)).ToString("F1") + "%";
            yield return waitTimer;
        }
        if (valueOp == 0)
        {
            indexAudience = audiencePecent.Length;
        }
        else
        {
            indexSpecialist = specalistsSlider.Length;
        }
        yield return null;
    }
    public void SkipPercentSpecialist()
    {
        if (indexSpecialist >= percentSpecialistValue.Length)
        {
            percentSpecialist.SetActive(false);
        }
        else
        {
            StopAllCoroutines();
            for (indexSpecialist = 0; indexSpecialist < percentSpecialistValue.Length; indexSpecialist++)
            {
                specalistsSlider[indexSpecialist].value = percentSpecialistValue[indexSpecialist];
                specialistPercent[indexSpecialist].text = (Mathf.RoundToInt(percentSpecialistValue[indexSpecialist] * 100)).ToString("F1") + "%";
            }
        }
    }
    public void SkipPercentAudience()
    {
        if (indexAudience >= percentAnswers.Length)
        {
            percentAudience.SetActive(false);
        }
        else
        {
            StopAllCoroutines();
            for (indexAudience = 0; indexAudience < percentAnswers.Length; indexAudience++)
            {
                audienceSliders[indexAudience].value = percentAnswers[indexAudience];
                audiencePecent[indexAudience].text = (Mathf.RoundToInt(percentAnswers[indexAudience] * 100)).ToString("F1") + "%";
            }
        }
    }
    public void StopNow()
    {
        hud.SetTrigger("Hide");
        stopScreen.SetActive(true);
        if (index == 0)
            currentPrize = 0;
        currentPrize /= 2;
        stopText.text = $"Que pena {playerName}, você desistiu e seu prêmio é: {currentPrize}$";
    }
    public void ReloadGame()
    {
        stopScreen.SetActive(false);
        GameManager.Instance.PlayerName = "";
        GameManager.Instance.LoadGame("Start");
        GameManager.Instance.EnableButtons();
        GameManager.Instance._Canvas.SetActive(true);
    }
}