#pragma once
#include <memory>
#include <string>
#include <iostream>

class ILogger
{
public:
    ILogger() : output_(std::cout.rdbuf()) 
    { }

    ILogger(ILogger& logger) : output_(logger.output_.rdbuf())
    { }

    virtual void SetOutput(std::ostream& output) = 0;
    virtual void Write(std::string& text) = 0;

    virtual ~ILogger()
    { }

protected:
    std::ostream output_;
};