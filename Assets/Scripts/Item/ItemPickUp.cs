using UnityEngine;

public class ItemPickUp : Interactible
{
    public Item item;
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("picking" + item.name);
        if (Inventory.Instance.Add(item))
        {
            Destroy(gameObject);

        }

    }
}
