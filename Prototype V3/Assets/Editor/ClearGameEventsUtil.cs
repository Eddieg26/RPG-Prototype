using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ClearGameEventsUtil : MonoBehaviour {

    private class GameEventLeakData {
        public string name;
        public int leakAmount;

        public GameEventLeakData(string name, int leakAmount) {
            this.name = name;
            this.leakAmount = leakAmount;
        }
    }

    [MenuItem("Tools/Clear Game Events")]
    private static void ClearGameEvents() {
        List<GameEventLeakData> leakData = new List<GameEventLeakData>();

        string[] gameEventGUIDs = AssetDatabase.FindAssets("t:GameEvent");
        foreach (string guid in gameEventGUIDs) {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameEvent gameEvent = AssetDatabase.LoadAssetAtPath<GameEvent>(path);
            if (gameEvent && gameEvent.ListenerCount > 0) {
                leakData.Add(new GameEventLeakData(gameEvent.name, gameEvent.ListenerCount));
                gameEvent.Clear();
            }
        }

        string[] gameActionGUIDs = AssetDatabase.FindAssets("t:GameAction");
        foreach (string guid in gameActionGUIDs) {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameAction gameAction = AssetDatabase.LoadAssetAtPath<GameAction>(path);
            if (gameAction && gameAction.ListenerCount > 0) {
                leakData.Add(new GameEventLeakData(gameAction.name, gameAction.ListenerCount));
                gameAction.Clear();
            }
        }

        string[] audioSettingsGUIDs = AssetDatabase.FindAssets("t:AudioSettings");
        foreach (string guid in audioSettingsGUIDs) {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            AudioSettings settings = AssetDatabase.LoadAssetAtPath<AudioSettings>(path);
            if (settings != null) {
                int leakAmount = GetAudioSettingsLeakAmount(settings);
                if (leakAmount > 0)
                    leakData.Add(new GameEventLeakData(settings.name, leakAmount));
                settings.MusicVolumeChanged = null;
                settings.SfxVolumeChanged = null;
                settings.ToggleMute = null;
            }
        }

        if (leakData.Count > 0) {
            leakData.ForEach((data) => {
                Debug.Log($"{data.name}: {data.leakAmount}");
            });
        } else
            Debug.Log("No Leaks detected");
    }

    private static int GetAudioSettingsLeakAmount(AudioSettings settings) {
        int leakAmount = settings.MusicVolumeChanged != null ? settings.MusicVolumeChanged.GetInvocationList().Length : 0;
        leakAmount += settings.SfxVolumeChanged != null ? settings.SfxVolumeChanged.GetInvocationList().Length : 0;
        leakAmount += settings.ToggleMute != null ? settings.ToggleMute.GetInvocationList().Length : 0;

        return leakAmount;
    }
}
