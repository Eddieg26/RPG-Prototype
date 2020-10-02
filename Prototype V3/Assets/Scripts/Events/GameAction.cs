using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Game Action", menuName = "Game/Event/Action")]
public class GameAction : ScriptableObject {
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public int ListenerCount { get { return listeners.Count; } }

    public bool AddListener(GameEventListener listener) {
        if (!listeners.Contains(listener)) {
            listeners.Add(listener);
            return true;
        }

        return false;
    }

    public bool RemoveListener(GameEventListener listener) {
        return listeners.Remove(listener);
    }

    public void Invoke() {
        listeners.ForEach(listener => listener.Invoke());
    }

    public List<GameEventListener> GetListeners() {
        return new List<GameEventListener>(listeners);
    }

    public void Clear() {
        listeners.Clear();
    }
}
