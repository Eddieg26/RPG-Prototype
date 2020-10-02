using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {
    [SerializeField, Range(StatConstants.MIN_LEVEL, StatConstants.MAX_LEVEL)] private int level;
    [SerializeField] private int currentExp;
    [SerializeField] private int maxExp;
    [SerializeField] private LevelUpEffect levelUpEffect;

    public int Level {get { return level; } }
    public int CurrentExp { get { return currentExp; } }
    public int MaxExp { get { return maxExp; } }
    public UnityAction<int, int> UpdateExp { get; set; }

    public void GainExp(int exp) {
        currentExp = level < StatConstants.MAX_LEVEL ? currentExp + exp : Mathf.Min(currentExp + exp, maxExp);

        if (currentExp >= maxExp && level < StatConstants.MAX_LEVEL)
            LevelUp();

        OnUpdateExp();
    }

    private void LevelUp() {
        maxExp = Mathf.Max(1, maxExp);

        while (currentExp >= maxExp) {
            maxExp *= 2;
            ++level;
        }

        levelUpEffect.Activate();
    }

    private void OnUpdateExp() {
        if (UpdateExp != null)
            UpdateExp(currentExp, maxExp);
    }
}
