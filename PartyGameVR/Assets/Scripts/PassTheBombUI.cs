using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassTheBombUI : MonoBehaviour {

    public Transform playerUIWrapper;
    public Transform gmUIWrapper;

    [SerializeField] UI_GM_Player[] gmUIPlayers = new UI_GM_Player[4];
    [SerializeField] GameObject gmBombTypesGO;
    [SerializeField] GameObject gmBombChosenGO;
    [SerializeField] GameObject gmChosePlayerTxt;
	[SerializeField] Text[] gmBombTimesTxt;

    [SerializeField] GameObject gmAbilityTypesGO;

    public void Setup() {
        GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (controller.gmPlayer != null) {
			gmUIWrapper.gameObject.SetActive(true);
            List<int> playerIndexes = controller.GetPlayerIndexes();
            foreach (UI_GM_Player ui in gmUIPlayers) {
                ui.gameObject.SetActive(false);
            }
            foreach (int i in playerIndexes) {
                gmUIPlayers[i].gameObject.SetActive(true);
            }
        } else {
            gmUIWrapper.gameObject.SetActive(false);
        }

		for(int i = 0; i < gmBombTimesTxt.Length; i++) {
			gmBombTimesTxt [i].text = controller.GetComponent<PassTheBomb> ().bombTypes [i].MinimumBombTime.ToString () + "-" + controller.GetComponent<PassTheBomb> ().bombTypes [i].MaximumBombTime.ToString ();
		}

        RestartRound();
    }

    public void RestartRound() {
        gmBombTypesGO.SetActive(true);
        gmBombChosenGO.SetActive(false);
        gmChosePlayerTxt.SetActive(false);
        gmAbilityTypesGO.SetActive(false);
    }

    public void GMBombChosen() {
        gmBombChosenGO.SetActive(true);
        gmChosePlayerTxt.SetActive(true);
    }

    public void GMAbilityChosen(int _index) {
        print("Mangler");
    }

    public void BombInPlay(bool _bool) {
        gmBombTypesGO.SetActive(!_bool);
        gmAbilityTypesGO.SetActive(_bool);

        if (_bool) {
            gmChosePlayerTxt.SetActive(false);
        }
    }


}
