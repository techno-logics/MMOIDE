using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

[System.Serializable]
[Node(false, "Standard/Data/Bool")]
public class BoolNode : Node
{
    public const string ID = "boolNode";
    public override string GetID { get { return ID; } }

    public bool value = false;

    public override Node Create(Vector2 pos)
    { // This function has to be registered in Node_Editor.ContextCallback
        BoolNode node = CreateInstance<BoolNode>();

        node.name = "Bool Node";
        node.rect = new Rect(pos.x, pos.y, 200, 50);

        NodeOutput.Create(node, "Value", "Bool"); ;

        return node;
    }

    protected internal override void NodeGUI()
    {
        GUILayout.Label("Value");
        value = GUILayout.Toggle(value, new GUIContent("Value", "The input value of type bool")); 
        OutputKnob(0);

        if (GUI.changed)
            NodeEditor.RecalculateFrom(this);
    }

    public override bool Calculate()
    {
        Outputs[0].SetValue<bool>(value);
        return true;
    }
}