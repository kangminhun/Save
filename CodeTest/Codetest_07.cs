using System;

public class Solution {
    public string solution(string[] id_pw, string[,] db) {
        string answer = "";
        for (int i = 0; i < db.GetLength(0); i++)
        {
           //answer= (id_pw[0] == db[i, 0]) ? ((id_pw[1] == db[i, 1]) ? "login" : "wrong pw") : "fail";
           //db[i, 0](아이디)
           //db[i, 1](비번)
            if(id_pw[0] == db[i, 0])
            {
                if((id_pw[1] == db[i, 1]))
                    return "login";
                else
                    return "wrong pw";
            }
        } 
        return "fail";
    }
}
//로그인 성공?
