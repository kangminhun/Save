using System;

public class Solution {
    public int solution(string[] babbling) {
         string[] canBabbling = new string[] { "aya", "ye", "woo", "ma" };
        int answer = 0;
        string[] doubleBabbling = new string[] { "ayaaya", "yeye", "woowoo", "mama" };
        //중복발음을 따로 변수로 빼줌
        for (int i = 0; i < babbling.Length; i++)
        {
            for (int j = 0; j < doubleBabbling.Length; j++)
            {
                if (babbling[i].Contains(doubleBabbling[j]))
                {
                   //String.Contains()함수를 이용하여 중복발음을 제거
                    babbling[i] = string.Empty; break;
                }
            }
            for (int j = 0; j < canBabbling.Length; j++)
            {
                if (babbling[i].Contains(canBabbling[j]))
                {
                    babbling[i] = babbling[i].Replace(canBabbling[j], string.Empty);
                    if (string.IsNullOrWhiteSpace(babbling[i]))
                    {
                    //발음 가능한 만큼 더해준다
                        answer++; break;
                    }
                }
            }
        }
        return answer;
    }
}
//옹알이
