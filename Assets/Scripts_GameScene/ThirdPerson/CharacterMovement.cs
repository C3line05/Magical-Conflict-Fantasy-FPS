using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    private const float MIN_MOVE_SPEED = 0.1f; //non può essere cambiato

    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _rotationSpeed;

    private Vector3 _currentSpeed;
    private Vector3 _wantedSpeed;

    private float _speedMagnitude;

    private float _vertical;
    private float _horizontal;

    private bool _defenseMovement;

    //Sezione Céline
    public CharacterActions characterActions;

    private void Start()
    {
        _speedMagnitude = _walkSpeed;
        PlayerInputSingleton.instance.Actions["Move"].performed += OnMoveInput;
        PlayerInputSingleton.instance.Actions["Sprint"].started += OnSprintStart;
        PlayerInputSingleton.instance.Actions["Sprint"].canceled += OnSprintEnd;

        //Sezione Céline
        GameObject Character1 = GameObject.FindGameObjectWithTag("Player");
        if (Character1 != null)
        {
            characterActions = Character1.GetComponent<CharacterActions>();
        }
    }


    private void OnDisable()
    {
        PlayerInputSingleton.instance.Actions["Move"].performed -= OnMoveInput;
        PlayerInputSingleton.instance.Actions["Sprint"].started -= OnSprintStart;
        PlayerInputSingleton.instance.Actions["Sprint"].canceled -= OnSprintEnd;
    }

    private void Update()
    {
        _wantedSpeed.z = _vertical * _speedMagnitude;
        _wantedSpeed.x = _horizontal * _speedMagnitude;

        if(Mathf.Abs(_horizontal) > MIN_MOVE_SPEED || Mathf.Abs(_vertical) > MIN_MOVE_SPEED)
        {
            OrientCharacterToCamera();
        }

        _currentSpeed = Vector3.MoveTowards(_currentSpeed, _wantedSpeed, _acceleration * Time.deltaTime); //MoveTowards dice muovi un float verso quel float là, NON muoviti verso quel punto

        _characterController.SimpleMove(_cameraPivot.TransformDirection(_currentSpeed)); //SimpleMove è un metodo di movimento per il CharacterController

        UpdateAnimator();

        if (characterActions.noMovement)
        {
            _defenseMovement = true;
            _speedMagnitude = 0;
        }

        else if (!characterActions.noMovement && _defenseMovement == true)
        {
            StartCoroutine(DefenseSpeedRegulator());
        }
    }

    IEnumerator DefenseSpeedRegulator()
    {
        //Sezione Céline
        _defenseMovement = false;
        _speedMagnitude = _walkSpeed;

        yield return new WaitForSeconds(0.1f);

        // Blocca la rotazione attuale come rotazione locale
        transform.localRotation = transform.localRotation;
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        Debug.Log($"Input Vector = {inputVector}");

        _horizontal = inputVector.x;
        _vertical = inputVector.y;
    }

    private void OnSprintStart(InputAction.CallbackContext context)
    {
        Debug.Log("Running is working");
        _speedMagnitude = _runSpeed;
    }

    private void OnSprintEnd(InputAction.CallbackContext context)
    {
        _speedMagnitude = _walkSpeed;
    }

    private void OnAttackStart(InputAction.CallbackContext context)
    {
        
    }
    private void OrientCharacterToCamera()
    {
        Vector3 eulerRotation = Vector3.MoveTowards(transform.rotation.eulerAngles, _cameraPivot.rotation.eulerAngles, _rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(eulerRotation);
    }
    private void UpdateAnimator() //prendi le variabili che interessano all'animator e settale all'animator attuale
    {
        _animator.SetFloat("Speed Z", _currentSpeed.z);
        _animator.SetFloat("Speed X", _currentSpeed.x);
    }
}
