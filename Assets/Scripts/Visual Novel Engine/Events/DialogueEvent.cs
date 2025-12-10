using System;
using UnityEngine;


[CreateAssetMenu(fileName = "New Dialogue Event", menuName = "Visual Novel Engine/Dialogue Events/Dialogue Event")]
public class DialogueEvent : ScriptableObject
{
    public event Action OnDialogueEvent;


    public virtual void TriggerDialogueEvent()
    {
        OnDialogueEvent?.Invoke();
    }
}
