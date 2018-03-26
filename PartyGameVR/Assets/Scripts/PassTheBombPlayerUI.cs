using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassTheBombPlayerUI : MonoBehaviour {

    [SerializeField] Text Playername;
    [SerializeField] Text TotalPoints;
    [SerializeField] Text CurrentPoints;

    private void Start() {
        ShowUI(false);
    }

    public void ShowUI(bool _show) {
        Playername.gameObject.SetActive(_show);
        TotalPoints.gameObject.SetActive(_show);
        CurrentPoints.gameObject.SetActive(_show);
        GetComponent<Image>().enabled = _show;
    }

    public void SetPlayername(string _name) {
        Playername.text = _name;
    }

    public void SetTotalPoints(float _points) {
        int points = (int)_points;
        TotalPoints.text = points.ToString();
        CurrentPoints.text = "0";
    }

    public void SetCurrentPoints(float _points) {
        int points = (int)_points;
        CurrentPoints.text = points.ToString();
    }

}
