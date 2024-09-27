using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
public class LeaderboardSystem
{
    // 设scores长度为N

    // 初始思路：将scores排序并输出前m个，时间复杂度为O(NlogN)
    // 进阶思考：考虑到只需要保留最大的m个数，可使用容量为m的最大堆，减少时间复杂度;    
    // 维护m容量的最大堆的时间复杂度为O(logM)
    // 共n个数，因此时间复杂度为O(NlogM)
    public class CustomComparer : IComparer<(int score, int index)>
    {
        public int Compare((int score, int index) x, (int score, int index) y)
        {
            int scoreComparison = y.score.CompareTo(x.score);
            return scoreComparison != 0 ? scoreComparison : x.index.CompareTo(y.index);
        }
    }

    public static List<int> GetTopScores(int[] scores, int m)
    {
        // 非法输入
        if (scores.Length == 0 || scores == null || m <= 0)
        {
            return new List<int>();
        }

        SortedSet<(int score, int index)> maxHeap = new SortedSet<(int score, int index)>(new CustomComparer());

        for (int i = 0; i < scores.Length; i++)
        {
            if (maxHeap.Count < m)
            {
                // 加入最大堆
                maxHeap.Add((scores[i], i));
            }
            else if (scores[i] > maxHeap.Max.score)
            {
                // 由于自定义的compare是升序排列，因此Max实际上是Min
                maxHeap.Remove(maxHeap.Max);
                maxHeap.Add((scores[i], i));
            }
        }

        List<int> topScores = new List<int>();
        foreach (var (score, _) in maxHeap)
        {
            topScores.Add(score);
        }
        return topScores;
    }

}
// 单元测试
public class LeaderboardSystemTests
{
    public void TestGetTopScores()
    {
        // Test case: Normal 
        int[] scores1 = { 50, 20, 80, 60, 90 };
        List<int> result1 = LeaderboardSystem.GetTopScores(scores1, 3);
        Debug.Assert(result1.SequenceEqual(new List<int> { 90, 80, 60 }));
        Console.WriteLine("pass 1");

        // Test case: Empty 
        int[] scores2 = { };
        List<int> result2 = LeaderboardSystem.GetTopScores(scores2, 3);
        Debug.Assert(result2.SequenceEqual(new List<int> { }));
        Console.WriteLine("pass 2");


        // Test case: m > length
        int[] scores3 = { 30, 10, 40 };
        List<int> result3 = LeaderboardSystem.GetTopScores(scores3, 5);
        Debug.Assert(result3.SequenceEqual(new List<int> { 40, 30, 10 }));
        Console.WriteLine("pass 3");


        // Test case: m = 0
        int[] scores4 = { 30, 10, 40 };
        List<int> result4 = LeaderboardSystem.GetTopScores(scores4, 0);
        Debug.Assert(result4.SequenceEqual(new List<int> { }));
        Console.WriteLine("pass 4");


        // Test case: m < 0
        int[] scores5 = { 30, 10, 40 };
        List<int> result5 = LeaderboardSystem.GetTopScores(scores5, -1);
        Debug.Assert(result5.SequenceEqual(new List<int> { }));
        Console.WriteLine("pass 5");

        // Test case: duplicate numbers
        int[] scores6 = { 30, 30, 30 };
        List<int> result6 = LeaderboardSystem.GetTopScores(scores6, 2);
        Debug.Assert(result6.SequenceEqual(new List<int> { 30, 30 }));
        Console.WriteLine("pass 6");
    }
}



