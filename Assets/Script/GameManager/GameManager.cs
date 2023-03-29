using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public string PlayerName { get { return playerName; } set { playerName = value; } }
    public Gameplay _Gameplay { get { return gameplay; } set { gameplay = value; } }
    public GameObject _Canvas { get { return canvas; } set { canvas = value; } }
    [SerializeField] Button[] optionsButtons;
    [SerializeField] TMP_InputField field;
    [SerializeField] GameObject canvas;
    [SerializeField] string playerName;
    [SerializeField] Gameplay gameplay;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    public void LoadGame(string name)
    {
        if (field.text == "")
            return;
        SceneManager.LoadScene(name);
        PlayerName = field.text;
        canvas.SetActive(false);
    }
    public void DisableButtons()
    {
        foreach (var item in optionsButtons)
            item.interactable = false;
    }
    public void EnableButtons()
    {
        foreach (var item in optionsButtons)
            item.interactable = true;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Shuffle(List<Question> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}