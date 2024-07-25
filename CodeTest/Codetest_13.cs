//문제 : 영어 알파벳으로 이루어진 문자열 str이 주어집니다. 각 알파벳을 대문자는 소문자로 소문자는 대문자로 변환해서 출력하는 코드를 작성해 보세요.

using System;
using System.Linq;

public class Example
{
    public static void Main()
    {
        String s;

        Console.Clear();
        s = Console.ReadLine();
        var selectResult = s.Select(c => char.IsUpper(c) ? char.ToLower(c) : char.ToUpper(c));
        String str =new String(selectResult.ToArray());
        Console.WriteLine(str);
    }
}
