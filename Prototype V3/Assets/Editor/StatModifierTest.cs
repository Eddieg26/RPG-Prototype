using UnityEngine;
using UnityEditor;

public class StatModifierTest : EditorWindow {
    private StatModifier modifierA;
    private StatModifier modifierB;
    private StatModifier modifierC;
    private StatModifier nullModD;
    private StatModifier nullModE;

    [MenuItem("Tools/StatModifierTest")]
    private static void ShowWindow() {
        StatModifierTest window = GetWindow<StatModifierTest>();
        window.Start();
        window.ShowUtility();
    }

    private void Start() {
        modifierA = new StatModifier(StatType.BaseAttack, 25);
        modifierB = modifierA;
        modifierC = new StatModifier(StatType.BaseAttack, 25);

        nullModD = null;
        nullModE = null;
    }

    private void OnGUI() {
        DrawStatModifier("Modifier A", ref modifierA);
        GUILayout.Space(10f);

        DrawStatModifier("Modifier B", ref modifierB);
        GUILayout.Space(10f);

        DrawStatModifier("Modifier C", ref modifierC);
        GUILayout.Space(20f);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Test Equality")) {
            TestEquality();
        }

        if (GUILayout.Button("Test InEquality")) {
            TestInEquality();
        }

        GUILayout.EndHorizontal();
    }

    private void DrawStatModifier(string title, ref StatModifier modifier) {
        GUILayout.Label(title, GUILayout.Width(150f), GUILayout.Height(EditorGUIUtility.singleLineHeight + 5));
        GUILayout.BeginHorizontal();
        GUILayout.Label("StatType", GUILayout.Width(75f));
        modifier.Type = (StatType)EditorGUILayout.EnumPopup(modifier.Type);
        modifier.Value = EditorGUILayout.IntField(modifier.Value, GUILayout.Width(50f));
        GUILayout.EndHorizontal();
    }

    private void TestEquality() {
        Debug.Log("ModifierA Equals ModifierB: " + modifierA.Equals(modifierB));
        Debug.Log("ModifierA == ModifierB: " + (modifierA == modifierB).ToString());
        Debug.Log("ModifierA Equals ModifierC: " + modifierA.Equals(modifierC));
        Debug.Log("ModifierA == ModifierC: " + (modifierA == modifierC).ToString());
        Debug.Log("ModifierA Equals NullModD: " + modifierA.Equals(nullModD));
        Debug.Log("ModifierA == NullModD: " + (modifierA == nullModD).ToString());
        Debug.Log("NullModD == NullModE: " + (nullModD == nullModE).ToString());
    }

    private void TestInEquality() {
        Debug.Log("ModifierA != ModifierB: " + (modifierA != modifierB).ToString());
        Debug.Log("ModifierA != ModifierC: " + (modifierA != modifierC).ToString());
        Debug.Log("ModifierA != NullModD: " + (modifierA != nullModD).ToString());
        Debug.Log("NullModD != NullModE: " + (nullModD != nullModE).ToString());
    }
}
