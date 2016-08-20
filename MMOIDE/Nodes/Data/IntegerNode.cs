using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System;

[System.Serializable]
[Node(false, "Standard/Data/Integer")]
public class IntegerNode : Node
{
    public const string ID = "integerNode";
    public override string GetID { get { return ID; } }

    public int value = 0;

    public override Node Create(Vector2 pos)
    { // This function has to be registered in Node_Editor.ContextCallback
        IntegerNode node = CreateInstance<IntegerNode>();

        node.name = "Integer Node";
        node.rect = new Rect(pos.x, pos.y, 200, 50); ;

        NodeOutput.Create(node, "Value", "Integer");

        return node;
    }

    protected internal override void NodeGUI()
    {
        value = Convert.ToInt32(RTEditorGUI.FloatField(new GUIContent("Value", "The input value of type integer"), value));
        OutputKnob(0);

        if (GUI.changed)
            NodeEditor.RecalculateFrom(this);
    }

    public override bool Calculate()
    {
        Outputs[0].SetValue<int>(value);
        return true;
    }
}