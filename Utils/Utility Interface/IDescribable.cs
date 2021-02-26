using UnityEngine;

namespace UtilityInterface
{
    public interface IDescribable
    {
        string DescribableName { get; }
        string DescribableDescription { get; }
        Sprite DescribableIcon { get; }
    }
}