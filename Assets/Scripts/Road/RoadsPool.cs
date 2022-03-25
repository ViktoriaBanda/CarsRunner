using System.Collections.Generic;
using UnityEngine;

public class RoadsPool : MonoBehaviour
{
    public List<GameObject> NotUsedItems { get; set; }
    public List<GameObject> UsedItems { get; set; }

    [SerializeField]
    private GameObject _itemPrefab;

    private void Awake()
    {
        NotUsedItems = new List<GameObject>();
        UsedItems = new List<GameObject>();

        for (var index = 0; index < 4; index++)
        {
            var newItem = Instantiate(_itemPrefab);
            newItem.SetActive(false);
            NotUsedItems.Add(newItem);
        }
    }

    public GameObject TakeFromPool() 
    {
        if (NotUsedItems.Count == 0)
        {
            AddPoolElements();
        }

        var itemToSpawn = NotUsedItems[NotUsedItems.Count - 1];
        NotUsedItems.RemoveAt(NotUsedItems.Count - 1);
        
        UsedItems.Add(itemToSpawn);

        itemToSpawn.SetActive(true);
        return itemToSpawn;
    }
    
    public void Release(GameObject item, Vector3 itemsPosition)
    {
         item.SetActive(false);
         item.transform.position = itemsPosition;
         UsedItems.Remove(item);
         NotUsedItems.Add(item);
    }
    
    public void ReleaseAll(Vector3 itemsPosition)
    {
        for (int i = 0; i < UsedItems.Count; i++)
        {
            UsedItems[i].gameObject.SetActive(false);
            UsedItems[i].transform.position = itemsPosition;
        }

        NotUsedItems.AddRange(UsedItems);
        UsedItems.Clear();
    }

    private void AddPoolElements()
    {
        var newItem = Instantiate(_itemPrefab);
        newItem.SetActive(false);
        NotUsedItems.Add(newItem);
    }
}
