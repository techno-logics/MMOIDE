using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

[System.Serializable]
[Node(false, "Standard/System/Pause")]
public class PauseNode : Node
{
    public const string ID = "pauseNode";
    public override string GetID { get { return ID; } }

    public string value = "";

    public override Node Create(Vector2 pos)
    { // This function has to be registered in Node_Editor.ContextCallback
        PauseNode node = CreateInstance<PauseNode>();

        node.name = "Pause Node";
        node.rect = new Rect(pos.x, pos.y, 200, 50); ;

        NodeInput.Create(node, "Execution", "Execution");

        NodeOutput.Create(node, "Execution", "Execution");

        return node;
    }

    protected internal override void NodeGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();

        GUILayout.Label("Execution");
        InputKnob(0);

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
        if (Inputs[0].connection != null)
            value = Inputs[0].connection.GetValue<string>();

        Outputs[0].SetValue<string>(value);
        return true;
    }

    public override string GetCode()
    {
        return "System.Console.ReadLine();";
    }
}