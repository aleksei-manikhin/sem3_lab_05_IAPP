using System;
using System.Collections.Generic;
using System.Linq;
using LinearAlgebra;

IMathVector a = new MathVector(new double[] { -7.5, 12, 4.5 });
IMathVector b = new MathVector(new List<double> { 2.5, -3, 1.5 });
IMathVector copy = new MathVector(a);
double number = -2.5;

Console.WriteLine($"Вектор a из массива = ({string.Join("; ", a.Cast<double>())})");
Console.WriteLine($"Вектор b из списка = ({string.Join("; ", b.Cast<double>())})");
copy[0] = 18.75;
Console.WriteLine($"Копия a после изменения первой координаты = ({string.Join("; ", copy.Cast<double>())})");
Console.WriteLine($"У исходного a первая координата равна {a[0]}");
Console.WriteLine($"Размерность a = {a.Dimensions}, длина a = {a.Length:0.###}");
Console.WriteLine($"Число для операций = {number}");

Console.WriteLine($"a.SumNumber({number}) = ({string.Join("; ", a.SumNumber(number).Cast<double>())})");
Console.WriteLine($"a.MultiplyNumber({number}) = ({string.Join("; ", a.MultiplyNumber(number).Cast<double>())})");
Console.WriteLine($"a.DivideNumber({number}) = ({string.Join("; ", a.DivideNumber(number).Cast<double>())})");
Console.WriteLine($"a.Sum(b) = ({string.Join("; ", a.Sum(b).Cast<double>())})");
Console.WriteLine($"a.Multiply(b) = ({string.Join("; ", a.Multiply(b).Cast<double>())})");
Console.WriteLine($"a.Divide(b) = ({string.Join("; ", a.Divide(b).Cast<double>())})");
Console.WriteLine($"a.ScalarMultiply(b) = {a.ScalarMultiply(b)}");
Console.WriteLine($"a.CalcDistance(b) = {a.CalcDistance(b):0.###}");

Console.WriteLine($"a + ({number}) = ({string.Join("; ", ((MathVector)a + number).Cast<double>())})");
Console.WriteLine($"a - ({number}) = ({string.Join("; ", ((MathVector)a - number).Cast<double>())})");
Console.WriteLine($"a * ({number}) = ({string.Join("; ", ((MathVector)a * number).Cast<double>())})");
Console.WriteLine($"a / ({number}) = ({string.Join("; ", ((MathVector)a / number).Cast<double>())})");
Console.WriteLine($"a + b = ({string.Join("; ", ((MathVector)a + b).Cast<double>())})");
Console.WriteLine($"a - b = ({string.Join("; ", ((MathVector)a - b).Cast<double>())})");
Console.WriteLine($"a * b = ({string.Join("; ", ((MathVector)a * b).Cast<double>())})");
Console.WriteLine($"a / b = ({string.Join("; ", ((MathVector)a / b).Cast<double>())})");
Console.WriteLine($"a % b = {(MathVector)a % b}");

try
{
    a.DivideNumber(0);
}
catch (DivideByZeroException error)
{
    Console.WriteLine($"Попытка деления на ноль: {error.Message}");
}
