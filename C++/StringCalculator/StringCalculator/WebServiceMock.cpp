#include "WebServiceMock.h"

std::string WebServiceMock::GetTextOnService() const
{
    return textOnWebService;
}

void WebServiceMock::HandleEvent(std::string& text)
{
    textOnWebService = text;
}
