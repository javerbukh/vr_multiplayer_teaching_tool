using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using UnityEngine.UI;

public class PlayerDataScript : RealtimeComponent
{
    private PlayerDataModel _model;
    private Text _scoreText;

    private void Awake()
    {
        _scoreText = GetComponent<Text>();
    }

    private PlayerDataModel model
    {
        set
        {
            if (_model != null)
            {
                // Unregister from events
                _model.playerScoreDidChange -= ScoreDidChange;
            }

            _model = value;

            if (_model != null)
            {
                // Update the score to match the new value
                UpdateDisplayScore();

                // Register for events so we'll know if the score changes later
                _model.playerScoreDidChange += ScoreDidChange;
            }
        }
    }

    private void ScoreDidChange(PlayerDataModel model, int value)
    {
        UpdateDisplayScore();
    }

    private void UpdateDisplayScore()
    {
        _scoreText.text = "Score: " + _model.playerScore;
    }
}
