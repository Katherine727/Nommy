using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilityToStartScriptByTag : MonoBehaviour {

    public string scriptName;
    public List<string> tags;
    public void StartInterface(string tag) {
        if (tags.Count > 0 && tags.Contains(tag)) {
            var script = GetComponent(scriptName) as Assets.Utils.ISwitchable;

            script.SwitchOn();
        }
    }

    public void StopInterface(string tag) {
        if (tags.Count > 0 && tags.Contains(tag)) {
            var script = GetComponent(scriptName) as Assets.Utils.ISwitchable;

            script.SwitchOff();
        }
    }
}
