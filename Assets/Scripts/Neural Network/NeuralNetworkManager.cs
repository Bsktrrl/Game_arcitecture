using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class NeuralNetworkManager : MonoBehaviour
{
    NeuralNetwork nn;

    public int input;
    public int hidden;
    public int output;

    void Start()
    {
        Training();
    }

    void Training()
    {
        nn = new NeuralNetwork();
        List<Training_Data> td = new List<Training_Data>();

        #region Setup traiding_Data
        for (int i = 0; i < 4; i++)
        {
            Training_Data temp = new Training_Data();
            td.Add(temp);
        }

        td[0].inputs.Add(1);
        td[0].inputs.Add(0);
        td[0].targets.Add(1);

        td[1].inputs.Add(0);
        td[1].inputs.Add(1);
        td[1].targets.Add(1);

        td[2].inputs.Add(1);
        td[2].inputs.Add(1);
        td[2].targets.Add(0);

        td[3].inputs.Add(0);
        td[3].inputs.Add(0);
        td[3].targets.Add(0);
        #endregion

        //(inputs, hidden_nodes, hidden_layers, outputs)
        nn.Setup(2, 2, 1, 1);

        //1,0 skal gi 1 i output
        //List<float> inputs = new List<float> {1, 0};

        //List<float> target = new List<float> {1};


        // Tren netverket ish 50 000 ganger som dette
        for (int i = 0; i < 50000; i++)
        {
            Training_Data dataTemp = new Training_Data();
            int index = Random.Range(0, 4);

            switch (index)
            {
                case 0:
                    dataTemp = td[0];
                    break;
                case 1:
                    dataTemp = td[1];
                    break;
                case 2:
                    dataTemp = td[2];
                    break;
                case 3:
                    dataTemp = td[3];
                    break;

                default:
                    break;
            }

            nn.BackPropagate(nn.FeedForward(dataTemp.inputs), dataTemp.targets);
        }

        print("Output [1, 0]: \t" + nn.FeedForward(new List<float> {1, 0})[0] + "\t -> 1");
        print("Output [0, 1]: \t" + nn.FeedForward(new List<float> {0, 1})[0] + "\t -> 1");
        print("Output [1, 1]: \t" + nn.FeedForward(new List<float> {1, 1})[0] + "\t -> 0");
        print("Output [0, 0]: \t" + nn.FeedForward(new List<float> {0, 0})[0] + "\t -> 0");
    }
}

public class Training_Data
{
    public List<float> inputs = new List<float>();
    public List<float> targets = new List<float>();
}
