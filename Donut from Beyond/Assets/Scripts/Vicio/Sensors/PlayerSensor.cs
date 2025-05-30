using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerSensor : MonoBehaviour
{
    public delegate void PlayerEnterEvent(Transform player);
    public delegate void PlayerExitEvent(Vector3 lastKnownPosition);
    
    public event PlayerEnterEvent OnPlayerEnter;
    public event PlayerExitEvent OnPlayerExit;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player_Movement player))
        {
            OnPlayerEnter?.Invoke(player.transform);
        }
    }

    private void OnTriggerExtir(Collider other)
    {
        if(other.TryGetComponent(out Player_Movement player))
        {
            OnPlayerExit?.Invoke(player.transform.position);
        }
    }
}
