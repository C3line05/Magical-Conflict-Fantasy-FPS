using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NovelGUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _speakerNameText;
    [SerializeField] private TMP_Text _dialogueText;
    // [SerializeField] private Image _leftPortraitImage;
    // [SerializeField] private Image _rightPortraitImage;
    //[SerializeField] private DialogueChoiceButton _dialogueChoiceButtonPrefab;
    //[SerializeField] private Transform _dialogueChoicesContainer;
    [SerializeField] private NovelScene _scene;
    [SerializeField] private GameObject _dialogueBox;

    private NovelDialogue _currentDialogue;
    private int _currentDialogueLineIndex = 0;

    public UnityEvent OnDialogueAdvanced;
    public UnityEvent OnSceneEnded;

    public static NovelGUI Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayerInputSingleton.instance.Actions["Dialogue"].performed += OnDialogueNext;
    }

    private void OnDestroy()
    {
        PlayerInputSingleton.instance.Actions["Dialogue"].performed -= OnDialogueNext;
    }

    public void OnDialogueNext(InputAction.CallbackContext ctx)
    {
        // Handle pointer down events (e.g., advancing dialogue)
        UpdateToNextDialogueLine();
        OnDialogueAdvanced?.Invoke();
    }

    public void SetCurrentDialogue(NovelDialogue dialogue)
    {
        _currentDialogue = dialogue;
        _currentDialogueLineIndex = -1;
        UpdateToNextDialogueLine();

    }

    public void UpdateToPreviousDialogueLine()
    {
        if (_currentDialogue == null)
            return;

        if(_currentDialogueLineIndex - 1 < 0)
        {
            return;
        }

        _currentDialogueLineIndex--;
        UpdateDialogueUI(_currentDialogue.GetDialogueLine(_currentDialogueLineIndex));
    }

    private void UpdateToNextDialogueLine()
    {
        if (_currentDialogue == null)
            return;

        if(_currentDialogueLineIndex + 1 >= _currentDialogue.DialogueLineCount)
        {
            DisplayChoices(_currentDialogue);
            return;
        }

        _currentDialogueLineIndex++;
        UpdateDialogueUI(_currentDialogue.GetDialogueLine(_currentDialogueLineIndex));
    }

    public void UpdateDialogueUI(DialogueLine dialogueLine)
    {
        _speakerNameText.text = dialogueLine.SpeakerName;
        _dialogueText.text = dialogueLine.DialogueText;

        dialogueLine.TriggerDialogueEvents();

        // Update portrait images based on direction
        //if (dialogueLine.PortraitDirection == Direction.Left)
        //{
        //    _leftPortraitImage.sprite = dialogueLine.SpeakerPortrait;
        //    _leftPortraitImage.gameObject.SetActive(true);
        //    _rightPortraitImage.gameObject.SetActive(false);
        //}
        //else if (dialogueLine.PortraitDirection == Direction.Right)
        //{
        //    _rightPortraitImage.sprite = dialogueLine.SpeakerPortrait;
        //    _rightPortraitImage.gameObject.SetActive(true);
        //    _leftPortraitImage.gameObject.SetActive(false);
        //}
    }

    private void DisplayChoices(NovelDialogue dialogue)
    {
        if(_currentDialogue.IsEndDialogue)
        {
            OnSceneEnded?.Invoke();
            Debug.Log("Scene Ended.");
            SceneManager.LoadScene(_currentDialogue.SceneToLoadAfter);
            return;
        }

        // Clear existing choice buttons
        //foreach (Transform child in _dialogueChoicesContainer)
        //{
        //    Destroy(child.gameObject);
        //}

        // Create new choice buttons
        //DialogueChoice[] choices = dialogue.DialogueChoices;
        //for (int i = 0; i < choices.Length; i++)
        //{
        //    DialogueChoiceButton choiceButton = Instantiate(_dialogueChoiceButtonPrefab, _dialogueChoicesContainer);
        //    choiceButton.SetChoiceText(choices[i].ChoiceText, i);
        //    choiceButton.OnChoiceSelected += OnChoiceSelected;
        //}
    }

    private void OnChoiceSelected(int choiceIndex)
    { 
        //foreach (Transform child in _dialogueChoicesContainer)
        //{
        //    Destroy(child.gameObject);
        //}

        DialogueChoice choice = _currentDialogue.DialogueChoices[choiceIndex];
        if (choice.NextDialogue != null)
        {
            SetCurrentDialogue(choice.NextDialogue);
        }
    }
    public void StartDialogue(NovelDialogue dialogue)
    {
        _dialogueBox.SetActive(true);
        SetCurrentDialogue(dialogue);

        Debug.Log("Textbox attiva");
    }

    public void EndDialogue()
    {
        _dialogueBox.SetActive(false);
    }
}