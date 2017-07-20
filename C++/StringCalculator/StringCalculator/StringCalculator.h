#pragma once
#include <string>
#include <memory>
#include "IObservable.h"

class StringCalculator final : public IObservable
{
public:
    int Add(std::string numbers);

    void Notify(std::string& text) override;
    void AddObserver(IObserver& observer) override;
    void RemoveObserver(IObserver& observer) override;

    void SetLogger(std::shared_ptr<ILogger>& logger);
private:
    void TryLog(std::string& text);

    std::shared_ptr<ILogger> logger_;
};