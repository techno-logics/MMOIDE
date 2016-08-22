using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

[System.Serializable]
[Node(false, "Standard/Logic/Condition")]
public class ConditionNode : Node
{
    public const string ID = "conditionNode";
    public override string GetID { get { return ID; } }

    public int input1Val = 0;
    public int input2Val = 0;

    public bool value = false;

    public enum ConditionTypes { GreaterThan };

    public ConditionTypes conditionType = ConditionTypes.GreaterThan;

    public override Node Create(Vector2 pos)
    { // This function has to be registered in Node_Editor.ContextCallback
        ConditionNode node = CreateInstance<ConditionNode>();

        node.name = "Condition Node";
        node.rect = new Rect(pos.x, pos.y, 200, 80);

        NodeOutput.Create(node, "Value", "Bool");

        NodeInput.Create(node, "Input 1", "Integer");
        NodeInput.Create(node, "Input 2", "Integer");

        return node;
    }

    protected internal override void NodeGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();

        GUILayout.Label("Input 1");
        InputKnob(0);

        GUILayout.Label("Input 2");
        InputKnob(1);

        GUILayout.EndVertical();
        GUILayout.BeginVertical();

        
        //OutputKnob(0);
        Outputs[0].DisplayLayout();

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        if (GUI.changed)
            NodeEditor.RecalculateFrom(this);
    }

    public override bool Calculate()
    {
        if (Inputs[0].connection != null)
            input1Val = Inputs[0].connection.GetValue<int>();

        if (Inputs[1].connection != null)
            input2Val = Inputs[1].connection.GetValue<int>();


        switch (conditionType)
        {
            case ConditionTypes.GreaterThan:
                value = input1Val > input2Val;
                break;
        }
        return true;
    }

    public override string GetCode()
    {
        return input1Val.ToString() + " > " + input2Val.ToString();
    }
}