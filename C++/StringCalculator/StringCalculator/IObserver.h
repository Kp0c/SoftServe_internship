#pragma once
#include <string>

class IObserver
{
public:
    virtual void HandleEvent(std::string& text) = 0;

    virtual ~IObserver() 
    { }
};