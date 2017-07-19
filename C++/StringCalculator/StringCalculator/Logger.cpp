#include "Logger.h"

void Logger::SetOutput(std::ostream& output)
{
    output_.set_rdbuf(output.rdbuf());
}

void Logger::Write(std::string& text)
{
    output_ << text;
}
