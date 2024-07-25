// 문제 : 정수로 이루어진 리스트 num_list가 주어집니다. num_list에서 가장 작은 5개의 수를 제외한 수들을 오름차순으로 담은 리스트를 return하도록 solution 함수를 완성해주세요.

using System;
using System.Collections.Generic;
using System.Linq;

public class Solution {
    public int[] solution(int[] num_list)
 {
     // num_list를 오름차순으로 정렬
    List<int> sortedList = num_list.ToList();
    sortedList.Sort();

    // 가장 작은 5개의 수를 제외한 리스트 생성
    List<int> resultList = new List<int>();
    if (sortedList.Count > 5)
    {
        resultList = sortedList.GetRange(5, sortedList.Count - 5);
    }

    // 결과를 배열로 변환하여 반환
    return resultList.ToArray();
 }
}
