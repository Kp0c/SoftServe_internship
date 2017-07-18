#include "StringCalculator.h"
#include <vector>

int PositionOfFirstDelimiter(std::string string, std::vector<std::string> delimiters)
{
    size_t minPos = std::string::npos;
    size_t pos = std::string::npos;
    for (std::string delim : delimiters)
    {
        pos = string.find(delim);
        if (pos != std::string::npos && (pos < minPos || minPos == std::string::npos))
        {
            minPos = pos;
        }
    }

    return minPos;
}

std::vector<int> Split(std::string numbers, std::vector<std::string> delimiters)
{
    std::vector<int> result;

    size_t pos = 0;
    while ((pos = PositionOfFirstDelimiter(numbers, delimiters)) != std::string::npos)
    {
        result.push_back(std::stoi(numbers.substr(0, pos)));
        numbers.erase(0, pos + 1);
    }

    if (numbers.length() > 0)
        result.push_back(std::stoi(numbers));

    return result;
}


int Add(std::string numbers)
{
    if (numbers.empty())
    {
        return 0;
    }

    std::vector<std::string> delimiters{ "\n", "," };
    std::vector<int> numbersV = Split(numbers, delimiters);

    int sum = 0;
    for (int i : numbersV)
    {
        sum += i;
    }

    return sum;
}
