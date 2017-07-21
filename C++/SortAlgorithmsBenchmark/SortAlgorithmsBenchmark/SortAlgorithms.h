#pragma once
#include <algorithm>
#include <thread>

void Swap(int& a, int& b)
{
    int c = a;
    a = b;
    b = c;
}

void BubbleSort(int count, int* arr)
{
    bool swappped = false;
    for (int i = 0; i < count; ++i)
    {
        swappped = false;

        for (int j = count - 1; j > i; --j)
        {
            if (arr[j] < arr[j - 1])
            {
                Swap(arr[j], arr[j - 1]);
                swappped = true;
            }
        }

        if (!swappped) break;
    }
}

void Merge(int* a, int low, int high, int mid)
{
    int i, j, k;
    int* temp = new int[high - low + 1];
    i = low;
    k = 0;
    j = mid + 1;

    while (i <= mid && j <= high)
    {
        if (a[i] < a[j])
        {
            temp[k] = a[i];
            k++;
            i++;
        }
        else
        {
            temp[k] = a[j];
            k++;
            j++;
        }
    }

    while (i <= mid)
    {
        temp[k] = a[i];
        k++;
        i++;
    }

    while (j <= high)
    {
        temp[k] = a[j];
        k++;
        j++;
    }

    for (i = low; i <= high; i++)
    {
        a[i] = temp[i - low];
    }

    delete[] temp;
}

void MergeSort(int* a, int low, int high)
{
    if (low < high)
    {
        int mid = (low + high) / 2;

        MergeSort(a, low, mid);
        MergeSort(a, mid + 1, high);

        Merge(a, low, high, mid);
    }
}

int Partition(int* arr, const int left, const int right)
{
    const int mid = left + (right - left) / 2;
    const int pivot = arr[mid];

    std::swap(arr[mid], arr[left]);
    int i = left + 1;
    int j = right;
    while (i <= j) {
        while (i <= j && arr[i] <= pivot) {
            i++;
        }

        while (i <= j && arr[j] > pivot) {
            j--;
        }

        if (i < j) {
            std::swap(arr[i], arr[j]);
        }
    }
    std::swap(arr[i - 1], arr[left]);
    return i - 1;
}

void QuickSort(int* arr, const int left, const int right)
{
    if (left >= right) {
        return;
    }

    int part = Partition(arr, left, right);

    QuickSort(arr, left, part - 1);
    QuickSort(arr, part + 1, right);
}

void QuickSortWithThreads(int* arr, const int left, const int right, const int size)
{
    if (left >= right) {
        return;
    }

    int part = Partition(arr, left, right);

    if (part - left > size/10)
    {
        std::thread t2(QuickSortWithThreads, arr, left, part - 1, size);
        QuickSortWithThreads(arr, part + 1, right, size);
        t2.join();
    }
    else
    {
        QuickSortWithThreads(arr, left, part - 1, size);
        QuickSortWithThreads(arr, part + 1, right, size);
    }
}

int Compare(const void* a, const void* b)
{
    return (*(int*)a - *(int*)b);
}