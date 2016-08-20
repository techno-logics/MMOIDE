using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

[System.Serializable]
[Node(false, "Standard/Flow/Start")]
public class StartNode : Node
{
    public const string ID = "startNode";
    public override string GetID { get { return ID; } }

    public string value = "";

    public override Node Create(Vector2 pos)
    { // This function has to be registered in Node_Editor.ContextCallback
        StartNode node = CreateInstance<StartNode>();

        node.name = "Start Node";
        node.rect = new Rect(pos.x, pos.y, 200, 50);

        NodeOutput.Create(node, "Value", "Execution");

        Global.Instance.StartNode = node;

        return node;
    }

    protected internal override void NodeGUI()
    {
        GUILayout.Label("Execution");
        OutputKnob(0);

        if (GUI.changed)
            NodeEditor.RecalculateFrom(this);
    }

    public override bool Calculate()
    {
        Outputs[0].SetValue<string>("");
        return true;
    }
}