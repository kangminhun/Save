using System;
using System.Text.RegularExpressions;
public class Solution {
    public string[] solution(string[] quiz)
    {
         string[] answer = new string[quiz.Length];
        string intstr_E = "";
        int num=0;
        for (int i = 0; i < quiz.Length; i++)
        {
            intstr_E = quiz[i].Substring(quiz[i].IndexOf("=") + 1).Trim();
            if (quiz[i].Contains("+"))
            {
                string[] result = quiz[i].Substring(0, quiz[i].IndexOf("=")).Trim().Split('+');
                for (int j = 0; j < result.Length; j++)
                {
                    num = int.Parse(result[0]) + int.Parse(result[1]);
                }
                if (num == int.Parse(intstr_E))
                {
                    answer[i] = "O";
                }
                else
                    answer[i] = "X";
            }
            else
            {
                string[] result = quiz[i].Substring(0, quiz[i].IndexOf("=")).Trim().Split(' ');
                for (int j = 0; j < result.Length; j++)
                {
                    num = int.Parse(result[0]) - int.Parse(result[2]);
                }
                if (num == int.Parse(intstr_E))
                {
                    answer[i] = "O";
                }
                else
                    answer[i] = "X";
            }   
        }
        return answer;
    }
}
