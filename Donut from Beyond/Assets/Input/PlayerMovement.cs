using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : IPlayerMovement
{
    private readonly Transform _playerTransform;
    private readonly float _speed;
    private readonly float _rotationSpeed;
    private readonly float _hoverHeight; //altura constante pa que siempre flote el monitor

    public PlayerMovement(Transform playerTransform, float speed, float rotationSpeed, float hoverHeight)
    {
        _playerTransform = playerTransform;
        _speed = speed;
        _rotationSpeed = rotationSpeed;
        _hoverHeight = hoverHeight;
    }

    public void Move(Vector2 direction)
    {
        //Convierte el input 2D a direccion en 3D
        Vector3 moveDir = new Vector3(direction.x, 0, direction.y);
        moveDir = _playerTransform.TransformDirection(moveDir);

        //calcula la nueva posicion
        Vector3 newPosition = _playerTransform.position + moveDir * _speed * Time.deltaTime;
        newPosition.y = _hoverHeight; // mantener altura fija

        _playerTransform.position = newPosition;
    }

    public void Look(Vector2 lookDirection)
    {
        if (lookDirection.sqrMagnitude < 0.01f) return; // No hace nada si el input es casi cero

        Vector3 dir = new Vector3(lookDirection.x, 0, lookDirection.y); // Convertir input a direccion en el mundo
        Quaternion targetRotation = Quaternion.LookRotation(dir); // Crear la rotacion hacia esa direccion

        // aplica rotacion
        _playerTransform.rotation = Quaternion.Slerp(_playerTransform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        // la funcion slerp suaviza el paso entre 2 rotaciones, funciona mejor que lerp ya que a veces no es preciso con rotaciones
        // y respeta el tiempo de time.deltaTime para que sea progresiva em cada frame OwO
    }
}

