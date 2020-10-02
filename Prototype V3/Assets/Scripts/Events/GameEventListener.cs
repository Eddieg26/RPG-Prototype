using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public interface IGameEventListener {}

public class GameEventListener<T> : IGameEventListener {
    private UnityAction<T> action;

    public GameEventListener(UnityAction<T> action) {
        this.action = action;
    }

    public void Invoke(T data) {
        action.Invoke(data);
    }
}

public class GameEventListener : IGameEventListener {
    private UnityAction action;

    public GameEventListener(UnityAction action) {
        this.action = action;
    }

    public void Invoke() {
        action.Invoke();
    }
}
