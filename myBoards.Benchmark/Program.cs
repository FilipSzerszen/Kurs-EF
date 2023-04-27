// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using myBoards.Benchmark;

Console.WriteLine("Hello, World!");
BenchmarkRunner.Run<TrackingBenchmark>();