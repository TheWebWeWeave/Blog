using System;
using System.Diagnostics;
using Xunit;

namespace blog_xunit;

public class UnitTest1
{
    [Theory]
    [CsvData("./Parameters.csv")]
    public void TestWithCSVData(string Category, string Post_Title, string Archive_Year)
    {
        Debug.WriteLine($"{Category} - {Post_Title} - {Archive_Year}");
    }
}