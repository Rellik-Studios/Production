using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Himanshu
{
    public static class Utility
    {

        public static IEnumerator WaitForRealSeconds(float wait)
        {
            float counter = 0f;
            while (counter < wait)
            {
                counter += Time.unscaledDeltaTime;
                yield return null;
            }
        }
        
        public static void Invoke(this MonoBehaviour _monoBehaviour, Action _action, float _delay)
        {
            _monoBehaviour.StartCoroutine(InvokeRoutine(_action, _delay));
        }
 
        private static IEnumerator InvokeRoutine(System.Action _action, float _delay)
        {
            yield return new WaitForSeconds(_delay);
            _action();
        }
        
        public static IEnumerator FillBar(this Image _fillImage, float _time, int _dir = 1, float _waitTime = 0f)
        {
            yield return new WaitForSeconds(_waitTime);
            var time = 0f;

            while (time < _time)
            {
                time += Time.deltaTime;
                _fillImage.fillAmount += _dir * Time.deltaTime / _time;
                yield return null;
            }
        }

        
        public static T Random<T>(this List<T> _this)
        {
            int rand = UnityEngine.Random.Range(0, _this.Count);
            Debug.Log(_this.Count);

            return _this[rand];
        }
    }
    
    
    public class Wrapper<T>
    {
        public T value;
        
        public Wrapper(T _value) => value = _value;
    }
}