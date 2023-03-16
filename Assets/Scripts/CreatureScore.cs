using Scripts.ObjectPool;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class CreatureScore : MonoBehaviour
    {
        [SerializeField] private Text _score;
        [SerializeField] private CreatureAI _creature;

        private PoolItem _poolItem;
        private string _defaultScore;

        private void Awake()
        {
            _poolItem = GetComponentInParent<PoolItem>();
        }

        private void Start()
        {
            _defaultScore = _score.text;

            _poolItem.OnRestart += OnRestart;
            _creature.OnChangeScore += OnChangeScore;
        }

        private void OnRestart()
        {
            _score.text = _defaultScore;
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