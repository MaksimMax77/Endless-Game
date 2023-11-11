using UnityEngine;

namespace CodeBase.Animations
{
    public class AnimationsControl : MonoBehaviour
    {
        [SerializeField] private string _xTransitionName;
        [SerializeField] private string _yTransitionName;
        [SerializeField] private Animator _animator;
        
        public void AnimateMove(float x, float y)
        {
            _animator.SetFloat(_xTransitionName, x);
            _animator.SetFloat(_yTransitionName, y);
        }
    }
}
