using UnityEngine;
using UnityEngine.UI;
using UIElements;
using System.Collections.Generic;

public class BountyDialogUI : MonoBehaviour {
    [SerializeField] private GameObject view;
    [SerializeField] private Text titleText;
    [SerializeField] private Text bountyInfoText;
    [SerializeField] private RewardItemView[] rewardViews;
    [SerializeField] private GameEvent startBountyEvent;
    [SerializeField] private GameEvent completeBountyEvent;
    [SerializeField] private GameEvent togglePauseEvent;
    [SerializeField] private float duration = 5f;

    private bool isShowing;
    private float timer;
    private GameEventListener<BountyRef> startBountyListener;
    private GameEventListener<BountyRef> completeBountyListener;
    private GameEventListener<bool> togglePauseListener;

    private void Awake() {
        startBountyListener = new GameEventListener<BountyRef>(OnStartBounty);
        completeBountyListener = new GameEventListener<BountyRef>(OnCompleteBounty);
        togglePauseListener = new GameEventListener<bool>(OnTogglePause);

        startBountyEvent.AddListener(startBountyListener);
        completeBountyEvent.AddListener(completeBountyListener);
        togglePauseEvent.AddListener(togglePauseListener);
    }

    private void Update() {
        if(isShowing && Time.time > timer + duration)
            Hide();
    }

    private void Show() {
        view.SetActive(true);
        isShowing = true;
        timer = Time.time;
    }

    private void Hide() {
        view.SetActive(false);
        isShowing = false;
    }

    private void OnStartBounty(BountyRef bounty) {
        titleText.text = $"Bounty Started: {bounty.ReferencedBounty.Name}";
        bountyInfoText.text = bounty.ReferencedBounty.Info;
        SetRewardViews(bounty.ReferencedBounty.RewardItems);
        Show();
    }

    private void OnCompleteBounty(BountyRef bounty) {
        titleText.text = $"Bounty Complete: {bounty.ReferencedBounty.Name}";
        bountyInfoText.text = bounty.ReferencedBounty.Info;
        SetRewardViews(bounty.ReferencedBounty.RewardItems);
        Show();
    }

    private void OnTogglePause(bool isPaused) {
        Hide();
    }

    private void SetRewardViews(List<ItemRef> rewards) {
        ClearRewardViews();

        int maxIndex = Mathf.Min(rewardViews.Length, rewards.Count);
        for (int index = 0; index < maxIndex; ++index)
            rewardViews[index].SetView(rewards[index]);
    }

    private void ClearRewardViews() {
        foreach (var view in rewardViews)
            view.Clear();
    }

    private void OnDestroy() {
        startBountyEvent.RemoveListener(startBountyListener);
        completeBountyEvent.RemoveListener(completeBountyListener);
        togglePauseEvent.RemoveListener(togglePauseListener);
    }
}
