#include "StringCalculator.h"
#include <vector>
#include <exception>

struct DelimiterAppearance
{
    size_t pos;
    int length;
};

DelimiterAppearance PositionOfFirstDelimiter(std::string string, std::vector<std::string> delimiters)
{
    DelimiterAppearance minPosDelimiter{ std::string::npos, 0 };
    size_t pos = std::string::npos;
    for (std::string delim : delimiters)
    {
        pos = string.find(delim);
        if (pos != std::string::npos && (pos < minPosDelimiter.pos || minPosDelimiter.pos == std::string::npos))
        {
            minPosDelimiter.pos = pos;
            minPosDelimiter.length = delim.length();
        }
    }

    return minPosDelimiter;
}

std::vector<int> Split(std::string numbers, std::vector<std::string> delimiters)
{
    std::vector<int> result;

    DelimiterAppearance pos;
    while ((pos = PositionOfFirstDelimiter(numbers, delimiters)).pos != std::string::npos)
    {
        if (std::stoi(numbers.substr(0, pos.pos)) < 1000)
        {
            result.push_back(std::stoi(numbers.substr(0, pos.pos)));
        }

        numbers.erase(0, pos.pos + pos.length);
    }

    int number = 0;
    if (numbers.length() > 0)
    {
        number = std::stoi(numbers);
        if (number < 1000)
        {
            result.push_back(number);
        }
    }

    return result;
}

std::vector<std::string> GetDelimiters(std::string string)
{
    std::string delimiterString = string.substr(2, string.find("\n") - 2);

    if (delimiterString.length() != 1)
    {
        std::vector<std::string> delimiters;
        int pos = std::string::npos;
        while ((pos = delimiterString.find("[")) != std::string::npos)
        {
            delimiters.push_back(delimiterString.substr(pos + 1, delimiterString.find("]")-pos-1));
            delimiterString = delimiterString.substr(delimiterString.find("]") + 1);
        }

        return delimiters;
    }
    else
    {
        return{ delimiterString };
    }
}

int Add(std::string numbers)
{
    if (numbers.empty())
    {
        return 0;
    }

    std::vector<std::string> delimiters;

    if (numbers.find("//") == 0)
    {
        delimiters = GetDelimiters(numbers);
        numbers = numbers.substr(numbers.find("\n") + 1);
    }
    else
    {
        delimiters.push_back(",");
    }

    delimiters.push_back("\n");

    std::vector<int> numbersV = Split(numbers, delimiters);

    std::string negatives;
    for (int i : numbersV)
    {
        if (i < 0)
        {
            negatives += std::to_string(i) + ", ";
        }
    }

    if (!negatives.empty())
    {
        throw std::invalid_argument("negatives not allowed: " + negatives.substr(0, negatives.length() - 2));
    }
    else
    {
        int sum = 0;
        for (int i : numbersV)
        {
            sum += i;
        }

        return sum;
    }
}
