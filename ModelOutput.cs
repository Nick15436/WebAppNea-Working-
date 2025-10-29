namespace WebAppNea;
using Microsoft.ML.Data;

public class ModelOutput
{
        [ColumnName("Score")]
        public float ClosingPrice;
}