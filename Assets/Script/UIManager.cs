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

	public Text output;

	public void Generate(){
		//output = string.Join("", sequence.GenerateSequence (sequenceLenght, poolSize, forceDifferentItems, avoidOrderedSequences, avoidRepeatedItems));
		int[] result = sequence.GenerateSequence (sequenceLenght, poolSize, forceDifferentItems, avoidOrderedSequences, avoidRepeatedItems);
		string chaine = "";
		for (int i = 0; i < result.Length; i++) {
			chaine += result[i].ToString ();
			if (i != result.Length - 1) {
				chaine += ", ";
			}
		}
		output.text = chaine;
	}
}
