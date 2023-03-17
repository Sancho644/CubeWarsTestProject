using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class CreatureScore : MonoBehaviour
    {
        [SerializeField] private Text _score;
        [SerializeField] private CreatureAI _creature;

        private void Start()
        {
            _creature.OnChangeScore += OnChangeScore;
        }

        private void OnChangeScore(int value)
        {
            _score.text = value.ToString();
        }

        private void OnDestroy()
        {
            _creature.OnChangeScore -= OnChangeScore;
        }
    }
}