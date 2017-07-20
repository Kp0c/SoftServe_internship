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

std::vector<int> Split(std::string numbers, std::vector<std::string>& delimiters)
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
    std::string delimiterString = string.substr(2, string.find("\\n") - 2);

    if (delimiterString.length() != 1)
    {
        std::vector<std::string> delimiters;

        int startOfGroupPosition = std::string::npos;
        int endOfGroupPosition = std::string::npos;

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
        return {delimiterString};
    }
}

int StringCalculator::Add(std::string numbers)
{
    if (numbers.empty())
    {
        return 0;
    }

    std::vector<std::string> delimiters;

    if (numbers.compare(0, 2, "//") == 0)
    {
        delimiters = GetDelimiters(numbers);
        numbers = numbers.substr(numbers.find("\\n") + 2);
    }
    else
    {
        delimiters.push_back(",");
    }

    delimiters.push_back("\\n");

    std::vector<int> splittedNumbers = Split(numbers, delimiters);

    std::string negatives;
    int sum = 0;
    for (int i : splittedNumbers)
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
        std::string exception_message = "negatives not allowed: " + negatives.substr(0, negatives.length() - 2);
        TryLog(exception_message);
        throw std::invalid_argument(exception_message);
    }

    TryLog(std::to_string(sum));
    return sum;
}

void StringCalculator::Notify(std::string& text)
{
    for (IObserver* obs : observers)
    {
        obs->HandleEvent(text);
    }
}

void StringCalculator::AddObserver(IObserver& observer)
{
    observers.push_back(&observer);
}

void StringCalculator::RemoveObserver(IObserver& observer)
{
    observers.remove(&observer);
}

void StringCalculator::SetLogger(std::shared_ptr<ILogger>& logger)
{
    logger_ = logger;
}

void StringCalculator::TryLog(std::string& text)
{
    if (logger_)
    {
        try
        {
            std::cout << std::endl << text << std::endl;
            logger_->Write(text);
        }
        catch (std::exception& ex)
        {
            Notify(std::string(ex.what()));
        }
    }
}