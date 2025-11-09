namespace WebAppNea;
using Microsoft.ML.Data;

public class ModelOutputRegression
{
        [ColumnName("Score")]
        public float ClosingPrice;
}