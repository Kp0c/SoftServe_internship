#pragma once
#include "ILogger.h"
#include "IObserver.h"
#include <list>

class IObservable
{
public:
    virtual void Notify(std::string& text) = 0;
    virtual void AddObserver(IObserver& observer) = 0;
    virtual void RemoveObserver(IObserver& observer) = 0;

    virtual ~IObservable()
    { }

protected:
    std::list<IObserver*> observers;
};