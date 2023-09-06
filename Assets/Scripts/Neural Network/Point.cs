using System;
using UnityEngine;

[Serializable]
public class Point : MonoBehaviour
{
    [SerializeField] float x = 0;
    [SerializeField] float y = 0;
    [SerializeField] float diff = 0;
    [SerializeField] int label;


    //--------------------


    public int GetLabel(Vector2 pos)
    {
        if (pos.x > pos.y)
        {
            label = 1;
        }
        else
        {
            label = -1;
        }

        return label;
    }
    public float GetDiff()
    {
        diff = x - y;

        return diff;
    }
    public float GetPos_X()
    {
        x = UnityEngine.Random.Range(PerceptronManager.instance.posX, PerceptronManager.instance.posX + PerceptronManager.instance.width);

        return x;
    }
    public float GetPos_Y()
    {
        y = UnityEngine.Random.Range(PerceptronManager.instance.posY, PerceptronManager.instance.posY + PerceptronManager.instance.height);

        return y;
    }

    public void SetPos_X(float _x)
    {
        x = _x;
    }
    public void SetPos_Y(float _y)
    {
        y = _y;
    }
}
