using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

[System.Serializable]
[Node(false, "Standard/Flow/While")]
public class WhileNode : Node
{
    public const string ID = "whileNode";
    public override string GetID { get { return ID; } }

    public bool condition = false;

    public override Node Create(Vector2 pos)
    {
        WhileNode node = CreateInstance<WhileNode>();

        node.name = "While Node";
        node.rect = new Rect(pos.x, pos.y, 200, 76);

        node.CreateInput("Execution", "Execution");
        node.CreateInput("Condition", "Bool");

        node.CreateOutput("Execution", "Execution");
        node.CreateOutput("While True", "Execution");

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
            condition = GUILayout.Toggle(condition, new GUIContent("", "Hardcoded condition value"));
        InputKnob(1);

        GUILayout.EndVertical();
        GUILayout.BeginVertical();

        Outputs[0].DisplayLayout();
        Outputs[1].DisplayLayout();

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        if (GUI.changed)
            NodeEditor.RecalculateFrom(this);
    }

    public override bool Calculate()
    {
        if (Inputs[1].connection != null)
            condition = Inputs[1].connection.GetValue<bool>();

        Outputs[0].SetValue<string>("");
        Outputs[1].SetValue<string>("");

        return true;
    }

    public override string GetCode()
    {
        string code = "";

        if (Inputs[1].connection != null)
        {
            code += "\nwhile (" + Inputs[1].connection.body.GetCode() 
                + ")\n{"
                + Global.Instance.GetCode(Global.Instance.GetConnectedNodes(Outputs[1]))
                + "\n}\n";
        }


        return code;
    }
}
