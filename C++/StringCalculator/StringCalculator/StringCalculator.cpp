#include "StringCalculator.h"
#include "StringCalculatorHelper.h"
#include <vector>
#include <exception>

DelimiterAppearance NextDelimiter(std::string string, std::vector<std::string> delimiters)
{
    DelimiterAppearance minPosDelimiter{ std::string::npos, 0 };
    size_t currentPosition = std::string::npos;
    for (std::string delimiter : delimiters)
    {
        currentPosition = string.find(delimiter);
        if (currentPosition != std::string::npos && currentPosition < minPosDelimiter.position)
        {
            minPosDelimiter.position = currentPosition;
            minPosDelimiter.length = delimiter.length();
        }
    }

    return minPosDelimiter;
}

std::vector<int> Split(std::string numbers, std::vector<std::string> delimiters)
{
    std::vector<std::string> splittedNumbers;

    DelimiterAppearance appearance;
    while ((appearance = NextDelimiter(numbers, delimiters)).position != std::string::npos)
    {
        if (std::stoi(numbers.substr(0, appearance.position)) < MAX_NUMBER)
        {
            splittedNumbers.push_back(numbers.substr(0, appearance.position));
        }

        numbers.erase(0, appearance.position + appearance.length);
    }

    if (numbers.length() > 0)
    {
        splittedNumbers.push_back(numbers);
    }

    std::vector<int> numbersVector;
    for (std::string str : splittedNumbers)
    {
        numbersVector.push_back(std::stoi(str));
    }

    return numbersVector;
}

std::vector<std::string> GetDelimiters(std::string string)
{
    std::string delimiterString = string.substr(2, string.find("\n") - 2);

    if (delimiterString.length() != 1)
    {
        std::vector<std::string> delimiters;
        int startOfGroupPosition = std::string::npos;
        int endOfGroupPosition;
        while ((startOfGroupPosition = delimiterString.find("[")) != std::string::npos)
        {
            startOfGroupPosition++;
            endOfGroupPosition = delimiterString.find("]");

            delimiters.push_back(delimiterString.substr(startOfGroupPosition, endOfGroupPosition - startOfGroupPosition));

            delimiterString = delimiterString.substr(endOfGroupPosition + 1);
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
    int sum = 0;
    for (int i : numbersV)
    {
        if (i < 0)
        {
            negatives += std::to_string(i) + ", ";
        }
        else
        {
            sum += i;
        }
    }

    if (!negatives.empty())
    {
        throw std::invalid_argument("negatives not allowed: " + negatives.substr(0, negatives.length() - 2));
    }
    else
    {
        return sum;
    }
}
