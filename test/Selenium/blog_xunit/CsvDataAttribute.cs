using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit.Sdk;

public class CsvDataAttribute : DataAttribute
{
    private string csvFile;

    public CsvDataAttribute(string v)
    {
        csvFile = v;
    }

    private static object[] ConvertParameters(IReadOnlyList<object> values, IReadOnlyList<Type> parameterTypes)
    {
        var result = new object[parameterTypes.Count];
        for (var idx = 0; idx < parameterTypes.Count; idx++)
        {
            result[idx] = ConvertParameter(values[idx], parameterTypes[idx]);
        }

        return result;
    }

    private static object ConvertParameter(object parameter, Type parameterType)
    {
        return parameterType == typeof(int) ? Convert.ToInt32(parameter) : parameter;
    }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        var data = new List<object[]>();
        //var csvFile = Path.Combine(Directory.GetCurrentDirectory(), "parameters.csv");
        var lines = File.ReadAllLines(csvFile);
        foreach (var line in lines)
        {
            var values = line.Split(',');
            data.Add(values);
        }
        return data;
    }
}