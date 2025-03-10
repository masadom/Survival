using UnityEngine;

public class ChestController : Interactible
{
    int numberOfItems;
    public Item[] items;
    public override void Interact()
    {
        base.Interact();
        Open();
    }
    void Open()
    {
        Debug.Log("cos");

        int ItemsListNumber = items.Length;
        Debug.Log(ItemsListNumber);



        numberOfItems = Random.Range(1, 3);
        for (int i = 0; i < numberOfItems; i++)
        {
            Inventory.Instance.Add(items[Random.Range(0, numberOfItems)]);

        }
    }

}
