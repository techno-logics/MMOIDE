using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

[System.Serializable]
[Node(false, "Standard/Flow/End")]
public class EndNode : Node
{
    public const string ID = "endNode";
    public override string GetID { get { return ID; } }

    public string value = "";

    public override Node Create(Vector2 pos)
    { // This function has to be registered in Node_Editor.ContextCallback
        EndNode node = CreateInstance<EndNode>();

        node.name = "End Node";
        node.rect = new Rect(pos.x, pos.y, 200, 50);

        NodeInput.Create(node, "Value", "Execution");

        return node;
    }

    protected internal override void NodeGUI()
    {
        GUILayout.Label("Execution");
        InputKnob(0);

        if (GUI.changed)
            NodeEditor.RecalculateFrom(this);
    }

    public override bool Calculate()
    {
        //Outputs[0].SetValue<string>("");
        return true;
    }
}