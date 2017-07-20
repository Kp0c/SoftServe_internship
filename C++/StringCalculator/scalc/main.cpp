#include <ScopedRedirectCout.h>
#include <StringCalculator.h>
#include <iostream>
#include <sstream>

int main(int argc, char** argv)
{
    int result = 0;
    std::string input(argv[1]);

    while (!input.empty())
    {
        {
            std::stringstream ss;
            ScopedRedirectCout redirect(ss);
            StringCalculator calculator;
            result = calculator.Add(input);
        }

        std::cout << "The result is " << result << std::endl;
        std::cout << "Another input please: ";
        std::getline(std::cin, input);
    } 

    return 0;
}