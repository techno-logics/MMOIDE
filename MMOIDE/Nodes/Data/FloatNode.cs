using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

[System.Serializable]
[Node(false, "Standard/Data/Float")]
public class FloatNode : Node
{
    public const string ID = "floatNode";
    public override string GetID { get { return ID; } }

    public float value = 1f;

    public override Node Create(Vector2 pos)
    { // This function has to be registered in Node_Editor.ContextCallback
        FloatNode node = CreateInstance<FloatNode>();

        node.name = "Float Node";
        node.rect = new Rect(pos.x, pos.y, 200, 50); ;

        NodeOutput.Create(node, "Value", "Float");
        //NodeOutput.Create(node, "Value", "Execution");

        return node;
    }

    protected internal override void NodeGUI()
    {
        value = RTEditorGUI.FloatField(new GUIContent("Value", "The input value of type float"), value);
        OutputKnob(0);

        if (GUI.changed)
            NodeEditor.RecalculateFrom(this);
    }

    public override bool Calculate()
    {
        Outputs[0].SetValue<float>(value);
        return true;
    }
}