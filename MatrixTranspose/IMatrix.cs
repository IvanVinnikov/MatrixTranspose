using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTranspose
{
    public interface IMatrix
    {
        int N { get; set; }
        int M { get; set; }
        List<Tuple<int, int, double>> NonZeroElements { get; set; }
        IMatrix Add(IMatrix matrix);
        IMatrix Multyply(IMatrix matrix);
        IMatrix MultyplyByNumber(double num);
        IMatrix Transpose();
    }
}
