using UnityEngine;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

[System.Serializable]
[Node(false, "Standard/System/Cast")]
public class CastNode : Node
{
    public const string ID = "castNode";
    public override string GetID { get { return ID; } }

    public string[] inputTypes = new string[] {"Integer", "Double", "Float", "String", "Bool"};

    public int inputID = 0;
    public int outputID = 0;

    //I know -> naming convention! Bla Bla ....
    public int tmp_int = 0;
    public int tmp_int_2 = 0;

    public object outputValue = "1";
    public object inputValue = "1";

    public string variableName = "tmp";

    public override Node Create(Vector2 pos)
    { // This function has to be registered in Node_Editor.ContextCallback
        CastNode node = CreateInstance<CastNode>();

        node.name = "Cast Node";
        node.rect = new Rect(pos.x, pos.y, 200, 200);

        NodeInput.Create(node, "Execution", "Execution");
        NodeOutput.Create(node, "Execution", "Execution");
        NodeInput.Create(node, "Variable Name", "String");

        NodeInput.Create(node, "ValueInt", "Integer");
        NodeOutput.Create(node, "ValueBool", "Integer");

        node.node_params.Add(variableName);

        return node;
    }

    protected internal override void NodeGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();

        //GUILayout.Label("Value");

        GUILayout.Label("Execution");
        InputKnob(0);

        if (Inputs[1].connection != null)
            GUILayout.Label(Inputs[1].name);
        else
            variableName = RTEditorGUI.TextField(GUIContent.none, variableName);
        InputKnob(1);

        GUILayout.Label("Value");
        InputKnob(2);

        tmp_int = inputID;

        inputID = GUILayout.SelectionGrid(inputID, inputTypes, 1);

        GUILayout.EndVertical();
        GUILayout.BeginVertical();

        GUILayout.Label("Execution");
        OutputKnob(0);

        GUILayout.Label("Value");
        OutputKnob(1);

        tmp_int_2 = outputID;

        outputID = GUILayout.SelectionGrid(outputID, inputTypes, 1);

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        if (GUI.changed)
            NodeEditor.RecalculateFrom(this);

        if (tmp_int != inputID || tmp_int_2 != outputID)
        {
            RemoveKnob(2, 3, true);
            RemoveKnob(1, 3, false);

            inputValue = "";
            

            //Node tmp_node = CreateInstance<CastNode>();
            //RemoveKnob(tmp_node, 0, 0, true);
            //RemoveKnob(tmp_node, 0, 0, false);

            if (inputID == 0)
            {
                CreateInput("Value", "Integer");
            }
            else if (inputID == 1)
            {
                CreateInput("Value", "Double");
                InputKnob(1);
                //tmp_node.CreateInput("Value", "Double");

                //Debug.Log("Num inputs: " + tmp_node.Inputs.Count.ToString());
                //Debug.Log("Num Knobs: " + tmp_node.nodeKnobs.Count.ToString());

                //Inputs[1] = tmp_node.Inputs[0];
                //nodeKnobs[2] = tmp_node.nodeKnobs[0];

                //RemoveKnob(tmp_node, 0, 0, true);
                //DestroyImmediate(tmp_node);
            }
            else if (inputID == 2)
            {
                CreateInput("Value", "Float");
            }
            else if (inputID == 3)
            {
                CreateInput("Value", "String");
            }
            else if (inputID == 4)
            {
                CreateInput("Value", "Bool");
            }

            if (outputID == 0)
            {
                CreateOutput("Value", "Integer");
            }
            else if (outputID == 1)
            {
                CreateOutput("Value", "Double");
            }
            else if (outputID == 2)
            {
                CreateOutput("Value", "Float");
            }
            else if (outputID == 3)
            {
                CreateOutput("Value", "String");
            }
            else if (outputID == 4)
            {
                CreateOutput("Value", "Bool");
            }
        }
    }

    public override bool Calculate()
    {
        if (Inputs[1].connection != null)
            variableName = Inputs[1].connection.GetValue<string>();

        if (node_params.Count == 0)
            node_params.Add(variableName);

        node_params[0] = variableName;

        if (inputID == 0)
        {
            if (Inputs[2].connection != null)
                inputValue = Inputs[2].connection.GetValue<int>();
        }
        else if (inputID == 1)
        {
            if (Inputs[2].connection != null)
                inputValue = Inputs[2].connection.GetValue<double>();
        }
        else if (inputID == 2)
        {
            if (Inputs[2].connection != null)
                inputValue = Inputs[2].connection.GetValue<float>();
        }
        else if (inputID == 3)
        {
            if (Inputs[2].connection != null)
                inputValue = Inputs[2].connection.GetValue<string>();
        }
        else if (inputID == 4)
        {
            if (Inputs[2].connection != null)
                inputValue = Inputs[2].connection.GetValue<bool>();
        }

        if (outputID == 0)
        {
            if (inputValue != "")
            {
                Outputs[1].SetValue<int>((int)System.Convert.ToInt16(inputValue));
            }
        }
        else if (outputID == 1)
        {
            if (inputValue != "")
            {
                Outputs[1].SetValue<double>((double)System.Convert.ToDouble(inputValue));
            }
        }
        else if (outputID == 2)
        {
            if (inputValue != "")
            {
                Outputs[1].SetValue<float>((float)inputValue);
            }
        }
        else if (outputID == 3)
        {
            Outputs[1].SetValue<string>(System.Convert.ToString(inputValue));
        }
        else if (outputID == 4)
        {
            if (inputValue != "")
            {
                Outputs[1].SetValue<bool>((bool)System.Convert.ToBoolean(inputValue));
            }
        }
        //Outputs[0].SetValue<bool>((bool)outputValue);
        return true;
    }

    public override string GetCode()
    {
        string tmp_str = variableName;
        if (tmp_str == "")
            tmp_str = "tmp";

        if (outputID == 0)
        {
            return "int " + variableName + " = " + System.Convert.ToString(Outputs[1].GetValue<int>()) + ";";
        }
        else if (outputID == 1)
        {
            return "double " + variableName + " = " + System.Convert.ToString(Outputs[1].GetValue<double>()) + ";";
        }
        else if (outputID == 2)
        {
            return "float " + variableName + " = " + System.Convert.ToString(Outputs[1].GetValue<float>()) + ";";
        }
        else if (outputID == 3)
        {
            return "string " + variableName + " = \"" + System.Convert.ToString(Outputs[1].GetValue<string>()) + "\";";
        }
        else if (outputID == 4)
        {
            return "bool " + variableName + " = " + System.Convert.ToString(Outputs[1].GetValue<bool>()) + ";";
        }

        return "";
    }
}
