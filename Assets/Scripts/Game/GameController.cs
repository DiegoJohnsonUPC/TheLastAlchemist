using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject AlchemyPanel;
    private bool OpenPanel = false;

    void Start()
    {
        AlchemyPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("OpenPanel"))
        {
            OpenPanel = !OpenPanel;
            AlchemyPanel.SetActive(OpenPanel);
        }
    }
}
