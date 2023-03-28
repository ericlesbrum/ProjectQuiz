using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomEvents : MonoBehaviour
{
    [SerializeField] UnityEvent[] events;
    public void CallEvents(int index)
    {
        events[index].Invoke();
    }
}