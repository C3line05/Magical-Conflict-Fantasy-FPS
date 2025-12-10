using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueLine 
{
    [SerializeField] private string _speakerName;
    //[SerializeField] private Sprite _speakerPortrait;
    //[SerializeField] private Direction _portraitDirection;
    [SerializeField, TextArea(5, 10)] private string _dialogueText;
    [SerializeField] private DialogueEvent[] _dialogueEvents;
    
    public string SpeakerName => _speakerName;
    //public Sprite SpeakerPortrait => _speakerPortrait;
    //public Direction PortraitDirection => _portraitDirection;
    public string DialogueText => _dialogueText;
    public DialogueEvent[] DialogueEvents => _dialogueEvents;

    public void TriggerDialogueEvents()
    {
        if(_dialogueEvents == null)
            return;

        foreach (var dialogueEvent in _dialogueEvents)
        {
            dialogueEvent.TriggerDialogueEvent();
        }
    }
}
