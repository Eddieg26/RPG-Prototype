using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New BountyList", menuName = "Game/Bounty/List")]
public class BountyList : ScriptableObject {
    [SerializeField] private List<BountyRef> bountyList;

    public int Count { get { return bountyList.Count; } }

    public BountyRef Get(int index) {
        return bountyList[index];
    }

    public BountyRef Add(Bounty bounty) {
        Debug.Assert(bounty != null, "Attempting to add null bounty to BountyList");
        
        BountyRef foundBounty = bountyList.Find((b) => b.ReferencedBounty == bounty);
        if (foundBounty == null) {
            BountyRef bountyRef = new BountyRef(bounty);
            bountyList.Add(bountyRef);
            return bountyRef;
        }

        return null;
    }

    public bool Remove(Bounty bounty) {
        return bountyList.RemoveAll((b) => b.ReferencedBounty == bounty) > 0;
    }

    public bool Remove(BountyRef bounty) {
        return bountyList.Remove(bounty);
    }

    public void Clear() {
        bountyList.Clear();
    }
}
