﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Spawner : MonoBehaviour
{
    public int openingDirection;
    //1 --> D
    //2 --> T
    //3 --> L
    //4 --> R
    private Room_Templates templates;
    private int rand;
    private bool spawned = false;
    private GameObject grid;
    void Start(){
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<Room_Templates>();
        grid = GameObject.FindGameObjectWithTag("Grid");
        Invoke("Spawn",0.1f);
    }

    void Spawn(){
        if(spawned == false){
            if(openingDirection == 1){
                //1 --> D
                rand = Random.Range(0,templates.bottomRooms.Length);
                var newObj = GameObject.Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                newObj.transform.parent = GameObject.Find("Grid").transform;
            } else if(openingDirection == 2){
                //2 --> T
                rand = Random.Range(0,templates.topRooms.Length);
                var newObj = GameObject.Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                newObj.transform.parent = GameObject.Find("Grid").transform;
            } else if(openingDirection == 3){
                //3 --> L
                rand = Random.Range(0,templates.leftRooms.Length);
                var newObj = GameObject.Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                newObj.transform.parent = GameObject.Find("Grid").transform;
            } else if(openingDirection == 4){
                //4 --> R
                rand = Random.Range(0,templates.rightRooms.Length);
                var newObj = GameObject.Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                newObj.transform.parent = GameObject.Find("Grid").transform;
            }
            spawned = true;
        }
        
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("SpawnPoint") && spawned == false){
            Destroy(gameObject);
        }
    }

    public void LastRoom(){
        /*
        if(spawned == false){
            if(openingDirection == 1){
                //1 --> D
                rand = Random.Range(0,templates.SingleBottomRooms.Length);
                var newObj = GameObject.Instantiate(templates.SingleBottomRooms[rand], transform.position, templates.SingleBottomRooms[rand].transform.rotation);
                newObj.transform.parent = GameObject.Find("Grid").transform;
            } else if(openingDirection == 2){
                //2 --> T
                rand = Random.Range(0,templates.SingleTopRooms.Length);
                var newObj = GameObject.Instantiate(templates.SingleTopRooms[rand], transform.position, templates.SingleTopRooms[rand].transform.rotation);
                newObj.transform.parent = GameObject.Find("Grid").transform;
            } else if(openingDirection == 3){
                //3 --> L
                rand = Random.Range(0,templates.SingleLeftRooms.Length);
                var newObj = GameObject.Instantiate(templates.SingleLeftRooms[rand], transform.position, templates.SingleLeftRooms[rand].transform.rotation);
                newObj.transform.parent = GameObject.Find("Grid").transform;
            } else if(openingDirection == 4){
                //4 --> R
                rand = Random.Range(0,templates.SingleRightRooms.Length);
                var newObj = GameObject.Instantiate(templates.SingleRightRooms[rand], transform.position, templates.SingleRightRooms[rand].transform.rotation);
                newObj.transform.parent = GameObject.Find("Grid").transform;
            }
            spawned = true;
        }
        */
        Debug.Log("d");
    }
}
