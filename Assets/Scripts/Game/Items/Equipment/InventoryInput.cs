﻿using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    
    [SerializeField] GameObject characterPanelGameObject;
    [SerializeField] GameObject craftingWindowGameObject;
    [SerializeField] GameObject equipmentPanelGameObject;
    [SerializeField] KeyCode[] toggleCharacterPanelKeys;
    [SerializeField] KeyCode[] toggleInventoryKeys;
    void Update()
    {
        for (int i = 0; i < toggleCharacterPanelKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleCharacterPanelKeys[i]))
            {
                characterPanelGameObject.SetActive(!characterPanelGameObject.activeSelf);
                craftingWindowGameObject.SetActive(!craftingWindowGameObject.activeSelf);

                if (characterPanelGameObject.activeSelf)
                {
                    equipmentPanelGameObject.SetActive(true);
                    ShowMouseCursor();
                }
                else
                    HideMouseCursor();
                break;
            }
        }

        for (int i = 0; i < toggleInventoryKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleInventoryKeys[i]))
            {
                if (!characterPanelGameObject.activeSelf)
                {
                    characterPanelGameObject.SetActive(true);
                    equipmentPanelGameObject.SetActive(false);
                    ShowMouseCursor();
                }
                else if (equipmentPanelGameObject.activeSelf)
                {
                    equipmentPanelGameObject.SetActive(false);
                }
                else
                {
                    characterPanelGameObject.SetActive(false);
                    HideMouseCursor();
                }
                break;
            }
        }
    }


    private void Start()
    {
        characterPanelGameObject.SetActive(false);
        craftingWindowGameObject.SetActive(false);
    }
    public void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ToggleEquipmentPanel()
    {
        equipmentPanelGameObject.SetActive(!equipmentPanelGameObject.activeSelf);
    }
}
