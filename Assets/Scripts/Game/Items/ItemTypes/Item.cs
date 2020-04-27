using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }
    public string ItemName;
    [Range(1,999)]
    public int MaximumStack = 1;
    public Sprite Icon;
    
    private void OnValidate()
    {
        id = ItemName;
    }

    public virtual Item GetCopy()
    {
        return this;
    }

    public virtual void Destroy()
    {

    }
}
