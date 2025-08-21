using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Shop Configuration")]
    [SerializeField] private List<GameObject> _itemPrefabs;
    [SerializeField] private List<ShopStand> _shopStands;

    private void Start()
    {
        AssignItemsToStands();
    }

    private void AssignItemsToStands()
    {
        List<GameObject> availablePrefabs = new List<GameObject>(_itemPrefabs);

        foreach (ShopStand stand in _shopStands)
        {
            if (availablePrefabs.Count == 0) break;

            int randomIndex = Random.Range(0, availablePrefabs.Count);
            GameObject randomPrefab = availablePrefabs[randomIndex];

            //�������� ScriptableObject ����� ItemHolder
            ItemHolder itemHolder = randomPrefab.GetComponent<ItemHolder>();
            if (itemHolder != null)
            {
                Item item = itemHolder.ItemData; //�������� ������ Item
                stand.AssignItem(item, randomPrefab);

                //���������� ������ ������ �� ������
                SpriteRenderer spriteRenderer = stand.DisplayPoint.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = item.ItemSprite; //������������� ������ ��������
                }
                else
                {
                    Debug.LogError($"No SpriteRenderer found on display point of stand {stand.name}");
                }
            }
            else
            {
                Debug.LogError($"ItemHolder not found on prefab {randomPrefab.name}");
            }

            availablePrefabs.RemoveAt(randomIndex);
        }
    }
}