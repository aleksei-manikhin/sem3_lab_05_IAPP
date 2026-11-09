using System;
using System.Collections.Generic;
using System.Linq;
using LinearAlgebra;

IMathVector a = new MathVector(new double[] { 3, 4 });
IMathVector b = new MathVector(new List<double> { 1, 2 });
IMathVector copy = new MathVector(a);

Show("Вектор a из массива", a);
Show("Вектор b из списка", b);
copy[0] = 10;
Show("Копия a после изменения первой координаты", copy);
Console.WriteLine($"У исходного a первая координата равна {a[0]}");
Console.WriteLine($"Размерность a = {a.Dimensions}, длина a = {a.Length}");

Show("a.SumNumber(2)", a.SumNumber(2));
Show("a.MultiplyNumber(2)", a.MultiplyNumber(2));
Show("a.DivideNumber(2)", a.DivideNumber(2));
Show("a.Sum(b)", a.Sum(b));
Show("a.Multiply(b)", a.Multiply(b));
Show("a.Divide(b)", a.Divide(b));
Console.WriteLine($"a.ScalarMultiply(b) = {a.ScalarMultiply(b)}");
Console.WriteLine($"a.CalcDistance(b) = {a.CalcDistance(b):0.###}");

Show("a + 2", (MathVector)a + 2);
Show("a - 2", (MathVector)a - 2);
Show("a * 2", (MathVector)a * 2);
Show("a / 2", (MathVector)a / 2);
Show("a + b", (MathVector)a + b);
Show("a - b", (MathVector)a - b);
Show("a * b", (MathVector)a * b);
Show("a / b", (MathVector)a / b);
Console.WriteLine($"a % b = {(MathVector)a % b}");

try
{
    a.DivideNumber(0);
}
catch (DivideByZeroException error)
{
    Console.WriteLine($"Попытка деления на ноль: {error.Message}");
}

static void Show(string label, IMathVector vector)
{
    Console.WriteLine($"{label} = ({string.Join("; ", vector.Cast<double>())})");
}
