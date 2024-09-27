using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography;
using Microsoft.VisualBasic;
public class TalentAssessmentSystem
{
    // 使用二分法寻找分割点，时间复杂度是O(log(min(m,n)));空间复杂度为O（1）因为只用了常数级变量存储高低指针
    public static double FindMedianTalentIndex(int[] fireAbility, int[] iceAbility)
    {
        // 保证第一个参数是较短的数组
        if (fireAbility.Length > iceAbility.Length)
        {
            return FindMedianTalentIndex(iceAbility, fireAbility);
        }

        // 将和合并后的中位数一分为二（如果为奇数，则前半部分比后半部分多一个数），这样中位数要么是前半部分的最大值，要么是前半最大值和后半最小值的平均值
        // 新合并数组中，前半部分的最大值，等于，原始两个数组中位于新数组前半部分的数的各自最大值的最大值，后半部分的最小值类似
        // 因此找寻中位数可以转化为找寻新数组二分后的前半最大值和后半最小值，可以进一步转化为找寻原始数组的分割点（将其分为的两部分，分别落在新数组的两部分）
        int n = fireAbility.Length;
        int m = iceAbility.Length;
        int low = 0, high = n; // 短数组的两个指针，指向前面和后面

        while (low <= high)
        {
            // 分割点
            int partitionFire = (low + high) / 2; // 在当前分割点下的短数组前半部分
            int partitionIce = (n + m + 1) / 2 - partitionFire; // 在当前分割点下的长数组前半部分

            // 计算目前两数组前半部分和后半部分的最大/最小值
            int maxLeftFire = (partitionFire == 0) ? int.MinValue : fireAbility[partitionFire - 1];
            int minRightFire = (partitionFire == n) ? int.MaxValue : fireAbility[partitionFire];

            int maxLeftIce = (partitionIce == 0) ? int.MinValue : iceAbility[partitionIce - 1];
            int minRightIce = (partitionIce == m) ? int.MaxValue : iceAbility[partitionIce];

            // 检查当前假设是否合法，既当前分割点是否能保证两种数组的最大最小值符合合并后数组的数值分布规律。
            if (maxLeftFire <= minRightIce && maxLeftIce <= minRightFire)
            {
                if ((n + m) % 2 == 1)
                {
                    return Math.Max(maxLeftFire, maxLeftIce);
                }
                // If total length is even, return the average of the two middle values
                else
                {
                    return (Math.Max(maxLeftFire, maxLeftIce) + Math.Min(minRightFire, minRightIce)) / 2.0;
                }
            }
            // 如果不合法，但是短数组的最大值比长数组最小值大，说明前者应该更小，分割点应该往前移动，因此高位指针变低
            else if (maxLeftFire > minRightIce)
            {
                high = partitionFire - 1;
            }
            // 短数组需要分割点更靠后
            else
            {
                low = partitionFire + 1;
            }
        }

        return 0; // 非法数组
    }
}
// 单元测试
public class TalentAssessmentSystemTests
{
    public void TestFindMedianTalentIndex()
    {
        // Test case 1: 示例
        int[] fireAbility1 = { 1, 3, 7, 9, 11 };
        int[] iceAbility1 = { 2, 4, 8, 10, 12, 14 };
        Debug.Assert(8 == TalentAssessmentSystem.FindMedianTalentIndex(fireAbility1, iceAbility1));
        Console.WriteLine("pass 1");

        // Test case 2: 相等长度数组
        int[] fireAbility2 = { 1, 2 };
        int[] iceAbility2 = { 3, 4 };
        Debug.Assert(2.5 == TalentAssessmentSystem.FindMedianTalentIndex(fireAbility2, iceAbility2));
        Console.WriteLine("pass 2");


        // Test case 3: 空数组
        int[] fireAbility3 = { };
        int[] iceAbility3 = { 1, 2, 3 };
        Debug.Assert(2 == TalentAssessmentSystem.FindMedianTalentIndex(fireAbility3, iceAbility3));
        Console.WriteLine("pass 3");

    }
}
