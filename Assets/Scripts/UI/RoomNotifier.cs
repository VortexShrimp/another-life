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
    public delegate void RoomEnteredHandler(Room newRoom);
    public static RoomEnteredHandler OnRoomEntered;

    public delegate void RoomStayHandler(Room currentRoom);
    public static RoomStayHandler OnRoomStay;

    public delegate void RoomExitHandler(Room oldRoom);
    public static RoomExitHandler OnRoomExit;

    [SerializeField] Room room;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") == false)
        {
            return;
        }

        OnRoomEntered?.Invoke(room);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") == false)
        {
            return;
        }

        OnRoomStay?.Invoke(room);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") == false)
        {
            return;
        }

        OnRoomExit?.Invoke(room);
    }
}
