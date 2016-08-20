using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

[System.Serializable]
[Node(false, "Standard/Data/String")]
public class StringNode : Node
{
    public const string ID = "stringNode";
    public override string GetID { get { return ID; } }

    public string value = "";

    public override Node Create(Vector2 pos)
    { // This function has to be registered in Node_Editor.ContextCallback
        StringNode node = CreateInstance<StringNode>();

        node.name = "String Node";
        node.rect = new Rect(pos.x, pos.y, 200, 50); ;

        NodeOutput.Create(node, "Value", "String");

        return node;
    }

    protected internal override void NodeGUI()
    {
        value = RTEditorGUI.TextField(new GUIContent("Value", "The input value of type string"), value);
        OutputKnob(0);

        if (GUI.changed)
            NodeEditor.RecalculateFrom(this);
    }

    public override bool Calculate()
    {
        Outputs[0].SetValue<string>(value);
        return true;
    }
}