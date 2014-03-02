using UnityEngine;
using System.Collections;

public class AbilityToStartScript : MonoBehaviour {


    public string scriptName;

    public void StartInterface() {
        var script = GetComponent(scriptName) as Assets.Utils.ISwitchable;
       
        script.SwitchOn();
    }

    public void StopInterface() {
        var script = GetComponent(scriptName) as Assets.Utils.ISwitchable;

        script.SwitchOff();
    }
}
