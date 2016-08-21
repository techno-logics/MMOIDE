using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using System;
using System.Runtime.Remoting;

public class Compile : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Creating");

        int tmp_int = 0;
        object tmp = tmp_int;

        Debug.Log(tmp.GetType());

        tmp = "dsfda;";

        Debug.Log(tmp.GetType());
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            bool end = false;

            Debug.Log("Entering loop!");

            Node nextNode = Global.Instance.GetStartNode().Outputs[0].connections[0].body;

            string code = "";

            while (!end)
            {
                Debug.Log(nextNode.name);

                code += "\n" + nextNode.GetCode();

                nextNode = nextNode.Outputs[0].connections[0].body;

                if (nextNode.GetID == "endNode")
                    end = true;
            }

            Global.Instance.CompileCode(code, "Start");
        }
	}
}
