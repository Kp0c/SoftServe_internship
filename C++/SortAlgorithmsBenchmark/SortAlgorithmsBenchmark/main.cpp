#include "SortAlgorithms.h"
#include <iostream>
#include <chrono>
#include <vector>
#include <string>
#include <functional>

constexpr int COUNT = 50000;

void benchMark(std::function<void(int* arr)>& func, char* name, int* sampleArr)
{
    auto start = std::chrono::steady_clock::now();

    int* arr = new int[COUNT];
    memcpy(arr, sampleArr, COUNT * sizeof(int));

    func(arr);

    auto end = std::chrono::steady_clock::now();
    auto diff = end - start;

    std::cout << name << std::endl;
    std::cout << std::chrono::duration<double, std::milli>(diff).count() << "ms" << std::endl;

    delete[] arr;
}

void BenchAll(int* sampleArr)
{
    std::function<void(int* arr)> bubbleSort(std::bind(BubbleSort, COUNT, std::placeholders::_1));
    std::function<void(int* arr)> mergeSort(std::bind(MergeSort, std::placeholders::_1, 0, COUNT - 1));
    std::function<void(int* arr)> stdQuickSort(std::bind(qsort, std::placeholders::_1, COUNT, sizeof(int), Compare));
    std::function<void(int* arr)> quickSort(std::bind(QuickSort, std::placeholders::_1, 0, COUNT - 1));
    std::function<void(int* arr)> quickSortWithThreads(std::bind(QuickSortWithThreads, std::placeholders::_1, 0, COUNT - 1, COUNT));

    if (COUNT <= 10000)
    {
        benchMark(bubbleSort, "bubble sort", sampleArr);
    }
    benchMark(mergeSort, "merge sort", sampleArr);
    benchMark(stdQuickSort, "std quick sort", sampleArr);
    benchMark(quickSort, "quick sort", sampleArr);
    benchMark(quickSortWithThreads, "quick sort with threads", sampleArr);
}

int main()
{
    int sampleArr[COUNT];

    srand(time(nullptr));

    for (int i = 0; i < COUNT; i++)
    {
        sampleArr[i] = rand()%1000;
    }

    BenchAll(sampleArr);
}
