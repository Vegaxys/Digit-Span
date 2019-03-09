using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public SequenceGenerator sequence;

	public int sequenceLenght;
	public int poolSize;
	public bool forceDifferentItems;
	public bool avoidOrderedSequences;
	public bool avoidRepeatedItems;

	public RectTransform[] test_Chance;
	public Text[] test_Text_Chance;
	public Text repeatedItemText, orederedSequencesText;

	public Text output;

	public void Generate(){
		int[] result = sequence.GenerateSequence (sequenceLenght, poolSize, forceDifferentItems, avoidOrderedSequences, avoidRepeatedItems);
		string chaine = "";
		for (int i = 0; i < result.Length; i++) {
			chaine += result[i].ToString ();
			if (i != result.Length - 1) {
				chaine += ", ";
			}
		}
		output.text = chaine;
		GetApparitionChance ();
		repeatedItemText.text = "Repeated items : " + sequence.TestRepeatedItems ().ToString ();
		orederedSequencesText.text = "Repeated sequences : " + sequence.TestOrderedSequences ().ToString ();
	}
	void GetApparitionChance(){
		for (int i = 0; i < 10; i++) {
			if (i < poolSize) {
				test_Text_Chance [i].text = String.Format ("{0:0.00}", (float)sequence.NumberOfSameNumberInSequence (i) / sequenceLenght * 100) + "%";
				test_Chance [i].sizeDelta = new Vector2 (30, ((float)sequence.NumberOfSameNumberInSequence (i) / sequenceLenght) * 200);
			} else {
				test_Text_Chance [i].text = "null";
				test_Chance [i].sizeDelta = new Vector2 (30, 1);
			}
		}
	}
}
