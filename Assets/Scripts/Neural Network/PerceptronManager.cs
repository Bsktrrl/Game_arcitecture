using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerceptronManager : MonoBehaviour
{
    public static PerceptronManager instance { get; set; }

    Perceptron p;
    public int perceptronAmount = 100;
    //[SerializeField] Point[] points = new Point[100];

    [Header("UI")]
    [SerializeField] Button trainingButton;

    [SerializeField] Image displayImage;
    [SerializeField] GameObject perceptron;
    public List<GameObject> perceptronList = new List<GameObject>();

    [HideInInspector] public float posX = 0;
    [HideInInspector] public float posY = 0;
    [HideInInspector] public float width = 0;
    [HideInInspector] public float height = 0;


    //--------------------


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        //Get Pos, Width and Height of image
        posX = displayImage.GetComponent<RectTransform>().position.x - displayImage.GetComponent<RectTransform>().rect.width / 2;
        posY = displayImage.GetComponent<RectTransform>().position.y - displayImage.GetComponent<RectTransform>().rect.height / 2;
        width = displayImage.GetComponent<RectTransform>().rect.width;
        height = displayImage.GetComponent<RectTransform>().rect.height;

        //Initialize Points
        for (int i = 0; i < perceptronAmount; i++)
        {
            perceptronList.Add(Instantiate(perceptron) as GameObject);
            perceptronList[perceptronList.Count - 1].transform.SetParent(displayImage.gameObject.transform);

            //Set Perceptron position
            perceptronList[perceptronList.Count - 1].transform.position = new Vector2(perceptronList[perceptronList.Count - 1].GetComponent<Point>().GetPos_X(), perceptronList[perceptronList.Count - 1].GetComponent<Point>().GetPos_Y());

            //Get Positions
            perceptronList[perceptronList.Count - 1].GetComponent<Point>().SetPos_X(perceptronList[perceptronList.Count - 1].transform.localPosition.x);
            perceptronList[perceptronList.Count - 1].GetComponent<Point>().SetPos_Y(perceptronList[perceptronList.Count - 1].transform.localPosition.y);

            //Set Perceptron color
            if (perceptronList[perceptronList.Count - 1].GetComponent<Point>().GetDiff() > 0)
            {
                perceptronList[perceptronList.Count - 1].GetComponent<Point>().SetOuterPoint(Color.black);
            }
            else
            {
                perceptronList[perceptronList.Count - 1].GetComponent<Point>().SetOuterPoint(Color.white);
            }
        }

        for (int i = 0; i < perceptronList.Count; i++)
        {
            Point point = perceptronList[i].GetComponent<Point>();
            Perceptron perceptron = perceptronList[i].GetComponent<Perceptron>();

            float[] input = { point.GetPos_X(), point.GetPos_Y()};
            int target = point.GetLabel();

            int guess = perceptron.Guess(input);
            if (guess == target)
            {
                point.SetInnerPoint(Color.green);
            }
            else
            {
                point.SetInnerPoint(Color.red);
            }
        }
    }


    //--------------------


    public void TrainingButton()
    {
        for (int i = 0; i < perceptronList.Count; i++)
        {
            Point point = perceptronList[i].GetComponent<Point>();
            Perceptron perceptron = perceptronList[i].GetComponent<Perceptron>();

            float[] input = { point.GetPos_X(), point.GetPos_Y() };
            int target = point.GetLabel();

            //Training
            perceptron.training(input, target);

            int guess = perceptron.Guess(input);
            if (guess == target)
            {
                point.SetInnerPoint(Color.green);
            }
            else
            {
                point.SetInnerPoint(Color.red);
            }
        }
    }
}
