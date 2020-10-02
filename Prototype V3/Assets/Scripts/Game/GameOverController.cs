using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {
    [SerializeField] private Image gameOverScreen;
    [SerializeField] private GameEvent playerDeathEvent;
    [SerializeField] private GameEvent changeBGMusicEvent;
    [SerializeField] private float startFadeDuration;

    private GameEventListener<EntityKillData> playerDeathListener;
    private float startFadeTimer;
    private bool isFading;

    private void Awake() {
        playerDeathListener = new GameEventListener<EntityKillData>(OnPlayerDeath);
        playerDeathEvent.AddListener(playerDeathListener);

        gameOverScreen.color = new Color(1f, 1f, 1f, 0f);
        gameOverScreen.fillAmount = 0f;
    }

    private void Update() {
        if(isFading && Time.time > startFadeTimer + startFadeDuration) {
            Color color = gameOverScreen.color;
            color.a += Time.deltaTime * 0.2f;
            gameOverScreen.color = color;
            gameOverScreen.fillAmount += Time.deltaTime * 0.2f;

            if(IsFloatEqual(gameOverScreen.color.a, 1f, 0.001f)) {
                isFading = false;
                color.a = 1f;
                gameOverScreen.color = color;
                gameOverScreen.fillAmount = 1f;
            }
        }
    }

    private void OnPlayerDeath(EntityKillData playerKillData) {
        isFading = true;
        startFadeTimer = Time.time;
        changeBGMusicEvent.Invoke(MusicType.GameOver);
    }

    private void OnDestroy() {
        playerDeathEvent.RemoveListener(playerDeathListener);
    }

    private static bool IsFloatEqual(float valueA, float valueB, float threshold) {
        return Mathf.Abs(valueA - valueB) < threshold;
    }
}
