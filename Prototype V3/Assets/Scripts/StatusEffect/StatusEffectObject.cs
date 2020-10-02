using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectObject : MonoBehaviour
{
    public void Destroy() {
        Destroy(this.gameObject);
    }
}
