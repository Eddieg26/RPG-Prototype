using UnityEngine;

public static class EntityDamageCalculator {

    public static int CalculateDamage(int attack, int defense) {
        if (defense == 0)
            return attack;

        float c = attack < defense ? defense / (float)Mathf.Max(1, attack) : attack / (float)Mathf.Max(1, defense);

        float b = c < 1f ? 1f - c / 2f : c / (Mathf.Pow(c, 2) * 2);
        return Mathf.Clamp(Mathf.CeilToInt(attack * b), 1, attack);
    }
}
