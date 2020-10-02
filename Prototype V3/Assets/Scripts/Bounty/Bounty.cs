using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Bounty", menuName = "Game/Bounty/Bounty")]
public class Bounty : ScriptableObject {
    [SerializeField] private string bountyName;
    [SerializeField, TextArea] private string info;
    [SerializeField] private EntityID targetEntity;
    [SerializeField] private int bountyCap;
    [SerializeField] private List<ItemRef> rewardItems;

    public string Name { get { return bountyName; } }
    public string Info { get { return info; } }
    public EntityID TargetEntity { get { return targetEntity; } }
    public int BountyCap { get { return bountyCap; } }
    public List<ItemRef> RewardItems { get { return rewardItems; } }
}
