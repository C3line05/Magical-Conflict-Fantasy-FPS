using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class DialogueChoice
{
    [SerializeField] private string _choiceText;
    [SerializeField] private NovelDialogue _nextDialogue;

    public string ChoiceText => _choiceText;
    public NovelDialogue NextDialogue => _nextDialogue;
}

