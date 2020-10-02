using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New SpriteBoard", menuName = "Game/Util/SpriteBoard")]
public class SpriteBoard : ScriptableObject {
    [SerializeField] private List<SpriteBoardInfo> sprites = new List<SpriteBoardInfo>();

    public Sprite GetSprite(SpriteBoardType type) {
        SpriteBoardInfo info = sprites.Find((sprite) => sprite.type == type);
        return info != null ? info.sprite : null;
    }
}

[System.Serializable]
public class SpriteBoardInfo {
    public Sprite sprite;
    public SpriteBoardType type;
}

public enum SpriteBoardType {
    None,
    ICON_ATTACK,
    ICON_DEFENSE
}
