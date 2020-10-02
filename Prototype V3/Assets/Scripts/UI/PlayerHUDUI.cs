using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDUI : MonoBehaviour {
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private Image expBar;
    [SerializeField] private Text healthText;
    [SerializeField] private Text manaText;
    [SerializeField] private RectTransform statusEffectViewHolder;

    private Player player;
    private StatusEffectView[] statusEffectViews;

    private void Start() {
        player = FindObjectOfType<Player>();
        player.UpdateExp += UpdateExpBar;

        Entity entity = player.GetComponent<Entity>();
        entity.UpdateHealth += UpdateHealthBar;
        entity.UpdateMana += UpdateManaBar;
        entity.UpdateStatusEffects += UpdateStatusEffects;

        statusEffectViews = statusEffectViewHolder.GetComponentsInChildren<StatusEffectView>();

        UpdateHealthBar(entity.Stats.Health, entity.Stats.GetMaxHealth());
        UpdateManaBar(entity.Stats.Mana, entity.Stats.GetMaxMana());
        UpdateExpBar(player.CurrentExp, player.MaxExp);
    }

    private void UpdateHealthBar(int health, int maxHealth) {
        healthBar.fillAmount = (health / (float)maxHealth);
        healthText.text = $"HP: {health}";
    }

    private void UpdateManaBar(int mana, int maxMana) {
        manaBar.fillAmount = (mana / (float)maxMana);
        manaText.text = $"HP: {mana}";
    }

    private void UpdateExpBar(int exp, int maxExp) {
        expBar.fillAmount = (exp / (float)Mathf.Max(maxExp, 1));
    }

    private void UpdateStatusEffects(StatusEffectRefList statusEffectList) {
        int maxIndex = Mathf.Min(statusEffectList.Count, statusEffectViews.Length);
        int index = 0;

        for (; index < maxIndex; ++index)
            statusEffectViews[index].UpdateView(statusEffectList.GetStatusEffect(index));

        for (; index < statusEffectViews.Length; ++index)
            statusEffectViews[index].Clear();
    }
}
