#pragma once
#include <memory>
#include <string>
#include <iostream>

class ILogger
{
public:
    virtual void SetOutput(std::ostream& output) = 0;
    virtual void Write(std::string& text) = 0;

    virtual ~ILogger()
    { }

protected:
    ILogger() : output_(std::cout.rdbuf())
    { }

    ILogger(ILogger& logger) : output_(logger.output_.rdbuf())
    { }

    std::ostream output_;
};