using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using System.Collections.Generic;
using System;
using System.Text;

[System.Serializable]
[Node(false, "Standard/Flow/Switch")]
public class SwitchNode : Node
{
    public const string ID = "switchNode";
    public override string GetID { get { return ID; } }

    public int switchValue = 0;
    private const int OFFSET = 2, DEF = 1; //Outputs[0] = "Execution" ; Outputs[1] = "default"
    public List<int> occupiedIndices = new List<int>();
    private int newestIndex = 0;

    public override Node Create(Vector2 pos)
    {
        SwitchNode node = CreateInstance<SwitchNode>();

        node.name = "Switch Node";
        node.rect = new Rect(pos.x, pos.y, 200, 170);

        node.CreateInput("Execution", "Execution");
        node.CreateInput("Input", "Integer"); //TODO: STRING SWITCH STATEMENT

        node.CreateOutput("Execution", "Execution");
        node.CreateOutput("Default", "Execution");

        return node;
    }

    protected internal override void NodeGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();

        GUILayout.Label("Execution");
        InputKnob(0); //DisplayLayout TODO
        
        GUILayout.Label(Inputs[1].name);
        InputKnob(1);

        newestIndex = Convert.ToInt32(RTEditorGUI.FloatField(new GUIContent("Case", "An integer case to add to (or remove from) the outputs"), newestIndex));
        if (GUILayout.Button("Add Case") && !used(newestIndex))
        {
            CreateOutput("Case " + newestIndex, "Execution");
            occupiedIndices.Add(newestIndex);
            if(occupiedIndices.Count > 3) rect.height += 18; //TODO: CHECK INCREASE VALUE IS PERFECT
        }

        if (GUILayout.Button("Remove Case") && used(newestIndex))
        {
            for (int i = OFFSET; i < Outputs.Count; i++)
            {
                if (Outputs[i].name == "Case " + newestIndex)
                {
                    RemoveKnob(i, i + Inputs.Count, false);
                    if (occupiedIndices.Count > 3) rect.height -= 18;
                    occupiedIndices.Remove(newestIndex);
                    break;
                }
            }
        }

        GUILayout.EndVertical();
        GUILayout.BeginVertical();

        for (int i = 0; i < Outputs.Count; i++)
        {
            Outputs[i].DisplayLayout();
        }

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        if (GUI.changed)
            NodeEditor.RecalculateFrom(this);
    }

    public override bool Calculate()
    {
        if (Inputs[1].connection != null)
            switchValue = Inputs[1].connection.GetValue<int>();

        Outputs[0].SetValue<string>("");
        Outputs[1].SetValue<string>("");

        return true;
    }

    public override string GetCode()
    {
        StringBuilder code = new StringBuilder();

        if (Inputs[1].connection != null)
        {
            code.Append("\nswitch (" + Inputs[1].connection.GetValue<int>() + ")\n{\n\t");
            
            for(int i = 0; i < occupiedIndices.Count; i++)
            {
                if (Outputs[i + OFFSET].connections.Count == 0) continue; //Nothing connected to case statement
                code.Append("case " + occupiedIndices[i] + ":\n");
                code.Append(Global.Instance.GetCode(Global.Instance.GetConnectedNodes(Outputs[i + OFFSET])));
                code.Append("\n\tbreak;\n\n\t");
            }

            code.Append("default: " + Global.Instance.GetCode(Global.Instance.GetConnectedNodes(Outputs[DEF])) + "\nbreak;");

            code.Append("\n}\n");
        }
        
        return code.ToString();
    }

    private bool used(int index)
    {
        foreach (int i in occupiedIndices)
            if (i == index)
                return true;
        return false;
    }
}
