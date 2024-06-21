using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public delegate void EnteredRangeHandler();
    public static EnteredRangeHandler OnEnteredRange;

    public delegate void ExitedRangeHandler();
    public static ExitedRangeHandler OnExitedRange;

    [SerializeField] Transform localPlayer;
    [SerializeField] float rotationSpeed;

    void Update()
    {
        // Make the NPC face our local player.
        Vector2 dir = localPlayer.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") == false)
        {
            return;
        }

        OnEnteredRange?.Invoke();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") == false)
        {
            return;
        }

        OnExitedRange?.Invoke();
    }
}
