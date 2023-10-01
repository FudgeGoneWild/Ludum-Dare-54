using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Points_Controller : MonoBehaviour
{
    [SerializeField] TMP_Text pointsText;
    [SerializeField] public int points;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdatePoints(int currpoints)
    {
        points += currpoints;
        pointsText.SetText(points.ToString());
        pointsText.gameObject.GetComponent<Animation>().Play();
    }

    // Update is called once per frame
  
}
