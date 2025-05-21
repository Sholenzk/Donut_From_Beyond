using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float hoverHeight = 1.5f;

    private IPlayerMovement _movement; // Referencia al movimiento
    private InputReader _inputReader; // Lee losinputs del jugador

    private void Awake()
    {
        _inputReader = new InputReader(); //crea el lector de inputs
        _movement = new PlayerMovement(transform, moveSpeed, rotateSpeed, hoverHeight); 
    }

    private void Update()
    {
        // Mueve y rotaa al jugador segun inputs cada frame owo
        _movement.Move(_inputReader.MovementInput);
        _movement.Look(_inputReader.LookInput);
    }
}
