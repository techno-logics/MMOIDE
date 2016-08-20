﻿using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

[System.Serializable]
[Node(false, "Standard/System/Print")]
public class PrintNode : Node
{
    public const string ID = "printNode";
    public override string GetID { get { return ID; } }

    public string value = "";

    public override Node Create(Vector2 pos)
    { // This function has to be registered in Node_Editor.ContextCallback
        PrintNode node = CreateInstance<PrintNode>();

        node.name = "Print Node";
        node.rect = new Rect(pos.x, pos.y, 200, 80); ;

        NodeInput.Create(node, "Execution", "Execution");
        NodeInput.Create(node, "Value", "String");

        NodeOutput.Create(node, "Execution", "Execution");

        return node;
    }

    protected internal override void NodeGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();

        GUILayout.Label("Execution");
        InputKnob(0);

        if (Inputs[1].connection != null)
            GUILayout.Label(Inputs[1].name);
        else
            value = RTEditorGUI.TextField(GUIContent.none, value);
        InputKnob(1);

        GUILayout.EndVertical();
        GUILayout.BeginVertical();

        GUILayout.Label("Execution");
        OutputKnob(0);

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        if (GUI.changed)
            NodeEditor.RecalculateFrom(this);
    }

    public override bool Calculate()
    {
        if (Inputs[1].connection != null)
            value = Inputs[1].connection.GetValue<string>();

        Outputs[0].SetValue<string>(value);
        return true;
    }

    public override string GetCode()
    {
        return "System.Console.WriteLine(\"" + value + "\");";
    }
}