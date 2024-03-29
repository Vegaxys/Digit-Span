﻿using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SequenceGenerator : MonoBehaviour {

	public List<int> result = new List<int> ();
	private  List<int> range = new List<int> ();
	private int[] schemaAscending = {012,123,234,345,456,567,678,789,024,135,246,357,468,579,036,147,258,369,048,159};
	private int[] schemaDescending = {210,321,432,543,654,765,876,987,420,531,642,753,864,975,630,741,852,963,840,951};

	private bool forceDifferentItems, avoidOrderedSequences, avoidRepeatedItems;
	private int sequenceLength;
	private int poolSize;

	public int []GenerateSequence(int _sequenceLength, int _poolSize, bool _forceDifferentItems, bool _avoidOrderedSequences, bool _avoidRepeatedItems){
		result.Clear ();

		forceDifferentItems = _forceDifferentItems;
		avoidOrderedSequences = _avoidOrderedSequences;
		avoidRepeatedItems = _avoidRepeatedItems;

		sequenceLength = Mathf.Clamp (_sequenceLength, 1, 1000);
		poolSize = Mathf.Clamp (_poolSize, 2, 10);

		for (int i = 0; i < sequenceLength; i++) {
			if (range.Count == 0) {
				FillRange ();
			}
			result.Add (GenerateNewNumber (i));
		}
		return result.ToArray ();
	}
	bool AvoidOrderedSequences(int item, int index){
		int test = int.Parse (item.ToString () + result [result.Count - 1].ToString () + result [result.Count - 2].ToString ());
		for (int x = 0; x < schemaAscending.Length; x++) {
			if (test == schemaAscending [x]) {
				return true;
			}
			if (test == schemaDescending [x]) {
				return true;
			}
		}
		return false;
	}
	bool AvoidRepeatedItem(int item){
		if (item == result [result.Count - 1]) {
			return true;
		}
		return false;
	}
	void FillRange(){
		range.Clear ();
		for (int i = 0; i < poolSize; i++) {
			range.Add (i);
		}
	}
	public int TestRepeatedItems(){
		int _result = 0;
		for (int i = 0; i < sequenceLength - 1; i++) {
			if (result [i] == result [i + 1]) {
				_result++;
			}
		}
		return _result;
	}
	public int TestOrderedSequences(){
		int _result = 0;
		for (int i = 0; i < result.Count - 3; i++) {
			int test = int.Parse (result [i].ToString () + result [i + 1].ToString () + result [i + 2].ToString ());

			for (int x = 0; x < schemaAscending.Length; x++) {
				if (test == schemaAscending [x]) {
					_result++;
				}
				if (test == schemaDescending [x]) {
					_result++;
				}
			}
		}
		return _result;
	}
	private int GenerateNewNumber(int index){
		int _result = 0;
		if (forceDifferentItems) {
			_result = range [Random.Range (0, range.Count)];
		} else {
			_result = Random.Range (0, poolSize);
		}
		if (avoidOrderedSequences && result.Count > 1 && AvoidOrderedSequences(_result, index)) {
			int temp = result [result.Count - 1];
			result [result.Count - 1] = _result;
			return temp;
		}
		if (avoidRepeatedItems && index > 0) {
			if (AvoidRepeatedItem (_result)) {
				return GenerateNewNumber (index);
			}
		}
		if (forceDifferentItems) {
			range.Remove (_result);
		}
		return _result;
	}
	public int NumberOfSameNumberInSequence(int number){
		int _result = 0;
		for (int i = 0; i < result.Count; i++) {
			if (number == result [i]) {
				_result++;
			}
		}
		return _result;
	}

	/*
	public int []GenerateSequence(int _sequenceLength, int _poolSize, bool _forceDifferentItems, bool _avoidOrderedSequences, bool _avoidRepeatedItems){
		result.Clear ();

		sequenceLength = Mathf.Clamp (_sequenceLength, 1, 1000);
		poolSize = Mathf.Clamp (_poolSize, 2, 10);

		int randomDigit = 0;
		if (_forceDifferentItems) {
			List<int> subSequence = new List<int> ();
			int offset = 0;
			if (((float)sequenceLength / poolSize) - (int)(sequenceLength / poolSize) != 0) {
				offset = 1;
			}
			for (int i = 0; i < Mathf.FloorToInt ((float)sequenceLength / poolSize) + offset; i++) {
				subSequence.Clear ();
				for (int x = 0; x < poolSize; x++) {
					subSequence.Add (x);
				}
				result.AddRange (subSequence);
			}
			int index = result.Count;
			for (int i = sequenceLength; i < index; i++) {
				result.RemoveAt (result.Count - 1);
			}
			//Suffle la list
			ShuffledList (subSequence);
		} else {
			for (int i = 0; i < sequenceLength; i++) {
				randomDigit = Random.Range (0, poolSize);
				result.Add (randomDigit);
			}
		}
		//Avoid Ordered Sequences
		if (_avoidOrderedSequences && ContainSameSequences()) {
			return GenerateSequence (sequenceLength, poolSize, _forceDifferentItems, _avoidOrderedSequences, _avoidRepeatedItems);
		}
		//Avoid Repeted Items
		if (_avoidRepeatedItems && ContainRepetedItems ()) {
			return GenerateSequence (sequenceLength, poolSize, _forceDifferentItems, _avoidOrderedSequences, _avoidRepeatedItems);
		}
		return result.ToArray ();
	}
	void ShuffledList(List<int> sequence){
		sequence = result;
		int randomDigit = 0;
		for (int i = 0; i < sequenceLength; i++) {
			randomDigit = Random.Range (0, sequence.Count);
			result.Add (sequence [randomDigit]);
			sequence.Remove (sequence [randomDigit]);
		}
	}
	bool ContainSameSequences(){
		for (int i = 0; i < result.Count - 3; i++) {
			int test = int.Parse (result [i].ToString () + result [i + 1].ToString () + result [i + 2].ToString ());

			for (int x = 0; x < schemaAscending.Length; x++) {
				if (test == schemaAscending [x]) {
					return true;
				}
				if (test == schemaDescending [x]) {
					return true;
				}
			}
		}
		return false;
	}
	bool ContainRepetedItems(){
		for (int i = 0; i < result.Count - 1; i++) {
			if (result [i] == result [i + 1]) {
				return true;
			}
		}
		return false;
	}
	*/
}
