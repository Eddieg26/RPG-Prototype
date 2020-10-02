using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerStatusViewUI : MonoBehaviour, IViewUI {
    [SerializeField] private GameObject view;
    [SerializeField] private Text levelLabel;
    [SerializeField] private Text healthLabel;
    [SerializeField] private Text manaLabel;
    [SerializeField] private Text expLabel;
    [SerializeField] private Text attackLabel;
    [SerializeField] private Text defenseLabel;
    [SerializeField] private Text dexterityLabel;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private Image expBar;
    [SerializeField] private GameEvent registerViewEvent;

    private Player player;
    private Entity playerEntity;
    private InputController inputController;

    private void Start() {
        registerViewEvent.Invoke<RegisterViewData>(new RegisterViewData(this, UIConstants.PLAYER_STATUS_VIEW_INDEX));
        inputController = GetComponent<InputController>();
    }

    public void Open() {
        player = FindObjectOfType<Player>();
        if(player)
            playerEntity = player.GetComponent<Entity>();

        if(player && playerEntity)
            UpdateView();

        inputController.SetAsFocusedController();

        view.SetActive(true);
    }

    public void Close() {
        view.SetActive(false);
    }

    public string GetTitle() {
        return "Status";
    }

    private void UpdateView() {
        levelLabel.text = $"LV.{player.Level}";
        healthLabel.text = $"HP.{playerEntity.Stats.Health}";
        manaLabel.text = $"HP.{playerEntity.Stats.Mana}";
        expLabel.text = $"Exp.{player.CurrentExp}/{player.MaxExp}";
        attackLabel.text = $"Atk.{playerEntity.Stats.GetAttack()}";
        defenseLabel.text = $"Def.{playerEntity.Stats.GetDefense()}";
        dexterityLabel.text = $"Dex.{playerEntity.Stats.GetDexterity()}";

        healthBar.fillAmount = playerEntity.Stats.Health / (float)playerEntity.Stats.GetMaxHealth();
        manaBar.fillAmount = playerEntity.Stats.Mana / (float)playerEntity.Stats.GetMaxMana();
        expBar.fillAmount = player.CurrentExp / (float)player.MaxExp;
    }

}
