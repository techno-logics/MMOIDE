using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

[System.Serializable]
[Node(false, "Standard/Calculation/Add")]
public class Add : Node
{
    public enum CalcType { Add, Substract, Multiply, Divide }
    public CalcType type = CalcType.Add;

    public const string ID = "addNode";
    public override string GetID { get { return ID; } }

    public float Input1Val = 1f;
    public float Input2Val = 1f;
    public string VariableName = "";
    public bool VariableExits = false;

    public override Node Create(Vector2 pos)
    {
        Add node = CreateInstance<Add>();

        node.name = "Add Node";
        node.rect = new Rect(pos.x, pos.y, 200, 130);

        node.CreateInput("Execution", "Execution");
        node.CreateInput("Input 1", "Float");
        node.CreateInput("Input 2", "Float");
        node.CreateInput("Variable", "String");

        node.CreateOutput("Execution", "Execution");
        node.CreateOutput("Output 1", "Float");

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
            Input1Val = RTEditorGUI.FloatField(GUIContent.none, Input1Val);
        InputKnob(1);
        // --
        if (Inputs[2].connection != null)
            GUILayout.Label(Inputs[2].name);
        else
            Input2Val = RTEditorGUI.FloatField(GUIContent.none, Input2Val);
        InputKnob(2);

        if (Inputs[3].connection != null)
            GUILayout.Label(Inputs[3].name);
        else
            VariableName = RTEditorGUI.TextField(GUIContent.none, VariableName);
        InputKnob(3);

        GUILayout.EndVertical();
        GUILayout.BeginVertical();

        Outputs[0].DisplayLayout();
        Outputs[1].DisplayLayout();

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        /*
#if UNITY_EDITOR
        type = (CalcType)UnityEditor.EditorGUILayout.EnumPopup(new GUIContent("Calculation Type", "The type of calculation performed on Input 1 and Input 2"), type);
#else
		GUILayout.Label (new GUIContent ("Calculation Type: " + type.ToString (), "The type of calculation performed on Input 1 and Input 2"));
//#endif
         * */

        if (GUI.changed)
            NodeEditor.RecalculateFrom(this);
    }

    public override bool Calculate()
    {
        if (Inputs[1].connection != null)
            Input1Val = Inputs[1].connection.GetValue<float>();
        if (Inputs[2].connection != null)
            Input2Val = Inputs[2].connection.GetValue<float>();
        if (Inputs[3].connection != null)
            VariableName = Inputs[3].connection.GetValue<string>();

        Outputs[0].SetValue<string>("");
        Outputs[1].SetValue<float>(Input1Val + Input2Val);

        return true;
    }

    public override string GetCode()
    {
        if (Input1Val == null)
            Input1Val = 0.0f;
        if (Input2Val == null)
            Input2Val = 0.0f;


        string code = "";

        if (!VariableExits)
        {

            code += "float " + VariableName + " = " + Input1Val.ToString() + " + " + Input2Val.ToString() + ";";
        }


        return code;
    }
}
