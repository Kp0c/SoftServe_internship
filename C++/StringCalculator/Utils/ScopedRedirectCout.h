#pragma once
#include <iostream>
#include <memory>

class ScopedRedirectCout
{
public:
    ScopedRedirectCout(std::ostream& redirectTo) 
        : original_(std::cout.rdbuf(redirectTo.rdbuf()))
    { }

    ~ScopedRedirectCout()
    {
        std::cout.rdbuf(original_);
    }

private:
    std::streambuf* original_;
};