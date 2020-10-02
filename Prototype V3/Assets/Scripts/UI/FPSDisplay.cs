using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FPSDisplay : MonoBehaviour {
    private Text text;
    const float fpsMeasurePeriod = 0.5f;
    private int fpsAccumulator = 0;
    private float fpsNextPeriod = 0;
    private int currentFPS;
    const string display = "{0} FPS";

    private void Start() {
        text = GetComponent<Text>();
        fpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
    }

    private void Update() {
        // measure average frames per second
        fpsAccumulator++;
        if (Time.realtimeSinceStartup > fpsNextPeriod) {
            currentFPS = (int)(fpsAccumulator / fpsMeasurePeriod);
            fpsAccumulator = 0;
            fpsNextPeriod += fpsMeasurePeriod;
            text.text = string.Format(display, currentFPS);
        }
    }
}
