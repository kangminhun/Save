using System;
public class Solution {
    public bool solution(int x) {
        bool answer = true;
        int n = 0;
        string str = x.ToString();
        for (int i = 0; i < str.Length; i++)
        {
            n += (int)Char.GetNumericValue(str[i]);
        }
        answer = x % n == 0 ? true : false;
        return answer;
    }
}
//하샤드 수 구하기
