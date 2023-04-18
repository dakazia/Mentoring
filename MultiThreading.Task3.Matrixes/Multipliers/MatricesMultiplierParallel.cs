using MultiThreading.Task3.MatrixMultiplier.Matrices;
using System.Threading.Tasks;
using System;

namespace MultiThreading.Task3.MatrixMultiplier.Multipliers
{
    public class MatricesMultiplierParallel : IMatricesMultiplier
    {
        public IMatrix Multiply(IMatrix m1, IMatrix m2)
        {
            var rowCount = m1.RowCount;
            var colCount = m2.ColCount;
            var commonDim = m1.ColCount;

            IMatrix result = new Matrix(rowCount, colCount);

            Parallel.For(0, rowCount, i =>
            {
                for (byte j = 0; j < colCount; j++)
                {
                    long sum = 0;
                    for (byte k = 0; k < commonDim; k++)
                    {
                        sum += m1.GetElement(i, k) * m2.GetElement(k, j);
                    }

                    result.SetElement(i, j, sum);
                }
            });

            return result;
        }
    }
}
