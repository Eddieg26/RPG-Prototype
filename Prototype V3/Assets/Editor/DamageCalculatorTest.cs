using UnityEngine;
using UnityEditor;

public class DamageCalculatorTest : EditorWindow {
    private int attack;
    private int defense;
    private int result;

    [MenuItem("Tools/DamageCalculator")]
    private static void ShowWindow() {
        GetWindow<DamageCalculatorTest>().Show();
    }

    private void OnGUI() {
        attack = EditorGUILayout.IntField("Attack", attack);
        defense = EditorGUILayout.IntField("Defense", defense);

        GUILayout.Space(20f);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Calculate", GUILayout.Width(150f))) {
            result = EntityDamageCalculator.CalculateDamage(attack, defense);
        }

        if (GUILayout.Button("Clear", GUILayout.Width(150f))) {
            result = 0;
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.Space(20f);

        if (result > 0)
            GUILayout.Label("Result: " + result);
    }
}
