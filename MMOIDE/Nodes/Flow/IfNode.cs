using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

[System.Serializable]
[Node(false, "Standard/Flow/If")]
public class IfNode : Node
{
    public const string ID = "ifNode";
    public override string GetID { get { return ID; } }

    public bool condition = false;

    public override Node Create(Vector2 pos)
    {
        IfNode node = CreateInstance<IfNode>();

        node.name = "If Node";
        node.rect = new Rect(pos.x, pos.y, 200, 130);

        node.CreateInput("Execution", "Execution");
        node.CreateInput("Condition", "Bool");

        node.CreateOutput("Execution", "Execution");
        node.CreateOutput("If True", "Execution");
        node.CreateOutput("If False", "Execution");

        return node;
    }

    protected internal override void NodeGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();

        GUILayout.Label("Execution");
        InputKnob(0);

        GUILayout.Label(Inputs[1].name);
        InputKnob(1);
        
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
    
        Outputs[0].SetValue<string>("");
        Outputs[1].SetValue<string>("");
        Outputs[2].SetValue<string>("");

        return true;
    }

    public override string GetCode()
    {

        string code = "";

        if (Inputs[1].connection != null)
        {
            code += "\nif(" + Inputs[1].connection.body.GetCode() + ")\n{";

            code += Global.Instance.GetCode(Global.Instance.GetConnectedNodes(Outputs[1]));

            code += "\n}\n";

            if (Outputs[2].connections.Count > 0)
            {
                code += "else\n{";

                code += Global.Instance.GetCode(Global.Instance.GetConnectedNodes(Outputs[2]));

                code += "\n}";
            }
            
        }


        return code;
    }
}
