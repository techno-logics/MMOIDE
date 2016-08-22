using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

[System.Serializable]
[Node(false, "Standard/Data/Variable")]
public class VariableNode : Node
{
    public const string ID = "variableNode";
    public override string GetID { get { return ID; } }

    public string variableName = "";

    public override Node Create(Vector2 pos)
    { // This function has to be registered in Node_Editor.ContextCallback
        VariableNode node = CreateInstance<VariableNode>();

        node.name = "Variable Node";
        node.rect = new Rect(pos.x, pos.y, 200, 50);

        NodeOutput.Create(node, "Value", "Variable"); ;

        return node;
    }

    protected internal override void NodeGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();

        GUILayout.Label("Value");

        GUILayout.EndVertical();
        GUILayout.BeginVertical();

        variableName = RTEditorGUI.TextField(new GUIContent("Variable Name"), variableName);
        OutputKnob(0);
        //Outputs[0].DisplayLayout();

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        if (GUI.changed)
            NodeEditor.RecalculateFrom(this);
    }

    public override bool Calculate()
    {
        if (node_params.Count == 0)
            node_params.Add(variableName);

        node_params[0] = variableName;

        Outputs[0].SetValue<string>(variableName);
        return true;
    }

    public override string GetCode()
    {
        return base.GetCode();
    }
}