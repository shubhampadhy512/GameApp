using UnityEngine;

namespace Season2
{
    public static class AnimatorExtensions
    {
        public static bool HasParameterOfType(this Animator animator, string paramName, AnimatorControllerParameterType type)
        {
            foreach (var param in animator.parameters)
            {
                if (param.type == type && param.name == paramName)
                    return true;
            }
            return false;
        }
    }
}