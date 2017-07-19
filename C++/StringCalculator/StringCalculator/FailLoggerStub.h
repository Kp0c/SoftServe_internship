#pragma once
#include "ILogger.h"
#include "IObservable.h"
#include <exception>

class FailLoggerStub final : public ILogger
{
public:
    void SetOutput(std::ostream& output) override
    { }

    void Write(std::string& text) override
    {
        throw std::exception("Stub exception.");
    }
};