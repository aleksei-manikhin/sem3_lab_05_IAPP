using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinearAlgebra;

public class MathVector : IMathVector, IEnumerable<double>
{
    private readonly double[] coordinates;

    public MathVector(IEnumerable coordinates)
    {
        ArgumentNullException.ThrowIfNull(coordinates);
        this.coordinates = coordinates.Cast<double>().ToArray();
    }

    public int Dimensions => coordinates.Length;

    public double this[int i]
    {
        get => coordinates[i];
        set => coordinates[i] = value;
    }

    public double Length => Math.Sqrt(ScalarMultiply(this));

    public IMathVector SumNumber(double number) =>
        new MathVector(coordinates.Select(value => value + number));

    public IMathVector MultiplyNumber(double number) =>
        new MathVector(coordinates.Select(value => value * number));

    public IMathVector DivideNumber(double number)
    {
        CheckDivisor(number);
        return new MathVector(coordinates.Select(value => value / number));
    }

    public IMathVector Sum(IMathVector vector)
    {
        CheckDimensions(vector);
        return new MathVector(coordinates.Select((value, i) => value + vector[i]));
    }

    public IMathVector Multiply(IMathVector vector)
    {
        CheckDimensions(vector);
        return new MathVector(coordinates.Select((value, i) => value * vector[i]));
    }

    public IMathVector Divide(IMathVector vector)
    {
        CheckDimensions(vector);
        return new MathVector(coordinates.Select((value, i) =>
        {
            double divisor = vector[i];
            CheckDivisor(divisor);
            return value / divisor;
        }));
    }

    public double ScalarMultiply(IMathVector vector)
    {
        CheckDimensions(vector);
        double sum = 0;
        for (int i = 0; i < Dimensions; i++)
            sum += coordinates[i] * vector[i];

        return sum;
    }

    public double CalcDistance(IMathVector vector)
    {
        CheckDimensions(vector);
        double sum = 0;
        for (int i = 0; i < Dimensions; i++)
        {
            double difference = coordinates[i] - vector[i];
            sum += difference * difference;
        }

        return Math.Sqrt(sum);
    }

    private void CheckDimensions(IMathVector vector)
    {
        ArgumentNullException.ThrowIfNull(vector);
        if (Dimensions != vector.Dimensions)
            throw new ArgumentException("Размерности векторов должны совпадать.", nameof(vector));
    }

    private static void CheckDivisor(double divisor)
    {
        if (divisor == 0)
            throw new DivideByZeroException("Делитель не может быть равен нулю.");
    }

    public IEnumerator<double> GetEnumerator() =>
        ((IEnumerable<double>)coordinates).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static IMathVector operator +(MathVector a, double number) => a.SumNumber(number);
    public static IMathVector operator +(MathVector a, IMathVector b) => a.Sum(b);
    public static IMathVector operator -(MathVector a, double number) => a.SumNumber(-number);
    public static IMathVector operator -(MathVector a, IMathVector b)
    {
        a.CheckDimensions(b);
        return a.Sum(b.MultiplyNumber(-1));
    }

    public static IMathVector operator *(MathVector a, double number) => a.MultiplyNumber(number);
    public static IMathVector operator *(MathVector a, IMathVector b) => a.Multiply(b);
    public static IMathVector operator /(MathVector a, double number) => a.DivideNumber(number);
    public static IMathVector operator /(MathVector a, IMathVector b) => a.Divide(b);
    public static double operator %(MathVector a, IMathVector b) => a.ScalarMultiply(b);
}
