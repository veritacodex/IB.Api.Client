using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IB.Api.Client.Helper;

class Table
{
    private readonly List<object> _columns;
    private readonly List<object[]> _rows;

    public Table(params string[] columns)
    {
        if (columns == null || columns.Length == 0)
        {
            throw new ArgumentException("Parameter cannot be null nor empty", nameof(columns));
        }

        _columns = new List<object>(columns);
        _rows = [];
    }

    public void AddRow(params object[] values)
    {
        if (values == null)
        {
            throw new ArgumentException("Parameter cannot be null", nameof(values));
        }

        if (values.Length != _columns.Count)
        {
            throw new ArgumentException("The number of values in row does not match columns count.");
        }

        _rows.Add(values);
    }

    private List<int> GetColumnsMaximumStringLengths()
    {
        List<int> columnsLength = [];

        for (int i = 0; i < _columns.Count; i++)
        {
            List<object> columnRow = [];
            int max = 0;

            columnRow.Add(_columns[i]);

            for (int j = 0; j < _rows.Count; j++)
            {
                columnRow.Add(_rows[j][i]);
            }

            for (int n = 0; n < columnRow.Count; n++)
            {
                int len = columnRow[n].ToString().Length;

                if (len > max)
                {
                    max = len;
                }
            }

            columnsLength.Add(max);
        }

        return columnsLength;
    }

    public override string ToString()
    {
        StringBuilder tableString = new();
        List<int> columnsLength = GetColumnsMaximumStringLengths();

        var rowStringFormat = Enumerable
            .Range(0, _columns.Count)
            .Select(i => " | {" + i + ",-" + columnsLength[i] + "}")
            .Aggregate((total, nextValue) => total + nextValue) + " |";

        string columnHeaders = string.Format(rowStringFormat, _columns.ToArray());
        List<string> results = _rows.Select(row => string.Format(rowStringFormat, row)).ToList();

        int maximumRowLength = Math.Max(0, _rows.Count != 0 ? _rows.Max(row => string.Format(rowStringFormat, row).Length) : 0);
        int maximumLineLength = Math.Max(maximumRowLength, columnHeaders.Length);

        string dividerLine = string.Join("", Enumerable.Repeat("-", maximumLineLength - 1));
        string divider = $" {dividerLine} ";

        tableString.AppendLine(divider);
        tableString.AppendLine(columnHeaders);

        foreach (var row in results)
        {
            tableString.AppendLine(divider);
            tableString.AppendLine(row);
        }

        tableString.AppendLine(divider);

        return tableString.ToString();
    }
}