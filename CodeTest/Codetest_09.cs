public class Solution {
    public string solution(string phone_number) 
    {
        string answer="";
        
        string str=phone_number.Substring(phone_number.Length - 4);
        
        for(int i=0;i<phone_number.Length-4;++i)
        {
            answer+="*";
        }
        answer += str;
        return answer;
        //핸드폰 번호 가리기
    }
}
