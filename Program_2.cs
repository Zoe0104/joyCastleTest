using System;
using System.Diagnostics;
public class EnergyFieldSystem
{
    // heights数组长度为n
    // 通过双指针，遍历一次数组，时间复杂度为O(n)
    // 仅需要五个变量记录，空间复杂度O(1)
    public static float MaxEnergyField(int[] heights)
    {
        if (heights == null || heights.Length < 2)
        {
            return 0;
        }

        int leftP = 0; // 左指针
        int rightP = heights.Length - 1; // 右指针
        int distance = 0; // 建筑物距离
        int maxArea = 0;  // 全局最大面积
        int curArea = 0;  // 当前最大距离

        while (leftP < rightP)
        {
            distance = rightP - leftP;

            // 出现不能放建筑的地方就跳过
            if (heights[rightP] == 0 || heights[leftP] == 0)
            {
                if (heights[rightP] == 0)
                {
                    rightP -= 1;
                }
                if (heights[leftP] == 0)
                {
                    leftP += 1;
                }
                continue;
            }

            // 计算面积并更新
            curArea = (heights[rightP] + heights[leftP]) * distance;
            maxArea = Math.Max(curArea, maxArea);

            // 只移动对应高度值较小的一边
            if (heights[leftP] >= heights[rightP])
            {
                rightP -= 1;
            }
            else
            {
                leftP += 1;
            }
        }

        return (float)maxArea / 2;
    }
}
// 单元测试
public class EnergyFieldSystemTests
{
    public void TestMaxEnergyField()
    {
        // Test case 1: Sample
        int[] heights1 = { 1, 8, 6, 2, 5, 4, 8, 3, 7 };
        Debug.Assert(52.5f == EnergyFieldSystem.MaxEnergyField(heights1));
        Console.WriteLine("pass 1");

        // Test case 2
        int[] heights2 = { 10, 15 };
        Debug.Assert(12.5f == EnergyFieldSystem.MaxEnergyField(heights2));
        Console.WriteLine("pass 2");


        // Test case 3
        int[] heights3 = { 10, 9, 8, 7, 6 };
        Debug.Assert(32f == EnergyFieldSystem.MaxEnergyField(heights3));
        Console.WriteLine("pass 3");


        // Test case 4: zero values
        int[] heights4 = { 0, 0, 10, 0, 0 };
        Debug.Assert(0 == EnergyFieldSystem.MaxEnergyField(heights4));
        Console.WriteLine("pass 4");


        // Test case 5: same
        int[] heights5 = { 5, 5, 5, 5, 5 };
        Debug.Assert(20.0f == EnergyFieldSystem.MaxEnergyField(heights5));
        Console.WriteLine("pass 5");

        // Test case 6
        int[] heights6 = { 10 };
        Debug.Assert(0 == EnergyFieldSystem.MaxEnergyField(heights6));
        Console.WriteLine("pass 6");
    }
}