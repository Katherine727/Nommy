using UnityEngine;
using System.Collections;

public class AbilityToStartScript : MonoBehaviour {


    public string scriptName;

    public void StartInterface() {
        var script = GetComponent(scriptName) as Assets.Utils.IStartable;
       
        script.Start();
    }

    public void StopInterface() {
        var script = GetComponent(scriptName) as Assets.Utils.IStopable;

        script.Stop();
    }
}
