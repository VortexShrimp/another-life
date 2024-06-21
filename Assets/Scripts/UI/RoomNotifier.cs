using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Room
{
    Bathroom,
    GuestBathroom,
    Bedroom,
    Kitchen,
    Pateo,
    LivingRoom,
    DiningRoom,
    Hallway
}

public class RoomNotifier : MonoBehaviour
{
    public delegate void NewRoomEnteredHandler(Room newRoom);
    public static NewRoomEnteredHandler OnNewRoomEntered;

    [SerializeField] Room room;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") == false)
        {
            return;
        }

        OnNewRoomEntered?.Invoke(room);
    }
}
