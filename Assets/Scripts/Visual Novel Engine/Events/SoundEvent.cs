using UnityEngine;

[CreateAssetMenu(fileName = "New Sound Dialogue Event", menuName = "Visual Novel Engine/Dialogue Events/Sound Event")   ]
public class DialogueSoundEvent : DialogueEvent
{
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private float _volume = 1.0f;

    public override void TriggerDialogueEvent()
    {
        base.TriggerDialogueEvent();
        AudioSource.PlayClipAtPoint(_audioClip, Vector3.zero, _volume);
    }
}