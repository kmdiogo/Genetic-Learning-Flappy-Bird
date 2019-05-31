using System.Collections.Generic;
using System;

public class NeuralNetwork
{
    public int inputNodes;
    private float[] hiddenNodes;
    private float[] outputNodes;

    private float[][] ihWeights;
    private float[][] hoWeights;
    public float fitness;



    static public NeuralNetwork CrossOver(NeuralNetwork a, NeuralNetwork b)
    {
        NeuralNetwork child = new NeuralNetwork(a.inputNodes, a.hiddenNodes.Length, a.outputNodes.Length);
        float crossWeight = UnityEngine.Random.Range(-0.9999f, 0.9999f);


        // Child Weights = (crossWeight * [Weights of a]) +  ( (1-crossWeight) * [Weights of b] )
        for (int i = 0; i < child.ihWeights.Length; i++)
        {
            for (int j = 0; j < child.ihWeights[i].Length; j++)
            {
                child.ihWeights[i][j] = (crossWeight * a.ihWeights[i][j]) + ((1 - crossWeight) * b.ihWeights[i][j]);
            }
        }
        for (int i = 0; i < child.hoWeights.Length; i++)
        {
            for (int j = 0; j < child.hoWeights[i].Length; j++)
            {
                child.hoWeights[i][j] = (crossWeight * a.hoWeights[i][j]) + ((1 - crossWeight) * b.hoWeights[i][j]);
            }
        }

        return child;
    }

    public void Mutate()
    {
        //Mutate a neuron in Input to Hidden Weights
        if (UnityEngine.Random.Range(0f, 1f) <= 0.5f)
        {
            int selectedNeuron = UnityEngine.Random.Range((int)0, (int)hiddenNodes.Length);
            int selectedWeight = UnityEngine.Random.Range((int)0, (int)ihWeights[selectedNeuron].Length);
            float selectedWeightValue = ihWeights[selectedNeuron][selectedWeight];
            float percent = 0.1f * selectedWeightValue;
            ihWeights[selectedNeuron][selectedWeight] = UnityEngine.Random.Range(selectedWeightValue - percent, selectedWeightValue + percent);
        }
        else
        {
            int selectedNeuron = UnityEngine.Random.Range((int)0, (int)outputNodes.Length);
            int selectedWeight = UnityEngine.Random.Range((int)0, (int)hoWeights[selectedNeuron].Length);
            float selectedWeightValue = hoWeights[selectedNeuron][selectedWeight];
            float percent = 0.1f * selectedWeightValue;
            hoWeights[selectedNeuron][selectedWeight] = UnityEngine.Random.Range(selectedWeightValue - percent, selectedWeightValue + percent);
        }
    }

    public NeuralNetwork(int numberOfInputNodes, int numberOfHiddenNodes, int numberOfOutputNodes)
    {
        inputNodes = numberOfInputNodes;
        //Initialize hidden and output Neurons
        hiddenNodes = new float[numberOfHiddenNodes];
        outputNodes = new float[numberOfOutputNodes];


        //Initialize weights from input layer to hidden layer
        List<float[]> InputHiddenWeights = new List<float[]>();
        for (int i = 0; i < numberOfHiddenNodes; i++)
        {
            InputHiddenWeights.Add(new float[numberOfInputNodes]);
            for (int j = 0; j < numberOfInputNodes; j++)
            {
                InputHiddenWeights[i][j] = UnityEngine.Random.Range(-0.9999f, 0.9999f);
            }
        }
        ihWeights = InputHiddenWeights.ToArray();



        //Initialize weights from hidden layer to output layer
        List<float[]> HiddenOutputWeights = new List<float[]>();
        for (int i = 0; i < numberOfOutputNodes; i++)
        {
            HiddenOutputWeights.Add(new float[numberOfHiddenNodes]);
            for (int j = 0; j < numberOfHiddenNodes; j++)
            {
                HiddenOutputWeights[i][j] = UnityEngine.Random.Range(-0.9999f, 0.9999f);
            }
        }
        hoWeights = HiddenOutputWeights.ToArray();
    }


    public float[] Query(float[] input)
    {
        //Calculate values going into hidden layer
        for (int i = 0; i < hiddenNodes.Length; i++)
        {
            float feedValue = 0f;
            for (int j = 0; j < inputNodes; j++)
            {
                feedValue += input[j] * ihWeights[i][j];
            }
            hiddenNodes[i] = sigmoid(feedValue);
        }

        //Calculate values going into output layer
        for (int i = 0; i < outputNodes.Length; i++)
        {
            float feedvalue = 0f;
            for (int j = 0; j < hiddenNodes.Length; j++)
            {
                feedvalue += hiddenNodes[j] * hoWeights[i][j];
            }
            outputNodes[i] = sigmoid(feedvalue);
        }


        return outputNodes;


    }

    float sigmoid(float x)
    {
        return (float)(1 / (1 + Math.Pow(Math.E, (x * -1))));
    }
}

