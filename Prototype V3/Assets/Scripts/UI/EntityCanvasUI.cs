using UnityEngine;
using UnityEngine.UI;

public class EntityCanvasUI : MonoBehaviour {
    [SerializeField] private Entity entity;
    [SerializeField] private Text nameText;
    [SerializeField] private Image healthBarBG;
    [SerializeField] private Image healthBarFG;

    private void Start() {
        entity.SetFocus += SetFocus;
        entity.UpdateHealth += UpdateHealthBar;
        entity.Die += OnEntityDeath;

        nameText.text = entity.ID.EntityName;

        SetFocus(false);
    }

    private void SetFocus(bool isFocused) {
        healthBarBG.gameObject.SetActive(isFocused);
        healthBarFG.gameObject.SetActive(isFocused);
        nameText.gameObject.SetActive(isFocused);
    }

    private void UpdateHealthBar(int currentHealth, int maxHealth) {
        healthBarFG.fillAmount = currentHealth / (float)maxHealth;
    }

    private void OnEntityDeath(Entity entity) {
        SetFocus(false);
    }
}
