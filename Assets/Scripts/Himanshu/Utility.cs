using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
        
        public static void Invoke(this MonoBehaviour _monoBehaviour, Action _action, float _delay, bool _realtime = false)
        {
            _monoBehaviour.StartCoroutine(InvokeRoutine(_action, _delay, _realtime));
        }
        
 
        private static IEnumerator InvokeRoutine(System.Action _action, float _delay, bool _realtime)
        {
            if (_realtime)
                yield return new WaitForSecondsRealtime(_delay);
            else
                yield return new WaitForSeconds(_delay);
            _action();
        }
        
        public static string RemoveSpecialCharacters(this string str)
        {
            string output = "";
            
            foreach (char c in str) {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_') {
                    output += c;
                }
            }
            return output.ToLower();
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
        
        public static float AngleInRad(this Vector3 vec1, Vector3 vec2) {
            return Mathf.Atan2(vec2.y - vec1.y, vec2.x - vec1.x);
        }
 
        //This returns the angle in degrees
        public static float AngleInDeg(this Vector3 vec1, Vector3 vec2) {
            return AngleInRad(vec1, vec2) * 180 / Mathf.PI;
        }

    }
    
    
    public class Wrapper<T>
    {
        public T value;
        
        public Wrapper(T _value) => value = _value;
    }
}