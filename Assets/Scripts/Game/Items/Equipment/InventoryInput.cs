using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    
    [SerializeField] GameObject characterPanelGameObject;
    [SerializeField] GameObject craftingUIGameObject;
    [SerializeField] GameObject equipmentPanelGameObject;
    [SerializeField] KeyCode[] toggleCharacterPanelKeys;
    [SerializeField] KeyCode[] toggleInventoryKeys;
    [SerializeField] KeyCode[] toggleCraftingKeys;
    void Update()
    {
        for (int i = 0; i < toggleCharacterPanelKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleCharacterPanelKeys[i]))
            {
                characterPanelGameObject.SetActive(!characterPanelGameObject.activeSelf);

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
        for (int i = 0; i < toggleCraftingKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleCraftingKeys[i]))
            {
                characterPanelGameObject.SetActive(!craftingUIGameObject.activeSelf);
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
        craftingUIGameObject.SetActive(false);
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
