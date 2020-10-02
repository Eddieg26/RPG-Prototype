using UnityEngine;

[System.Serializable]
public class BountyRef {
    [SerializeField] private Bounty referencedBounty;
    [SerializeField] private int currentProgress;

    public Bounty ReferencedBounty { get { return referencedBounty; } }
    public int CurrentProgress {
        get { return currentProgress; }
        set { currentProgress = value; }
    }

    public BountyRef(Bounty referencedBounty) {
        this.referencedBounty = referencedBounty;
    }

    public bool IsComplete() {
        return referencedBounty != null ? currentProgress >= referencedBounty.BountyCap : false;
    }
}
