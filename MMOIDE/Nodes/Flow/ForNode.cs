using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

[System.Serializable]
[Node(false, "Standard/Flow/For")]
public class ForNode : Node
{
    public const string ID = "forNode";
    public override string GetID { get { return ID; } }

    public int index = 0;
    public bool condition = false;
    public int increment = 1;

    public override Node Create(Vector2 pos)
    {
        ForNode node = CreateInstance<ForNode>();

        node.name = "For Node";
        node.rect = new Rect(pos.x, pos.y, 200, 150);

        node.CreateInput("Execution", "Execution");
        node.CreateInput("Condition", "Bool");
        node.CreateInput("Start Value", "Integer");
        node.CreateInput("Increment", "Integer");

        node.CreateOutput("Execution", "Execution");
        node.CreateOutput("Loop", "Execution");
        node.CreateOutput("Index", "Integer");

        return node;
    }

    protected internal override void NodeGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();

        GUILayout.Label("Execution");
        InputKnob(0);

        if (Inputs[1].connection != null) //Condition
            GUILayout.Label(Inputs[1].name);
        else
            condition = GUILayout.Toggle(condition, new GUIContent("Condition to Test", "Hardcoded condition value")); //TODO: MAKE TEXT APPEAR TO THE LEFT OF THE GRAPHIC.
        InputKnob(1);

        if (Inputs[2].connection != null) //Start value
            GUILayout.Label(Inputs[2].name);
        else
            index = System.Convert.ToInt32(RTEditorGUI.FloatField(new GUIContent("Index", "Hardcoded condition value"), index));
        InputKnob(2);

        if (Inputs[3].connection != null) //Start value
            GUILayout.Label(Inputs[3].name);
        else
            increment = System.Convert.ToInt32(RTEditorGUI.FloatField(new GUIContent("Increment", "Hardcoded condition value"), increment));
        InputKnob(3);

        GUILayout.EndVertical();
        GUILayout.BeginVertical();

        Outputs[0].DisplayLayout();
        Outputs[1].DisplayLayout();
        Outputs[2].DisplayLayout();

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        if (GUI.changed)
            NodeEditor.RecalculateFrom(this);
    }

    public override bool Calculate()
    {
        if (Inputs[1].connection != null)
            condition = Inputs[1].connection.GetValue<bool>();
        return true;
    }

    public override string GetCode()
    {
        string code = "";

        if (Inputs[1].connection != null)
        {
            code += "\nfor(int i = " + Inputs[2].connection.GetValue<int>() + "; " + Inputs[2].connection.body.GetCode() + "; i += " + Inputs[3].connection.GetValue<int>() + ")\n{"
                + Global.Instance.GetCode(Global.Instance.GetConnectedNodes(Outputs[1]))
                + "\n}\n";
        }


        return code;
    }
}
