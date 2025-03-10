using UnityEngine;

public class ChestController : Interactible
{
    int numberOfItems;
    public Item[] items;
    Item randomitem;


    public override void Interact()
    {
        base.Interact();
        Open();
    }
    void Open()
    {
        gameObject.SetActive(false);

        int ItemsListNumber = items.Length;
        Debug.Log(ItemsListNumber);



        numberOfItems = Random.Range(1, 4);
        for (int i = 0; i < numberOfItems; i++)
        {
            randomitem = items[Random.Range(0, ItemsListNumber)];
            Inventory.Instance.Add(randomitem);
            Debug.Log("You get: "+randomitem.name);

        }

        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.SetActive(true);
    }


    

}
