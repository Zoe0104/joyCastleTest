using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
public class TreasureHuntSystem
{
    // 时间复杂度：因为需要遍历所有treasure，所以时间复杂度为O(n)
    // 空间复杂度：需要存储每个位置的dp值，因此是O(n)
    // 进阶挑战1：如MaxTreasureValue_extraChance所示，加入另一个dp列表记录可能用过道具的情况
    // 进阶挑战2：负值自动处理
    public static int MaxTreasureValue(int[] treasures)
    {
        // 传入为空列表
        if (treasures == null || treasures.Length == 0)
        {
            return 0;
        }

        // 只有一个宝箱
        if (treasures.Length == 1)
        {
            return treasures[0];
        }

        // 只有两个宝箱
        if (treasures.Length == 2)
        {
            return Math.Max(treasures[0], treasures[1]);
        }

        // dp[i]为前i个宝箱的最大值
        // dp[i]=max(dp[i-1],dp[i-2]+treasure[i]),其中前一种情况表示不选第i个，后一种情况表示选择第i个，因此不能选择第i-1个
        // 边界条件为dp[0]和dp[1]
        List<int> dp = new List<int>();
        dp.Add(treasures[0]);
        dp.Add(Math.Max(treasures[0], treasures[1]));
        int tem = 0;

        for (int i = 2; i < treasures.Length; i++)
        {
            tem = Math.Max(dp[i - 1], dp[i - 2] + treasures[i]);
            dp.Add(tem);
        }

        return dp[dp.Count - 1];
    }

    public static int MaxTreasureValue_extraChance(int[] treasures)
    {
        // 传入为空列表
        if (treasures == null || treasures.Length == 0)
        {
            return 0;
        }

        // 只有一个宝箱
        if (treasures.Length == 1)
        {
            return treasures[0];
        }

        // 只有两个宝箱
        if (treasures.Length == 2)
        {
            return Math.Max(treasures[0], treasures[1]);
        }

        // dp[i]为前i个宝箱的最大值
        // dp[i]=max(dp[i-1],dp[i-2]+treasure[i]),其中前一种情况表示不选第i个，后一种情况表示选择第i个，因此不能选择第i-1个
        // 边界条件为dp[0]和dp[1]
        List<int> dp = new List<int>();
        dp.Add(treasures[0]);
        dp.Add(Math.Max(treasures[0], treasures[1]));
        dp.Add(Math.Max(dp[1], dp[0] + treasures[2]));
        int tem = 0;

        // dp2[i]为可能使用特殊能力的时候，前i个宝箱的最大值
        // dp2[i]=max(dp2[i-1],dp[i-3]+treasure[i]+treasure[i-1])
        // 前者表示在i-1个宝箱中就使用过特殊能力的值，后者表示在保证第i-2个宝箱没选中的时候，同时选中第i-1和第i个宝箱的情况
        // 边界条件为dp2[0]和dp[1]
        List<int> dp2 = new List<int>();
        dp2.Add(treasures[0]);
        dp2.Add(Math.Max(dp2[0], dp2[0] + treasures[1]));
        dp2.Add(Math.Max(dp2[1], dp[0] + treasures[1] + treasures[2]));
        int tem2 = 0;

        for (int i = 3; i < treasures.Length; i++)
        {
            tem = Math.Max(dp[i - 1], dp[i - 2] + treasures[i]);
            dp.Add(tem);

            tem2 = Math.Max(dp2[i - 1], dp[i - 3] + treasures[i] + treasures[i - 1]);
            dp2.Add(tem2);
        }

        return Math.Max(dp2[treasures.Length - 1], dp[treasures.Length - 1]);
    }
}
// 单元测试
public class TreasureHuntSystemTests
{
    public void TestMaxTreasureValue()
    {
        // Test case 1: example
        int[] treasures1 = { 3, 1, 5, 2, 4 };
        Debug.Assert(12 == TreasureHuntSystem.MaxTreasureValue(treasures1));
        Console.WriteLine("pass 1");

        // Test case 2: 1 treasure
        int[] treasures2 = { 10 };
        Debug.Assert(10 == TreasureHuntSystem.MaxTreasureValue(treasures2));
        Console.WriteLine("pass 2");

        // Test case 3: 2 treasures
        int[] treasures3 = { 10, 20 };
        Debug.Assert(20 == TreasureHuntSystem.MaxTreasureValue(treasures3));
        Console.WriteLine("pass 3");

        // Test case 4: same
        int[] treasures4 = { 5, 5, 5, 5, 5 };
        Debug.Assert(15 == TreasureHuntSystem.MaxTreasureValue(treasures4));
        Console.WriteLine("pass 4");


        // Test case 5: include negative num
        int[] treasures5 = { 1, -2, 9, -1, 6 };
        Debug.Assert(16 == TreasureHuntSystem.MaxTreasureValue(treasures5));
        Console.WriteLine("pass 5");

        int[] treasures6 = { 1, 0, 1, 2 };
        Console.WriteLine(TreasureHuntSystem.MaxTreasureValue_extraChance(treasures6));
    }
}
