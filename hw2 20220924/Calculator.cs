using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour {
    private List<float> numbers = new List<float>(); // to store numbers
    private List<char> operators = new List<char>(); // to store operators
    private string str;

    void OnGUI() {
        // to show the operations and results
        GUI.TextField(new Rect(350, 75, 230, 50), str);

        // buttons of numbers
        if (GUI.Button(new Rect(350, 330, 110, 40), "0")) str += "0";
        if (GUI.Button(new Rect(350, 280, 50, 40), "1")) str += "1";
        if (GUI.Button(new Rect(410, 280, 50, 40), "2")) str += "2";
        if (GUI.Button(new Rect(470, 280, 50, 40), "3")) str += "3";
        if (GUI.Button(new Rect(350, 230, 50, 40), "4")) str += "4";
        if (GUI.Button(new Rect(410, 230, 50, 40), "5")) str += "5";
        if (GUI.Button(new Rect(470, 230, 50, 40), "6")) str += "6";
        if (GUI.Button(new Rect(350, 180, 50, 40), "7")) str += "7";
        if (GUI.Button(new Rect(410, 180, 50, 40), "8")) str += "8";
        if (GUI.Button(new Rect(470, 180, 50, 40), "9")) str += "9";

        // buttons of operators
        if (GUI.Button(new Rect(530, 230, 50, 90), "+")) str += "+";
        if (GUI.Button(new Rect(530, 180, 50, 40), "-")) str += "-";
        if (GUI.Button(new Rect(470, 130, 50, 40), "x")) str += "x";
        if (GUI.Button(new Rect(530, 130, 50, 40), "/")) str += "/";

        // buttons of funtion
        if (GUI.Button(new Rect(470, 330, 50, 40), ".")) str += ".";
        if (GUI.Button(new Rect(350, 130, 50, 40), "C")) Init();
        if (GUI.Button(new Rect(410, 130, 50, 40), "<<")) { // button of backspace
            if (str.Length > 0)
                str = str.Substring(0, str.Length - 1); 
            else str = "";
        }
        if (GUI.Button(new Rect(530, 330, 50, 40), "=")) {
            str = calculate(str).ToString();
            numbers.Clear();
            operators.Clear();
        }

    }

    float calculate(string str) {
        if (str == "") 
            return 0;
        pre_treat(str);
        for (int i = 0; i < operators.Count; ++i) {
            if(operators[i] == 'x' || operators[i] == '/') {
                float tmp;
                float left = numbers[i];
                float right = numbers[i + 1];
                if (operators[i] == 'x') 
                    tmp = left * right;
                else if (right == 0)
                    tmp = 999999999; // to indicate that you cannot divide by 0
                else tmp = left / right;
                numbers.RemoveAt(i);
                numbers[i] = tmp;
                operators.RemoveAt(i);
                i--;
            }
            
        }
        for (int i = 0; i < operators.Count; i++) {
            float tmp;
            float left = numbers[i];
            float right = numbers[i + 1];
            if (operators[i] == '+') 
                tmp = left + right;
            else tmp = left - right;
            numbers.RemoveAt(i);
            numbers[i] = tmp;
            operators.RemoveAt(i);
            i--;
        }
        return numbers[0];
    }

    void pre_treat(string str)
    {
        string tmp = "";
        for (int i = 0; i < str.Length; ++i) {
            if (i == 0 && str[i] == '-') {
                str = "0" + str; // if str[0] == '0', add '0' to make a negative number into a form like '0 - ...'
            }
            if (i >= str.Length - 1) {
                tmp += str[i];
                numbers.Add(float.Parse(tmp));
            }
            else if (is_number(str[i])) {
                tmp += str[i];
            }
            else {
                numbers.Add(float.Parse(tmp));
                tmp = "";
                if (is_operator(str[i]))
                {
                    operators.Add(str[i]);
                }
            }
        }
    }

    bool is_number(char c) {
        return (c == '0' || c == '1' || c == '2' || c == '3' ||
                c == '4' || c == '5' || c == '6' || c == '7' ||
                c == '8' || c == '9' || c == '.');
    }

    bool is_operator(char c) {
        return (c == '+' || c == '-' || c == 'x' || c == '/');
    }

    void Start() {
        Init();
    }

    void Init() {
        str = "";
    }
    
    void Update() {
        
    }
}
