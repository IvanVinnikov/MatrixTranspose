using MatrixTranspose;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MatrixTranspose.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void SetNValue_SetNWithWrongValue_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            Matrix matrix = new Matrix();
            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => matrix.N = -1);
        }

        [TestMethod()]
        public void SetNValue_SetNWithCorrectValue()
        {
            // Arrange
            Matrix matrix = new Matrix();
            // Act
            matrix.N = 5;
            // Assert          
            Assert.AreEqual(5, matrix.N);
        }

        [TestMethod()]
        public void SetMValue_SetMWithWrongValue_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            Matrix matrix = new Matrix();
            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => matrix.M = -1);
        }

        [TestMethod()]
        public void SetMValue_SetMWithCorrectValue()
        {
            // Arrange
            Matrix matrix = new Matrix();
            // Act
            matrix.M = 3;
            // Assert          
            Assert.AreEqual(3, matrix.M);
        }

        [TestMethod()]
        public void SetListOfTuples_SetListWithEmptyValue_ThrowsArgumentNullException()
        {
            // Arrange
            Matrix matrix = new Matrix();
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => matrix.NonZeroElements = null);
        }

        [TestMethod()]
        public void SetListOfTuplesValue_SetListWithCorrectValue()
        {
            // Arrange
            Matrix matrix = new Matrix(2, 2);
            List<Tuple<int, int, double>> testList = new List<Tuple<int, int, double>>
            {
                new Tuple<int, int, double>(0, 0, 1.56),
                new Tuple<int, int, double>(1, 1, 2.5)
            };
            // Act
            matrix.NonZeroElements = testList;
            // Assert
            Assert.AreEqual(testList, matrix.NonZeroElements);
        }

        [TestMethod()]
        public void SetElemPosition_SetElemPositionValueWithWrongValue_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            Matrix matrix = new Matrix(3, 3, new List<Tuple<int, int, double>>());
            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                matrix.NonZeroElements = new List<Tuple<int, int, double>> { new Tuple<int, int, double>(3, 0, 1.0) });
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                matrix.NonZeroElements = new List<Tuple<int, int, double>> { new Tuple<int, int, double>(0, 3, 1.0) });
        }

        [TestMethod()]
        public void SetElemPosition_SetElemPositionValueWithNegativeValue_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            Matrix matrix = new Matrix(2, 2, new List<Tuple<int, int, double>>());
            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                matrix.NonZeroElements = new List<Tuple<int, int, double>> { new Tuple<int, int, double>(-1, -1, 1.0) });
        }

        [TestMethod()]
        public void SetElemPosition_SetElemPositionValueWithCorrectValue()
        {
            // Arrange
            Matrix matrix = new Matrix(3, 3, new List<Tuple<int, int, double>>());
            List<Tuple<int, int, double>> validList = new List<Tuple<int, int, double>>
            {
                new Tuple<int, int, double>(0, 0, 1.0),
                new Tuple<int, int, double>(2, 2, 2.5)
            };
            // Act
            matrix.NonZeroElements = validList;
            // Assert
            CollectionAssert.AreEqual(validList, matrix.NonZeroElements);
        }

        [TestMethod()]
        public void AddMatrix_MatrixWithDifferentSizes_TrowsInvalidOperationException()
        {
            // Arrange
            Matrix matrix1 = new Matrix(3, 3, new List<Tuple<int, int, double>> { new Tuple<int, int, double>(0, 0, 1.0) });
            Matrix matrix2 = new Matrix(2, 2, new List<Tuple<int, int, double>> { new Tuple<int, int, double>(0, 0, 1.0) });
            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => matrix1.Add(matrix2));
        }

        [TestMethod()]
        public void AddMatrix_MatrixWithCorrectSize()
        {
            // Arrange
            Matrix matrix1 = new Matrix(2, 2, new List<Tuple<int, int, double>>
            {
                new Tuple<int, int, double>(0, 0, 1.0),
                new Tuple<int, int, double>(1, 1, 2.0)
            });

            Matrix matrix2 = new Matrix(2, 2, new List<Tuple<int, int, double>>
            {
                new Tuple<int, int, double>(0, 0, 3.0),
                new Tuple<int, int, double>(1, 1, 4.0)
            });

            List<Tuple<int, int, double>> expectedElements = new List<Tuple<int, int, double>>
            {
                new Tuple<int, int, double>(0, 0, 4.0),  // 1.0 + 3.0
                new Tuple<int, int, double>(1, 1, 6.0)   // 2.0 + 4.0
            };

            Matrix expectedMatrix = new Matrix(2, 2, expectedElements);
            // Act
            Matrix resultMatrix = (Matrix)matrix1.Add(matrix2);
            // Assert
            Assert.IsTrue(resultMatrix.Equals(expectedMatrix));
        }

        [TestMethod()]
        public void MultyplyMatrix_MatrixWithDifferentSizes_TrowsInvalidOperationException()
        {
            // Arrange
            Matrix matrix1 = new Matrix(3, 2, new List<Tuple<int, int, double>> { new Tuple<int, int, double>(0, 0, 1.0) });
            Matrix matrix2 = new Matrix(3, 3, new List<Tuple<int, int, double>> { new Tuple<int, int, double>(0, 0, 1.0) });
            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => matrix1.Multyply(matrix2));
        }

        [TestMethod()]
        public void MultyplyMatrix_MatrixWithCorrectSize()
        {
            // Arrange
            Matrix matrix1 = new Matrix(2, 3, new List<Tuple<int, int, double>>
            {
                new Tuple<int, int, double>(0, 0, 1.0),
                new Tuple<int, int, double>(0, 1, 2.0),
                new Tuple<int, int, double>(1, 2, 3.0)
            });

            Matrix matrix2 = new Matrix(3, 2, new List<Tuple<int, int, double>>
            {
                new Tuple<int, int, double>(0, 0, 4.0),
                new Tuple<int, int, double>(1, 1, 5.0),
                new Tuple<int, int, double>(2, 0, 6.0)
            });

            List<Tuple<int, int, double>> expectedElements = new List<Tuple<int, int, double>>
            {
                new Tuple<int, int, double>(0, 0, 4.0),  // 1*4 + 2*0 + 0*6
                new Tuple<int, int, double>(0, 1, 10.0),  // 1*0 + 2*5 + 0*0
                new Tuple<int, int, double>(1, 0, 18.0)   // 0*4 + 0*0 + 3*6
            };

            Matrix expectedMatrix = new Matrix(2, 2, expectedElements);
            // Act
            Matrix resultMatrix = (Matrix)matrix1.Multyply(matrix2);
            // Assert
            Assert.IsTrue(resultMatrix.Equals(expectedMatrix));
        }

        [TestMethod()]
        public void MultyplyByNumberMatrix_NumberWithCorrectValue()
        {
            // Arrange
            Matrix matrix = new Matrix(2, 2, new List<Tuple<int, int, double>>
            {
                new Tuple<int, int, double>(0, 0, 1.0),
                new Tuple<int, int, double>(1, 1, 2.0)
            });

            double num = 3.0;

            List<Tuple<int, int, double>> expectedElements = new List<Tuple<int, int, double>>
            {
                new Tuple<int, int, double>(0, 0, 3.0),  // 1.0 * 3
                new Tuple<int, int, double>(1, 1, 6.0)   // 2.0 * 3
            };

            Matrix expectedMatrix = new Matrix(2, 2, expectedElements);
            // Act
            Matrix resultMatrix = (Matrix)matrix.MultyplyByNumber(num);
            // Assert
            Assert.IsTrue(resultMatrix.Equals(expectedMatrix));
        }

        [TestMethod()]
        public void TransposeMatrix_CorrectAnswer1()
        {
            // Arrange
            Matrix matrix = new Matrix(2, 3, new List<Tuple<int, int, double>>
            {
                new Tuple<int, int, double>(0, 0, 1.0),
                new Tuple<int, int, double>(0, 2, 2.0),
                new Tuple<int, int, double>(1, 1, 3.0)
            });

            List<Tuple<int, int, double>> expectedElements = new List<Tuple<int, int, double>>
            {
                new Tuple<int, int, double>(0, 0, 1.0),
                new Tuple<int, int, double>(2, 0, 2.0),
                new Tuple<int, int, double>(1, 1, 3.0)
            };

            Matrix expectedMatrix = new Matrix(3, 2, expectedElements);

            // Act
            Matrix resultMatrix = (Matrix)matrix.Transpose();
            // Assert
            Assert.IsTrue(resultMatrix.Equals(expectedMatrix));
        }

        [TestMethod()]
        public void TransposeMatrix_CorrectAnswer2()
        {
            // Arrange
            Matrix matrix = new Matrix(2, 2, new List<Tuple<int, int, double>>
            {
                new Tuple<int, int, double>(0, 1, 4.0),
                new Tuple<int, int, double>(1, 0, 5.0)
            });

            List<Tuple<int, int, double>> expectedElements = new List<Tuple<int, int, double>>
            {
                new Tuple<int, int, double>(1, 0, 4.0),
                new Tuple<int, int, double>(0, 1, 5.0)
            };

            Matrix expectedMatrix = new Matrix(2, 2, expectedElements);
            // Act
            Matrix resultMatrix = (Matrix)matrix.Transpose();
            // Assert
            Assert.IsTrue(resultMatrix.Equals(expectedMatrix));
        }

        [TestMethod()]
        public void EqualsTest_CorrectResult()
        {
            // Arrange
            Matrix matrix1 = new Matrix(2, 2, new List<Tuple<int, int, double>>
            {
                new Tuple<int, int, double>(0, 0, 1.0),
                new Tuple<int, int, double>(1, 1, 2.0)
            });

            Matrix matrix2 = new Matrix(2, 2, new List<Tuple<int, int, double>>
            {
                new Tuple<int, int, double>(0, 0, 1.0),
                new Tuple<int, int, double>(1, 1, 2.0)
            });
            // Act & Assert
            Assert.IsTrue(matrix1.Equals(matrix2));
        }

        [TestMethod()]
        public void NotEqualsTes_CorrectResult()
        {
            // Arrange
            Matrix matrix1 = new Matrix(2, 2, new List<Tuple<int, int, double>>
            {
                new Tuple<int, int, double>(0, 0, 1.0),
                new Tuple<int, int, double>(1, 1, 2.0)
            });

            Matrix matrix2 = new Matrix(2, 2, new List<Tuple<int, int, double>>
            {
                new Tuple<int, int, double>(0, 0, 1.0),
                new Tuple<int, int, double>(1, 1, 3.0)
            });
            // Act & Assert
            Assert.IsTrue(matrix1.NotEquals(matrix2));
        }

        [TestMethod()]
        public void GetHashCodeTest_CorrectResult()
        {
            // Arrange
            Matrix matrix1 = new Matrix(2, 2, new List<Tuple<int, int, double>>
            {
                new Tuple<int, int, double>(0, 0, 1.0),
                new Tuple<int, int, double>(1, 1, 2.0)
            });

            Matrix matrix2 = new Matrix(2, 2, new List<Tuple<int, int, double>>
            {
                new Tuple<int, int, double>(0, 0, 1.0),
                new Tuple<int, int, double>(1, 1, 2.0)
            });
            // Act & Assert
            Assert.AreEqual(matrix1.GetHashCode(), matrix2.GetHashCode());
        }

        [TestMethod()]
        public void ToStringTest_CorrectResult()
        {
            // Arrange
            Matrix matrix = new Matrix(2, 2, new List<Tuple<int, int, double>>
             {
                new Tuple<int, int, double>(0, 0, 1.5),
                new Tuple<int, int, double>(1, 1, 2.7)
             });
            // Act & Assert
            Assert.AreEqual(matrix.ToString(), "1,5 0 \n0 2,7 \n");
        }
    }
}