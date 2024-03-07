using System;

namespace Foni.Code.TweenSystem.Easing
{
    public delegate float EaseDelegate(float normalizedTime, float from, float to);

    public static partial class StrategyFactory
    {
        public static EaseDelegate GetStrategy(EEasing easing)
        {
            return easing switch
            {
                EEasing.Linear => Linear,
                EEasing.SineIn => SineIn,
                EEasing.SineOut => SineOut,
                EEasing.SineInOut => SineInOut,
                EEasing.SineOutIn => SineOutIn,
                EEasing.QuadIn => QuadIn,
                EEasing.QuadOut => QuadOut,
                EEasing.QuadInOut => QuadInOut,
                EEasing.QuadOutIn => QuadOutIn,
                EEasing.CubicIn => CubicIn,
                EEasing.CubicOut => CubicOut,
                EEasing.CubicInOut => CubicInOut,
                EEasing.CubicOutIn => CubicOutIn,
                EEasing.QuartIn => QuartIn,
                EEasing.QuartOut => QuartOut,
                EEasing.QuartInOut => QuartInOut,
                EEasing.QuartOutIn => QuartOutIn,
                EEasing.QuintIn => QuintIn,
                EEasing.QuintOut => QuintOut,
                EEasing.QuintInOut => QuintInOut,
                EEasing.QuintOutIn => QuintOutIn,
                EEasing.ExpoIn => ExpoIn,
                EEasing.ExpoOut => ExpoOut,
                EEasing.ExpoInOut => ExpoInOut,
                EEasing.ExpoOutIn => ExpoOutIn,
                EEasing.CircIn => CircIn,
                EEasing.CircOut => CircOut,
                EEasing.CircInOut => CircInOut,
                EEasing.CircOutIn => CircOutIn,
                EEasing.BackIn => BackIn,
                EEasing.BackOut => BackOut,
                EEasing.BackInOut => BackInOut,
                EEasing.BackOutIn => BackOutIn,
                EEasing.ElasticIn => ElasticIn,
                EEasing.ElasticOut => ElasticOut,
                EEasing.ElasticInOut => ElasticInOut,
                EEasing.ElasticOutIn => ElasticOutIn,
                EEasing.BounceIn => BounceIn,
                EEasing.BounceOut => BounceOut,
                EEasing.BounceInOut => BounceInOut,
                EEasing.BounceOutIn => BounceOutIn,
                _ => throw new ArgumentOutOfRangeException(nameof(easing), easing, null),
            };
        }
    }
}