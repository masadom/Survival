using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "Inventory")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public int amount = 1;
    public int stackSize = 1;

    public virtual void Use()
    {
        Debug.Log("Using " +name);
    }
}

   
