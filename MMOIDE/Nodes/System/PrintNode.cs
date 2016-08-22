using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

[System.Serializable]
[Node(false, "Standard/System/Print")]
public class PrintNode : Node
{
    public const string ID = "printNode";
    public override string GetID { get { return ID; } }

    public string value = "";
    public string variableName = "";

    public override Node Create(Vector2 pos)
    { // This function has to be registered in Node_Editor.ContextCallback
        PrintNode node = CreateInstance<PrintNode>();

        node.name = "Print Node";
        node.rect = new Rect(pos.x, pos.y, 200, 100); ;

        NodeInput.Create(node, "Execution", "Execution");
        NodeInput.Create(node, "Value", "String");
        NodeInput.Create(node, "Variable Name", "Variable");

        NodeOutput.Create(node, "Execution", "Execution");

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
            value = RTEditorGUI.TextField(GUIContent.none, value);
        InputKnob(1);

        GUILayout.Label("Variable Name");
        InputKnob(2);

        GUILayout.EndVertical();
        GUILayout.BeginVertical();

        GUILayout.Label("Execution");
        OutputKnob(0);

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        if (GUI.changed)
            NodeEditor.RecalculateFrom(this);
    }

    public override bool Calculate()
    {
        variableName = "";

        if (Inputs[1].connection != null)
            value = Inputs[1].connection.GetValue<string>();

        if (Inputs[2].connection != null)
            variableName = Inputs[2].connection.GetValue<string>();
            

        Outputs[0].SetValue<string>(value);
        return true;
    }

    public override string GetCode()
    {
        //if (Inputs[0].connection.body.GetID == "castNode")
        //{
            //Debug.Log("Connected to CastNode");
          //  return "System.Console.WriteLine(System.Convert.ToString(" + Inputs[0].connection.body.node_params[0] + "));";
        //}
        if (variableName != "")
        {
            Debug.Log(variableName);
            return "System.Console.WriteLine(System.Convert.ToString(" + variableName + "));";
        }
        else
        {
            return "System.Console.WriteLine(\"" + value + "\");";
        }
    }
}