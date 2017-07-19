#pragma once
#include "IObserver.h"

class WebServiceMock final : public IObserver
{
public:
    std::string GetTextOnService() const;

private:
    std::string textOnWebService;
    void HandleEvent(std::string& text) override;
};