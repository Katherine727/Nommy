using UnityEngine;
using System.Collections;

public class AbilityToStartScript : MonoBehaviour {


    public string scriptNameToStart;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartScript() {
        var script = GetComponent(scriptNameToStart) as Assets.Utils.IStartable;
       
        script.Start();
    }
}
