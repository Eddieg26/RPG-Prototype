using UnityEngine;
using System.Collections.Generic;

public class PlayerBountyController : MonoBehaviour {
    [SerializeField] private BountyList bountyList;
    [SerializeField] private GameEvent startBountyEvent;
    [SerializeField] private GameEvent completeBountyEvent;
    [SerializeField] private GameEvent entityKillEvent;

    private PlayerInventory inventory;
    private GameEventListener<EntityKillData> entityKillListener;

    private void Awake() {
        inventory = GetComponent<PlayerInventory>();

        entityKillListener = new GameEventListener<EntityKillData>(OnKillEntity);
        entityKillEvent.AddListener(entityKillListener);
    }

    public void AddBounty(Bounty bounty) {
        BountyRef bountyRef = bountyList.Add(bounty);
        if (bountyRef != null)
            startBountyEvent.Invoke(bountyRef);
    }

    private void OnKillEntity(EntityKillData killdata) {
        List<BountyRef> completedBounties = new List<BountyRef>();

        for (int index = 0; index < bountyList.Count; ++index) {
            BountyRef bounty = bountyList.Get(index);
            if (bounty.ReferencedBounty.TargetEntity == killdata.TargetEntity) {
                bounty.CurrentProgress++;
                if (bounty.IsComplete())
                    completedBounties.Add(bounty);
            }
        }

        ProcessCompletedBounties(completedBounties);
    }

    private void ProcessCompletedBounties(List<BountyRef> completedBounties) {
        completedBounties.ForEach((bounty) => {
            completeBountyEvent.Invoke(bounty);
            GiveRewards(bounty.ReferencedBounty.RewardItems);
            bountyList.Remove(bounty);
        });
    }

    private void GiveRewards(List<ItemRef> rewards) {
        rewards.ForEach((item) => inventory.AddItem(item));
    }

    private void OnDestroy() {
        entityKillEvent.RemoveListener(entityKillListener);
        bountyList.Clear();
    }
}
