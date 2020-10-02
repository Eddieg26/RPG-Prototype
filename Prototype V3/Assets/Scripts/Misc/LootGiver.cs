using UnityEngine;
using System.Collections.Generic;

public class LootGiver : MonoBehaviour {
    [SerializeField] Transform lootSpawnPosition;
    [SerializeField] private ItemDrop itemDropPrefab;
    [SerializeField] private List<ItemRef> loot;

    public void DropLoot() {
        if(itemDropPrefab == null || loot.Count == 0)
            return;

        loot.ForEach(item => CreateItemDrop(item));

        loot.Clear();
    }

    private void CreateItemDrop(ItemRef item) {
        Vector3 position = lootSpawnPosition.position + Random.insideUnitSphere;
        position.y = lootSpawnPosition.position.y;
        GameObject itemDropObject = Instantiate<GameObject>(itemDropPrefab.gameObject, position, Quaternion.identity);
        ItemDrop itemDrop = itemDropObject.GetComponent<ItemDrop>();
        itemDrop.SetItem(item);

        ForceRigidbody forceRigidbody = itemDropObject.GetComponent<ForceRigidbody>();
        if(forceRigidbody) {
            Vector3 forward = position - transform.position;
            forceRigidbody.ApplyForce(forward.normalized);
        }
    }
}
