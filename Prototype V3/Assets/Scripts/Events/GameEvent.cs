using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Game Event", menuName = "Game/Event/Event")]
public class GameEvent : ScriptableObject {
    private List<IGameEventListener> listeners = new List<IGameEventListener>();

    public int ListenerCount { get { return listeners.Count; } }

    public bool AddListener<T>(GameEventListener<T> listener) {
        if (listeners.Count > 0) {
            GameEventListener<T> firstListener = (GameEventListener<T>)listeners[0];
            if (firstListener == null) {
                Debug.Log($"Could not add a listener to GameEvent: {name}");
                return false;
            }
        }

        if (!listeners.Contains(listener)) {
            listeners.Add(listener);
            return true;
        }

        return false;
    }

    public bool RemoveListener(IGameEventListener listener) {
        return listeners.Remove(listener);
    }

    public void Invoke<T>(T data) {
        listeners.ForEach(baseListener => {
            GameEventListener<T> listener = (GameEventListener<T>)baseListener;
            if (listener != null)
                listener.Invoke(data);
            else
                Debug.Log($"Could not invoke a listener in GameEvent: {name}");
        });
    }

    public List<IGameEventListener> GetListeners() {
        return new List<IGameEventListener>(listeners);
    }

    public void Clear() {
        listeners.Clear();
    }
}