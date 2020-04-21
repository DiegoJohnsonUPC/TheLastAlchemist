using UnityEngine;

public class ItemChest : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Inventory inventory;


    private bool isInRange;

    private void OnValidate()
    {
        if(inventory == null)
            inventory = FindObjectOfType<Inventory>();
    }

    private void Update()
    {
        if (isInRange)
        {
            Item itemCopy = item.GetCopy();
            inventory.AddItem(itemCopy);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckCollision(other.gameObject, true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CheckCollision(other.gameObject, false);
    }

    private void CheckCollision(GameObject gameObject, bool state)
    {
        if (gameObject.CompareTag("Player"))
            isInRange = state;
    }
}
