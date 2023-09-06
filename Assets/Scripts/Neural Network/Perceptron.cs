using Unity.VisualScripting;
using UnityEngine;

public class Perceptron : MonoBehaviour
{
    float[] weights = new float[2];
    float learningRate = 0.1f;

    //Constructor
    void Start()
    {
        //initialize the weights randomly
        for (int i = 0; i < weights.Length; i++)
        {
            float rand = Random.Range(-1, 1);

            if (rand < 0)
            {
                weights[i] = -1;
            }
            else
            {
                weights[i] = 1;
            }
        }
    }

    public int Guess(float[] inputs)
    {
        float sum = 0;

        for (int i = 0; i < weights.Length; i++)
        {
            sum += inputs[i] * weights[i];
        }

        int output = Sign(sum);

        return output;
    }

    //The activation function
    int Sign(float n)
    {
        if (n >= 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    public void training(float[] inputs, int target)
    {
        int guess = Guess(inputs);
        int error = target - guess;

        //Tune all the weights
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] += error * inputs[i] * learningRate;
        }
    }
}
