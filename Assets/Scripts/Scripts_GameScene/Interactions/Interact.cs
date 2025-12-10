using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class Interact : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_interactText;
    [SerializeField] private NovelScene m_dialogue;

    private void Awake()
    {
        m_interactText.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_interactText.enabled = true;
            PlayerInputSingleton.instance.Actions["Interact"].performed += OnDialogueInteract;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_interactText.enabled = false;
            NovelGUI.Instance.EndDialogue();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PlayerInputSingleton.instance.Actions["Interact"].performed -= OnDialogueInteract;
        }
    }

    private void OnDialogueInteract(InputAction.CallbackContext context)
    {
        NovelGUI.Instance.StartDialogue(m_dialogue.StartingDialogue);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        m_interactText.enabled = false;
    }
}
