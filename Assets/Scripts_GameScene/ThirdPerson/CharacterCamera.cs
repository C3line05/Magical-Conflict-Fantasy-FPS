using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _elevation;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _verticalSpeed;
    [SerializeField] private bool _invertMouse;

    private float _verticalInput;
    private float _horizontalInput;

    private Vector3 GetWantedPosition()
    {
        return _target.position + _offset;
    }

    private void OnValidate() //Funzione che viene chiamata quando modifichiamo un qualsiasi parametro serializzato in editor --> possiamo fare quello che facciamo in Play, da Editor
    {
        transform.position = GetWantedPosition();
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Mouse X");
        _verticalInput = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, _horizontalInput * _horizontalSpeed * Time.deltaTime);
        _elevation.Rotate(Vector3.right, _verticalInput * _verticalSpeed * (_invertMouse ? 1 : -1) * Time.deltaTime); //è invert mouse? Sì o no
    }

    private void LateUpdate()
    {
        transform.position = GetWantedPosition();
    }
}
