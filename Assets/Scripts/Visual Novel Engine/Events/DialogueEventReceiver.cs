using UnityEngine;
using System;
using UnityEngine.Events;


public class DialogueEventReceiver : MonoBehaviour
{
    [Serializable]
    private class DialogueEventResponsePair
    {
        [SerializeField] private DialogueEvent _dialogueEvent;

        public UnityEvent response;
    }

    [SerializeField] private DialogueEventResponsePair[] _eventResponsePairs;
}