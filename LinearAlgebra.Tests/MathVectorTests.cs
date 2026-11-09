using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LinearAlgebra.Tests;

public class MathVectorTests
{
    private static MathVector Vector(params double[] coordinates) => new(coordinates);

    [Fact]
    public void Constructor_Array_CopiesCoordinates()
    {
        double[] source = { 2, -3 };
        var vector = new MathVector(source);
        source[0] = 9;
        Assert.Equal(new double[] { 2, -3 }, vector.ToArray());
    }

    [Fact]
    public void Constructor_List_PreservesCoordinateOrder()
    {
        IMathVector vector = new MathVector(new List<double> { 3, -1 });
        Assert.Equal(new double[] { 3, -1 }, vector.Cast<double>().ToArray());
    }

    [Fact]
    public void Constructor_Vector_CreatesIndependentCopy()
    {
        IMathVector source = Vector(2);
        var copy = new MathVector(source);
        copy[0] = 5;
        Assert.Equal(2, source[0]);
    }

    [Fact]
    public void Constructor_Null_ThrowsArgumentNullException() =>
        Assert.Throws<ArgumentNullException>(() => new MathVector(null!));

    [Fact]
    public void Constructor_NonDoubleCoordinate_ThrowsInvalidCastException() =>
        Assert.Throws<InvalidCastException>(() => new MathVector(new[] { "abc" }));

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(3)]
    public void Dimensions_EqualsCoordinateCount(int count) =>
        Assert.Equal(count, Vector(new double[count]).Dimensions);

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void Indexer_ValidIndex_UpdatesSelectedCoordinate(int index)
    {
        var vector = Vector(1, 2, 3);
        vector[index] = 7;
        Assert.Equal(7, vector[index]);
        double[] expected = { 1, 2, 3 };
        expected[index] = 7;
        Assert.Equal(expected, vector.ToArray());
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(3)]
    public void Indexer_OutsideBounds_RejectsReadAndWrite(int index)
    {
        var vector = Vector(1, 2, 3);
        Assert.Throws<IndexOutOfRangeException>(() => { _ = vector[index]; });
        Assert.Throws<IndexOutOfRangeException>(() => { vector[index] = 7; });
    }

    [Theory]
    [InlineData(0, new double[] { })]
    [InlineData(0, new double[] { 0, 0 })]
    [InlineData(5, new double[] { -3, 4 })]
    public void Length_ReturnsEuclideanNorm(double expected, double[] coordinates) =>
        Assert.Equal(expected, Vector(coordinates).Length);

    // Операторы вызывают методы интерфейса. Эталон вычислен независимо от обоих.
    [Theory]
    [InlineData('+', -4, 6)]
    [InlineData('-', -8, 2)]
    [InlineData('*', -12, 8)]
    [InlineData('/', -3, 2)]
    public void ScalarOperator_ReturnsNewVectorWithExpectedCoordinates(
        char operation, double first, double second)
    {
        var a = Vector(-6, 4);
        IMathVector result = operation switch
        {
            '+' => a + 2,
            '-' => a - 2,
            '*' => a * 2,
            '/' => a / 2,
            _ => throw new ArgumentOutOfRangeException(nameof(operation))
        };
        Assert.Equal(new[] { first, second }, result.Cast<double>().ToArray());
        result[0] = 99;
        Assert.Equal(new double[] { -6, 4 }, a.ToArray());
    }

    [Theory]
    [InlineData('+', -4, 2)]
    [InlineData('-', -8, 6)]
    [InlineData('*', -12, -8)]
    [InlineData('/', -3, -2)]
    public void VectorOperator_EqualDimensions_ReturnsIndependentResult(
        char operation, double first, double second)
    {
        var a = Vector(-6, 4);
        var b = Vector(2, -2);
        IMathVector result = operation switch
        {
            '+' => a + b,
            '-' => a - b,
            '*' => a * b,
            '/' => a / b,
            _ => throw new ArgumentOutOfRangeException(nameof(operation))
        };
        Assert.Equal(new[] { first, second }, result.Cast<double>().ToArray());
        result[0] = 99;
        Assert.Equal(new double[] { -6, 4 }, a.ToArray());
        Assert.Equal(new double[] { 2, -2 }, b.ToArray());
    }

    [Fact]
    public void ScalarProductOperator_ReturnsSumOfCoordinateProducts() =>
        Assert.Equal(-20, Vector(-6, 4) % Vector(2, -2));

    [Theory]
    [InlineData(1, 2, 0)]
    [InlineData(-2, 6, 5)]
    public void CalcDistance_ReturnsEuclideanDistance(double x, double y, double expected) =>
        Assert.Equal(expected, Vector(1, 2).CalcDistance(Vector(x, y)));

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public void BinaryMethods_UnequalDimensions_ThrowArgumentException(int otherDimensions)
    {
        var a = Vector(1, 2);
        var b = Vector(Enumerable.Repeat(1d, otherDimensions).ToArray());
        Assert.Throws<ArgumentException>(() => a.Sum(b));
        Assert.Throws<ArgumentException>(() => a.Multiply(b));
        Assert.Throws<ArgumentException>(() => a.Divide(b));
        Assert.Throws<ArgumentException>(() => a.ScalarMultiply(b));
        Assert.Throws<ArgumentException>(() => a.CalcDistance(b));
    }

    [Fact]
    public void BinaryMethods_NullOperand_ThrowArgumentNullException()
    {
        var a = Vector(1);
        Assert.Throws<ArgumentNullException>(() => a.Sum(null!));
        Assert.Throws<ArgumentNullException>(() => a.Multiply(null!));
        Assert.Throws<ArgumentNullException>(() => a.Divide(null!));
        Assert.Throws<ArgumentNullException>(() => a.ScalarMultiply(null!));
        Assert.Throws<ArgumentNullException>(() => a.CalcDistance(null!));
    }

    [Fact]
    public void DivideNumber_ZeroDivisor_ThrowsDivideByZeroException() =>
        Assert.Throws<DivideByZeroException>(() => Vector(1).DivideNumber(0));

    [Fact]
    public void Divide_ZeroInDivisorVector_ThrowsDivideByZeroException() =>
        Assert.Throws<DivideByZeroException>(() => Vector(3, 4).Divide(Vector(2, 0)));
}
