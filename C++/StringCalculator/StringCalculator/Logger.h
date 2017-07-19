#pragma once
#include "ILogger.h"
#include "IObserver.h"

class Logger final : public ILogger
{
public:
    void SetOutput(std::ostream& output) override;
    void Write(std::string& text) override;
};