namespace WebAppNea;
using Microsoft.ML.Data;

public class ModelOutputClassification
{
    [ColumnName("Score")]
    public string Direction;
}