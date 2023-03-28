using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Presenter : MonoBehaviour
{
    [SerializeField] Animator hudPresenter, presenterAnimator;
    [SerializeField] GameObject intro, reactionScreen;
    [SerializeField] TextMeshProUGUI phraseText, introText;
    [TextArea(10, 50)]
    [SerializeField] string[] dialogues;
    [SerializeField] int index;
    [SerializeField] bool skipText;
    [SerializeField] string phrase;
    private void Start()
    {
        GameManager.Instance._Presenter = this;
    }
    public void IntroTextManipulation()
    {
        if (index < dialogues.Length)
        {
            if (!skipText)
            {
                skipText = true;
                StartCoroutine(TypingText(dialogues[index], introText, 0));
            }
            else
            {
                StopAllCoroutines();
                skipText = false;
                introText.text = dialogues[index];
                index++;
            }
        }
        else
        {
            Destroy(intro);
            hudPresenter.gameObject.SetActive(true);
            GameManager.Instance._Gameplay.GetQuestion();
            HUDManipulation("Show");
        }
    }
    public void HUDManipulation(string name)
    {
        hudPresenter.SetTrigger(name);
    }
    public void ReactionToQuestion(Question question)
    {
        phrase = "";
        switch (question.difficulty)
        {
            case TypeOfDifficulty.Easy:
                phrase = Words.easy;
                break;
            case TypeOfDifficulty.Medium:
                phrase = Words.medium;
                break;
            case TypeOfDifficulty.Hard:
                phrase = Words.hard;
                break;
        }
        //presenterAnimator.SetTrigger("Reaction " + question.difficulty.ToString());
        StartCoroutine(TypingText(phrase, phraseText, 1));
        reactionScreen.SetActive(true);
    }
    public void SkipDialogue()
    {
        if (!phrase.Contains(phraseText.text))
        {
            StopAllCoroutines();
            phraseText.text = phrase;
        }
        else
        {
            reactionScreen.SetActive(false);
        }
    }

    public IEnumerator TypingText(string text, TextMeshProUGUI textMeshPro, int valueOP)
    {
        textMeshPro.text = "";
        var waitTimer = new WaitForSeconds(0.1f);
        foreach (char c in text)
        {
            textMeshPro.text = textMeshPro.text + c;
            yield return waitTimer;
        }
        if (valueOP == 0)
        {
            skipText = false;
        }
        yield return null;
    }
}
public static class Words
{
    public static string easy = "Muito facil";
    public static string medium = "Da pra responder";
    public static string hard = "Pede ajuda";
}