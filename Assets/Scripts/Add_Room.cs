using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add_Room : MonoBehaviour
{
    private Room_Templates templates;
    void Start()
    {

        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<Room_Templates>();
        templates.rooms.Add(this.gameObject);
        if(templates.rooms.Count == 10){
            templates.finish = true;  
        }
        
        
    }

}

