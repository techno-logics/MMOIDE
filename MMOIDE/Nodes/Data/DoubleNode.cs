using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System;

[System.Serializable]
[Node(false, "Standard/Data/Double")]
public class DoubleNode : Node
{
    public const string ID = "doubleNode";
    public override string GetID { get { return ID; } }

    public double value = 0;

    public override Node Create(Vector2 pos)
    { // This function has to be registered in Node_Editor.ContextCallback
        DoubleNode node = CreateInstance<DoubleNode>();

        node.name = "Double Node";
        node.rect = new Rect(pos.x, pos.y, 200, 50);

        NodeOutput.Create(node, "Value", "Double");

        return node;
    }

    protected internal override void NodeGUI()
    {
        string tmp = Convert.ToString(value);

        tmp = RTEditorGUI.TextField(new GUIContent("Value", "The input value of type string"), tmp);
        OutputKnob(0);

        value = Convert.ToDouble(tmp);

        if (GUI.changed)
            NodeEditor.RecalculateFrom(this);
    }

    public override bool Calculate()
    {
        Outputs[0].SetValue<double>(value);
        return true;
    }
}