using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassTheBombPlayerUI : MonoBehaviour {

    [SerializeField] Text Playername;
    [SerializeField] Text TotalPoints;
    [SerializeField] Text CurrentPoints;
    Image Background;

    private void Awake() {
        Background = GetComponent<Image>();
        ShowUI(false);

    }

    public void ShowUI(bool _show) {
        Playername.gameObject.SetActive(_show);
        TotalPoints.gameObject.SetActive(_show);
        CurrentPoints.gameObject.SetActive(_show);
        GetComponent<Image>().enabled = _show;
    }

    public void SetUI(string _name, Color _color) {
		Playername.text = _name;
		Background.color = _color;
	}

	public void SetTotalPoints(float _currentPoints, float _points) {
        int points = (int)_points;
        TotalPoints.text = points.ToString();
		StartCoroutine (SetTotalPointsEnum (points - (int)_currentPoints, points));
    }

	IEnumerator SetTotalPointsEnum(int _old, int _new) {
		float tempPoints = _old;
		float tempCurrent = _new - _old;
		while(tempPoints < _new) {
			tempPoints += Time.deltaTime * (_new - _old) / 2f;
			tempCurrent -= Time.deltaTime * (_new - _old) / 2f;
			CurrentPoints.text = ((int)tempCurrent).ToString();
			TotalPoints.text = ((int)tempPoints).ToString ();
			yield return null;
		}
	}

    public void SetCurrentPoints(float _points) {
        int points = (int)_points;
        CurrentPoints.text = points.ToString();
    }

}
