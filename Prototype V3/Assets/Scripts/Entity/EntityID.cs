using UnityEngine;

[CreateAssetMenu(fileName = "New EntityID", menuName = "Game/Entity/ID")]
public class EntityID : ScriptableObject {
    [SerializeField] private string entityName;
    [SerializeField] private EntityTag tag;

    public string EntityName { get { return entityName; } }
    public EntityTag Tag { get { return tag; } }
}
