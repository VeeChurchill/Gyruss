using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private int _score = 0;

    public void ChangeScore(int amount)
    {
        _score += amount;
        _text.text = "Score: " + _score;
    }
}
