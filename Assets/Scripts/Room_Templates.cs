using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Templates : MonoBehaviour
{
   public bool finish = false;
   public GameObject[] voidRooms;
   public GameObject[] bottomRooms;
   public GameObject[] topRooms;
   public GameObject[] leftRooms;
   public GameObject[] rightRooms;
   public GameObject[] SingleBottomRooms;
   public GameObject[] SingleTopRooms;
   public GameObject[] SingleLeftRooms;
   public GameObject[] SingleRightRooms;
   public List<GameObject> rooms;
   public Hashtable positions = new Hashtable();
}
